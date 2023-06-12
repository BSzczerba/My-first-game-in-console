using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Objekt
    {
        protected int spowolnienie=0;
        protected int licznik_spowolnienia = 0;
        protected int x,y;
        protected char symbol;
        public char[,] tekstura = {
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' },
        { ' ', ' ', ' ', ' ', ' ' }};
        protected int zdrowie=1;
        public new int Zdrowie
        {
            get
            {
                return zdrowie;
            }
        }
        public new int X
        {
            get
            {
                return x;
            }
            set
            {
                if (value >= 0)
                {
                    this.x = value;
                }
            }
        }
        public new int Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value >= 0)
                {
                    this.y = value;
                }
            }
        }
        public char Symbol
        {
            get
            {
                return symbol;
            }
        }
        public char Rysuj(int dx, int dy)
        {
            return tekstura[dx, dy];
        }
        public virtual void poruszaj(char[,] tab, int dx, int num_x)
        {

        }
        public virtual void obrazenia(int obr)
        {
            zdrowie = zdrowie + obr;
        }
        public virtual void nowy(ref Objekt x)
        {

        }
        public virtual void reakcja(int zrodlo,int cel, ref List<Objekt> lista)
        {

        }    
    }
   
}
