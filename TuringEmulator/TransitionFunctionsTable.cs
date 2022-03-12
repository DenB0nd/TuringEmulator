using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringEmulator
{
    public class TransitionFunctionsTable : IEnumerable<TransitionFunction>
    {

        private List<TransitionFunction> transitionFunctions = new();

        static private readonly TransitionFunctionsTable _default = new();

        static public TransitionFunctionsTable Default { get { return _default; } }

        public TransitionFunctionsTable() { }

        public TransitionFunctionsTable(IEnumerable<TransitionFunction> collection) => Add(collection);

        public TransitionFunctionsTable(TransitionFunctionsTable table)
        {
            ArgumentNullException.ThrowIfNull(table);

            transitionFunctions = new List<TransitionFunction>(table.transitionFunctions);
        }

        public IEnumerator<TransitionFunction> GetEnumerator() => transitionFunctions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(TransitionFunction tf)
        {
            ArgumentNullException.ThrowIfNull(tf);

            transitionFunctions.Add(tf);

            RemoveCopies();
        }

        public void Add(IEnumerable<TransitionFunction> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            transitionFunctions.AddRange(collection);

            RemoveCopies();
        }

        public TransitionFunction FindFunctionToPerform(char symbol, int state)
        {
            return transitionFunctions.FirstOrDefault(s => s.TapeSymbol == symbol && s.CurrentState == state) ?? TransitionFunction.Default;
        }

        private void RemoveCopies()
        {
            transitionFunctions = transitionFunctions
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
