using System.Collections;
using System.Text;

namespace TuringEmulator
{
    public class InfiniteTape : IEnumerable<char>, ICloneable
    {
        private StringBuilder Tape { get; }

        private int Origin { get; set; } = 0;

        static public InfiniteTape Default => new InfiniteTape("", 0);

        public InfiniteTape(StringBuilder str, int origin = 0)
        {
            ArgumentNullException.ThrowIfNull(str);

            Tape = new StringBuilder("").Append(str);
            Origin = origin;
        }

        public InfiniteTape(string str = "", int origin = 0) : this(new StringBuilder(str), origin) { }

        public InfiniteTape(InfiniteTape tape) : this(new StringBuilder("").Append(tape.Tape), tape.Origin) { } 

        public void Clear()
        {
            Tape.Clear();
            Origin = 0;
        }

        public char this[int index]
        {
            get
            {
                if (IndexRightSideTape(index) || IndexLeftSideTape(index))
                {
                    return ' ';
                }

                return Tape[Origin + index];
            }
            set
            {
                if (IndexRightSideTape(index))
                {
                    Tape.Append(' ', index - (Tape.Length - Origin)).Append(value);
                }
                else if (IndexLeftSideTape(index))
                {
                    Tape.Insert(0, new StringBuilder(value.ToString()).Append(' ', Math.Abs(index) - Origin - 1));

                    Origin += Math.Abs(index) - Origin;
                }
                else Tape[Origin + index] = value;
            }
        }

        public override string ToString() => Tape.ToString();

        public IEnumerator<char> GetEnumerator()
        {
            for (int i = 0; i < Tape.Length; i++)
            {
                yield return Tape[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public object Clone()
        {
            return new InfiniteTape(Tape, Origin);
        }

        private bool IndexRightSideTape(int index) => index > Tape.Length - Origin - 1;
        private bool IndexLeftSideTape(int index) => index < Math.Abs(Origin);
    }
}
