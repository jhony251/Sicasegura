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
            string htmlBruto = "<!DOCTYPE html> <html> <head> <meta charset='UTF-8'> <style type='text/css'> .texto{width: 120px; text-align: left; border:1px solid; } .total{background-color: #ddd; } table {border-collapse: collapse; } td {border:1px solid; text-align: center; width: 70px; } </style> </head> <body> <h3 style='text-align: center'>Informe Q medio diario tomado por ACEQUIAS (l/s)</h3> <table> <tr> <th class='texto'>TRAMOS</th> <th class='texto'>TITULAR</th> <th class='texto'>ACEQUIA</th> </tr> <tr> <td class='texto' rowspan='3'>FINAL VEGA ALTA</td> <td class='texto'>ALGUAZAS</td> <td class='texto'>Alguazas</td> </tr> <tr> <td class='texto'>MOLINA</td> <td class='texto'>Mayor Molina</td> </tr> <tr> <td class='texto'>ARCHENA</td> <td class='texto'>Archena</td> </tr> <tr> <td></td> <td class='total' colspan='2'></td> </tr> <tr> <td class='texto' rowspan='2'>VEGA MEDIA</td> <td class='texto' rowspan='2'>JJ.HH.</td> <td class='texto'>Aljufía</td> </tr> <tr> <td class='texto'>Barreras</td> </tr> <tr> <td></td> <td class='total' colspan='2'>Total JJ.HH.</td> </tr> <tr> <td class='texto' rowspan='13'>VEGA BAJA</td> <td class='texto' rowspan='6'>JPAO</td> <td class='texto'>Alquibla</td> </tr> <tr> <td class='texto'>Molina</td> </tr> <tr> <td class='texto'>Huertos</td> </tr> <tr> <td class='texto'>Vieja Almoradí</td> </tr> <tr> <td class='texto'>Puertas Murcia</td> </tr> <tr> <td class='texto'>Mudamiento</td> </tr> <tr> <td class='total' colspan='2'></td> </tr> <tr> <td class='texto' rowspan='2'>JPA ALFEITAMÍ</td> <td class='texto'>Mayor Almoradí</td> </tr> <tr> <td class='texto'>Del Río</td> </tr> <tr> <td class='total' colspan='2'></td> </tr> <tr> <td class='texto'>JPA ROJALES</td> <td class='texto'>Comuna</td> </tr> <tr> <td class='texto'>JPA CALLOSA</td> <td class='texto'>Callosa</td> </tr> <tr> <td class='texto'>JPA CATRAL</td> <td class='texto'>Catral</td> </tr> <tr> <td class='total' colspan='3'><b>Total V. Baja, datos disponibles</b></td> </tr> </table> <p> <b>Los valores diarios de este informe representan el valor medio de las lecturas de caudal tomadas desde las 08:00 del día en cuestión hasta las 07:59 del día siguiente.<br>Todos los valores de caudal reunidos en este informe vienen representados en litros por segundo (l/s).</b> <br><br> <b>Leyenda: &emsp;- ND</b>: Datos No Disponibles</p> </body> </html>";
            string[] separadores = { "</tr>" };
            string[] codigoHTML = htmlBruto.Split(separadores, System.StringSplitOptions.None);

            for (int dia = 0; dia < intervalo; dia++)
            {
                DateTime fecha = fechaFinal.AddDays(-1 * (intervalo - dia));
                string[] fechaPartes = fecha.ToShortDateString().Split('/');
                char columna = 'D';
                columna += (char)dia;

                hojaActual.Cells[columna + "3"].Value = fechaPartes[0] + "/" + fechaPartes[1];
                codigoHTML[0] += "<td>" + fechaPartes[0] + "/" + fechaPartes[1] + "</td>";

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
                        codigoHTML[Convert.ToInt16(filasAcequias[i]) - 3] += "<td>" + Math.Round((Convert.ToDouble(temp) * 1000), 0).ToString("N0") + "</td>"; // El -3 del íncide es para ajustar el número de fila introducido arriba al índice de fila del código html
                    }
                    else
                    {
                        hojaActual.Cells[columna + filasAcequias[i]].Value = "ND";
                        codigoHTML[Convert.ToInt16(filasAcequias[i]) - 3] += "<td>" + "ND" + "</td>";
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
                            codigoHTML[row - 3] += "<td class='total'>" + Math.Round(total, 0).ToString("N0") + "</td>";
                        }
                        else
                        {
                            hojaActual.Cells[columna + row.ToString()].Value = totalVegaBaja;
                            codigoHTML[row - 3] += "<td class='total'>" + Math.Round(totalVegaBaja, 0).ToString("N0") + "</td>";
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
            email.Set_asunto("Informe diario de derivación Acequias entre Contraparada y Vega Baja");
            email.Set_destinatarios("vhuescar@utemursiya.es");
            string temp20 = string.Join("</tr>", codigoHTML);
            email.Set_cuerpo(string.Join("</tr>", codigoHTML));
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