using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using OfficeOpenXml;
using System.Text;
using System.Configuration;

namespace SicaSegura
{

    public partial class _Default : System.Web.UI.Page
    {
        private string[] codigosAcequias = {"VA015P01", "VA016P01", "VA013P01",
                                             "VM002P05", "VM001P03",
                                             "VB001P01", "VB002P01", "VB003P01", "VB075P01", "ND", "ND", "VB071P01", "ND", "VB057P01", "ND", "ND"};
        private string[] filasAcequias = {"4", "5", "6",
                                           "8", "9",
                                           "11", "12", "13", "14", "15", "16", "18", "19", "21", "22", "23"};
        private int nSubtotales = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            SicaSegura.SICA_DB sicaDB = new SicaSegura.SICA_DB();
            DateTime fechaFinal = DateTime.Today.AddHours(8);
            int intervalo = 15;
            string[] fechaFinalPartes = fechaFinal.ToShortDateString().Split('/');
            ExcelPackage excel = new ExcelPackage(new FileInfo(@"" + ConfigurationManager.AppSettings["directorioInformeOjosVegaMedia"] + "\\Plantilla.xlsx"));
            var hojaActual = excel.Workbook.Worksheets["Hoja1"];

            for (int dia = 0; dia < intervalo; dia++)
            {
                DateTime fecha = fechaFinal.AddDays(-1 * (intervalo - dia));
                string[] fechaPartes = fecha.ToShortDateString().Split('/');
                char columna = 'D';
                columna += (char)dia;

                hojaActual.Cells[columna + "3"].Value = fechaPartes[0] + "/" + fechaPartes[1];

                // Bucle para obtener y escribir los valores de acequias de la BBDD            
                for (int i = 0; i < codigosAcequias.Length; i++)
                {
                    string codigoSICA = codigosAcequias[i];
                    if (codigoSICA != "ND")
                    {
                        string SQL = "SELECT AVG(Caudal_M3S) AS Media " +
                                 "FROM dbo.PVYCR_DatosAcequias " +
                                 "WHERE (CodigoPVYCR='" + codigoSICA + "') AND " +
                                    "(Fecha_Medida BETWEEN '" + fecha.ToString() + "' AND '" + fecha.AddDays(1).AddMinutes(-1).ToString() + "') AND " +
                                    "((Cod_Fuente_Dato='13') OR (Cod_Fuente_Dato='10'))";
                        string temp = sicaDB.GetDataSIGVECTOR(SQL).Rows[0][0].ToString();
                        hojaActual.Cells[columna + filasAcequias[i]].Value = (Convert.ToDouble(temp) * 1000);
                    }
                    else
                    {
                        hojaActual.Cells[columna + filasAcequias[i]].Value = "ND";
                    }

                }

                /* Calculo de totales por tramos
                 * Recorremos la columna de nuevo acumulando los valores que hemos escrito antes y escribiéndolos en los huecos que han quedado en blanco
                */
                int indice = 0;
                int row = 4;
                double total = 0;
                double totalVegaBaja = 0;
                while (indice < 5)
                {
                    var rowVal = hojaActual.Cells[columna + row.ToString()].Value;
                    if (rowVal != null)
                    {
                        try
                        {
                            total += Convert.ToDouble(hojaActual.Cells[columna + row.ToString()].Value);
                        }
                        catch (Exception ee)
                        {

                        }
                    }
                    else
                    {
                        if (indice >= 2 && indice <= 4)
                        {
                            totalVegaBaja += total;
                        }
                        if (indice < 4)
                        {
                            hojaActual.Cells[columna + row.ToString()].Value = total;
                        }
                        else
                        {
                            hojaActual.Cells[columna + row.ToString()].Value = totalVegaBaja;
                        }
                        total = 0;
                        indice++;
                    }
                    row++;
                }




            }

            excel.SaveAs(new FileInfo(@"" + ConfigurationManager.AppSettings["directorioInformeOjosVegaMedia"] + "\\Informe_" + fechaFinalPartes[0] + fechaFinalPartes[1] + fechaFinalPartes[2] + ".xlsx"));            

            SicaSegura.SICA_Mailing email = new SicaSegura.SICA_Mailing();
            email.Set_formatoHTML();
            email.Set_asunto("Informe Hidrología Acequias " + fechaFinal.ToShortDateString());
            email.Set_destinatarios("vhuescar@utemursiya.es");
            string cuerpoEmail = XlsxToHTML(@"" + ConfigurationManager.AppSettings["directorioInformeOjosVegaMedia"] + "\\Informe_" + fechaFinalPartes[0] + fechaFinalPartes[1] + fechaFinalPartes[2] + ".xlsx");
            email.Set_cuerpo(cuerpoEmail);
            email.Set_adjunto(@"" + ConfigurationManager.AppSettings["directorioInformeOjosVegaMedia"] + "\\Informe_" + fechaFinalPartes[0] + fechaFinalPartes[1] + fechaFinalPartes[2] + ".html");
            email.Enviar_mail();


        }

        public string XlsxToHTML(string path)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("<table>");

            ExcelPackage excelPackage = new ExcelPackage(new FileInfo(@"" + path));
            {
                ExcelWorkbook workbook = excelPackage.Workbook;
                if (workbook != null)
                {
                    ExcelWorksheet worksheet = workbook.Worksheets["Hoja1"];
                    if (worksheet != null)
                    {
                        for (int i = 1; i <= 25; i++)
                        {
                            stringBuilder.Append("<tr>");
                            for (int j = 1; j <= 25; j++)
                            {
                                try
                                {
                                    string cadenaTemp = Convert.ToString(worksheet.Cells[i, j].Value);
                                    double dobleTemp = Math.Round(Convert.ToDouble(cadenaTemp), 0);
                                    stringBuilder.Append("<td>" + dobleTemp.ToString() + "</td>");
                                }
                                catch (Exception ee)
                                {
                                    stringBuilder.Append("<td>" + worksheet.Cells[i, j].Value + "</td>");
                                }

                            }
                            stringBuilder.Append("</tr>");
                        }

                    }
                }
            }

            stringBuilder.Append("</table>");

            return stringBuilder.ToString();
        }
    }
}