using System;
using Pudelko.Enums;

namespace Pudelko
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            Console.WriteLine();
            Console.WriteLine("Milimeter 160, 370, 1200");
            Pudelko ass1 = new Pudelko(160, 370, 1200, UnitOfMeasure.milimeter);

            Console.WriteLine(ass1.UnitOfMeasure);
            Console.WriteLine(ass1.ToString("m"));
            Console.WriteLine(ass1.ToString("cm"));
            Console.WriteLine(ass1.ToString("mm"));


            Console.WriteLine();
            Console.WriteLine("Centimeter 16, 37, 120");
            Pudelko ass2 = new Pudelko(16, 37, 120, Enums.UnitOfMeasure.centimeter);

            Console.WriteLine(ass2.UnitOfMeasure);
            Console.WriteLine(ass2.ToString("m"));
            Console.WriteLine(ass2.ToString("cm"));
            Console.WriteLine(ass2.ToString("mm"));


            Console.WriteLine();
            Console.WriteLine("Meter 0.16, 0.37, 1.20");
            Pudelko ass3 = new Pudelko(0.16, 0.37, 1.20, Enums.UnitOfMeasure.meter);

            Console.WriteLine(ass3.UnitOfMeasure);
            Console.WriteLine(ass3.ToString("m"));
            Console.WriteLine(ass3.ToString("cm"));
            Console.WriteLine(ass3.ToString("mm"));


            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Volume");
            Console.WriteLine(ass1.Objetosc);
            Console.WriteLine(ass2.Objetosc);
            Console.WriteLine(ass3.Objetosc);

            Console.WriteLine();
            Console.WriteLine("Area");
            Console.WriteLine(ass1.Pole);
            Console.WriteLine(ass2.Pole);
            Console.WriteLine(ass3.Pole);


            //Equals
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Equals");
            Console.WriteLine(ass1.Equals(ass2));
            Console.WriteLine(ass2.Equals(ass3));
            Console.WriteLine(ass3.Equals(ass1));

            //Hashcode
            //Operators
        }
    }
}
