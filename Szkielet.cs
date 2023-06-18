using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Szkielet:Przeciwnik
    {           
        public Szkielet()
        {
            zdrowie = 3;
            symbol = 'S';
            spowolnienie = 2;
            tekstura[1, 4] = ']';
            tekstura[2, 2] = 'B';
            tekstura[2, 4] = '|';
            tekstura[3, 2] = '#';
            tekstura[3, 1] = '-';
            tekstura[3, 3] = '-';
            tekstura[3, 4] = '|';         
            tekstura[4, 1] = '/';
            tekstura[4, 3] = '\\';
            tekstura[4, 4] = '|';
        }
        public override void odwroc()
        {
            base.odwroc();
            char pom;
            for (int i = 0; i < 5; i++)
            {
                pom = tekstura[i, 0];
                tekstura[i, 0] = tekstura[i,4];
                tekstura[i, 4] = pom;
            }
        }
        public override void nowy(ref Objekt x)
        {
             x = new Szkielet();
        }
    }
}
