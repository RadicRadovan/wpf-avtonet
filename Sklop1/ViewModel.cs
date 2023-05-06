using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Sklop1
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static ViewModel instance = null;
        public ObservableCollection<Oglas> oglasi;
        public StringCollection znamke;
        public List<String> vsiPogoni = new List<String> { "bencin", "disel", "hibrid","electro"};
        public List<String> vsiTipi = new List<string>() { "limuzina", "karavan", "suv" };

        public static ViewModel getInstance()
        {
            if (instance == null)
            {
                instance = new ViewModel();
            }

            return instance;
        }
        public ObservableCollection<Oglas> Oglasi
        {
            get { return oglasi; }
            set
            {
                if(oglasi!= value)
                {
                    oglasi = value;
                    OnPropertyChange(nameof(oglasi));
                }
            }
        }

        private ViewModel()
        {
            Oglasi = new ObservableCollection<Oglas>();
            
            if (Properties.Settings.Default.Znamke == null)
            {
                Properties.Settings.Default.Znamke = new StringCollection();
            }
            znamke = Properties.Settings.Default.Znamke;
        }

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Oglas izbran;

        public List<String> VsiPogoni
        {
            get { return vsiPogoni; }
        }
        
        public List<String> VsiTipi
        {
            get { return vsiTipi; }
        }
        
        public StringCollection Znamke
        {
            get { return znamke; }
        }
        public Oglas Izbran
        {
            get { return izbran; }
            set
            {
                if (izbran != value)
                {
                    izbran = value;
                    if ( Izbran.NazivAvta != null && Izbran.Znamka != null)
                    {
                        NazivDodan = Izbran.NazivAvta;
                        ZnamkaDodana = Izbran.Znamka;
                        TipDodan = Izbran.TipAvta;
                        PogonDodan = Izbran.Pogon;
                        KategorijaDodana = Izbran.Kategorija;
                    }
                    OnPropertyChange(nameof(izbran));
                }
            }
        }

        private String nazivDodan;
        private String znamka;
        private String slikaDodana;
        private String tipDodan;
        private String pogonDodan;
        private String kategorijaDodana;
        
        public String TipDodan
        {
            get { return tipDodan; }
            set
            {
                if (tipDodan != value)
                {
                    tipDodan = value;
                    OnPropertyChange(nameof(tipDodan));
                }
            }
        }
        public String SlikaDodana
        {
            get { return slikaDodana; }
            set
            {
                if (slikaDodana != value)
                {
                    slikaDodana = value;
                    OnPropertyChange(nameof(slikaDodana));
                }
            }
        }
        public String PogonDodan
        {
            get { return pogonDodan; }
            set
            {
                if (pogonDodan != value)
                {
                    pogonDodan = value;
                    OnPropertyChange(nameof(pogonDodan));
                }
            }
        }
        public String KategorijaDodana
        {
            get { return kategorijaDodana; }
            set
            {
                if (kategorijaDodana != value)
                {
                    kategorijaDodana = value;
                    OnPropertyChange(nameof(kategorijaDodana));
                }
            }
        }
        public String NazivDodan
        {
            get { return nazivDodan; }
            set
            {
                if (nazivDodan != value)
                {
                    nazivDodan = value;
                    OnPropertyChange(nameof(nazivDodan));
                }
            }
        }
        
        public String ZnamkaDodana
        {
            get { return znamka; }
            set
            {
                if (znamka != value)
                {
                    znamka = value;
                    OnPropertyChange(nameof(znamka));
                }
            }
        }

        public ICommand AddItem => new RelayCommand(AddCar);
        public ICommand DeleteItem => new RelayCommand(DeleteCar);
        public ICommand UpdateItem => new RelayCommand(UpdateCar);

        private void AddCar(object o)
        {
            if (Izbran.NazivAvta == null && Izbran.Znamka == null)
            {
                MessageBox.Show("Vnesi podatke");
                return;
            }

            izbran.TipAvta = "limuzina";
            Oglasi.Add(izbran);
            OnPropertyChange(nameof(Oglasi));
        }

        private void DeleteCar(object o)
        {
            if (Izbran == null)
            {
                MessageBox.Show("Izberi avto");
                return;
            }

            Oglasi.Remove(Izbran);
        }

        private void UpdateCar(object o)
        {
            if (Izbran == null)
            {
                MessageBox.Show("Izberi avto");
                return;
            }

            if (Izbran.NazivAvta != NazivDodan)
            {
                Izbran.NazivAvta = NazivDodan;
            }
            if (Izbran.Znamka != ZnamkaDodana)
            {
                Izbran.Znamka = ZnamkaDodana;
            }
            if (Izbran.TipAvta != TipDodan)
            {
                Izbran.TipAvta = TipDodan;
            }
            if (Izbran.Pogon != PogonDodan)
            {
                Izbran.Pogon = PogonDodan;
            }
            if (Izbran.Kategorija != KategorijaDodana)
            {
                Izbran.Kategorija = KategorijaDodana;
            }
            if (Izbran.Slika != SlikaDodana && SlikaDodana != null)
            {
                Izbran.Slika = SlikaDodana;
            }
            OnPropertyChange(nameof(Izbran));
            OnPropertyChange(nameof(Oglasi));
        }
    }

    public class RelayCommand : ICommand
    {
        #region Private Members

        readonly Action<object> _execute;
        readonly Func<object, bool> _canExecute;

        #endregion

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}