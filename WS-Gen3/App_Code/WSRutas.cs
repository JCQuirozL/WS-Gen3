using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descripción breve de WSRutas
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class WSRutas : System.Web.Services.WebService
{

    public WSRutas()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int insDireccion(string calle, string numero, string colonia, string ciudad, string estado, string cp)
    {
        SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

        if (cnx.State == System.Data.ConnectionState.Open)
        {
            cnx.Close();
        }
        SqlCommand cmd = new SqlCommand("Direcciones_Insertar", cnx);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Calle", calle);
        cmd.Parameters.AddWithValue("@Numero", numero);
        cmd.Parameters.AddWithValue("@Colonia", colonia);
        cmd.Parameters.AddWithValue("@Ciudad", ciudad);
        cmd.Parameters.AddWithValue("@Estado", estado);
        cmd.Parameters.AddWithValue("@CP", cp);
        cnx.Open();
        int IdOrigenDestino = int.Parse(cmd.ExecuteScalar().ToString());
        cnx.Close();
        return IdOrigenDestino;
    }
    [WebMethod]
    public string InsertarCargamento(long idruta, string descripcion, double peso)
    {
        SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
        if (cnx.State == System.Data.ConnectionState.Open)
        {
            cnx.Close();
        }

        cnx.Open();
        string query = "Cargamentos_Insertar";
        SqlCommand cmd = new SqlCommand(query, cnx);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Ruta_id", idruta);
        cmd.Parameters.AddWithValue("@Descripcion", descripcion);
        cmd.Parameters.AddWithValue("@Peso", peso);
        cmd.ExecuteNonQuery();
        cnx.Close();
        return "OK";
    }



}
