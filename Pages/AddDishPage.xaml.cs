using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;
using coursework3.Classes;
using coursework3.Model;

namespace coursework3.Pages
{
    public partial class AddDishPage : Page
    {
        //Dish newDish = new Dish();
        //ObservableCollection<Composition> composition = new ObservableCollection<Composition>();
        ProductFromDb productFromDb = new ProductFromDb();
        TypeFromDb typeFromDb = new TypeFromDb();
        DishFromDb dishFromDb = new DishFromDb();
        string imgPath = "";
        public AddDishPage()
        {
            InitializeComponent();
            //ProductsComboBox.ItemsSource = productFromDb.GetProductsName();
            //img.DataContext = newDish;
            //ProductsListView.ItemsSource = composition;
        }

        // Валидация числового ввода
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Обработчики кнопок
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg)|*.png;*.jpg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Обработка выбранного файла
                imgPath = openFileDialog.FileName;
                img.Source = new BitmapImage(new Uri(imgPath, UriKind.RelativeOrAbsolute));
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsComboBox.SelectedItem != null &&
                !string.IsNullOrWhiteSpace(ProductWeightTextBox.Text))
            {
                //composition.Add(new Composition(-1, ProductsComboBox.SelectedValue.ToString(), int.Parse(ProductWeightTextBox.Text)));
                ProductWeightTextBox.Clear();
            }
        }

        private void AddDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (DishNameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Имя блюда не введено");
                return;
            }
            if (DishWeightTextBox.Text == string.Empty)
            {
                MessageBox.Show("Вес блюда не введен");
                return;
            }
            if (TypeComboBox.Text == string.Empty)
            {
                MessageBox.Show("Категория блюда не выбрана");
                return;
            }
            if (BaseComboBox.Text == string.Empty)
            {
                MessageBox.Show("Основа блюда не выбрана");
                return;
            }
            if (DishWeightTextBox.Text == string.Empty)
            {
                MessageBox.Show("Вес блюда не введен");
                return;
            }
            //if (composition.Count == 0)
            //{
            //    MessageBox.Show("Состав блюда пуст");
            //    return;
            //}
            //newDish.DishName = DishNameTextBox.Text;
            MessageBox.Show(TypeComboBox.SelectionBoxItemStringFormat);
            MessageBox.Show(TypeComboBox.Items[TypeComboBox.SelectedIndex].ToString());
            MessageBox.Show(TypeComboBox.SelectedItem.ToString().Substring(38));
            MessageBox.Show(BaseComboBox.SelectedItem.ToString().Substring(38));

            //newDish.Type = TypeComboBox.SelectedItem.ToString().Substring(38);
            //newDish.Base = BaseComboBox.SelectedItem.ToString().Substring(38);
            //newDish.Weight = int.Parse(DishWeightTextBox.Text);
            //newDish.ImageWithPath = imgPath;

            int idDish;
            //dishFromDb.AddDish(newDish, composition, out idDish);
            //MessageBox.Show(idDish.ToString());
        }
    }
}
