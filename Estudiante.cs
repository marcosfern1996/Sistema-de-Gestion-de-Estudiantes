using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics.CodeAnalysis;

namespace Sistema_de_Gestión_de_Estudiante
{
    public class Estudiante : Persona
    {
        public int legajo { get; set; }
        public int promedio { get; set; }


        public void Agregar()
        {

            Bbdd.AbrirConeccionSql(false);
            SqlCommand cmd = new SqlCommand();
           /* cmd = new SqlCommand("select MAX(legajo) from Estudiantes;", Bbdd.client);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                legajo = reader.GetInt32(0) + 1;
            }
            reader.Close();*/
            cmd = new SqlCommand("INSERT INTO Estudiantes(Nombre,edad,promedio,Dni) VALUES (@nombre,@edad,@promedio,@dni)", Bbdd.client);

            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@edad", edad);
            cmd.Parameters.AddWithValue("@promedio", promedio);
            cmd.Parameters.AddWithValue("@dni", dni);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" aca esta la falla " + ex);
            }
            Bbdd.CerrarConecionSql(false);
        }

        public List<Estudiante> Listar()
        {
            List<Estudiante> estudiantes = new List<Estudiante>();
            Bbdd.AbrirConeccionSql(false);
            SqlCommand sql = new SqlCommand("SELECT * FROM Estudiantes;", Bbdd.client);
            SqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                Estudiante estudiante = new Estudiante()
                {
                    legajo = reader.GetInt32(0),
                    nombre = reader.GetString(1),
                    edad = reader.GetInt32(2),
                    promedio = reader.GetInt32(3),
                    dni = reader.GetInt32(4)
                };
                estudiantes.Add(estudiante);
            }
            reader.Close();
            Bbdd.CerrarConecionSql(false);
            return estudiantes;

        }

        public string TipoCmd(string filtro, string busqueda, string filtroAvanzado)
        {
            string comando;
            switch (filtro)
            {
                case "Promedio":
                    comando = $"SELECT * FROM Estudiantes WHERE {filtro} {filtroAvanzado} {busqueda};";
                    return comando;
                    break;
                case "Nombre":
                    comando = $"SELECT * FROM Estudiantes WHERE {filtro} {filtroAvanzado} '{busqueda}';";
                    return comando;
                    break;
                case "Edad":
                    comando = $"SELECT * FROM Estudiantes WHERE {filtro} {filtroAvanzado} {busqueda};";
                    return comando;
                    break;
                case "Legajo":
                    comando = $"SELECT * FROM Estudiantes WHERE {filtro} {filtroAvanzado} {busqueda};";
                    return comando;
                    break;

            }
            return null;
        }

        public List<Estudiante> Buscar(bool FiltroNumerico, string filtro, string busqueda, string filtroAvanzado)
        {

            try
            {
                List<Estudiante> estudiantes = new List<Estudiante>();
                Bbdd.AbrirConeccionSql(false);
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand = new SqlCommand(TipoCmd(filtro, busqueda, filtroAvanzado), Bbdd.client);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Estudiante estudiante = new Estudiante
                    {
                        legajo = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        edad = reader.GetInt32(2),
                        promedio = reader.GetInt32(3),
                        dni = reader.GetInt32(4),
                    };
                    estudiantes.Add(estudiante);
                }
                reader.Close();
                return estudiantes;

            }
            catch (Exception ex)
            {
                MessageBox.Show("error");
                return null;
            }

        }

        public Estudiante ActualizarBuscar(int legajo)
        {
            Estudiante estudiante = new Estudiante();
            Bbdd.AbrirConeccionSql(false);
            SqlCommand cmd = new SqlCommand($"select Nombre, Edad, Promedio, Dni from Estudiantes where Legajo = {legajo};", Bbdd.client);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                estudiante.nombre = reader.GetString(0);
                estudiante.edad = reader.GetInt32(1);
                estudiante.promedio = reader.GetInt32(2);
                estudiante.dni = reader.GetInt32(3);
                
               
            }
            ;
            reader.Close();

            return estudiante;


        }

        public void ActualizarSubir(Estudiante estudiante)
        {
            Bbdd.AbrirConeccionSql(false);
            string query = "Update Estudiantes set Nombre = @name , edad = @edad, promedio = @promedio, Dni = @dni " +
                "where Legajo = @legajo;";
            SqlCommand cmd = new SqlCommand(query, Bbdd.client);
            cmd.Parameters.AddWithValue("@legajo", estudiante.legajo);
            cmd.Parameters.AddWithValue("@name", estudiante.nombre);
            cmd.Parameters.AddWithValue("@edad", estudiante.edad);
            cmd.Parameters.AddWithValue("@promedio", estudiante.promedio);
            cmd.Parameters.AddWithValue("@dni", estudiante.dni);
            cmd.ExecuteNonQuery();
        }
        public void Eliminar(string legajo)
        {
            Bbdd.AbrirConeccionSql(false);
            string query = $"delete from Estudiantes where Legajo = @legajo;";
            SqlCommand cmd = new SqlCommand(query,Bbdd.client);
            cmd.Parameters.AddWithValue("@legajo",legajo);
            cmd.ExecuteNonQuery ();
            MessageBox.Show("El alumno fue eliminado del sistema.");

        }

    }
}
