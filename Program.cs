using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2013EmeltIdegenMajus
{
    class Szamok
    {
        public static List<Szamok> Adat = new List<Szamok>();

        public int Sorszam;
        public string Kerdes;
        public string ValaszSor;
        public int Valasz;
        public int Pont;
        public string Temakor;

        public Szamok(int sorszam, string kerdes, string valasz, int evszam, int pont, string temakor)
        {
            Sorszam = sorszam;
            Kerdes = kerdes;
            ValaszSor = valasz;
            Valasz = evszam;
            Pont = pont;
            Temakor = temakor;
        }

        public static void ElsoFeladat(string fajl)
        {
            int sorszam = 0;

            using (StreamReader olvas = new StreamReader(fajl))
            {
                while (!olvas.EndOfStream)
                {
                    sorszam++;
                    string kerdes = olvas.ReadLine();
                    string valasz = olvas.ReadLine();

                    string[] split = valasz.Split(' ');
                    int ev = Convert.ToInt32(split[0]);
                    int pont = Convert.ToInt32(split[1]);
                    string tema = split[2];

                    Szamok szamok = new Szamok(sorszam, kerdes, valasz, ev, pont, tema);

                    Adat.Add(szamok);
                }
            }
        }

        public static void MasodikFeladat() => Console.WriteLine($"2. feladat: Kérdések száma: {Adat.Select(x => x.Kerdes).Count()}");
        public static void HarmadikFeladat()
        {
            int matekFeladatokSzama = Adat.Where(x => x.Temakor == "matematika").Count();
            int egyPontos = Adat.Where(x => x.Temakor == "matematika" && x.Pont == 1).Count();
            int ketPontos = Adat.Where(x => x.Temakor == "matematika" && x.Pont == 2).Count();
            int haromPontos = Adat.Where(x => x.Temakor == "matematika" && x.Pont == 3).Count();

            Console.WriteLine($"3. feladat: Az adatfájlban {matekFeladatokSzama} feladat van, 1 pontot ér {egyPontos} feladat, 2 pontot ér {ketPontos} feladat, 3 pontot ér {haromPontos} feladat.");
        }

        public static void NegyedikFeladat()
        {
            Console.WriteLine($"4. feladat: Számértékek: {Adat.Min(x => x.ValaszSor)} - {Adat.Max(x => x.ValaszSor)}");
        }

        public static void OtodikFeladat()
        {
            Console.WriteLine($"5. feladat: Témakörök");

            foreach (var temakor in Adat.GroupBy(x => x.Temakor))
            {
                Console.WriteLine($" -{temakor.Key}");
            }
        }

        public static void HatodikFeladat()
        {
            Console.Write("6. feladat: Milyen témakörből szeretne kérdést kapni: ");
            string megadottTemakor = Console.ReadLine();

            var temakorFeladatok = Adat.Where(x => x.Temakor == megadottTemakor).ToList();

            Random rnd = new Random();

            int index = rnd.Next(1, temakorFeladatok.Count);

            string kerdes = temakorFeladatok[index].Kerdes;
            int helyesValasz = temakorFeladatok[index].Valasz;
            int kapottPontok = temakorFeladatok[index].Pont;

            Console.Write($"{kerdes}: ");
            int megadottValasz = Convert.ToInt32(Console.ReadLine());

            if (megadottValasz == helyesValasz)
            {
                Console.WriteLine($"A válasz {kapottPontok} pontot ér.");
                Console.WriteLine($"A válasz helyes volt.");
            }
            else if (megadottValasz != helyesValasz)
            {
                Console.WriteLine($"A válasz 0 pontot ér.");
                Console.WriteLine($"A helyes válasz: {helyesValasz}");
            }

        }

        public static void HetedikFeladat()
        {
            int osszpont = 0;
            StreamWriter ki = new StreamWriter(@"teszfel.txt");
            Random rnd = new Random();
            var kerdesek = Adat.Select(x => new { x, i = rnd.Next() }).OrderBy(x => x.i).Take(10).Select(s => s.x).ToList();

            for (int i = 0; i < kerdesek.Count; i++)
            {
                osszpont += kerdesek[i].Pont;

                ki.WriteLine($"{kerdesek[i].Pont} {kerdesek[i].Valasz} {kerdesek[i].Kerdes}");
                ki.Flush();
            }

            ki.WriteLine($"A feladatsorra összesen {osszpont} pont adható.");

            ki.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Feladatok();

            Console.ReadKey();
        }

        private static void Feladatok()
        {
            Szamok.ElsoFeladat("felszam.txt");
            Szamok.MasodikFeladat();
            Szamok.HarmadikFeladat();
            Szamok.NegyedikFeladat();
            Szamok.OtodikFeladat();
            Szamok.HatodikFeladat();
            Szamok.HetedikFeladat();
        }
    }
}
