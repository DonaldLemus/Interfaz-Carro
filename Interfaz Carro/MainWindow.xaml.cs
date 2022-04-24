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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Interfaz_Carro
{
    public partial class MainWindow : Window
    {
        ListBoxItem currentTrack;
        ListBoxItem PreviousTrack;
        Brush currentTrackIndicator;
        Brush TrackColor;
        DispatcherTimer timer;
        private bool isDragging;

        public Carro carrito { get; set; }

        public MainWindow(Carro _tmpcarro)
        {
            InitializeComponent();
            carrito = _tmpcarro;
            LabelDatosCarro.Content = $"Marca: {carrito.Marca} \nPropietario: {carrito.dueño} \nColor {carrito.Color}";
            //Pone de color azul la canción que se está reproduciendo
            currentTrackIndicator = Brushes.Blue;
            //Pone de color verde las canciones
            TrackColor = listaDeReproduccion.Foreground;

            timer = new DispatcherTimer();
            //Crea un intervalo de un segundo
            timer.Interval = TimeSpan.FromSeconds(1); // Tick se dispara cada segundo.
            timer.Tick += new EventHandler(timer_Tick);

            isDragging = false;
        }

        private void ButtonEncender_Click(object sender, RoutedEventArgs e)
        {
            LabelEstatusCarro.Content = carrito.EncenderMotor();
        }

        private void ButtonAcelerar_Click(object sender, RoutedEventArgs e)
        {
            LabelAcelDesa.Content = carrito.acelerar();
        }

        private void ButtonDesacelerar_Click(object sender, RoutedEventArgs e)
        {
            LabelAcelDesa.Content = carrito.desacelerar();
        }

        private void ButtonFrenar_Click(object sender, RoutedEventArgs e)
        {
            LabelAcelDesa.Content = carrito.frenar();
        }
        private void ButtonBocinar_Click(object sender, RoutedEventArgs e)
        {
            LabelBocinar.Content = carrito.bocinar();
        }

        private void ButtonApagar_Click(object sender, RoutedEventArgs e)
        {
            LabelApagar.Content = carrito.apagarmotor();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                SliderTimeLine.Value = MEmusicPlayer.Position.TotalSeconds;
            }
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            if (listaDeReproduccion.HasItems)//HasItems indica si el listbox tiene elementos
            {
                MEmusicPlayer.Pause(); //Pause es un metodo de MediaElement
            }
        }
        private void PlayTrack()
        {
            if (listaDeReproduccion.SelectedItem != currentTrack)
            {
                if (currentTrack != null)
                {
                    PreviousTrack = currentTrack;
                    PreviousTrack.Foreground = TrackColor;
                }
                currentTrack = (ListBoxItem)listaDeReproduccion.SelectedItem;
                currentTrack.Foreground = currentTrackIndicator;
                MEmusicPlayer.Source = new Uri(currentTrack.Tag.ToString());
                SliderTimeLine.Value = 0;
                MEmusicPlayer.Play();
            }
            else
            {
                MEmusicPlayer.Play();
            }
        }
        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (listaDeReproduccion.HasItems)
            {
                PlayTrack();
            }
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            if (listaDeReproduccion.HasItems)
            {
                MEmusicPlayer.Stop();
                SliderTimeLine.Value = 0;
            }
        }
        
        private void MoveToPrecedentTrack()
        {
            //Para moverse a la cancion anterior
            if (listaDeReproduccion.Items.IndexOf(currentTrack) > 0)
            {
                listaDeReproduccion.SelectedIndex = listaDeReproduccion.Items.IndexOf(currentTrack) - 1;
                PlayTrack();
            }
            else if (listaDeReproduccion.Items.IndexOf(currentTrack) == 0)
            {
                listaDeReproduccion.SelectedIndex = listaDeReproduccion.Items.Count - 1;
                PlayTrack();
            }

        }

        private void ButtonAnterior_Click(object sender, RoutedEventArgs e)
        {
            
            if (listaDeReproduccion.HasItems)
            {
                MoveToPrecedentTrack();
            }
        }
        private void MoveToNextTrack()
        {
            //Para moverse a la siguiente canción
            if (listaDeReproduccion.Items.IndexOf(currentTrack) < listaDeReproduccion.Items.Count - 1)
            {
                listaDeReproduccion.SelectedIndex = listaDeReproduccion.Items.IndexOf(currentTrack) + 1;
                PlayTrack();
                return;
            }
            else if (listaDeReproduccion.Items.IndexOf(currentTrack) == listaDeReproduccion.Items.Count - 1)
            {
                listaDeReproduccion.SelectedIndex = 0;
                PlayTrack();
                return;
            }

        }

        private void ButtonSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (listaDeReproduccion.HasItems)
            {
                MoveToNextTrack();
            }
        }

        private void SliderTimeLine_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void SliderTimeLine_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            //Al momento de mover este slider la canción la podremos retroceder o adelantar
            isDragging = false;
            MEmusicPlayer.Position =
                TimeSpan.FromSeconds(SliderTimeLine.Value);
        }
         private void SliderTimeLine_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MEmusicPlayer.Position =
                TimeSpan.FromSeconds(SliderTimeLine.Value);
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void MEmusicPlayer_MediaEnded_1(object sender, RoutedEventArgs e)
        {
            //Al terminar la canción se mueve a la siguiente
            SliderTimeLine.Value = 0;
            MoveToNextTrack();
        }

        private void MEmusicPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (MEmusicPlayer.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = MEmusicPlayer.NaturalDuration.TimeSpan;
                SliderTimeLine.Maximum = ts.TotalSeconds;
                SliderTimeLine.SmallChange = 1;
            }

            timer.Start();
        }

        private void listaDeReproduccion_DragEnter_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void listaDeReproduccion_Drop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string fileName in s)
            {
                if (System.IO.Path.GetExtension(fileName) == ".mp3" ||
                    System.IO.Path.GetExtension(fileName) == ".MP3")
                {
                    ListBoxItem lstItem = new ListBoxItem();
                    lstItem.Content = System.IO.Path.GetFileNameWithoutExtension(fileName);
                    lstItem.Tag = fileName;
                    listaDeReproduccion.Items.Add(lstItem);
                }
            }
            if (currentTrack == null)
            {
                listaDeReproduccion.SelectedIndex = 0;
                PlayTrack();
            }
        }
    }
}