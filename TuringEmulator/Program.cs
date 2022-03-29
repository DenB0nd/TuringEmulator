using System.Diagnostics;
using System.Text;

namespace TuringEmulator
{
    static class Program
    {
        static void Main(string[] args)
        {
            TransitionFunction transitionFunction = new TransitionFunction();
            Console.WriteLine(transitionFunction.ToString());
            Console.WriteLine(TransitionFunction.Default.ToString());
            Console.WriteLine(transitionFunction.Equals(TransitionFunction.Default));
            
        }
    }
}

// '1' * 400

// handmade cycle - first 123 then near 120
// handmade cycle add - first 115 then near 120
// machine run - first 115 then from 108 to 123
// machine run add - first 139 then near 120

// '1' * 1000

// handmade cycle - near 330
// machine run - near 330

// run/cycle doesn't matter?
// a little bit better not to use Add
