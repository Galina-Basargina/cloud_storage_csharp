using System;
using System.Windows;
using coursework3.Model;

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
            FrameClass.Page = Page;
            FrameClass.Page.Navigate(new Pages.UsersListPage());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Задание минимальных размеров окна приложения

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Width <= 800) this.Width = 800+1;
            if (this.Height <= 600) this.Height = 600+1;    
        }
    }
}

