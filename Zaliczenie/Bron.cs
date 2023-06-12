using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zaliczenie
{
    abstract class Bron
    {
        protected int x, y;
        public abstract void Uzyj(Gracz gracz,ref List <Objekt>lista);
        public virtual void Przygotuj()
        {

        }
        public void Rysuj(int x, int y)
        {
            this.x = x;
            this.y = y;
            RysunekBroni();

        }

        protected abstract void RysunekBroni();
    }
}
