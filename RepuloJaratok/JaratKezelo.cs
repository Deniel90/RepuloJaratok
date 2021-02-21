using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepuloJaratok
{
    public class JaratKezelo
    {
        public class Jarat
        {
            string jaratszam;
            string repuloter_kezdeti;
            string repuloter_cel;
            DateTime indulasIdopontja;
            int keses;

            public Jarat(string jaratszam, string repuloter_kezdeti, string repuloter_cel, DateTime indulasIdopontja, int keses)
            {
                this.jaratszam = jaratszam;
                this.repuloter_kezdeti = repuloter_kezdeti;
                this.repuloter_cel = repuloter_cel;
                this.indulasIdopontja = indulasIdopontja;
                this.keses = keses;
            }

            public string Jaratszam { get => jaratszam; set => jaratszam = value; }
            public string Repuloter_kezdeti { get => repuloter_kezdeti; set => repuloter_kezdeti = value; }
            public string Repuloter_cel { get => repuloter_cel; set => repuloter_cel = value; }
            public DateTime IndulasIdopontja { get => indulasIdopontja; set => indulasIdopontja = value; }
            public int Keses { get => keses; set => keses = value; }
        }

        public List<Jarat> jaratok;

        public JaratKezelo()
        {
            jaratok = new List<Jarat>();
        }

        public void UjJarat(string jaratszam, string repuloter_kezdeti, string repuloter_cel, DateTime indulasIdopontja, int keses)
        {
            if (repuloter_kezdeti == repuloter_cel)
            {
                throw new ArgumentException("A kezdeti és a célállomás nem lehet ugyanaz!");
            }

            if (indulasIdopontja < DateTime.Now)
            {
                throw new ArgumentException("Ez a járat már elment.");
            }

            var ujJarat = new Jarat(jaratszam, repuloter_kezdeti, repuloter_cel, indulasIdopontja, keses);

            if (jaratok.Count == 0 || jaratok[jaratok.FindIndex(i => i.Jaratszam == jaratszam)] == null)
            {
                jaratok.Add(ujJarat);
            }
            else
            {
                throw new ArgumentException("Ez a járatszám már létezik.");
            }
        }

        public void Keses(string jaratszam, int keses)
        {
            if (jaratok.Count > 0 && jaratok[jaratok.FindIndex(i => i.Jaratszam == jaratszam)] == null)
            {
                throw new ArgumentException("Ilyen járatszám nem létezik!");
            }
            else
            {
                if ((jaratok[jaratok.FindIndex(i => i.Jaratszam == jaratszam)].Keses + keses) > 0)
                {
                    jaratok[jaratok.FindIndex(i => i.Jaratszam == jaratszam)].Keses += keses;
                }
                else
                {
                    throw new NegativKesesException();
                }
            }
        }


        public DateTime MikorIndul(string jaratszam)
        {
            if (jaratok[jaratok.FindIndex(i => i.Jaratszam == jaratszam)] == null)
            {
                throw new ArgumentOutOfRangeException("Nincs ilyen járat!");
            }
            else
            {
                var induloJarat = jaratok[jaratok.FindIndex(i => i.Jaratszam == jaratszam)];
                induloJarat.IndulasIdopontja = induloJarat.IndulasIdopontja.AddMinutes(induloJarat.Keses);
                return induloJarat.IndulasIdopontja;
            }
        }

        //A járatszámok, amely a megadott reptérről indulnak.
        public List<string> JaratokRepuloterrol(string repuloter_kezdeti)
        {            
            if (jaratok.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Nincsenek induló járatok.");
            }

            List<string> induloJaratokSzama = new List<string>();

            foreach (var jarat in jaratok)
            {
                if (jarat.Repuloter_kezdeti == repuloter_kezdeti)
                {
                    induloJaratokSzama.Add(jarat.Jaratszam);
                }
            }
            if (induloJaratokSzama.Count < 1)
            {
                throw new ArgumentOutOfRangeException("Erről a reptérről nem indul járat.");
            }
            return induloJaratokSzama;
        }
    }
}
