using System;
using PudelkoL.Enums;

namespace PudelkoL
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.WriteLine("1, 2, 0.5");
            Pudelko ass4 = new Pudelko(1, 2, 0.5);

            Console.WriteLine(ass4.UnitOfMeasure);
            Console.WriteLine(ass4.ToString("m"));
            Console.WriteLine(ass4.ToString("cm"));
            Console.WriteLine(ass4.ToString("mm"));


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
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Hashcode");
            Console.WriteLine(ass1.GetHashCode());
            Console.WriteLine(ass2.GetHashCode());
            Console.WriteLine(ass3.GetHashCode());

            //Operators
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Hashcode");
            Console.WriteLine(ass1 == ass2);
            Console.WriteLine(ass2 == ass3);
            Console.WriteLine(ass3 == ass1);
            Console.WriteLine(!(ass1 != ass2));
            Console.WriteLine(!(ass2 != ass3));
            Console.WriteLine(!(ass3 != ass1));

            //Conversions
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Conversions");

            ValueTuple<int, int, int> testTuple = (100, 2768, 450);
            Pudelko ass5 = testTuple;

            Console.WriteLine(ass5.ToString());
            Console.WriteLine(ass5.UnitOfMeasure);
            Console.WriteLine(ass5.ToString("m"));
            Console.WriteLine(ass5.ToString("cm"));
            Console.WriteLine(ass5.ToString("mm"));

            double[] ass6 = (double[])ass5;
            Console.Write($"{ass6[0]} {ass6[1]} {ass6[2]}");


            //Index
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Indexer");
            Console.WriteLine(ass1.ToString());
            Console.WriteLine(ass1[0]);
            Console.WriteLine(ass1[1]);
            Console.WriteLine(ass1[2]);
            Console.WriteLine();
            Console.WriteLine(ass2.ToString());
            Console.WriteLine(ass2[0]);
            Console.WriteLine(ass2[1]);
            Console.WriteLine(ass2[2]);
            Console.WriteLine();
            Console.WriteLine(ass3.ToString());
            Console.WriteLine(ass3[0]);
            Console.WriteLine(ass3[1]);
            Console.WriteLine(ass3[2]);

            //Foreach
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Foreach");
            Console.WriteLine(ass1.ToString());
            foreach(double x in ass1) Console.WriteLine(x);
            Console.WriteLine();
            Console.WriteLine(ass2.ToString());
            foreach (double x in ass2) Console.WriteLine(x);
            Console.WriteLine();
            Console.WriteLine(ass3.ToString());
            foreach (double x in ass3) Console.WriteLine(x);

            //Parse
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Parse");
            Console.WriteLine(new Pudelko(2.5, 9.321, 0.1) == Pudelko.Parse("2.500 m × 9.321 m × 0.100 m"));
            Pudelko ass7 = Pudelko.Parse("2.500 m × 9.321 m × 0.100 m");

            Console.WriteLine(ass7.UnitOfMeasure);
            Console.WriteLine(ass7.ToString("m"));
            Console.WriteLine(ass7.ToString("cm"));
            Console.WriteLine(ass7.ToString("mm"));

            //+ Operator
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("+ Operator");

            Pudelko smallBox1 = new Pudelko(2,2,2, UnitOfMeasure.meter);
            Pudelko smallBox2 = new Pudelko(1, 2, 1, UnitOfMeasure.meter);
            Console.WriteLine(smallBox1.ToString("mm") + "  +  " + smallBox2.ToString("mm"));
            Pudelko bigBox = smallBox1 + smallBox2;
            Console.WriteLine(bigBox.ToString("mm"));


            //+ Tests
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Tests");

            
            Pudelko p = new Pudelko(20, 30, 40, UnitOfMeasure.milimeter);
            Pudelko p2 = new Pudelko(40, 20, 30, UnitOfMeasure.milimeter);
            Console.WriteLine(p.Equals(p2));
            Console.WriteLine(p.Equals(p));
        }
    }
}
