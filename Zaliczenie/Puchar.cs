using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Puchar:Objekt
    {
        public Puchar()
        {
            symbol = 'P';
            tekstura[0, 0] = '[';
            tekstura[0, 4] = ']';
            tekstura[0, 1] = 'O';
            tekstura[0, 2] = 'O';
            tekstura[0, 3] = 'O';
            tekstura[1, 1] = 'O';
            tekstura[1, 2] = 'O';
            tekstura[1, 3] = 'O';
            tekstura[2, 2] = 'V';
            tekstura[3, 2] = '|';
            tekstura[4, 1] = '#';
            tekstura[4, 2] = '#';
            tekstura[4, 3] = '#';
        }
        
    }
}
