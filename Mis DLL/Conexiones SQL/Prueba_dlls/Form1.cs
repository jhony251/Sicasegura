using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Prueba_dlls
{
    public partial class Form1 : Form
    {
        private DataSet ds = new DataSet();
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conexionCHS = new SqlConnection();
            Conexiones_SQL.ConexionSQL conexionSQL = new Conexiones_SQL.ConexionSQL();
            //conexionCHS = conexionSQL.Crear_Conexion("10.31.224.18", "SIGVECTOR", "PruebaVPN", "limonero");
            conexionCHS = conexionSQL.Crear_Conexion("Server=10.31.224.18; database=SIGVECTOR; Password=actividades;Persist Security Info=true;User ID=PVYCR_Basico");
            listBox1.DataSource = conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, "SELECT table_name FROM Information_Schema.Tables where Table_Type = 'BASE TABLE'")).Tables[0].DefaultView; ;
            listBox1.DisplayMember = "table_name";
            
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            SqlConnection conexionCHS = new SqlConnection();
            Conexiones_SQL.ConexionSQL conexionSQL = new Conexiones_SQL.ConexionSQL();
            //conexionCHS = conexionSQL.Crear_Conexion("10.31.224.18", "SIGVECTOR", "PruebaVPN", "limonero");
            conexionCHS = conexionSQL.Crear_Conexion("Server=10.31.224.18; database=SIGVECTOR; Password=actividades;Persist Security Info=True;User ID=PVYCR_Basico");
            dataGridprueba.DefaultCellStyle.NullValue = "<NULL>";
            DataRowView drv = (DataRowView)listBox1.Items[listBox1.SelectedIndex];
            ds =(DataSet) conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, "SELECT * FROM " + drv.Row.ItemArray.GetValue(0).ToString())) ;
            dataGridprueba.DataSource = ds.Tables[0].DefaultView; 
            ToolStripLabel tooltotales = new ToolStripLabel();
            tooltotales = (ToolStripLabel) toolStrip2.Items[1];
            tooltotales.Text = "Número de registros: " + Convert.ToString(dataGridprueba.RowCount);
            txtInforme.Text = "";
        }

        private void dataGridprueba_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridprueba.RowCount > 0)
            {
                ToolStripLabel tooltotales = new ToolStripLabel();
                tooltotales = (ToolStripLabel)toolStrip2.Items[3];
                tooltotales.Text = "Número de registros seleccionados: " + dataGridprueba.SelectedRows.Count.ToString();
            }
            else
            {
                ToolStripLabel tooltotales = new ToolStripLabel();
                tooltotales = (ToolStripLabel)toolStrip2.Items[3];
                tooltotales.Text = "Número de registros seleccionados: 0";
            }
        }

        private void GenerarInforme_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)listBox1.Items[listBox1.SelectedIndex];
            
            //SELECT COLUMN_NAME FROM Information_Schema.Columns WHERE TABLE_NAME = '" + drv.Row.ItemArray.GetValue(0).ToString() + "'"
            SqlConnection conexionCHS = new SqlConnection();
            Conexiones_SQL.ConexionSQL conexionSQL = new Conexiones_SQL.ConexionSQL();
            //conexionCHS = conexionSQL.Crear_Conexion("10.31.224.18", "SIGVECTOR", "PruebaVPN", "limonero");
            conexionCHS = conexionSQL.Crear_Conexion("Server=10.31.224.18; database=SIGVECTOR; Password=actividades;Persist Security Info=True;User ID=PVYCR_Basico");
            listBox2.DataSource = conexionSQL.Convertir_SQLDA_en_DS(conexionSQL.Ejecutar_SQL(conexionCHS, "SELECT COLUMN_NAME FROM Information_Schema.Columns WHERE TABLE_NAME = '" + drv.Row.ItemArray.GetValue(0).ToString() + "'")).Tables[0].DefaultView; ;
            listBox2.DisplayMember = "COLUMN_NAME";

            for (int i = 0; i < dataGridprueba.Columns.Count ; i++)
            {
                string columna = dataGridprueba.Columns[i].Name.ToString();           
                this.bateria_de_consultas(columna, ref txtInforme,i);
            }
        }


        private void bateria_de_consultas(string columna,ref TextBox txtInf_Result,int intcolumna)
        {
            //string valor;
            int contador_nulos = 0;
            int contador_ceros = 0;
            string tipo_dato = ds.Tables[0].Columns[intcolumna].DataType.ToString();
            for (int i = 0; i < dataGridprueba.RowCount - 1; i++)
            {
                //valor = dataGridprueba.Rows[i].Cells[columna].Value;
                
                if (ds.Tables[0].Rows[i].IsNull(intcolumna))
                {
                    contador_nulos++;
                }
                else if (ds.Tables[0].Columns[intcolumna].DataType == System.Type.GetType("System.Double"))
                {
                    if ((double) ds.Tables[0].Rows[i].ItemArray.GetValue(intcolumna) == 0)
                    {
                        contador_ceros++;
                    }
                }
            }
            txtInf_Result.Text += "\r\n\r\n" + columna + "\r\n=========================\r\n";
            txtInf_Result.Text += "\r\nTipo de dato de la columna = " + tipo_dato;
            txtInf_Result.Text += "\r\nNúmero de campos nulos = " + contador_nulos.ToString();
            if (ds.Tables[0].Columns[intcolumna].DataType == System.Type.GetType("System.Double"))
            {
                txtInf_Result.Text += "\r\nNúmero de campos a 0 = " + contador_ceros.ToString();
            }
        }

    }
}