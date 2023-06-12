using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Cegla:Blok
    {
        public Cegla()
        {
            symbol = 'c';
            tekstura[0, 0] = 'H';
            tekstura[0, 1] = 'X';
            tekstura[0, 2] = 'X';
            tekstura[0, 3] = 'H';
            tekstura[0, 4] = 'H';
            tekstura[1, 0] = 'X';
            tekstura[1, 1] = 'X';
            tekstura[1, 2] = 'H';
            tekstura[1, 3] = 'H';
            tekstura[1, 4] = 'X';
            tekstura[2, 0] = 'H';
            tekstura[2, 1] = 'X';
            tekstura[2, 2] = 'X';
            tekstura[2, 3] = 'H';
            tekstura[2, 4] = 'H';
            tekstura[3, 0] = 'X';
            tekstura[3, 1] = 'H';
            tekstura[3, 2] = 'H';
            tekstura[3, 3] = 'X';
            tekstura[3, 4] = 'X';
            tekstura[4, 0] = 'H';
            tekstura[4, 1] = 'X';
            tekstura[4, 2] = 'X';
            tekstura[4, 3] = 'H';
            tekstura[4, 4] = 'H';

        }
    }
}
