using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Pistolet:Bron
    {
        int magazynek;
        int amunicja;
        public Pistolet(int iloscNaboi)
        {
            magazynek = iloscNaboi;
            amunicja = iloscNaboi;
        }
        public override void Przygotuj()
        {
            amunicja = magazynek;
        }
        public override void Uzyj(Gracz gracz,ref List<Objekt> lista)
        {
            if (amunicja > 0)
            {
                Kula kula = new Kula(gracz, 13, 2);
                lista.Add(kula);
                amunicja--;
            }
        }

        protected override void RysunekBroni()
        {
            Console.SetCursorPosition(x, y);
            Console.Write("r");
        }
    }
}
