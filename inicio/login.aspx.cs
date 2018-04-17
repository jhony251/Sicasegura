using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.Security.Cryptography;
using System.Web.Services.Description; 


public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string usuario="";
        string aplicacion = "";
        try {  usuario = Session["Username"].ToString();  aplicacion = Request.QueryString["app"]; }
        catch { Response.Redirect("index.aspx"); }
        if (aplicacion == null)
        {
            Response.Redirect("index.aspx");
            //Response.Redirect(baseurl +  "/inicio/index.aspx?usuar=" + usuario);
        }
        else
        {
                string[] perfil = new string[3];
                perfil = comprobarperfil(usuario, aplicacion);
                TXT_Usuario.Text = usuario;
                TXT_Descripcion.Text = perfil[0].ToString();
                TXT_ID.Text = perfil[1].ToString();
                TXT_nombre.Text = perfil[2].ToString();
                string paramtrs = usuario + "~" + TXT_Descripcion.Text + "~" + TXT_ID.Text + "~" + TXT_nombre.Text;
                switch (aplicacion)
                {
                    case "SICA": Response.Redirect("../sicasegura/indexcas.aspx?args=" + paramtrs); break;
                    case "SICA_ESQUEMAS": Response.Redirect("../esquemassegura/indexcas.aspx?args=" + paramtrs); break;
                    case "SICA_SCADA": Response.Redirect("../scadaSegura/indexcas.asp?trusted=SI"); break;
                    case "SICA_INFORMES": Response.Redirect("../SicaSeguraInformes/indexcas.aspx?args=" + paramtrs); break;
                    //case "SICA_INFORMES": Response.Redirect("../SicaSeguraInformes/default.aspx"); break;
                }
            
            
        }
    }
    public string[] comprobarperfil(string USR, string APP)
    {
        String[] perfil = new String[3];
        try
        {
            WebReference.ServicioAplicacionesImplService WS = new WebReference.ServicioAplicacionesImplService();
            WebReference.rol[] rol = WS.obtenerRolesUsuario(USR, APP);
            perfil[0] = rol[0].descripcion.ToString();
            perfil[1] = rol[0].id.ToString();
            perfil[2] = rol[0].nombre.ToString();
        }
        catch (Exception ee)
        {
            //Response.Write(ee.Message + aplicacion);
            Response.Redirect("notallowed.aspx");
        }
        return perfil;
        
    }
    public String LeerIPAddress()
    {
        String name = System.Net.Dns.GetHostName();
        String ip = Request.ServerVariables["remote_host"].ToString();
        return ip;
    }
    #region Encriptar
    /// <summary>
    /// Método para encriptar un texto plano usando el algoritmo (Rijndael).
    /// Este es el mas simple posible, muchos de los datos necesarios los
    /// definimos como constantes.
    /// </summary>
    /// <param name="textoQueEncriptaremos">texto a encriptar</param>
    /// <returns>Texto encriptado</returns>
    public static string Encriptar(string textoQueEncriptaremos)
    {
        return Encriptar(textoQueEncriptaremos,
          "pass75dc@avz10", "s@lAvz", "MD5", 1, "@1B2c3D4e5F6g7H8", 128);
    }
    /// <summary>
    /// Método para encriptar un texto plano usando el algoritmo (Rijndael)
    /// </summary>
    /// <returns>Texto encriptado</returns>
    public static string Encriptar(string textoQueEncriptaremos, string passBase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoQueEncriptaremos);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passBase,
          saltValueBytes, hashAlgorithm, passwordIterations);
        byte[] keyBytes = password.GetBytes(keySize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged()
        {
            Mode = CipherMode.CBC
        };
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes,
          initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor,
         CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        string cipherText = Convert.ToBase64String(cipherTextBytes);
        return cipherText;
    }
    #endregion
    #region Desencriptar
    /// <summary>
    /// Método para desencriptar un texto encriptado.
    /// </summary>
    /// <returns>Texto desencriptado</returns>
    public static string Desencriptar(string textoEncriptado)
    {
        return Desencriptar(textoEncriptado, "pass75dc@avz10", "s@lAvz", "MD5",
          1, "@1B2c3D4e5F6g7H8", 128);
    }
    /// <summary>
    /// Método para desencriptar un texto encriptado (Rijndael)
    /// </summary>
    /// <returns>Texto desencriptado</returns>
    public static string Desencriptar(string textoEncriptado, string passBase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        byte[] cipherTextBytes = Convert.FromBase64String(textoEncriptado);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passBase,
          saltValueBytes, hashAlgorithm, passwordIterations);
        byte[] keyBytes = password.GetBytes(keySize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged()
        {
            Mode = CipherMode.CBC
        };
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes,
          initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor,
          CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0,
          plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        string plainText = Encoding.UTF8.GetString(plainTextBytes, 0,
          decryptedByteCount);
        return plainText;
    }
    #endregion


}
