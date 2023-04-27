using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Sklop1.Properties;

namespace Sklop1
{
    public class ViewModelOkna : INotifyPropertyChanged
    {
        
        public StringCollection znamke;
        
        public ViewModelOkna()
        {
            if (Properties.Settings.Default.Znamke == null)
            {
                Properties.Settings.Default.Znamke = new StringCollection();
            }
            znamke = Properties.Settings.Default.Znamke;
        }

        private String novaZnamka;
        private String selectedZnamka;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand DodajZnamko => new RelayCommand(AddZnamka);
        public ICommand OdstraniZnamko => new RelayCommand(DeleteZnamka);
        public ICommand PosodobiZnamko => new RelayCommand(UpdateZnamka);

        private void UpdateZnamka(object o)
        {
            if (NovaZnamka == null || SelectedZnamka == null)
            {
                MessageBox.Show("Vnesi podatke");
                return;
            }
            int index = Properties.Settings.Default.Znamke.IndexOf(SelectedZnamka);
            Properties.Settings.Default.Znamke[index] = NovaZnamka;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            znamke = Properties.Settings.Default.Znamke;
            OnPropertyChange(nameof(znamke));
        }
        private void DeleteZnamka(object o)
        {
            if (SelectedZnamka == null)
            {
                MessageBox.Show("Izberi znamko");
                return;
            }
            Properties.Settings.Default.Znamke.Remove(SelectedZnamka);
            Properties.Settings.Default.Save();
            znamke = Properties.Settings.Default.Znamke;
            OnPropertyChange(nameof(znamke));
        }
            private void AddZnamka(object o )
        {
            if (NovaZnamka == null)
            {
                MessageBox.Show("Vnesi podatke");
                return;
            }
            
            Properties.Settings.Default.Znamke.Add(NovaZnamka);
            Properties.Settings.Default.Save();
            znamke = Properties.Settings.Default.Znamke;
            OnPropertyChange(nameof(znamke));
        }
        public String NovaZnamka
        {
            get { return novaZnamka; }
            set
            {
                if (novaZnamka != value)
                {
                    novaZnamka = value;
                    OnPropertyChange(nameof(novaZnamka));
                }
            }
        }

        public String SelectedZnamka
        {
            get { return selectedZnamka; }
            set
            {
                if (selectedZnamka != value)
                {
                    selectedZnamka = value;
                    OnPropertyChange(nameof(selectedZnamka));
                }
            }
        }
    }
}
