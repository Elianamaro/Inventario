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
        public Registro_TiendasEntities1 db = new Registro_TiendasEntities1();
        int id_categoria = 0;
        public FormCategorias()
        {
            InitializeComponent();
            cargarCategorias();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() != "")
            {
                if (id_categoria == 0)
                {
                    if (buscarNombre(txtNombre.Text.Trim()))
                    {
                        Categorias categoria = new Categorias();
                        categoria.nombre = txtNombre.Text.Trim();
                        db.Categorias.Add(categoria);
                        db.SaveChanges();
                        limpiar();
                        cargarCategorias();
                    }
                    else
                    {
                        MessageBox.Show("Esta categoría ya está registrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (buscarNombre(txtNombre.Text.Trim()))
                    {
                        var Categoria = db.Categorias.Find(id_categoria);
                        if (Categoria != null)
                        {
                            Categoria.nombre = txtNombre.Text;
                            db.SaveChanges();
                            limpiar();
                            cargarCategorias();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Esta categoría ya está registrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe ingrese el nombre de una categoría");
            }
        }
        private bool buscarNombre(string nombre)
        {
            var eq = db.Categorias.FirstOrDefault(c => c.nombre == nombre);
            if (eq != null)
            {
                return false;
            }
            return true;
        }
        private void dgvMarcas_MouseClick(object sender, MouseEventArgs e)
        {
            id_categoria = int.Parse(dgvCategorias.CurrentRow.Cells[0].Value.ToString());
            txtNombre.Text = dgvCategorias.CurrentRow.Cells[1].Value.ToString();

            btnEliminar.Enabled = true;
        }
        private void limpiar()
        {
            id_categoria = 0;
            txtNombre.Text = "";
            dgvCategorias.ClearSelection();
            btnEliminar.Enabled = false;
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
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
    }
}
