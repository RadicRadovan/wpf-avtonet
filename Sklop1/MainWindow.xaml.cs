using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Sklop1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel vm = ViewModel.getInstance();
            this.DataContext = vm;
            vm.oglasi.Add(new Oglas("Tamic oglas", "TAM", "bencin", "limuzina"));
            vm.oglasi.Add(new Oglas("Lada niva", "LADA",  "disel","SUV"));
            vm.oglasi.Add(new Oglas("Yugo 45", "Zastava","electro", "karavan" ));
            LV1.MouseDoubleClick += new MouseButtonEventHandler(PrikaziItem);
            LV1.Items.Refresh();
        }

        private void PrikaziItem(object sender, MouseButtonEventArgs e)
        {
            if (LV1.SelectedItem != null)
            {
                MessageBox.Show($"Izbrali ste oglas: {LV1.SelectedItem.ToString()}",
                    "Oglas", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Update(Object sender, RoutedEventArgs routedEventArgs)
        {
            UpdateWindow updateWindow = new UpdateWindow();
            var vm = this.DataContext as ViewModel;
            if(vm.Izbran == null)
            {   
                MessageBox.Show("Izberite oglas za posodobitev", "Oglas", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            updateWindow.Show();
        }

        private void Izhod(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        
        private void openSettings(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
        private void OdpriDodajOkno(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }

        private void Uvozi(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = ".xml",
                Filter ="Images (*.xml," + "All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            openFileDialog.ShowDialog();
            var vm = this.DataContext as ViewModel;
            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    try
                    {
                        XmlSerializer xml = new XmlSerializer(vm.Oglasi.GetType());
                        ObservableCollection<Oglas> oglasi = (ObservableCollection<Oglas>)xml.Deserialize(sr);
                        if (oglasi != null)
                        {
                            vm.Oglasi = oglasi;
                        }

                        LV1.Items.Refresh();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("PODATKI NISO KOMPATIBILNI: " + exception.Message);
                    }
                    finally
                    {
                        vm.Oglasi = new ObservableCollection<Oglas>();
                    }

                }
            }
        }

        private void Izvozi(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";
            saveFileDialog.InitialDirectory = @"D:\";
            saveFileDialog.Title = "Save a XML File";
            saveFileDialog.ShowDialog();
            var vm = this.DataContext as ViewModel;
            var name = saveFileDialog.FileName;
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                using (StreamWriter sw = new StreamWriter(name))
                {
                    XmlSerializer xml = new XmlSerializer(vm.Oglasi.GetType());
                    xml.Serialize(sw, vm.oglasi);
                }   
            }
        }
    }
}