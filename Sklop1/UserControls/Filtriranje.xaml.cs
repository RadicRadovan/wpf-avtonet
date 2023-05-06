using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Sklop1.UserControls
{
    public partial class Filtriranje : UserControl
    {
        String kategorija;
        List<String> checkBoxes = new List<String> { };
        String tipPogona = "vsi";

        public Filtriranje()
        {
            InitializeComponent();
            var vm = ViewModel.getInstance();
            this.DataContext = vm;
           refreshList();
        }

        private void refreshList()
        {
            CollectionViewSource.GetDefaultView(ViewModel.getInstance().Oglasi).Filter = allFilter;
        }

        private bool allFilter(object item)
        {
            if (tipPogonaFilter(item) &&  checkBoxFilter(item))
            {
                return true;
            }
                return false;
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
            if (checkBoxes.Contains("suv"))
            {
                checkBoxes.Remove("suv");
            }
            else
            {
                checkBoxes.Add("suv");
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
            refreshList();
        }

        private void VsiEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "vsi";
            refreshList();
        }

        private void BencinEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "bencin";
            refreshList();
        }

        private void DiselEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "disel";
            refreshList();
        }

        private void HibridEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "hibrid";
            refreshList();
            
        }

        private void ElectroEvent(object sender, RoutedEventArgs e)
        {
            tipPogona = "electro";
            refreshList();
        }
    }
}