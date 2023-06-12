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
      private ConsoleKey KrokWLewo;
      private ConsoleKey KrokWPrawo;
      private ConsoleKey Skok;
      private ConsoleKey UzyjPrzedmiotu;
      private ConsoleKey ZmienPrzedmiot;
      private ConsoleKey PrzeladujBron;
      private List<ConsoleKey> ListaKlawiszy = new List<ConsoleKey>();

        public Sterowanie()
        {          
            string[] lines = File.ReadAllLines("sterowanie.txt");          
            Enum.TryParse<ConsoleKey>(lines[0], out KrokWPrawo);
            Enum.TryParse<ConsoleKey>(lines[1], out KrokWLewo);
            Enum.TryParse<ConsoleKey>(lines[2], out Skok);
            Enum.TryParse<ConsoleKey>(lines[3], out UzyjPrzedmiotu);
            Enum.TryParse<ConsoleKey>(lines[4], out ZmienPrzedmiot);
            Enum.TryParse<ConsoleKey>(lines[5], out PrzeladujBron);
            ListaKlawiszy.Add(KrokWPrawo);
            ListaKlawiszy.Add(KrokWLewo);
            ListaKlawiszy.Add(Skok);
            ListaKlawiszy.Add(UzyjPrzedmiotu);
            ListaKlawiszy.Add(ZmienPrzedmiot);
            ListaKlawiszy.Add(PrzeladujBron);
        }      
        public void ZapiszDoPliku()
        {
            string[] lines =
        {
            $"{KrokWPrawo}", $"{KrokWLewo}", $"{Skok}", $"{UzyjPrzedmiotu}", $"{ZmienPrzedmiot}", $"{PrzeladujBron}"
        };

            File.WriteAllLines("sterowanie.txt", lines);
        }
        private bool SprawdzCzyJestZajęty(ConsoleKey wartosc)
        {
            for (int i = 0; i < ListaKlawiszy.Count; i++)
            {               
                if (wartosc == ListaKlawiszy[i])
                {
                    return false;
                }
            }
            return true;
        }
        private void Usun(ConsoleKey wartosc)
        {
            for (int i = 0; i < ListaKlawiszy.Count; i++)
            {
                if (wartosc == ListaKlawiszy[i])
                {
                    ListaKlawiszy.RemoveAt(i);
                    return;
                }
            }
        }
        private void Zajety(ConsoleKey wartosc)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"Klawisz {wartosc.ToString()} jest już zajęty");
            Console.ResetColor();
            Console.ReadLine();
        }
        private ConsoleKey Zamien(ConsoleKey wartosc, ConsoleKey poprzedni)
        {
            Usun(poprzedni);
            ListaKlawiszy.Add(wartosc);
            GC.Collect();
            return wartosc;
        }
        public ConsoleKey lewo
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
        public ConsoleKey prawy
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
        public ConsoleKey skok
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
        public ConsoleKey uzyj
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
        public ConsoleKey zmien
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
        public ConsoleKey przeladuj
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
