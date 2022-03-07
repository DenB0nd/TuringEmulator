using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TuringEmulator;

namespace TuringEmulatorTests
{
    [TestClass]
    public class TuringMachineTests
    {

        [TestMethod]
        public void AlphabetSetTest()
        {
            TuringMachine tm = new TuringMachine();
            string[] alphabets = { "", "   ", " 12233344445555!!!" };
            string[] results = { " ", " ", " 12345!" };

           for(int i = 0; i < alphabets.Length; i++)
            {
                tm.Alphabet = alphabets[i];
                Assert.AreEqual(tm.Alphabet, results[i]);
            }
        }

    }
}