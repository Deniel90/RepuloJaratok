using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepuloJaratok.Teszt
{
    [TestFixture]
    class JaratokTeszt
    {
        JaratKezelo j;

        [SetUp]
        public void Setup()
        {
            j = new JaratKezelo();
        }


        /* --- Új járat tesztek --- */
        [TestCase]
        public void JaratSzamEgyedi()
        {
            j.UjJarat("LH1338", "Frankfurt", "Budapest", new DateTime(2022, 2, 20, 14, 00, 00), 0);
            Assert.Throws<ArgumentException>( () => {
                j.UjJarat("LH1338", "Frankfurt", "Budapest", new DateTime(2022, 2, 20, 14, 00, 00), 0);
            });
        }

        [TestCase]
        public void StartEsCelUgyanaz()
        {
            Assert.Throws<ArgumentException>( () => {
                j.UjJarat("LH1338", "Budapest", "Budapest", new DateTime(2022, 2, 20, 14, 00, 00), 0);
            });
        }

        [TestCase]
        public void IndulasAMaiNapnalKorabban()
        {
            Assert.Throws<ArgumentException>(() => {
                j.UjJarat("LH1338", "Frankfurt", "Budapest", new DateTime(2021, 1, 20, 14, 00, 00), 0);
            });
        }


        /* --- Késés tesztek --- */
        [TestCase]
        public void RosszJaratszam()
        {
            j.UjJarat("LH1338", "Frankfurt", "Budapest", new DateTime(2022, 2, 20, 14, 00, 00), 0);
            Assert.Throws<ArgumentOutOfRangeException>( () => {
                j.Keses("LH1888", 10);
            });
        }

        [TestCase]
        public void KesesekOsszeadodnak()
        {
            j.UjJarat("LH1338", "Frankfurt", "Budapest", new DateTime(2022, 2, 20, 14, 00, 00), 0);
            j.Keses("LH1338", 5);
            j.Keses("LH1338", 10);
            Assert.AreEqual(j.jaratok[j.jaratok.FindIndex(i => i.Jaratszam == "LH1338")].Keses, 15);
        }

        [TestCase]
        public void KesesNemLehetNegativ()
        {
            j.UjJarat("LH1338", "Frankfurt", "Budapest", new DateTime(2022, 2, 20, 14, 00, 00), 0);
            Assert.Throws<NegativKesesException>( () => j.Keses("LH1338", -10) );
        }


        /* --- MikorIndul tesztek --- */
        [TestCase]
        public void NemLetezoInduloJarat()
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => j.MikorIndul("LH1338") );
        }

        [TestCase]
        public void KesesBeleszamolva()
        {
            j.UjJarat("LH1338", "Frankfurt", "Budapest", new DateTime(2022, 2, 20, 14, 00, 00), 0);
            j.Keses("LH1338", 10);
            Assert.AreEqual(10, j.MikorIndul("LH1338").Minute);
        }


        /* --- InduloJaratok tesztek --- */
        [TestCase]
        public void NemLetezoIndulasiHely()
        {
            j.UjJarat("LH1338", "Frankfurt", "Budapest", new DateTime(2022, 2, 20, 14, 00, 00), 0);
            Assert.Throws<ArgumentOutOfRangeException>(() => j.JaratokRepuloterrol("AA8888") );
        }

        [TestCase]
        public void NincsenekJaratok()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => j.JaratokRepuloterrol("LH1338"));
        }
    }
}
