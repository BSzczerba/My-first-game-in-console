using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Piesc:Bron
    {
        public override void Uzyj(Gracz gracz,ref List<Objekt> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].X == (gracz.X) + (1 * gracz.Zwrot) && lista[i].Y == gracz.Y && lista[i] is Przeciwnik)
                {
                    lista[i].obrazenia(-1);               
                }
            }
        }

        protected override void RysunekBroni()
        {
            Console.SetCursorPosition(x, y);
            Console.Write("o");
        }
    }
}
