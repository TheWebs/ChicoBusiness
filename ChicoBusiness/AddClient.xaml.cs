using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ChicoBusiness
{
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : MetroWindow
    {
        SimplerAES magia = new SimplerAES();
        public AddClient()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string zenome = magia.Encrypt(nome.Text);
                string zevi = magia.Encrypt(vi.Text);
                string zevp = magia.Encrypt(vp.Text);
                string time = magia.Encrypt(DateTime.Today.ToString());
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\Pessoas\\" + zenome + ".cb", zenome + "*" + zevi + "*" + zevp + "*" + time);
            }catch(Exception ex)
            {
                MessageBox.Show("Erro! Nao foi possivel adicionar o cliente " + nome.Text + "!", "Chico Business : Erro!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            MessageBox.Show("Cliente " + nome.Text + " foi adicionado com sucesso!", "Chico Business : Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

            this.Close();
        }
    }
}
