using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sklop1
{
    public class Oglas : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string nazivAvta;
        private string znamka;
        private string slika;
        private string kategorija;
        private string pogon;
        private string tipAvta;

        public Oglas()
        {
        }

        public Oglas(string naziv, string znamka,  String pogon, string tipAvta = "limuzina")
        {
            this.nazivAvta = naziv;
            this.znamka = znamka;
            this.tipAvta = tipAvta;
            this.Pogon = pogon;
        }

        public string NazivAvta
        {
            get { return nazivAvta; }
            set
            {
                if (value.GetType() != typeof(string))
                {
                    throw new Exception("Napačen podatkovni tip naziva avta");
                }
                else
                {
                    nazivAvta = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Slika
        {
            get { return slika; }
            set
            {
                if (value.GetType() != typeof(string))
                {
                    throw new Exception("Napačen podatkovni tip slike avta");
                }
                else
                {
                    slika = value;
                    OnPropertyChanged(nameof(slika));
                }
            }
        }

        public string Znamka
        {
            get { return znamka; }
            set
            {
                if (value.GetType() != typeof(string))
                {
                    throw new Exception("Napačen podatkovni tip znamke avta");
                }
                else
                {
                    znamka = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Kategorija
        {
            get { return kategorija; }
            set
            {
                if (value.GetType() != typeof(string))
                {
                    throw new Exception("Napačen podatkovni tip kategorije avta");
                }
                else
                {
                    kategorija = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Pogon
        {
            get { return pogon; }
            set
            {
                if (value.GetType() != typeof(string))
                {
                    throw new Exception("Napačen podatkovni tip pogona avta");
                }
                else
                {
                    pogon = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TipAvta
        {
            get { return tipAvta; }
            set
            {
                if (value.GetType() != typeof(string))
                {
                    throw new Exception("Napačen podatkovni tip tipa(kategorije) avta");
                }
                else
                {
                    tipAvta = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HaveImage
        {
            get { return !string.IsNullOrEmpty(slika); }
        }

        public override string ToString()
        {
            return $"{nazivAvta}-{znamka}";
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}