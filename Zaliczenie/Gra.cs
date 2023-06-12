using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GUITools;
using static System.Console;


namespace Zaliczenie
{
    class Gra
    {
        protected int num_y,num_x;
        private char[,] obraz = new char[40, 120];
        private Gracz gracz = new Gracz();
        private Puchar puchar = new Puchar();
        protected char[,] plansza;
        private List<Objekt> list = new List<Objekt>();
        private List<Objekt> lista_objektow = new List<Objekt>();
        private List<Objekt> obiekty_na_ekranie = new List<Objekt>();
        private int[,] pozycja_objektow;
        private Sterowanie sterowanie = new Sterowanie();
        char[,] aktualny_widok;
        public Gra()
        {
            for (int x = 0; x < 40; x++)
            {
                for (int y = 0; y < 120; y++)
                {
                    obraz[x, y] = ' ';
                }
            }
        }
        public virtual void Powitanie()
        {
            Console.SetWindowSize(200, 50);          
            Menu menu = Nowe_menu();
            menu.Konfiguruj(new string[]
              {"Rozpocznij grę","Rysuj planszę","Sterowanie","Zakończ" });
            int decyzja = 0;
            do
            {
                CzyscEkran();
                decyzja = menu.Otworz();
                if (decyzja == 0)
                {
                    CzyscEkran();
                    Setup();
                    Start();
                    break;
                }
                else if (decyzja == 1)
                {
                    CzyscEkran();
                }
                else if (decyzja == 2)
                {                     
                     Menu st = new Menu(ConsoleColor.Blue,ConsoleColor.DarkBlue);                                     
                    int d = 0;
                     do
                     {
                        st.Konfiguruj(new string[] { $"Ruch w Prawo        {sterowanie.prawy}", $"Ruch w Lewo         {sterowanie.lewo}", $"Skok                {sterowanie.skok}", $"Użyj Przedmiotu     {sterowanie.uzyj}", 
                         $"Zmiana Przedmiotu   {sterowanie.zmien}",  $"Przeładuj Broń      {sterowanie.przeladuj}","Zapisz Ustawienia", "Zakończ" });
                        CzyscEkran();
                        d = st.Otworz();
                        if (d==0)
                        {
                            var klawisz = Console.ReadKey(true);
                            sterowanie.prawy = klawisz.Key;

                        }
                        else if (d==1)
                        {
                            var klawisz = Console.ReadKey(true);
                            sterowanie.lewo = klawisz.Key;
                        }
                        else if (d == 2)
                        {
                            var klawisz = Console.ReadKey(true);
                            sterowanie.skok = klawisz.Key;
                        }
                        else if (d == 3)
                        {
                            var klawisz = Console.ReadKey(true);
                            sterowanie.uzyj = klawisz.Key;
                        }
                        else if (d == 4)
                        {
                            var klawisz = Console.ReadKey(true);
                            sterowanie.zmien = klawisz.Key;
                        }
                        else if (d == 5)
                        {
                            var klawisz = Console.ReadKey(true);
                            sterowanie.przeladuj = klawisz.Key;
                        }
                        else if (d == 6)
                        {
                            sterowanie.ZapiszDoPliku();
                        }
                    } while (d != -1 && d != 7);
                    CzyscEkran();
                }
            } while (decyzja != -1 && decyzja != 3);
        }

        protected virtual Menu Nowe_menu()
        {
            return new Menu();
        }

        public virtual void Setup()
        {           
            Blok cegla = new Cegla();
            Blok rynna = new Rynna();
            Blok gora_rynny = new Gora_rynny();
            Kula kula = new Kula();
            Szkielet szkielet = new Szkielet();
            Zolw zolw = new Zolw();
            list.Add(cegla);
            list.Add(rynna);
            list.Add(gora_rynny);
            list.Add(gracz);
            list.Add(puchar);
            list.Add(szkielet);
            list.Add(zolw);
            list.Add(kula);
            Wczytaj();
        }
        public void Start()
        {
            Console.CursorVisible = false;
            while (true)
            {
                aktualizacja();
                Wypisz();              
                if (gracz.Skok)
                {
                    gracz.skok(plansza);                    
                }
                else
                {
                    gracz.Grawitacja(plansza);                   
                }
                if (Console.KeyAvailable)
                {
                    var klawisz = Console.ReadKey(true);
                    ObslugaKlawiatury(klawisz);
                }
                Thread.Sleep(100);
                for (int i = 0; i < lista_objektow.Count; i++)
                {
                    if (!(lista_objektow[i] is Gracz))
                    {
                        lista_objektow[i].poruszaj(plansza, 1, num_x);
                    }
                    for (int j = 0; j < lista_objektow.Count; j++)
                    {
                        if (lista_objektow[i].X == lista_objektow[j].X && lista_objektow[i].Y == lista_objektow[j].Y && i!=j)
                        {
                            lista_objektow[i].reakcja(i, j, ref lista_objektow);
                            lista_objektow[j].reakcja(j, i, ref lista_objektow);
                        }
                    }                  
                    if (lista_objektow[i].Zdrowie<=0)
                    {
                        if(lista_objektow[i] is Gracz)
                        {
                            Przegrana();
                            return;
                        }
                        lista_objektow.RemoveAt(i);
                        GC.Collect();
                    }
                }
                if (gracz.Y == num_y - 1)
                {
                    Przegrana();
                    break;
                }
                if (gracz.X == puchar.X && gracz.Y == puchar.Y)
                {
                    Wygrana();
                    break;
                }
            }
        }
        private void aktualizacja()
        {
            aktualny_widok = Przesuwanie_ekranu(gracz.X,gracz.Y);           
            for (int x = 0; x < 40; x++)
            {
                for (int y = 0; y < 120; y++)
                {

                    if (aktualny_widok[x / 5, (y / 5)] != '0')
                    {
                        foreach(Objekt a in list)
                        {
                            if(a.Symbol == aktualny_widok[x/5, y/5])
                            {
                                obraz[x, y] = a.Rysuj(x%5,y%5);
                                break;
                            }
                        }                      
                    }
                    else
                    {
                        obraz[x, y] = ' ';
                    }
                    for (int i = 0; i < obiekty_na_ekranie.Count; i++)
                    {
                        if (pozycja_objektow[i, 0] == x / 5 && pozycja_objektow[i, 1] == y / 5)
                        {
                            obraz[x, y] = obiekty_na_ekranie[i].Rysuj(x % 5, y % 5);
                            break;
                        }
                    }
                }
            }
           
        }
        public void Wypisz()
        {
            int z;
            if(gracz.Zwrot==1)
            {
                z = 4;
            }
            else
            {
                z = 0;
            }
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < 40; y++)
            {
                for (int x = 0; x < 120; x++)
                {
                    Console.Write(obraz[y, x]);
                    if (y % 5 == 3 && x % 5 == z && aktualny_widok[y / 5, x / 5] == 'G')
                    {
                        gracz.bron.Rysuj(x, y);
                    }
                }
                Console.WriteLine("");
            }          
        }
        public virtual void Wygrana()
        {
            CzyscEkran();
            Console.WriteLine("Gratulacje!!!");
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
        public virtual void Przegrana()
        {
            CzyscEkran();
            Console.WriteLine("GAME OVER");
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
        public void CzyscEkran()
        {
            Console.ResetColor();
            Console.Clear();
        }
        protected virtual void Wczytaj()
        {
            string[] lines = File.ReadAllLines("plansza.txt");
            foreach (string line in lines)
            {
                num_x = line.Length;
                num_y++;               
            }          
            plansza = new char[num_y,num_x];
            int num = 0;      
            foreach (string line in lines)
            {
                    for (int x = 0; x < num_x; x++)
                    {
                    if (Czy_blok(line[x]))
                    {
                        plansza[num, x] = line[x];
                    } 
                    else
                    {
                        plansza[num, x] = '0';
                        Dodaj_objekt(line[x], x, num);
                    }
                    }                
               num++;               
            }
        }
        private bool Czy_blok(char symbol)
        {
            foreach (Objekt a in list)
            {
                if (symbol == a.Symbol && !(a is Blok))
                {
                    return false;
                }
            }
            return true;
        }
        private void Dodaj_objekt(char symbol,int x, int y)
        {
            foreach (Objekt a in list)
            {
                if (symbol == a.Symbol)
                {
                    if (symbol=='G')
                    {
                        gracz.X = x;
                        gracz.Y = y;
                        lista_objektow.Add(gracz);
                        break;
                    }
                    else if (symbol == 'P')
                    {
                        puchar.X = x;
                        puchar.Y = y;
                        lista_objektow.Add(puchar);
                        break;
                    }
                    else
                    {
                            Objekt s = null;  
                            a.nowy(ref s);
                            s.X = x;
                            s.Y = y;
                            lista_objektow.Add(s);
                            break;                                           
                    }                    
                }
            }

        }
        private char[,] Przesuwanie_ekranu(int x,int y)
        {
            obiekty_na_ekranie.Clear();
            pozycja_objektow = new int[lista_objektow.Count,lista_objektow.Count];
            GC.Collect();
            int p_granica = 0, l_granica = 0 , d_granica=0 ,g_granica=0;
            l_granica = x - 12;
            p_granica = x + 12;
            d_granica = y + 4;
            g_granica = y -4;
            if (l_granica<0)
            {
                l_granica = 0;
                p_granica = 24;
            }
            else if (p_granica > num_x)
            {
                l_granica = num_x-24;
                p_granica = num_x;
            }
            if(g_granica < 0)
            {
                d_granica = 8;
                g_granica = 0;
            }
            else if (d_granica > num_y)
            {
                g_granica = num_y - 8;
                d_granica = num_y;
            }
            int licz = 0;
            int licz_w = 0;
            char[,] tab = new char[8,24];
            for (int z = g_granica; z < d_granica; z++)
            {
                for (int u = l_granica; u < p_granica; u++)
                {
                    tab[licz_w, licz] = plansza[z, u];
                    foreach (Objekt a in lista_objektow)
                    {
                        if (a.X==u && a.Y == z)
                        {
                            pozycja_objektow[obiekty_na_ekranie.Count, 0] = licz_w;
                            pozycja_objektow[obiekty_na_ekranie.Count, 1] = licz;
                            obiekty_na_ekranie.Add(a);
                            if (a is Gracz)
                            {
                                tab[licz_w, licz] = 'G';
                            } 
                        }                        
                    }                   
                    licz = (licz + 1)%24;                 
                }

                licz_w = (licz_w + 1) % 8;
            }
            return tab;
        }
        protected virtual void ObslugaKlawiatury(ConsoleKeyInfo klawisz)
        {
            if(klawisz.Key == sterowanie.skok)
            {
                gracz.skok(plansza);              
            }
            else if(klawisz.Key == sterowanie.lewo)
            {
                gracz.Poruszaj(-1, plansza, num_x);
            }
            else if (klawisz.Key == sterowanie.prawy)
            {
                gracz.Poruszaj(1, plansza, num_x);
            }
            else if (klawisz.Key == sterowanie.zmien)
            {
                gracz.ZmienBron();
            }
            else if (klawisz.Key == sterowanie.uzyj)
            {
                gracz.bron.Uzyj(gracz, ref lista_objektow);
            }
            else if (klawisz.Key == sterowanie.przeladuj)
            {
                gracz.bron.Przygotuj();
            }           
        }
    }  
}