using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITools
    {
        public class Menu
        {
            private string[] elementy = new string[0];
            private int max = 30;
            private ConsoleColor zaznaczony;
            private ConsoleColor tlo;

        public Menu()
        {
            zaznaczony = ConsoleColor.Green;
            tlo = ConsoleColor.DarkGreen;
        }
        public Menu(ConsoleColor Zaznaczony, ConsoleColor Tlo)
        {
            zaznaczony = Zaznaczony;
            tlo = Tlo;
        }
        public void Konfiguruj(string[] elementyMenu)
            {
                if (elementyMenu != null)
                {
                    elementy = elementyMenu;
                }
            }
            public int Otworz()
            {
                int wybrany = 0;
                ConsoleKeyInfo Klawisz;
                Console.CursorVisible = false;
                do
                {
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < elementy.Length; i++)
                    {

                        if (i == wybrany)
                    {
                        Console.BackgroundColor = zaznaczony;
                    }
                    else
                        {
                            Console.BackgroundColor = tlo;
                        }

                        if (elementy[i].Length > max)
                        {
                            max = elementy[i].Length;
                        }
                        Console.WriteLine(elementy[i].PadRight(max));
                    }
                    Klawisz = Console.ReadKey(true);
                    if (Klawisz.Key == ConsoleKey.UpArrow && wybrany > 0)
                    {
                        wybrany--;
                    }
                    else if (Klawisz.Key == ConsoleKey.DownArrow && wybrany < elementy.Length - 1)
                    {
                        wybrany++;
                    }
                    else if (Klawisz.Key == ConsoleKey.Escape)
                    {
                        wybrany = -1;
                    }

                } while (Klawisz.Key != ConsoleKey.Escape && Klawisz.Key != ConsoleKey.Enter);
                Console.CursorVisible = true;
                return wybrany;
            }

  

    }
    }



