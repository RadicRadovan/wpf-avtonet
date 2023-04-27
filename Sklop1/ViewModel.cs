using System;
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
            set { oglasi = value; }
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
                    }
                    OnPropertyChange(nameof(izbran));
                }
            }
        }

        private String nazivDodan;
        private String znamka;

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

            if ( NazivDodan == null || ZnamkaDodana == null)
            {
                MessageBox.Show("Vnesi podatke: {0}, {1}" + NazivDodan, ZnamkaDodana);
                return;
            }
            Izbran.NazivAvta = NazivDodan;
            Izbran.Znamka = ZnamkaDodana;
            OnPropertyChange(nameof(Oglasi));
            OnPropertyChange(nameof(Izbran));
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