using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    class Miecz : Bron
    {
        bool podniesiony = true;
        public override void Uzyj(Gracz gracz,ref List<Objekt> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if(lista[i].X == (gracz.X) + (1*gracz.Zwrot) && lista[i].Y == gracz.Y && lista[i] is Przeciwnik)
                {
                    lista[i].obrazenia(-3);
                }
            }
            podniesiony = !podniesiony;
        }

        protected override void RysunekBroni()
        {
            if (podniesiony)
            {
                Console.SetCursorPosition(x, y - 1);
                Console.Write("|");
                Console.SetCursorPosition(x, y);
                Console.Write("T");
            }
            else
            {
                Console.SetCursorPosition(x + 1, y - 1);
                Console.Write("/");
                Console.SetCursorPosition(x, y);
                Console.Write("X");
            }

        }
    }
}
