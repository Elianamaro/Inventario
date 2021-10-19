using Inventario.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario.Formularios
{
    public partial class FormProductos : Form
    {
        private SoloNumeros sn = new SoloNumeros();
        
        private Registro_TiendasEntities1 db = new Registro_TiendasEntities1();
        int id_producto = 0;
        public FormProductos()
        {
            InitializeComponent();
            cargarMarcas();
            cargarCategorias();
            cargarProductos();
            cargartiendas();
        }
        
        private void cargarMarcas()
        {
            var listaMarcas = (from m in db.Marca
                               orderby m.nombre_marca
                               select new
                               {
                                   id = m.id_marca,
                                   Nombre = m.nombre_marca
                               }).ToList();

            cbMarcas.DataSource = listaMarcas;
            cbMarcas.ValueMember = "id";
            cbMarcas.DisplayMember = "Nombre";
            cbMarcas.SelectedIndex = -1;
        }

        private void cargarCategorias()
        {
            //select * from categoria 
            var listaCategorias = db.Categorias.OrderBy(c => c.nombre).ToList();

            cbCategorias.DataSource = listaCategorias;
            cbCategorias.ValueMember = "id_categoria";
            cbCategorias.DisplayMember = "nombre";

            cbCategorias.SelectedIndex = -1;
        }
        private void cargartiendas()
        {
            var listaTiendas = db.Tienda.OrderBy(t => t.nombre).ToList();

            cbTienda.DataSource = listaTiendas;
            cbTienda.ValueMember = "id_tienda";
            cbTienda.DisplayMember = "nombre";

            cbTienda.SelectedIndex = -1;
        }

        public void cargarProductos()
        {
            var listaProductos = (from p in db.Producto
                                  select new
                                  {
                                      p.id_producto,
                                      p.id_marca,
                                      p.id_categoria,
                                      p.id_tienda,
                                      FechaRecepcion = p.fecha_recepcion,
                                      Marca = p.Marca.nombre_marca,
                                      Categoría = p.Categorias.nombre,
                                      Código = p.codigo_producto,
                                      Nombre = p.nombre_producto,
                                      Compra = p.precio_compra,
                                      Venta = p.precio_venta,
                                      Desripción = p.descripcion_producto
                                  }).ToList();
            dgvProductos.DataSource = listaProductos;

            dgvProductos.Columns[0].Visible = false;
            dgvProductos.Columns[1].Visible = false;
            dgvProductos.Columns[2].Visible = false;
            dgvProductos.Columns[3].Visible = false;
        }
        private string Validar()
        {
            string mensaje = "";
            if (string.IsNullOrEmpty(cbMarcas.Text.Trim()))
                mensaje = "Debe seleccionar una Marca \n";
            if (string.IsNullOrEmpty(cbCategorias.Text.Trim()))
                mensaje += "Debe seleccionar una Categoría \n";
            if (string.IsNullOrEmpty(cbTienda.Text.Trim()))
                mensaje += "Debe seleccionar una Tienda \n";
            if (string.IsNullOrEmpty(txtCodigo.Text.Trim()))
                mensaje += "Debe ingresar un Código \n";
            if (dateTimePicker1.Value.Date > DateTime.Now.Date)
                mensaje += "La fecha de ingreso no debe superar a la fecha actual \n";
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                mensaje += "Debe ingresar un Nombre \n";
            if (string.IsNullOrEmpty(txtCompra.Text.Trim()))
                mensaje += "Debe ingresar un Precio de compra \n";
            if (string.IsNullOrEmpty(txtVenta.Text.Trim()))
                mensaje += "Debe ingresar un Precio de Venta \n";
            return mensaje;
        }
        private void Guardar()
        {
            Producto p = new Producto();
            p.codigo_producto = txtCodigo.Text.Trim();
            p.nombre_producto = txtNombre.Text.Trim();
            p.fecha_recepcion = DateTime.Parse(dateTimePicker1.Value.ToString());
            p.precio_compra = int.Parse(txtCompra.Text);
            p.precio_venta = int.Parse(txtVenta.Text);
            p.descripcion_producto = txtDescripcion.Text.Trim();
            p.id_marca = int.Parse(cbMarcas.SelectedValue.ToString());
            p.id_categoria = int.Parse(cbCategorias.SelectedValue.ToString());
            p.id_tienda = int.Parse(cbTienda.SelectedValue.ToString());

            db.Producto.Add(p);
            db.SaveChanges();
        }
        private void Modificar()
        {
            Producto p = db.Producto.Find(id_producto);
            p.codigo_producto = txtCodigo.Text.Trim();
            p.nombre_producto = txtNombre.Text.Trim();
            p.fecha_recepcion = dateTimePicker1.Value;
            p.precio_compra = int.Parse(txtCompra.Text);
            p.precio_venta = int.Parse(txtVenta.Text);
            p.descripcion_producto = txtDescripcion.Text.Trim();
            p.id_marca = int.Parse(cbMarcas.SelectedValue.ToString());
            p.id_categoria = int.Parse(cbCategorias.SelectedValue.ToString());
            p.id_tienda = int.Parse(cbTienda.SelectedValue.ToString());

            db.SaveChanges();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string error = Validar();
            if (error != "")
            {
                MessageBox.Show(error, "Falta Informacion");
            }
            else
            {
                if (id_producto == 0)
                {
                    Guardar();
                }
                else
                {
                    Modificar();
                }
                MessageBox.Show("Registro guardado!!!");
                cargarProductos();
                Limpiar();
            }
        }

        private void Limpiar()
        {
            id_producto = 0;
            cbMarcas.SelectedIndex = -1;
            cbCategorias.SelectedIndex = -1;
            cbTienda.SelectedIndex = -1;
            txtCodigo.Text = string.Empty;
            txtNombre.Text = "";
            txtCompra.Text = "";
            txtVenta.Text = "";
            txtDescripcion.Text = "";

            dgvProductos.ClearSelection();
            btnEliminar.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (id_producto > 0)
            {
                var resultado = MessageBox.Show("¿Desea eliminar el producto " + txtNombre.Text + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (resultado == DialogResult.Yes)
                {
                    Producto p = db.Producto.Find(id_producto);
                    db.Producto.Remove(p);
                    db.SaveChanges();
                    cargarProductos();
                    Limpiar();
                }
            }
        }

        private bool verficiaCodigo(string codigo)
        {
            bool result = false;
            Producto producto = db.Producto.FirstOrDefault(p => p.codigo_producto.Equals(codigo) && p.id_producto != id_producto);
            if (producto != null)
                result = true;

            return result;
        }

        private void txtCodigo_Leave(object sender, EventArgs e){
                if (txtCodigo.Text.Trim() != "")
                {
                    if (verficiaCodigo(txtCodigo.Text.Trim()))
                    {
                        MessageBox.Show("El código ingresado ya está registrado");
                        txtCodigo.Text = "";
                    }
                }
        }
        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMarcas marca = new FormMarcas();
            marca.Show();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            sn.soloNumeros(e);
        }

        private void txtCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            sn.soloNumeros(e);
        }

        private void txtVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            sn.soloNumeros(e);
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            sn.soloNumeros(e);
        }

        private void dgvProductos_MouseClick(object sender, MouseEventArgs e)
        {
            id_producto = int.Parse(dgvProductos.CurrentRow.Cells[0].Value.ToString());
            cbMarcas.SelectedValue = int.Parse(dgvProductos.CurrentRow.Cells[1].Value.ToString());
            cbCategorias.SelectedValue = int.Parse(dgvProductos.CurrentRow.Cells[2].Value.ToString());
            cbTienda.SelectedValue = int.Parse(dgvProductos.CurrentRow.Cells[5].Value.ToString());
            txtCodigo.Text = dgvProductos.CurrentRow.Cells[6].Value.ToString();
            txtNombre.Text = dgvProductos.CurrentRow.Cells[7].Value.ToString();
            txtCompra.Text = dgvProductos.CurrentRow.Cells[8].Value.ToString();
            txtVenta.Text = dgvProductos.CurrentRow.Cells[9].Value.ToString();
            txtDescripcion.Text = dgvProductos.CurrentRow.Cells[10].Value.ToString();

            btnEliminar.Enabled = true;
        }
    }
}
