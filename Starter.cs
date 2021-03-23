using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace circuit_png
{
    public static class Starter
    {
         public static void RunApplication(Parser text)
         {
            string[] lines = text.ParseFile(@"NewCircuit.txt");
            if (lines == null)
            {
                Console.WriteLine("NULL TEXT");
                Console.ReadKey();
                return;
            }
            if (lines[0].Equals("#New Circuit"))
            {
                lines = lines.Where(w => w != lines[0]).ToArray();
                Bitmap bitmap2 = new Bitmap(Convert.ToInt32(1024), Convert.ToInt32(1024), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bitmap2);
                foreach (string line in lines)
                {
                    string[] subs = text.ParseLine(line);
                    Console.WriteLine(subs[0]);
                    switch (subs[0])
                    {
                        case "Impedance":
                            DrawHelpers.DrawImpedance(g, (new ArraySegment<string>(subs, 1, 5)).ToArray());
                            break;
                        case "Capacior":
                            DrawHelpers.DrawCapacitor(g, (new ArraySegment<string>(subs, 1, 5)).ToArray());
                            break;
                        case "Inductor":
                            DrawHelpers.DrawInductor(g, (new ArraySegment<string>(subs, 1, 5)).ToArray());
                            break;
                        case "Resistor":
                            DrawHelpers.DrawReistor(g, (new ArraySegment<string>(subs, 1, 5)).ToArray());
                            break;
                        case "Battery":
                            DrawHelpers.DrawVoltageSource(g, (new ArraySegment<string>(subs, 1, 5)).ToArray());
                            break;
                        case "Wire":
                            DrawHelpers.DrawWire(g, (new ArraySegment<string>(subs, 1, 4)).ToArray());
                            break;
                        default:
                            Console.WriteLine("Foramat is failed 2");
                            Console.ReadKey();
                            break;

                    }
                }
                bitmap2.Save(@"C:\Users\yuhon\source\repos\circuit_png\circuit_png\test.png", ImageFormat.Png);
                Console.WriteLine("Save as file");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Foramat is failed");
                Console.ReadKey();
            }
            return;
        }
    }
}
