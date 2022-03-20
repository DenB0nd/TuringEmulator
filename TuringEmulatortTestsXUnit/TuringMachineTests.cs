using Xunit;
using TuringEmulator;
using System;
using System.Collections.Generic;

namespace TuringEmulatortTestsXUnit
{
    public class TuringMachineTests
    {
        public IEnumerable<object[]> AlphabetData()
        {
            yield return new object[] { "", " " };
            yield return new object[] { "   ", " " };
            yield return new object[] { "112223334445555!", "12345! " };
        }
        [Theory]
        [MemberData("AlphabetData")]
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

        [Fact]
        public void TuringMachine_RunTest_Swap0and1()
        {
            TuringMachine machine = new();
            machine.Alphabet = " 01";
            machine.State = 0;
            machine.Tape = new InfiniteTape("0101010", 0);
            TransitionFunctionsTable table = new(
                new[]
                {
                    new TransitionFunction(0, '0', 0, '1', Directions.Right),
                    new TransitionFunction(0, '1', 0, '0', Directions.Right),
                    new TransitionFunction(0, ' ', TuringMachine.HALT, ' ', Directions.None)
                });
            machine.Table = table;

            machine.Run();
            Assert.Equal(new InfiniteTape("1010101 "),machine.Tape);
        }

        public IEnumerable<Object> CopySubroutineData()
        {
            Random random = new Random();
            for(int i = 0; i < 10; i++)
            {
                InfiniteTape tape = new InfiniteTape(new String('1', random.Next(i * i)));
                InfiniteTape expected = new InfiniteTape($"{tape.ToString} {tape.ToString}");
                yield return new object[]
                    {
                        tape,
                        expected
                    };

            }
        }
            


        [Theory]
        [MemberData("CopySubroutineData")]
        public void TuringMachine_RunTest_CopySubroutine(InfiniteTape tape, InfiniteTape expected)
        {
            TuringMachine machine = new();
            machine.Alphabet = " 01";
            machine.State = 0;
            machine.Tape = tape;
            TransitionFunctionsTable table = new TransitionFunctionsTable(new[]
                {
                    new TransitionFunction(0, ' ', TuringMachine.HALT, ' ', Directions.None),
                    new TransitionFunction(0, '1', 1, ' ', Directions.Right),
                    new TransitionFunction(1, ' ', 2, ' ', Directions.Right),
                    new TransitionFunction(1, '1', 1, '1', Directions.Right),
                    new TransitionFunction(2, ' ', 3, '1', Directions.Left),
                    new TransitionFunction(2, '1', 2, '1', Directions.Right),
                    new TransitionFunction(3, ' ', 4, ' ', Directions.Left),
                    new TransitionFunction(3, '1', 3, '1', Directions.Left),
                    new TransitionFunction(4, ' ', 0, '1', Directions.Right),
                    new TransitionFunction(4, '1', 4, '1', Directions.Left)
                });
            machine.Table = table;

            machine.Run();

            Assert.Equal(expected, machine.Tape);
        }

        [Fact]
        public void TuringMachine_RunTest_SayHelloInsteadOfAnyword()
        {
            TuringMachine machine = new();
            machine.Alphabet = "abcdefghijklmnopqrstuvwxyz";
            machine.State = 0;
            string tapeString = "abhhcewrkbapbslmxlzmbozfsmbinwarijcnvjznrbabaebczbeb";
            machine.Tape = new InfiniteTape(tapeString, 0);
            TransitionFunctionsTable table = new TransitionFunctionsTable(new[]
                {
                    new TransitionFunction(0, ' ', TuringMachine.HALT, ' ', Directions.None),
                    new TransitionFunction(1, ' ', 2, ' ', Directions.Left),
                    new TransitionFunction(2, ' ', 2, ' ', Directions.Left),
                    new TransitionFunction(2, 'h', 3, 'h', Directions.Right),
                    new TransitionFunction(3, ' ', 4, 'e', Directions.Right),
                    new TransitionFunction(4, ' ', 5, 'l', Directions.Right),
                    new TransitionFunction(5, ' ', 6, 'l', Directions.Right),
                    new TransitionFunction(6, ' ', TuringMachine.HALT, 'o', Directions.None),
                });

            foreach (var item in tapeString)
            {
                table.Add(new TransitionFunction(0, item, 1, 'h', Directions.Right));
                table.Add(new TransitionFunction(1, item, 1, ' ', Directions.Right));
            }

            machine.Table = table;
            machine.Run();

            Assert.Equal(new InfiniteTape($"hello{new String(' ', tapeString.Length - 4)}"), machine.Tape);
        }
    }
}