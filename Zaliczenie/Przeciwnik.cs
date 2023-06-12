using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Przeciwnik:Objekt
    {
        public Przeciwnik():base()
        {

        }
        private int zwrot = 1;
        public override void poruszaj(char[,] tab, int dx, int num_x)
        {
            dx *= zwrot;
            licznik_spowolnienia++;
            if (licznik_spowolnienia>spowolnienie)
            {
                licznik_spowolnienia = 0;
            }
            if(licznik_spowolnienia==0)
            {
                if (!(x + dx > num_x) && !(x + dx < 0))
                {
                    if (tab[y, x + dx] == '0' && tab[y + 1, x + dx] != '0')
                    {
                        x += dx;
                    }
                    else
                    {
                        odwroc();
                    }
                }
                else
                {
                    odwroc();
                }
            }          
        }
        public virtual void odwroc()
        {
            zwrot *= -1;
        }        
        public override void reakcja(int zrodlo, int cel, ref List<Objekt> lista)
        {
            if (lista[cel] is Gracz)
            {
                lista[cel].obrazenia(-1);
            }
        }
    }
}
