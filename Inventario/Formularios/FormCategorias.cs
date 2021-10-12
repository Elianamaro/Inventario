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
    public partial class FormCategorias : Form
    {
        private Registro_TiendasEntities1 db = new Registro_TiendasEntities1();
        int id_categoria = 0;
        public FormCategorias()
        {
            InitializeComponent();
            cargarCategorias();
        }

        private string Validar()
        {
            string aviso = "";
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                aviso = "Debe ingresar nombre de la nueva categoria \n";
            return aviso;
        }
        private void Guardar()
        {
            Categorias c = new Categorias();
            c.nombre = txtNombre.Text.Trim();

            db.Categorias.Add(c);
            db.SaveChanges();
        }
        private void Editar()
        {
            Categorias c = db.Categorias.Find(id_categoria);
            c.nombre = txtNombre.Text.Trim();

            db.SaveChanges();
        }

        private bool buscarNombre(string nombre)
        {
            bool resp = false;

            Categorias cate = db.Categorias.FirstOrDefault(c => c.nombre.Equals(nombre) && c.id_categoria != id_categoria);
            if (cate != null)
                resp = true;

            return resp;
        }
 
        private void limpiar()
        {
            id_categoria = 0;
            txtNombre.Text = "";
            dgvCategorias.ClearSelection();
            btnEliminar.Enabled = false;
        }


        private void cargarCategorias()
        {
            var listaCategorias = (from c in db.Categorias
                                   orderby c.nombre
                                   select new
                                   {
                                       id = c.id_categoria,
                                       nombre = c.nombre
                                   }).ToList();

            dgvCategorias.DataSource = listaCategorias;
            dgvCategorias.Columns[0].Visible = false;
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
                if (id_categoria == 0)
                {
                    Guardar();
                }
                else
                {
                    Editar();
                }
                MessageBox.Show("Categoria guardada exitosamente!!!");
                cargarCategorias();
                limpiar();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (id_categoria > 0)
            {
                Categorias categoria = db.Categorias.Find(id_categoria);

                if (categoria != null)
                {

                    db.Categorias.Remove(categoria);
                    db.SaveChanges();
                    MessageBox.Show("Eliminado con éxito!");
                    limpiar();
                    cargarCategorias();
                }
            }
        }

        private void dgvCategorias_MouseClick(object sender, MouseEventArgs e)
        {
            id_categoria = int.Parse(dgvCategorias.CurrentRow.Cells[0].Value.ToString());
            txtNombre.Text = dgvCategorias.CurrentRow.Cells[1].Value.ToString();

            btnEliminar.Enabled = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() != "")
            {
                if (buscarNombre(txtNombre.Text.Trim()))
                {
                    MessageBox.Show("La categoria ya esta registrada");
                    txtNombre.Text = "";
                }
            }
        }
    }
}
