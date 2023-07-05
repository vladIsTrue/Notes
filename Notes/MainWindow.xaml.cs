using Notes.Model;
using Notes.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {


        private readonly string PATH = $"{Environment.CurrentDirectory}\\dataModelsList.json";
        private BindingList<dataModel> dataModelsList;
        private FileIOService fileIOService;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fileIOService = new FileIOService(PATH);

            try
            {
                dataModelsList = fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            listNotes.ItemsSource = dataModelsList;
            dataModelsList.ListChanged += DataModelsList_ListChanged;
        }
        private void DataModelsList_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                fileIOService.SaveData(sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            // не самый лучший способ обновления listbox
            listNotes.ItemsSource = dataModelsList;
        }

        private void listNotes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            nameNote.Text = ((dataModel)listNotes.SelectedItem).NameNote;
            contentNote.Text = ((dataModel)listNotes.SelectedItem).ContentNote;
        }

        private void saveKeyDown(object sender, KeyEventArgs e)
        {
            if (nameNote.Text != null && e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                dataModel value = dataModelsList.SingleOrDefault(_value => _value.NameNote == nameNote.Text);

                if (value == null)
                {
                    // уникальный заголовок, следовательно его точно никто удалять не собирается 
                    dataModelsList.Add(new dataModel(nameNote.Text, contentNote.Text));
                }
                else
                {
                    value.ContentNote = contentNote.Text;
                }

                // вот это можно было бы вынести в отдельную функцию мб, тк дублирование кода и все дела 
                nameNote.Text = null;
                contentNote.Text = null;
            }
        }

        private void escapeKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                nameNote.Text = null;
                contentNote.Text = null;
            }
        }
        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            saveKeyDown(sender, e);

            escapeKeyDown(sender, e);
        }

        private void removeItem(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                dataModel delItem = dataModelsList.SingleOrDefault(value => value.NameNote == ((dataModel)listNotes.SelectedItem).NameNote);

                dataModelsList.Remove(delItem);
            }
        }
    }
}