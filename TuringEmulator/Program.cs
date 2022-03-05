using System.Text;

namespace TuringEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("bruh");
            string str = "abcd";
            Console.WriteLine(str[^1]);
            InfTape it = new(str, 2);
            Console.WriteLine(it[-2]);
            it[-2] = 'q';
            Console.WriteLine(it[-2]);
            Console.WriteLine(it[-1]);
            Console.WriteLine(it[0]);
            Console.WriteLine(it[1]);
            Console.WriteLine(it);
         
        }
    }
}