using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Sklop1.Properties;

namespace Sklop1
{
    public class ViewModelOkna : INotifyPropertyChanged
    {
        public StringCollection znamke;
        private string fileName;
        private string hours;
        private string minutes;
        private string secondes;
        public ViewModelOkna()
        {
            if (Properties.Settings.Default.Znamke == null)
            {
                Properties.Settings.Default.Znamke = new StringCollection();
            }
            if (Properties.Settings.Default.Timer == null)
            {
                Properties.Settings.Default.Timer = new StringCollection();
                Properties.Settings.Default.Timer.Add("0");
                Properties.Settings.Default.Timer.Add("0");
                Properties.Settings.Default.Timer.Add("0");
            }

            if (string.IsNullOrEmpty(Properties.Settings.Default.FileName))
            {
                Properties.Settings.Default.FileName = "C:\\Users\\";
            }
            Properties.Settings.Default.Save();
            znamke = Properties.Settings.Default.Znamke;
        }

        private String novaZnamka;
        private String selectedZnamka;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FileName
        {
            get { return Properties.Settings.Default.FileName; }
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    Properties.Settings.Default.FileName = fileName;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                    OnPropertyChange(nameof(fileName));
                }
            }
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

        private void AddZnamka(object o)
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

        public string Hours
        {
            get
            {
                if (Properties.Settings.Default.Timer[0] != null && Properties.Settings.Default.Timer[0] != "")
                {
                    hours = Properties.Settings.Default.Timer[0];
                }
                return hours;
            }
            set
            {
                if (hours != value)
                {
                    hours = value;
                    Properties.Settings.Default.Timer[0] = hours;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                    OnPropertyChange(nameof(hours));
                }
            }
        } 
        public string Minutes
        {
            get
            {
                if (Properties.Settings.Default.Timer[1] != null && Properties.Settings.Default.Timer[1] != "")
                {
                    minutes = Properties.Settings.Default.Timer[1];
                }
                return minutes;
            }
            set
            {
                if (minutes != value)
                {
                    minutes = value;
                    Properties.Settings.Default.Timer[1] = minutes;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                    OnPropertyChange(nameof(minutes));
                }
            }
        }    
        
        public string Secondes
        {
            get
            {
                if (Properties.Settings.Default.Timer[2] != null && Properties.Settings.Default.Timer[2] != "")
                {
                    secondes = Properties.Settings.Default.Timer[2];
                    
                }
                return secondes;
            }
            set
            {
                if (secondes != value)
                {
                    secondes = value;
                    Properties.Settings.Default.Timer[2] = secondes;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                    OnPropertyChange(nameof(secondes));
                }
            }
        }
    }
}