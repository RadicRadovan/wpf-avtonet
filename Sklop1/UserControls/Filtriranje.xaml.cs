using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Sklop1.UserControls
{
    public partial class Filtriranje : UserControl, INotifyPropertyChanged
    {
        String kategorija;
        List<String> checkBoxes = new List<String> {};
        String tipPogona = "vsi";
        private String izbranaZnamka;
        
        public String IzbranaZnamka
        {
            get { return izbranaZnamka; }
            set
            {
                if (izbranaZnamka != value)
                {
                    izbranaZnamka = value;
                    OnPropertyChanged(nameof(izbranaZnamka));
                }
            }
        }
        public Filtriranje()
        {
            InitializeComponent();
            var vm = ViewModel.getInstance();
            this.DataContext = vm;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi);
            CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi);
            view.Filter = allFilter;
            // view.Filter = tipPogonaFilter;
            // view2.Filter = checkBoxFilter;
        }

        private bool allFilter(object item)
        {
            if (tipPogonaFilter(item) &&  checkBoxFilter(item) && znamkeFilter(item))
            {
                return true;
            }
                return false;
        }
        
        private bool znamkeFilter(object item)
        {
            if (String.IsNullOrEmpty(IzbranaZnamka))
            {
                return true;
            }
            else
            {
                var oglasi = ViewModel.getInstance().Oglasi; 
                return (((Oglas)item).Znamka.IndexOf(izbranaZnamka, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }  
        
        private bool tipPogonaFilter(object item)
        {
            if (tipPogona == "vsi" || String.IsNullOrEmpty(tipPogona))
            {
                return true;
            }
            else
            {
                var oglasi = ViewModel.getInstance().Oglasi; 
                return (((Oglas)item).Pogon.IndexOf(tipPogona, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            // {
            //     return true;
            // }
            // else
            // {
            //     var oglasi = ViewModel.getInstance().Oglasi; 
            //     return (((Oglas)item).Kategorija.IndexOf(kategorija, StringComparison.OrdinalIgnoreCase) >= 0);
            // }
        }

        private bool checkBoxFilter(object item)
        {
            if (checkBoxes.Count == 0)
            {
                return true;
            }
            foreach (var checkBox in checkBoxes)
            {
                if ((((Oglas)item).TipAvta.IndexOf(checkBox, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    return true;
                }
            }

            return false;
        }
        
        private void Znamke_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }

        private void Limuzina_OnClick(object sender, RoutedEventArgs e)
        {
            if (checkBoxes.Contains("limuzina"))
            {
                checkBoxes.Remove("limuzina");
            }
            else
            {
                checkBoxes.Add("limuzina");
            }
            
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }

        private void Karavan_OnChecked(object sender, RoutedEventArgs e)
        {
            if (checkBoxes.Contains("karavan"))
            {
                checkBoxes.Remove("karavan");
            }
            else
            {
                checkBoxes.Add("karavan");
            }
            
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }

        private void Suv_OnChecked(object sender, RoutedEventArgs e)
        {
            if (checkBoxes.Contains("SUV"))
            {
                checkBoxes.Remove("SUV");
            }
            else
            {
                checkBoxes.Add("SUV");
            }
            
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            this.CheckBoxLimuzina.IsChecked = false;
            this.CheckBoxKaravan.IsChecked = false;
            this.CheckBoxSUV.IsChecked = false;
            this.vsi.IsChecked = true;
            tipPogona = "vsi";
            checkBoxes.Clear();
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }

        private void VsiEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "vsi";
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }

        private void BencinEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "bencin";
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }

        private void DiselEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "disel";
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }

        private void HibridEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "hibrid";
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }

        private void ElectroEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "electro";
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Refresh();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}