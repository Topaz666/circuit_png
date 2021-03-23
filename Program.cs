using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
/*
 * Drawing the circuit elements by using the keywords of R, L, C, P, for Resistor, Inductor, capacitor and Power respectively
*/
namespace circuit_png
{
    class Program
    {
        static void Main()
        {
            Parser text = new Parser();
            Starter.RunApplication(text);
            text.Dispose();
            return;
        }
    }
}
