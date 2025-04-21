using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.IO;
using Microsoft.Win32;
using Azure.Messaging;

namespace Sistema_de_Gestión_de_Estudiante
{
    /// <summary>
    /// Lógica de interacción para GestionDeEstudiantes.xaml
    /// </summary>
    public partial class GestionDeEstudiantes : Window
    {
        Estudiante estudiante;

        public GestionDeEstudiantes()
        {
            InitializeComponent();

            this.Closing += GestionDeEstudiantes_Closing;
        }

        private void GestionDeEstudiantes_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            bool listo = true;
            estudiante = new Estudiante(); 
            
            if (VerificarDatos(text_dni.Text, text_dni.Name)) { estudiante.dni = int.Parse(text_dni.Text); } else { listo = false; }
            estudiante.nombre = text_nombre.Text;
            if (VerificarDatos(text_edad.Text, text_edad.Name)) { estudiante.edad = int.Parse(text_edad.Text); } else { listo = false; }
            if (VerificarDatos(text_promedio.Text, text_promedio.Name)) { estudiante.promedio = int.Parse(text_promedio.Text); } else { listo = false; }

            if (listo)
            {
              
                estudiante.Agregar();

                text_dni.Text = "";
                text_nombre.Text = "";
                text_edad.Text = "";
                text_promedio.Text = "";

                MessageBox.Show("Datos cargados Exitosamente. ");
            }
        }

        public bool VerificarDatos(string casilla, string name)
        {
            int num;

            if (!string.IsNullOrEmpty(casilla))
            {

                if (int.TryParse(casilla, out num))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("ingrese solo numeros en " + name);
                    return false;
                }


            }
            return false;

        }


        private void ButtonListar_Click(object sender, RoutedEventArgs e)
        {
            estudiante = new Estudiante();
            dataGrid_listar.ItemsSource = estudiante.Listar();

        }

        private void button_buscar_Click(object sender, RoutedEventArgs e)
        {
            estudiante = new Estudiante();

            text_filtro_avanzado.Text = ComprobacionDeFiltroDeBusqueda(text_filtro_avanzado.Text);
                resultado_busqueda.ItemsSource = estudiante.Buscar(false, text_filtro.Text, text_buscar.Text, text_filtro_avanzado.Text);

        }
        private string ComprobacionDeFiltroDeBusqueda(string filtro)
        {
            switch (filtro)
            {
                case "Mayor": return ">"; break;
                case "Menor": return "<"; break;
                case "Igual": return "="; break;
                case "Mayor Igual": return ">="; break;
                case "Menor Igual": return "<="; break;
            }
            return "=";
        }

        private void ButtonImprimir_Click(object sender, RoutedEventArgs e)
        {
            Imprimir("Lista de Alumnos.csv", dataGrid_listar);
        }


        public void Imprimir(string nombrearchivo, DataGrid dataGrid)
        {
            SaveFileDialog sfd = new SaveFileDialog() {
            Filter = "Archivos CSV (*.csv)|*.csv",
            FileName = nombrearchivo,
            Title = "Guardar Archivo"
            };
            if (sfd.ShowDialog() == true) {
                try
                {
                    using (StreamWriter streamWriter = new StreamWriter(sfd.FileName)) {
                        streamWriter.WriteLine("Legajo,Nombre,Edad,Promedio");
                        foreach(var item in dataGrid.Items)
                        {
                            if(item is Estudiante estudiante)
                            {
                                streamWriter.WriteLine($"{estudiante.legajo},{estudiante.nombre},{estudiante.edad},{estudiante.promedio}");
                            }
                            
                        }streamWriter.Close();

                    }
                }
                catch (Exception ex) { }
                }
        }

        private void ButtonImprimir2_Click(object sender, RoutedEventArgs e)
        {
            Imprimir("Busqueda_Alumnos.csv", resultado_busqueda);
        }

        private void button_legajo_Busqueda_Click(object sender, RoutedEventArgs e)
        {
            estudiante = new Estudiante();
            if (VerificarDatos(text_legajo.Text, text_legajo.Name)){
                estudiante = estudiante.ActualizarBuscar(int.Parse(text_legajo.Text));
            }

            text_dni_actualizar.Text = estudiante.dni.ToString();
            text_nombre_actualizar.Text = estudiante.nombre;
            text_edad_actualizar.Text = estudiante.edad.ToString();
            text_promedio_actualizar.Text = estudiante.promedio.ToString();
        }

        private void button_legajo_update_Click(object sender, RoutedEventArgs e)
        {
            estudiante = new Estudiante();
            estudiante.legajo = int.Parse(text_legajo.Text);
            estudiante.nombre = text_nombre_actualizar.Text;
            estudiante.edad = int.Parse(text_edad_actualizar.Text);
            estudiante.promedio = int.Parse(text_promedio_actualizar.Text);
            estudiante.dni = int.Parse(text_dni_actualizar.Text);

            estudiante.ActualizarSubir(estudiante);
        }

        private void eliminar_legajo_buscar_Click(object sender, RoutedEventArgs e)
        {
            estudiante = new Estudiante();
            if (VerificarDatos(text_legajo_Eliminar.Text, text_legajo_Eliminar.Name))
            {
                estudiante = estudiante.ActualizarBuscar(int.Parse(text_legajo_Eliminar.Text));
            }

            text_nombre_Eliminar.Text = estudiante.nombre;
            text_edad_Eliminar.Text = estudiante.edad.ToString();

        }

        private void eliminar_legajo_Click(object sender, RoutedEventArgs e)
        {
            
            estudiante=new Estudiante();
            estudiante.Eliminar(text_legajo_Eliminar.Text);
        }
    }

}
