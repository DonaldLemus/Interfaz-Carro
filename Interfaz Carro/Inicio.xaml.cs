using Interfaz_Carro.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Interfaz_Carro
{
    public partial class Inicio : Window
    {
        Carro car1;
        public Inicio()
        {
            InitializeComponent();
        }

        public void CrearCarro()
        {
            string marca = TextBoxMarca.Text;
            string dueño = TextBoxDueño.Text;
            int maxvel = Convert.ToInt32(TextBoxMaxVel.Text);
            string color = TextBoxColor.Text;
            car1 = new Carro(marca, maxvel, dueño, color);
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            CrearCarro();
            MainWindow formulario = new MainWindow(car1);
            formulario.Show();
        }
    }
}
