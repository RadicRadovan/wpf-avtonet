using System.Windows;
using Microsoft.Win32;

namespace Sklop1
{
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel.getInstance(); 
            ViewModel.getInstance().Izbran = new Oglas();
        }

        private void dodaj(object sender, RoutedEventArgs e)
        {
            ViewModel vm = this.DataContext as ViewModel;
            vm.AddItem.Execute(true);
            vm.Izbran = new Oglas();
        }

        private void addImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = ".jpg",
                Filter ="Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            var vm = this.DataContext as ViewModel;
            openFileDialog.ShowDialog();
            vm.Izbran.Slika = openFileDialog.FileName;
        }
    }
}