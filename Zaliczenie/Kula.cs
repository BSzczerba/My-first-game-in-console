using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Kula:Objekt
    {
        private int zwrot; 
        private int zasieg;
        private int obrazenia;
        public Kula()
        {
            zwrot = 1;
            tekstura[3, 4] = 'o';
            tekstura[3, 3] = '-';
            tekstura[3, 2] = '-';
            y = 0;
            x = 0;
            zasieg = 10;
            obrazenia=3;
            symbol = 'K';
        }
        public Kula(Gracz gracz,int zasieg,int obrazenia)
        {
            zwrot = gracz.Zwrot;
            if(zwrot==1)
            {
                tekstura[3, 4] = 'o';
                tekstura[3, 3] = '-';
                tekstura[3, 2] = '-';
            }
            else
            {
                tekstura[3, 0] = 'o';
                tekstura[3, 1] = '-';
                tekstura[3, 2] = '-';
            }
            y = gracz.Y;
            x = gracz.X;
            this.zasieg = zasieg;
            this.obrazenia = obrazenia;
            symbol = 'K';
        }
        public Kula(Gracz gracz)
        {
            zwrot = gracz.Zwrot;
            if (zwrot == 1)
            {
                tekstura[3, 4] = 'o';
                tekstura[3, 3] = '-';
                tekstura[3, 2] = '-';
            }
            else
            {
                tekstura[3, 0] = 'o';
                tekstura[3, 1] = '-';
                tekstura[3, 2] = '-';
            }
            y = gracz.Y;
            x = gracz.X;
            this.zasieg = 10;
            this.obrazenia = 1;
            symbol = 'K';
        }
        public override void poruszaj(char[,] tab, int dx, int num_x)
        {
            if (x + zwrot < num_x && x + zwrot >= 0)
            {
                if (tab[y, x + zwrot] == '0' && zasieg > 0)
                {
                        x += zwrot;
                        zasieg--;
                }
                else
                {
                    zdrowie = 0;
                }
            }
            else
            {
                zdrowie = 0;
            }
        }
        public override void reakcja(int zrodlo,int cel,ref List<Objekt> lista)
        {                 
           if(lista[cel] is Przeciwnik && zdrowie != 0)
           {
                lista[cel].obrazenia(obrazenia * -1);
                zdrowie = 0;           
           }                                                   
        }
    }
}
