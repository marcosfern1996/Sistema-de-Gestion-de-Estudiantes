using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sistema_de_Gestión_de_Estudiante
{

    public partial class ConeccionnBbdd : Window
    {
        private static string _dataSourse;
        private static string _initialCatalog;

        public ConeccionnBbdd()
        {
            InitializeComponent();
            GestionDeEstudiantes gestionDeEstudiantes = new GestionDeEstudiantes();

            this.Closed += ConeccionnBbdd_Closed;
        }

        private void ConeccionnBbdd_Closed(object? sender, EventArgs e)
        {
            Application.Current.Shutdown();

        }

        public void CopiarDatosDeConeccion()
        {
            Bbdd._dataSourse = text_dataSourse.Text;
            Bbdd._initialCatalog = text_initialCatalog.Text;
        }



        private void ConectarBbdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CopiarDatosDeConeccion();

                Bbdd.AbrirConeccionSql(true);
                GestionDeEstudiantes gestionDeEstudiantes = new GestionDeEstudiantes();
                if (Bbdd.client.State == System.Data.ConnectionState.Open)
                {
                    gestionDeEstudiantes.Show();

                    this.Hide();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ocurrio un error de Conección:" + ex.Message);
            }

        }


    }
}
