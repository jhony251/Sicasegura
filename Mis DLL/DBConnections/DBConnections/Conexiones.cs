using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DBConnections
{
    public class Conexiones 
    {
        public string cnnn = "";
        /// <summary>
        /// Dll para crear cadenas de conexion a BBDD en base a un fichero XML
        /// </summary>
        /// <param name="bd">Parametro en el que decimos la BD a la que nos queremos conectar</param>
        /// <param name="application">Parametro que identifica la aplicacion que se va a conectar a la base de datos</param>
        public Conexiones(string bd, string application)
        { 
            XmlDocument xDoc = new XmlDocument();//La ruta del documento XML permite rutas relativas 
                                                //respecto del ejecutable!
            //xDoc.Load("C:\\cnn.xml");
            xDoc.Load(Properties.Settings.Default.Ubicacion.ToString());
            XmlNodeList ITEMS = xDoc.GetElementsByTagName("BBDD");
            XmlNodeList lista = ((XmlElement)ITEMS[0]).GetElementsByTagName("Cadena"); 
            foreach (XmlElement nodo in lista) 
            { 
                int i = 0; 
                XmlNodeList nIP = nodo.GetElementsByTagName("IP"); 
                XmlNodeList nNAME = nodo.GetElementsByTagName("NAME"); 
                XmlNodeList nUSR = nodo.GetElementsByTagName("USR");
                XmlNodeList nPASS = nodo.GetElementsByTagName("PASS");
                if (nNAME[0].InnerText == bd)
                {
                    System.Data.SqlClient.SqlConnectionStringBuilder CNNC = new System.Data.SqlClient.SqlConnectionStringBuilder();
                    CNNC.DataSource = nIP[0].InnerText.ToString();
                    CNNC.UserID = nUSR[0].InnerText.ToString();
                    CNNC.Password = nPASS[0].InnerText.ToString();
                    CNNC.InitialCatalog = nNAME[0].InnerText.ToString();
                    cnnn = CNNC.ConnectionString;

                }

                
            
                //Console.WriteLine("Elemento nombre ... {0} {1} {2}", nNombre[i].InnerText, nApellido1[i].InnerText, nApellido2[i++].InnerText); 
            }
            
        }
    }
}
