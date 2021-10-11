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
    public partial class FormMarcas : Form
    {
        private Registro_TiendasEntities1 db = new Registro_TiendasEntities1();
        int idMarca = 0;
        public FormMarcas()
        {
            InitializeComponent();
            cargarMarcas();
        }

        private bool buscarNombre(string nombre)
        {
            bool resultado = false;
            var q = db.Marca.FirstOrDefault(m => m.nombre_marca == nombre);
            if (q != null)
                resultado = true;
            
            return resultado;
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
            dgvMarcas.DataSource = listaMarcas;

            dgvMarcas.Columns[0].Visible = false;
        }
        private void limpiar()
        {
            idMarca = 0;
            txtNombre.Text = "";
            dgvMarcas.ClearSelection();

            btnEliminar.Enabled = false;
        }

        private void Guardar()
        {
            Marca m = new Marca();
            m.nombre_marca = txtNombre.Text.Trim();

            db.Marca.Add(m);
            db.SaveChanges();
        }
        private string Validar()
        {
            string aviso = "";
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                aviso = "Ingrese nombre para la marca \n";
            return aviso;
        }
        private void Editar()
        {
            Marca m = db.Marca.Find(idMarca);
            m.nombre_marca = txtNombre.Text.Trim();

            db.SaveChanges();
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() != "")
            {
                if (buscarNombre(txtNombre.Text.Trim()))
                {
                    MessageBox.Show("Esta marca ya esta registrada");
                    txtNombre.Text = "";
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string error = Validar();
            if (error != "")
            {
                MessageBox.Show(error, "Datos Insuficientes");
            }
            else
            {
                if (idMarca == 0)
                {
                    Guardar();
                }
                else
                {
                    Editar();
                }
                MessageBox.Show("Registro Guardado!!!");
                cargarMarcas();
                limpiar();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idMarca > 0)
            {
                var respuesta = MessageBox.Show("¿Desea eliminar la marca " + txtNombre.Text + "?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (respuesta == DialogResult.Yes)
                {
                    Marca m = db.Marca.Find(idMarca);
                    db.Marca.Remove(m);

                    MessageBox.Show("Eliminado con éxito!");
                    db.SaveChanges();
                    cargarMarcas();
                    limpiar();
                }
            }
        }

        private void dgvMarcas_MouseClick(object sender, MouseEventArgs e)
        {
            idMarca = int.Parse(dgvMarcas.CurrentRow.Cells[0].Value.ToString());
            txtNombre.Text = dgvMarcas.CurrentRow.Cells[1].Value.ToString();

            btnEliminar.Enabled = true;
        }
    }
}
