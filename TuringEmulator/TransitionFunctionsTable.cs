using System.Collections;

namespace TuringEmulator
{
    public class TransitionFunctionsTable : IEnumerable<TransitionFunction>
    {
        public int Count 
        {
            get
            {
                return _transitionFunctions.Count;
            }
        }

        private List<TransitionFunction> _transitionFunctions = new();

        static public TransitionFunctionsTable Default => new();

        public TransitionFunctionsTable() { }

        public TransitionFunctionsTable(IEnumerable<TransitionFunction> collection) => Add(collection);

        public TransitionFunctionsTable(TransitionFunctionsTable table)
        {
            ArgumentNullException.ThrowIfNull(table);

            _transitionFunctions = new List<TransitionFunction>(table._transitionFunctions);
        }

        public IEnumerator<TransitionFunction> GetEnumerator() => _transitionFunctions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(TransitionFunction tf)
        {
            ArgumentNullException.ThrowIfNull(tf);

            _transitionFunctions.Add(tf);

            RemoveCopies();
        }

        public void Add(IEnumerable<TransitionFunction> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            _transitionFunctions.AddRange(collection);

            RemoveCopies();
        }

        public TransitionFunction FindFunctionToPerformOrDefault(char symbol, int state)
        {
            return _transitionFunctions.FirstOrDefault(s => s.TapeSymbol == symbol && s.CurrentState == state) ?? TransitionFunction.Default;
        }

        private void RemoveCopies()
        {
            _transitionFunctions = _transitionFunctions
                .AsParallel()
                .Where(s => s != null)
                .GroupBy(g => new
                {
                    g.CurrentState,
                    g.TapeSymbol
                })
                .Select(f => f.First())
                .ToList();
        }

    }
}
