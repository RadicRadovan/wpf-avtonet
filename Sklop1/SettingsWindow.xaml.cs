using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Sklop1
{
    public partial class SettingsWindow : Window
    {
        DispatcherTimer timer;
        private bool isChecked = false;
        public SettingsWindow()
        {
            InitializeComponent();
            ViewModelOkna viewModel = new ViewModelOkna();
            this.DataContext = viewModel;
            Znamke.ItemsSource = viewModel.znamke;
            timer = new DispatcherTimer();
            timer.Tick += save;
            if (Properties.Settings.Default.IsEnable)
            { 
                isChecked = true;
                timer.Interval = new TimeSpan(
                    int.Parse(Properties.Settings.Default.Timer[0]), 
                    int.Parse(Properties.Settings.Default.Timer[1]),
                    int.Parse(Properties.Settings.Default.Timer[2])
                );
                timer.Start();
            }
        }

        public string IsChecked
        {
            get { return isChecked.ToString(); }
        }

        private void save(object sender, EventArgs e)
        {
            var vmMain = ViewModel.getInstance();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.FileName))
            {
                using (StreamWriter sw = new StreamWriter(Properties.Settings.Default.FileName))
                {
                    XmlSerializer xml = new XmlSerializer(vmMain.Oglasi.GetType());
                    xml.Serialize(sw, vmMain.oglasi);
                }   
            }
        }
        private void DodajClikck(object sender, RoutedEventArgs e)
        {
            ViewModelOkna vm = (ViewModelOkna)this.DataContext;
            vm.DodajZnamko.Execute(null);
            Znamke.Items.Refresh();
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {
            ViewModelOkna vm = (ViewModelOkna)this.DataContext;
            vm.OdstraniZnamko.Execute(null);
            Znamke.Items.Refresh();
        }
        
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            ViewModelOkna vm = (ViewModelOkna)this.DataContext;
            vm.PosodobiZnamko.Execute(null);
            Znamke.Items.Refresh();
        }
        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void addLocation(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";
            saveFileDialog.InitialDirectory = @"D:\";
            saveFileDialog.Title = "Save a XML File";
            saveFileDialog.ShowDialog();
            var vm = this.DataContext as ViewModelOkna;
            vm.FileName = saveFileDialog.FileName;
        }

        private void setOn(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as ViewModelOkna;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.FileName) &&
                Properties.Settings.Default.Timer.Count != 0)
            {
                isChecked = true;
                Properties.Settings.Default.IsEnable = true;
                timer.Interval = new TimeSpan(
                    int.Parse(Properties.Settings.Default.Timer[0]), 
                    int.Parse(Properties.Settings.Default.Timer[1]),
                    int.Parse(Properties.Settings.Default.Timer[2])
                    );
                timer.Start();   
            }
        }
        private void setOff(object sender, RoutedEventArgs e)
        {
            isChecked = false;
            Properties.Settings.Default.IsEnable = false;
            if (timer.IsEnabled)
            {
                timer.Stop();   
            }
        }
    }
}