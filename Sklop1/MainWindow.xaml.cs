using System;
using System.Windows;
using System.Windows.Input;

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
            vm.oglasi.Add(new Oglas("Tamic oglas", "TAM", "limuzina"));
            vm.oglasi.Add(new Oglas("Lada niva", "LADA","limuzina"));
            vm.oglasi.Add(new Oglas("Yugo 45", "Zastava","karavan"));
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
        
    }
}