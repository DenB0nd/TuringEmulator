using Xunit;
using TuringEmulator;
using System;

namespace TuringEmulatortTestsXUnit
{
    public class TuringMachineTests
    {
        [Theory]
        [InlineData("", " ")]
        [InlineData("   ", " ")]
        [InlineData("112223334445555!", "12345! ")]
        public void AlphabetSetTest(string alphabet, string expected)
        {
            TuringMachine machine = new TuringMachine();
            machine.Alphabet = alphabet;
            Assert.Equal(expected, machine.Alphabet);
        }

        [Fact]
        public void TransitionFunctionsTable_AddOneFunction()
        {
            TransitionFunctionsTable table = new TransitionFunctionsTable();
            int count = table.Count;
            table.Add(new TransitionFunction(0, '1', 1, '1', Directions.Right));

            Assert.True(table.Count - count == 1, $"{table.Count - count}");
        }

        [Fact]
        public void TransitionFunctionsTable_AddFunctions()
        {
            TransitionFunctionsTable table = new TransitionFunctionsTable();
            int count = table.Count;
            table.Add(new[]
                {
                    new TransitionFunction(0, ' ', 0, ' ', Directions.Right),
                    new TransitionFunction(0, '1', 1, '1', Directions.Right)
                });

            Assert.True(table.Count - count == 2, $"{table.Count - count}");
        }

        [Fact]
        public void TransitionFunctionsTable_AddNull()
        {
            TransitionFunctionsTable table = new TransitionFunctionsTable();
            int count = table.Count;
            TransitionFunction functionNull = null;
            TransitionFunctionsTable tableNull = null;

            Assert.Throws<ArgumentNullException>(() => table.Add(functionNull));
            Assert.Throws<ArgumentNullException>(() => table.Add(tableNull));
        }
    }
}