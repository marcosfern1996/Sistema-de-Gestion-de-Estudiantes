using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Sistema_de_Gestión_de_Estudiante
{
    public static class Bbdd
    {
        public static string _dataSourse;
        public static string _initialCatalog;
        

        public static SqlConnection client = new SqlConnection($"Data Source={_dataSourse};Initial Catalog={_initialCatalog};Integrated Security=True;Trust Server Certificate=True");


        public static void AbrirConeccionSql( bool mostrarMensaje)
        {
            try
            {
                if (client.State != System.Data.ConnectionState.Open)
                {

                    client = new SqlConnection($"Data Source={_dataSourse};Initial Catalog={_initialCatalog};Integrated Security=True;Trust Server Certificate=True");
                    client.Open();
                    if( mostrarMensaje )
                    MessageBox.Show("Coneccion con exito.");
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("ubo un error" + ex);
            }


        }

        public static void CerrarConecionSql(bool mostrarMensaje)
        {
            if (client.State == System.Data.ConnectionState.Open)
            {
                client.Close();
                if( mostrarMensaje )
                MessageBox.Show("Coneccion Cerrada.");
            }

        }
    }
}
