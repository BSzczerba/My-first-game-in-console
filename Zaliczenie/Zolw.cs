using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Zolw:Przeciwnik
    {
        public Zolw()
        {
            spowolnienie = 4;
            zdrowie = 5;
            symbol = 'Z';
            tekstura[2, 1] = '/';
            tekstura[2, 2] = '#';
            tekstura[2, 3] = '\\';
            tekstura[2, 4] = 'O';
            tekstura[3, 0] = '-';
            tekstura[3, 1] = '#';
            tekstura[3, 2] = '#';
            tekstura[3, 3] = '#';
            tekstura[3, 4] = 'O';
            tekstura[4, 1] = 'B';
            tekstura[4, 3] = 'B';
        }
        public override void odwroc()
        {
            base.odwroc();
            char pom;
            for (int i = 0; i < 5; i++)
            {
                pom = tekstura[i, 0];
                tekstura[i, 0] = tekstura[i, 4];
                tekstura[i, 4] = pom;
            }
        }
        public override void nowy(ref Objekt x)
        {
            x = new Zolw();
        }
    }
}
