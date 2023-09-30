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

namespace ZamlApp2
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private MainWindow _mw;
        public MenuWindow(MainWindow mw)
        {
            this._mw = mw;
            InitializeComponent();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this._mw.continueGame = false;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this._mw.continueGame = true;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            this._mw.END_GAME = true;
        }
    }
}
