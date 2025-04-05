using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using coursework3.Class;
using coursework3.Model;
using coursework3.Pages;

namespace coursework3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FrameClass.dishFrame = dishFrame;
            FrameClass.dishFrame.Navigate(new Pages.UsersListPage());
            //FrameClass.productFrame = productFrame;
            //FrameClass.productFrame.Navigate(new Pages.UserDetailsPage());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Width <= 800) this.Width = 800+1;
            if (this.Height <= 600) this.Height = 600+1;
            
        }

        public void onLoad()
        {
            //MessageBox.Show("load on start");
            GridLoad.Visibility = Visibility.Visible;
            //System.Threading.Thread.Sleep(5000);
            //MessageBox.Show("load on finish");


        }

        public void offLoad()
        {
            GridLoad.Visibility = Visibility.Collapsed;
        }
    }
}

