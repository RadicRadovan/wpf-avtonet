using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Sklop1.UserControls
{
    public partial class Iskanje : UserControl
    {
        public Iskanje()
        {
            InitializeComponent();
            var vm = ViewModel.getInstance();
            this.DataContext = vm;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi);
            view.Filter = isci;
        }

        private bool isci(object item)
        {
           
            if (String.IsNullOrEmpty(iskanjeText.Text))
            {
                return true;
            }
            else
            {
                var oglasi = ViewModel.getInstance().Oglasi; 
                return (((Oglas)item).NazivAvta.IndexOf(iskanjeText.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
        public void OnTextChange (object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Filter = isci;
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }
    }
}