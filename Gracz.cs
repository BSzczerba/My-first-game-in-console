using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zaliczenie
{
    class Gracz : Objekt
    {
        bool czy_skok=false;
        private int max_wysokosc_skoku = 2;
        private int skok_wysokosc=0;
        List<Bron> uzbrojenie;
        private int aktywnaBron = -1;
        private int zwrot = 1;
        public Gracz()
        {
            uzbrojenie = new List<Bron>();
            symbol = 'G';
            tekstura[2, 2] = '@';
            tekstura[2, 1] = '~';
            tekstura[3, 2] = '@';
            tekstura[3, 1] = '-';
            tekstura[3, 3] = '-';
            tekstura[4, 1] = '/';
            tekstura[4, 3] = '\\';
            Bron miecz = new Miecz();
            Bron ak47 = new Pistolet(3);
            Podnies(ref miecz);
            Podnies(ref ak47);
        }
        public Bron bron
        {
            get
            {
                if (aktywnaBron == -1)
                {
                    return new Piesc();
                }                   
                else

                {
                    return uzbrojenie[aktywnaBron];
                }
            }
        }      
        public void PrzygotujBron()
        {
            bron.Przygotuj();
        }
        public void ZmienBron()
        {
            aktywnaBron++;
            if (aktywnaBron == uzbrojenie.Count)
            {
                aktywnaBron = -1;
            }
        }
        public void Podnies(ref Bron bron)
        {
            if (uzbrojenie.Count < 10)
            {
                uzbrojenie.Add(bron);
                bron = null;
            }
        }

        public bool Skok
        {
            get
            {
                return czy_skok;
            }
            set
            {
                czy_skok = value;
            }
        }
        public int Zwrot
        {
            get
            {
                return zwrot;
            }
        }
        
        public void Poruszaj(int dx,char[,] plansza,int num_x)         
        {
            if (x + dx < num_x  && x+dx >= 0)
            {
                if (plansza[y, x + dx] == '0')
                {
                    x += dx;
                }
            }
            if(zwrot!=dx)
            {
                odwroc();
            }          
        }
        private void odwroc()
        {
            zwrot *= -1;
            char pom;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    pom = tekstura[i,j];
                    tekstura[i, j] = tekstura[i, 4-j];
                    tekstura[i, 4 - j] = pom;
                }
            }
        }
        public void Grawitacja(char[,] tab)
        {
            if (tab[y + 1, x] == '0')
            {
                y++;
            }          
        }
        public void skok(char[,] tab)
        {           
            if (y > 0)
            {
                if (tab[y + 1, x] != '0' && czy_skok == false)
                {
                    czy_skok = true;
                }
                else
                {
                    if (tab[y - 1, x] == '0' && y != 1 && skok_wysokosc < max_wysokosc_skoku)
                    {
                        y--;
                        skok_wysokosc++;
                    }
                    else
                    {
                        czy_skok = false;
                        skok_wysokosc = 0;
                    }
                }               
            }
        }

    }
}