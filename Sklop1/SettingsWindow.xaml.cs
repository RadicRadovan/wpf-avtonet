using System.Windows;

namespace Sklop1
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            ViewModelOkna viewModel = new ViewModelOkna();
            this.DataContext = viewModel;
            Znamke.ItemsSource = viewModel.znamke;
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
    }
}