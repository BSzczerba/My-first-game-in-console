using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Sterowanie
    {
      private ConsoleKeyInfo KrokWLewo;
      private ConsoleKeyInfo KrokWPrawo;
      private ConsoleKeyInfo Skok;
      private ConsoleKeyInfo UzyjPrzedmiotu;
      private ConsoleKeyInfo ZmienPrzedmiot;
      private ConsoleKeyInfo PrzeladujBron;
      private List<ConsoleKeyInfo> ListaKlawiszy = new List<ConsoleKeyInfo>();

        public Sterowanie()
        {
            string[] lines = File.ReadAllLines("sterowanie.txt");           
              KrokWLewo = new ConsoleKeyInfo((char)ConsoleKey.LeftArrow, ConsoleKey.LeftArrow,false,false,false);
              KrokWPrawo = new ConsoleKeyInfo((char)ConsoleKey.RightArrow, ConsoleKey.RightArrow, false, false, false);
              Skok = new ConsoleKeyInfo((char)ConsoleKey.UpArrow, ConsoleKey.UpArrow, false, false, false);
              UzyjPrzedmiotu = new ConsoleKeyInfo((char)ConsoleKey.E, ConsoleKey.E, false, false, false);
              ZmienPrzedmiot = new ConsoleKeyInfo((char)ConsoleKey.F, ConsoleKey.F, false, false, false);
              PrzeladujBron = new ConsoleKeyInfo((char)ConsoleKey.R, ConsoleKey.R, false, false, false);
            ListaKlawiszy.Add(KrokWPrawo);
            ListaKlawiszy.Add(KrokWLewo);
            ListaKlawiszy.Add(Skok);
            ListaKlawiszy.Add(UzyjPrzedmiotu);
            ListaKlawiszy.Add(ZmienPrzedmiot);
            ListaKlawiszy.Add(PrzeladujBron);
           /* int licz = 0;
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(ConsoleKeyInfo));
            foreach (string line in lines)
            {
                ListaKlawiszy[licz].KeyChar = line;
                licz++;
            }*/
        }
        public void ZapiszDoPliku()
        {
            string[] lines =
        {
            $"{KrokWPrawo.Key}", $"{KrokWLewo.Key}", $"{Skok.Key}", $"{UzyjPrzedmiotu.Key}", $"{ZmienPrzedmiot.Key}", $"{PrzeladujBron.Key}"
        };

            File.WriteAllLines("sterowanie.txt", lines);
        }
        private bool SprawdzCzyJestZajęty(ConsoleKeyInfo wartosc)
        {
            for (int i = 0; i < ListaKlawiszy.Count; i++)
            {               
                if (wartosc.Key == ListaKlawiszy[i].Key)
                {
                    return false;
                }
            }
            return true;
        }
        private void Usun(ConsoleKeyInfo wartosc)
        {
            for (int i = 0; i < ListaKlawiszy.Count; i++)
            {
                if (wartosc.Key == ListaKlawiszy[i].Key)
                {
                    ListaKlawiszy.RemoveAt(i);
                    return;
                }
            }
        }
        private void Zajety(ConsoleKeyInfo wartosc)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"Klawisz {wartosc.Key} jest już zajęty");
            Console.ResetColor();
            Console.ReadLine();
        }
        private ConsoleKeyInfo Zamien(ConsoleKeyInfo wartosc, ConsoleKeyInfo poprzedni)
        {
            Usun(poprzedni);
            ListaKlawiszy.Add(wartosc);
            GC.Collect();
            return wartosc;
        }
        public ConsoleKeyInfo lewo
        {
            get
            {
                return KrokWLewo;
            }
            set
            {
                if (SprawdzCzyJestZajęty(value))
                {
                    KrokWLewo = Zamien(value,KrokWLewo);
                }
                else
                {
                    Zajety(value);
                }
            }
        }
        public ConsoleKeyInfo prawy
        {
            get
            {
                return KrokWPrawo;
            }
            set
            {
                if (SprawdzCzyJestZajęty(value))
                {
                    KrokWPrawo = Zamien(value,KrokWPrawo);
                }
                else
                {
                    Zajety(value);
                }
            }
        }
        public ConsoleKeyInfo skok
        {
            get
            {
                return Skok;
            }
            set
            {
                if (SprawdzCzyJestZajęty(value))
                {
                    Skok = Zamien(value,skok);
                }
                else
                {
                    Zajety(value);
                }
            }
        }
        public ConsoleKeyInfo uzyj
        {
            get
            {
                return UzyjPrzedmiotu;
            }
            set
            {
                if (SprawdzCzyJestZajęty(value))
                {
                    UzyjPrzedmiotu = Zamien(value,UzyjPrzedmiotu);
                }
                else
                {
                    Zajety(value);
                }
            }
        }
        public ConsoleKeyInfo zmien
        {
            get
            {
                return ZmienPrzedmiot;
            }
            set
            {
                if (SprawdzCzyJestZajęty(value))
                {
                    ZmienPrzedmiot = Zamien(value,ZmienPrzedmiot);
                }
                else
                {
                    Zajety(value);
                }
            }
        }
        public ConsoleKeyInfo przeladuj
        {
            get
            {
                return PrzeladujBron;
            }
            set
            {
                if (SprawdzCzyJestZajęty(value))
                {
                    PrzeladujBron = Zamien(value,PrzeladujBron);
                }
                else
                {
                    Zajety(value);
                }
            }
        }
    }
}
