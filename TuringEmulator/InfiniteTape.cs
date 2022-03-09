using System.Collections;
using System.Text;

namespace TuringEmulator
{
    public class InfiniteTape : IEnumerable<char>
    {
        private StringBuilder Tape { get; } = new(" ");
        private int Origin { get; set; } = 0;

        static private readonly InfiniteTape _default = new InfiniteTape(" ", 0);
        static public InfiniteTape Default { get { return _default; } }

        public InfiniteTape(string str = " ", int origin = 0) => Set(str, origin);

        public InfiniteTape(StringBuilder str, int origin = 0)
        {
            ArgumentNullException.ThrowIfNull(str);

            Set(str.ToString(), origin);
        }

        public InfiniteTape(InfiniteTape tape)
        {
            ArgumentNullException.ThrowIfNull(tape);

            Set(tape.Tape.ToString(), tape.Origin);
        }

        public void Set(string str = " ", int origin = 0)
        {
            ArgumentNullException.ThrowIfNull(str);

            Clear();
            Tape.Append(str);
            Origin = origin;
        }

        public void Clear() => Tape.Clear();

        public char this[int index]
        {
            get
            {
                if (index > Tape.Length - Origin - 1 || index < -Origin)
                    return ' ';

                return Tape[Origin + index];
            }
            set
            {
                if (index > Tape.Length - Origin - 1)
                    Tape.Append(' ', index - (Tape.Length - Origin)).Append(value);
                else if (index < -Origin)
                {
                    Tape.Insert(0, new StringBuilder(value.ToString()).Append(' ', -index - Origin - 1));

                    Origin += -index - Origin;
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
    }
}
