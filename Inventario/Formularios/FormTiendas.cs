using Inventario.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario.Formularios
{
    
    public partial class FormTiendas : Form
    {
        private SoloNumeros sn = new SoloNumeros();
        private Registro_TiendasEntities1 db = new Registro_TiendasEntities1();
        int idTienda = 0;
        public FormTiendas()
        {
            InitializeComponent();
            cargartiendas();
        }

        private void txtFono_KeyPress(object sender, KeyPressEventArgs e)
        {
            sn.soloNumeros(e);
        }
        private void cargartiendas()
        {
            var listatiendas = (from t in db.Tienda
                                orderby t.nombre
                                select new
                                {
                                    t.id_tienda,
                                    tiendanom = t.nombre,
                                    tiendaubi = t.ubicacion,
                                    tiendacorreo = t.correo,
                                    tiendafono = t.telefono
                                }).ToList();
            DGVTiendas.DataSource = listatiendas;

            DGVTiendas.Columns[0].Visible = false;
        }
        private string Validacion()
        {
            string mensaje = "";

            if (string.IsNullOrEmpty(txtTienda.Text.Trim()))
                mensaje = "Debe ingresar nombre de la tienda \n";
            if (string.IsNullOrEmpty(txtUbicacion.Text.Trim()))
                mensaje += "Debe ingresar ubicacion \n";
            if (string.IsNullOrEmpty(txtFono.Text.Trim()))
                mensaje += "Debe ingresar telefono de contacto \n";
            if (string.IsNullOrEmpty(txtCorreo.Text.Trim()))
                mensaje += "Debe ingresar correo electronico \n";
            return mensaje;
        }

        private void guardartienda()
        {
            Tienda t = new Tienda();

            t.nombre = txtTienda.Text.Trim();
            t.ubicacion = txtUbicacion.Text.Trim();
            t.telefono = int.Parse(txtFono.Text);
            t.correo = txtCorreo.Text.Trim();

            db.SaveChanges();
        }
        private void editartienda()
        {
            Tienda t = db.Tienda.Find(idTienda);

            t.nombre = txtTienda.Text.Trim();
            t.ubicacion = txtUbicacion.Text.Trim();
            t.telefono = int.Parse(txtFono.Text);
            t.correo = txtCorreo.Text.Trim();

            db.SaveChanges();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string error = Validacion();
            if (error != "")
            {
                MessageBox.Show(error, "Datos Insuficientes");
            }
            else
            {
                if (idTienda == 0)
                {
                    guardartienda();
                }
                else
                {
                    editartienda();
                }
                MessageBox.Show("Tienda Guardada exitosamente!!!");
                cargartiendas();
                Limpiar();
            }
        }
        private void Limpiar()
        {
            idTienda = 0;
            txtTienda.Text = "";
            txtUbicacion.Text = "";
            txtFono.Text = "";
            txtCorreo.Text = "";

            DGVTiendas.ClearSelection();
            btnEliminar.Enabled = false;
        }

        private void DGVTiendas_MouseClick(object sender, MouseEventArgs e)
        {
            idTienda = int.Parse(DGVTiendas.CurrentRow.Cells[0].Value.ToString());
            txtTienda.Text = DGVTiendas.CurrentRow.Cells[1].Value.ToString();
            txtUbicacion.Text = DGVTiendas.CurrentRow.Cells[2].Value.ToString();
            txtFono.Text = DGVTiendas.CurrentRow.Cells[3].Value.ToString();
            txtCorreo.Text = DGVTiendas.CurrentRow.Cells[4].Value.ToString();

            btnEliminar.Enabled = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idTienda > 0)
            {
                var respuesta = MessageBox.Show("¿Desea eliminar el producto " + txtTienda.Text + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (respuesta == DialogResult.Yes)
                {
                    Tienda t = db.Tienda.Find(idTienda);

                    db.Tienda.Remove(t);
                    db.SaveChanges();
                    cargartiendas();
                    Limpiar();
                }
            }
        }
        private bool verificarnombre(string nombre)
        {
            bool resultado = false;
            Tienda tienda = db.Tienda.FirstOrDefault(t => t.nombre.Equals(nombre) && t.nombre != nombre);
            if (tienda != null)
                resultado = true;

            return resultado;
        }

        private void txtTienda_Leave(object sender, EventArgs e)
        {
            if (txtTienda.Text.Trim() != "")
            {
                if (verificarnombre(txtTienda.Text.Trim()))
                {
                    MessageBox.Show("La tienda ya existe");
                    txtTienda.Text = "";
                }
            }
        }
        private bool email_bien_escrito(string email)
        {
            string mailFormat = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, mailFormat))
            {
                if (Regex.Replace(email, mailFormat, string.Empty).Length == 0)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
            bool verificar = email_bien_escrito(txtCorreo.Text);
            if (verificar == false)
            {
                MessageBox.Show("El correo no tiene el formato correcto");
                txtCorreo.Focus();
            }
        }
    }
}
