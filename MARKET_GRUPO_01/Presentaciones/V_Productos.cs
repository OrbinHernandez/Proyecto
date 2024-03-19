using Capa_Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MARKET_GRUPO_01.Presentaciones
{
    public partial class V_Productos : Form
    {
        AbrirForm AF;
        public V_Inicio inicio;
        N_Productos nProductos;
        N_UnidadMedida nUnidadMedida;
        N_Categoria nCategoria;
        public V_Productos(V_Inicio ini)
        {
            InitializeComponent();
            AF = new AbrirForm();
            nProductos = new N_Productos();
            nUnidadMedida = new N_UnidadMedida();
            nCategoria = new N_Categoria();
            inicio = ini;
        }
        void SeleccionarSoloUno(DataGridViewCellEventArgs e, DataGridView dgv)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    DataGridViewCheckBoxCell otherChk = (DataGridViewCheckBoxCell)row.Cells[e.ColumnIndex];
                    otherChk.Value = (row.Index == e.RowIndex);
                }
            }
        }
        void HabilitarSoloColumnaSeleccion(DataGridView dgv)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.ReadOnly = true;
            }
            dgv.Columns[0].ReadOnly = false;
        }
        void cargar()
        {

            if (BtnProductos.Checked)
            {
                HabilitarSoloColumnaSeleccion(DgvProductos);
                DgvProductos.DataSource = nProductos.ObtenerProductos();
            }
            else if (BtnCategoria.Checked)
            {
                HabilitarSoloColumnaSeleccion(DgvCategoria);
                DgvCategoria.DataSource = nCategoria.ObtenerCategoria();
            }
            else if (BtnUnidadMedida.Checked)
            {
                HabilitarSoloColumnaSeleccion(DgvUnidades);
                DgvUnidades.DataSource = nUnidadMedida.ObtenerUnidadesMedida();
            }
        }
        void ConsultarCategoria(int CategoriaID)
        {
            V_Categoria vcategoria = new V_Categoria();
            var categoria = nCategoria.ObtenerCategoria().FirstOrDefault(g => g.CategoriaId == CategoriaID);
            if (categoria != null)
            {
                vcategoria.LblTitulo.Text = "Editar Grupo Descuento";
                vcategoria.TxtIdCategoria.Text = categoria.CategoriaId.ToString();
                vcategoria.TxtDescripcion.Text = categoria.Descripcion;
                vcategoria.TxtCodigo.Text = categoria.Codigo;
                vcategoria.ChkActivo.Checked = categoria.Estado;
                AF.VentanaEmergente(vcategoria, inicio, true);
            }
        }
        void ConsultarUnidadMedida(int unidadMedidaID)
        {
            V_UnidadMedida vUnidadMedida = new V_UnidadMedida();
            var unidadMedida = nUnidadMedida.ObtenerUnidadesMedida().FirstOrDefault(g => g.UnidadMedidaId == unidadMedidaID);
            if (unidadMedida != null)
            {
                vUnidadMedida.LblTitulo.Text = "Editar Grupo Descuento";
                vUnidadMedida.TxtIdUnidadMedida.Text = unidadMedida.UnidadMedidaId.ToString();
                vUnidadMedida.TxtDescripcion.Text = unidadMedida.Descripcion;
                vUnidadMedida.TxtCodigo.Text = unidadMedida.Codigo;
                vUnidadMedida.ChkActivo.Checked = unidadMedida.Estado;
                AF.VentanaEmergente(vUnidadMedida, inicio, true);
            }
        }
        void ConsultarProducto(int Idproducto)
        {
            V_Producto vProducto = new V_Producto();
            var producto = nProductos.ObtenerProductos().FirstOrDefault(g => g.ProductoId == Idproducto);
            if (producto != null)
            {
                vProducto.LblTitulo.Text = "Editar producto";
                vProducto.TxtIdProducto.Text = producto.ProductoId.ToString();
                vProducto.TxtPrecioCompra.Text = producto.PrecioCompra.ToString();
                vProducto.ChkActivo.Checked = producto.Estado;
                vProducto.CmbCategoria.SelectedValue = producto.CategoriaId;
                vProducto.CmbUnidadMedida.SelectedValue = producto.UnidadMedidaId;
                AF.VentanaEmergente(vProducto, inicio, true);
            }
        }

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            TabControlMain.SelectedTab = TabProductos;
            cargar();
        }

        private void BtnCategoria_Click(object sender, EventArgs e)
        {
            TabControlMain.SelectedTab = TabCategoria;
            cargar();
        }

        private void BtnUnidadMedida_Click(object sender, EventArgs e)
        {
            TabControlMain.SelectedTab = tabUnidadMedida;
            cargar();
        }

        private void V_Productos_Load(object sender, EventArgs e)
        {
            cargar();
        }
        void editar()
        {
            if (BtnProductos.Checked == true)
            {
                if (DgvProductos.SelectedCells.Count > 0)
                {
                    int rowIndex = DgvProductos.SelectedCells[0].RowIndex;
                    DataGridViewCheckBoxCell checkBoxCell = DgvProductos.Rows[rowIndex].Cells["Selecion"] as DataGridViewCheckBoxCell;

                    if (checkBoxCell?.Value is true)
                    {
                        // Obtener el ID directamente de la celda
                        if (DgvProductos.Rows[rowIndex].Cells["ProductoId"].Value is int id)
                        {
                            ConsultarProducto(id);
                        }
                    }
                }
            }
            else if (BtnCategoria.Checked == true)
            {
                if (DgvCategoria.SelectedCells.Count > 0)
                {
                    int rowIndex = DgvCategoria.SelectedCells[0].RowIndex;
                    DataGridViewCheckBoxCell checkBoxCell = DgvCategoria.Rows[rowIndex].Cells["SeleccionCategoria"] as DataGridViewCheckBoxCell;

                    if (checkBoxCell?.Value is true)
                    {
                        // Obtener el ID directamente de la celda
                        if (DgvCategoria.Rows[rowIndex].Cells["CategoriaId"].Value is int id)
                        {
                            ConsultarCategoria(id);
                        }
                    }
                }
            }
            else if (BtnUnidadMedida.Checked == true)
            {
                if (DgvUnidades.SelectedCells.Count > 0)
                {
                    int rowIndex = DgvUnidades.SelectedCells[0].RowIndex;
                    DataGridViewCheckBoxCell checkBoxCell = DgvUnidades.Rows[rowIndex].Cells["SeleccionUnidad"] as DataGridViewCheckBoxCell;

                    if (checkBoxCell?.Value is true)
                    {
                        // Obtener el ID directamente de la celda
                        if (DgvUnidades.Rows[rowIndex].Cells["UnidadMedidaId"].Value is int id)
                        {
                            ConsultarUnidadMedida(id);
                        }
                    }
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            editar();
        }

        private void DgvUnidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarSoloUno(e, DgvUnidades);
        }

        private void DgvCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarSoloUno(e, DgvCategoria);
        }

        private void DgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarSoloUno(e, DgvProductos);
        }

        private void BtnNuevoProducto_Click(object sender, EventArgs e)
        {
            AF.VentanaEmergente(new V_Producto(), inicio, true);
        }

        private void BtnNuevoCategoria_Click(object sender, EventArgs e)
        {
            AF.VentanaEmergente(new V_Categoria(), inicio, true);
        }

        private void BtnNuevUnidadMedida_Click(object sender, EventArgs e)
        {
            AF.VentanaEmergente(new V_UnidadMedida(), inicio, true);
        }
    }
}
