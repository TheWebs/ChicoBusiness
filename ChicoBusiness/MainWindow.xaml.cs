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
using MahApps.Metro.Controls;
using System.IO;
using System.Windows.Threading;

namespace ChicoBusiness
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        double tempopassadodias;
        double tempopassadohoras;
        double tempopassadominutos;
        double tempopassadosegundos;
        SimplerAES magia = new SimplerAES();
        public MainWindow()
        {
            InitializeComponent();
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 40;
            foreach(string ze in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Pessoas"))
            {
                FileInfo lo = new FileInfo(ze);
                listBox.Items.Add(magia.Decrypt(lo.Name.Replace(lo.Extension, "")));
            }
            listBox.SelectedIndex = 0;
            string texto = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Pessoas\\" + magia.Encrypt(listBox.SelectedItem.ToString()) + ".cb");
            string[] detalhes = texto.Split('*');
            NomeHomem.Content = magia.Decrypt(detalhes[0]);
            ValorInicial.Content = magia.Decrypt(detalhes[1]) + " €";
            ValorPrometido.Content = magia.Decrypt(detalhes[2]) + " €";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new System.EventHandler(timer_Tick);
            timer.Start();
            

        }


        void timer_Tick(object sender, EventArgs e)
        {
            string texto = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Pessoas\\" + magia.Encrypt(listBox.SelectedItem.ToString()) + ".cb");
            string[] detalhes = texto.Split('*');
            ObterTempo(magia.Decrypt(detalhes[3]));
            tempolabel.Content = ((Int32)tempopassadodias).ToString() + " dias, " + ((Int32)tempopassadohoras).ToString() + " horas, " + ((Int32)tempopassadominutos).ToString() + " minutos e " + ((Int32)tempopassadosegundos).ToString() + " segundos";

        }

        // 0- nome; 1- valor inicial; 2- valor prometido
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                string texto = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Pessoas\\" + magia.Encrypt(listBox.SelectedItem.ToString()) + ".cb");

                string[] detalhes = texto.Split('*');
                NomeHomem.Content = magia.Decrypt(detalhes[0]);
                ValorInicial.Content = magia.Decrypt(detalhes[1]) + " €";
                ValorPrometido.Content = magia.Decrypt(detalhes[2]) + " €";
                ObterTempo(magia.Decrypt(detalhes[3]));
                tempolabel.Content = ((Int32)tempopassadodias).ToString() + " dias, " + ((Int32)tempopassadohoras).ToString() + " horas, " + ((Int32)tempopassadominutos).ToString() + " minutos e " + ((Int32)tempopassadosegundos).ToString() + " segundos";
            }

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddClient janela = new AddClient();
            janela.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            foreach (string ze in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Pessoas"))
            {
                FileInfo lo = new FileInfo(ze);
                listBox.Items.Add(magia.Decrypt(lo.Name.Replace(lo.Extension, "")));
            }
            listBox.SelectedIndex = 0;
        }

        public void ObterTempo(string inicio)
        {
            string data = inicio.Replace('/', '-');
            // 1.
            // Parse the date and put in DateTime object.
            DateTime startDate = DateTime.Parse(data);

            // 2.
            // Get the current DateTime.
            DateTime now = DateTime.Now;

            // 3.
            // Get the TimeSpan of the difference.
            TimeSpan elapsed = now.Subtract(startDate);

            // 4.
            // Get number of days ago.
            tempopassadodias = elapsed.Days;
            tempopassadohoras = elapsed.Hours;
            tempopassadominutos = elapsed.Minutes;
            tempopassadosegundos = elapsed.Seconds;

        }
    }
}
