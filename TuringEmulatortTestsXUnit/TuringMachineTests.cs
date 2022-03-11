using Xunit;
using TuringEmulator;

namespace TuringEmulatortTestsXUnit
{
    public class TuringMachineTests
    {
        [Theory]
        [InlineData("", " ")]
        [InlineData("   ", " ")]
        [InlineData("112223334445555!","12345! ")]
        public void AlphabetSetTest(string alphabet, string expected)
        {
            TuringMachine machine = new TuringMachine();
            machine.Alphabet = alphabet;
            Assert.Equal(expected, machine.Alphabet);
        }
    }
}