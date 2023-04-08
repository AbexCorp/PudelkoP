using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using PudelkoL.Enums;
using System.Collections;
using System.Transactions;

namespace PudelkoL
{
    public sealed class Pudelko : IEquatable<Pudelko>, IEnumerable<double>, IFormattable
    {
        //To do
        //15
        //16


        //2,546m = 254,6cm = 2546mm
        //max size = 10m x 10m x 10m
        //unmutable
        //default is 10 x 10 x 10 cm
        //sealed (can't inherit)

        //>>>> Constructor <<<<
        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if (a == null)
                a = UnitUtility.OneToAny(100, unit);
            if(b == null)
                b = UnitUtility.OneToAny(100, unit);
            if(c == null)
                c = UnitUtility.OneToAny(100, unit);


            //Store size as milimeters
            double[] dimensions = UnitUtility.ThreeFromAnyToAny((double)a, (double)b, (double)c, unit, UnitOfMeasure.milimeter);

            if (dimensions[0] > 10000 || dimensions[1] > 10000 || dimensions[2] > 10000)
                throw new ArgumentOutOfRangeException("Any side of the box can't be longer than 10m");
            if (dimensions[0] < 1 || dimensions[1] < 1 || dimensions[2] < 1)
                throw new ArgumentOutOfRangeException("Each side of the box must be at least 1mm wide");


            this.a = dimensions[0];
            this.b = dimensions[1];
            this.c = dimensions[2];
            unitOfMeasure = unit;

            //This probably converts the numbers two times for some reason
            //dimensionArray[0] = UnitUtility.OneToAny(a, UnitOfMeasure);
            //dimensionArray[1] = UnitUtility.OneToAny(b, UnitOfMeasure);
            //dimensionArray[2] = UnitUtility.OneToAny(c, UnitOfMeasure);

            dimensionArray[0] = A;
            dimensionArray[1] = B;
            dimensionArray[2] = C;
        }



        //>>>> Variables <<<<
        private readonly double a;
        private readonly double b;
        private readonly double c;
        private readonly UnitOfMeasure unitOfMeasure;
        public readonly double[] dimensionArray = new double[3];


        //>>>> Properties <<<<
        public double A
        { get { return UnitUtility.ToMeter(a); } }
        public double B
        { get { return UnitUtility.ToMeter(b); } }
        public double C
        { get { return UnitUtility.ToMeter(c); } }
        public UnitOfMeasure UnitOfMeasure { get { return unitOfMeasure; } }

        public double Objetosc
        {
            get { return Math.Round((A * B * C), 9); }
        }
        public double Pole
        {
            get { return Math.Round((2 * (A * B) + 2 * (B * C) + 2 * (C * A)), 6); }
        }

        public Pudelko Current => throw new NotImplementedException();




        //>>>> To String <<<<
        //#FIX#
        //The IFormatProvider should be a variable that contains the culture(???), 
        //it then passes the culture to other functions and they use it instead of hard typed one.
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString(format);
        }

        override public string ToString()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            return $"{UnitUtility.ToMeter(a)} m × {UnitUtility.ToMeter(b)} m × {UnitUtility.ToMeter(c)} m";
        }

        public string ToString(string format = "m")
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            switch (format)
            {
                case null:
                case "m":
                    return $"{UnitUtility.ToMeter(a):F3} m × {UnitUtility.ToMeter(b):F3} m × {UnitUtility.ToMeter(c):F3} m";

                case "cm":
                    return $"{UnitUtility.ToCentimeter(a):F1} cm × {UnitUtility.ToCentimeter(b):F1} cm × {UnitUtility.ToCentimeter(c):F1} cm";

                case "mm":
                    return $"{a} mm × {b} mm × {c} mm";

                default:
                    throw new FormatException("This format is not supported");
            }
        }



        //>>>> Equatable <<<<
        public override bool Equals(object obj)
        {
            if (obj is Pudelko)
                return Equals((Pudelko)obj);

            return base.Equals(obj);
        }
        public bool Equals(Pudelko box)
        {
            if (box is null || GetType() != box.GetType())
            {
                return false;
            }
            // TODO: write your implementation of Equals() here

            double[] boxA = UnitUtility.ThreeFromAnyToAny(A, B, C, UnitOfMeasure.meter, UnitOfMeasure.milimeter);
            double[] boxB = UnitUtility.ThreeFromAnyToAny(box.A, box.B, box.C, UnitOfMeasure.meter, UnitOfMeasure.milimeter);

            Array.Sort(boxA);
            Array.Sort(boxB);

            if (boxA[0] == boxB[0] && boxA[1] == boxB[1] && boxA[2] == boxB[2])
                return true;

            return false;
        }



        //>>>> HashCode <<<<
        public override int GetHashCode()
        {
            return HashCode.Combine(a, b, c);
            //return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + unitOfMeasure.GetHashCode();
            //return base.GetHashCode();
        }



        //>>>> Operators <<<<
        public static bool operator ==(Pudelko boxA, Pudelko boxB)
        {
            return boxA.Equals(boxB);
        }
        public static bool operator !=(Pudelko boxA, Pudelko boxB)
        {
            return !boxA.Equals(boxB);
        }

        public static Pudelko operator +(Pudelko boxA, Pudelko boxB)
        {
            double[] sidesBoxA = UnitUtility.ThreeFromMeter(boxA.A, boxA.B, boxA.C);
            double[] sidesBoxB = UnitUtility.ThreeFromMeter(boxB.A, boxB.B, boxB.C);

            double[][] allSizes = GetAllPossibleBoxSizes(sidesBoxA[0], sidesBoxA[1], sidesBoxA[2], sidesBoxB[0], sidesBoxB[1], sidesBoxB[2]);
            Pudelko newPudelko = new Pudelko();

            double maxVolume = 1000000000001; //10m*10m*10m + 1mm
            //Console.WriteLine();/////////////////////////////////////////////////
            //int index = -1;////////////////////////////////////////////

            for (int i = 0; i < 18; i++)
            {
                try //Check if it's possible to create a box with given sizes. If not, move to the next sizes,
                {
                    //Console.WriteLine(allSizes[i][0] + " " + allSizes[i][1] + " " + allSizes[i][2] + " [" + i + "]");/////////////////////////////////////
                    //Console.WriteLine((allSizes[i][0] * allSizes[i][1] * allSizes[i][2]));/////////////////////////////////////////
                    Pudelko testSize = new Pudelko(allSizes[i][0], allSizes[i][1], allSizes[i][2], UnitOfMeasure.milimeter);
                }
                catch (ArgumentOutOfRangeException)
                {
                    continue;
                }

                if( (allSizes[i][0] * allSizes[i][1] * allSizes[i][2]) < maxVolume )
                {
                    maxVolume = (allSizes[i][0] * allSizes[i][1] * allSizes[i][2]);
                    newPudelko = new Pudelko(allSizes[i][0], allSizes[i][1], allSizes[i][2], UnitOfMeasure.milimeter);
                    //index = i;///////////////////////////////////////
                }
            }
            
            if(maxVolume == 1000000000001)
                throw new ArgumentOutOfRangeException("These boxes are too large");
            //Console.WriteLine("index:" + index);/////////////////////////////////////////////////
            return newPudelko;
        }
        private static double[][] GetAllPossibleBoxSizes(double a, double b, double c, double x, double y, double z)
        {
            double[][] allSizes = new double[18][];

            allSizes[0] = CalculateBoxSize(a,b,c, x,y,z);
            allSizes[1] = CalculateBoxSize(a,b,c, x,z,y);
            allSizes[2] = CalculateBoxSize(a,b,c, y,x,z);
            allSizes[3] = CalculateBoxSize(a,b,c, y,z,x);
            allSizes[4] = CalculateBoxSize(a,b,c, z,x,y);
            allSizes[5] = CalculateBoxSize(a,b,c, z,y,x);

            allSizes[6] = CalculateBoxSize(b,a,c, x,y,z);
            allSizes[7] = CalculateBoxSize(b,a,c, x,z,y);
            allSizes[8] = CalculateBoxSize(b,a,c, y,x,z);
            allSizes[9] = CalculateBoxSize(b,a,c, y,z,x);
            allSizes[10] = CalculateBoxSize(b,a,c, z,x,y);
            allSizes[11] = CalculateBoxSize(b,a,c, z,y,x);

            allSizes[12] = CalculateBoxSize(c,a,b, x,y,z);
            allSizes[13] = CalculateBoxSize(c,a,b, x,z,y);
            allSizes[14] = CalculateBoxSize(c,a,b, y,x,z);
            allSizes[15] = CalculateBoxSize(c,a,b, y,z,x);
            allSizes[16] = CalculateBoxSize(c,a,b, z,x,y);
            allSizes[17] = CalculateBoxSize(c,a,b, z,y,x);

            return allSizes;
        }
        private static double[] CalculateBoxSize(double a, double b, double c, double x, double y, double z)
        {
            double sideA = a + x;

            double sideB = 0;
            if (b >= y)
                sideB = b;
            else
                sideB = y;
            /*
            if(b - y <= 0)
            {
                //sideB = b;
                if (b >= y)
                    sideB = b;
                else
                    sideB = y;
            }
            else
                sideB = (b + Math.Abs(b - y));
            */

            double sideC = 0;
            if (c >= z)
                sideC = c;
            else
                sideC = z;
            /*
            if (c - z <= 0)
            {
                //sideC = c;
                if (c >= z)
                    sideC = c;
                else
                    sideC = z;
            }
            else
                sideC = (c + Math.Abs(c - z));
            */

            return new double[] {sideA, sideB, sideC };
        }



        //>>>> Conversions <<<<
        public static explicit operator double[](Pudelko box)
        {
            return new double[] { box.A, box.B, box.C };
        }
        public static implicit operator Pudelko(ValueTuple<int, int, int> tuple)
        {
            return new Pudelko(tuple.Item1, tuple.Item2, tuple.Item3, UnitOfMeasure.milimeter);
        }



        //>>>> Indexer <<<<
        public double this[int index]
        {
            get
            {
                if(index < 0 || index > 2)
                    throw new IndexOutOfRangeException();
                return dimensionArray[index];
            }
        }



        //>>>> Iterator <<<<
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < dimensionArray.Length; i++)
            {
                yield return dimensionArray[i];
            }
        }

        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            for (int i = 0; i < dimensionArray.Length; i++)
            {
                yield return dimensionArray[i];
            }
        }



        //>>>> Parse <<<<
        public static Pudelko Parse(string text)
        {
            //new P(2.5, 9.321, 0.1) == P.Parse("2.500 m × 9.321 m × 0.100 m")
            if (text == null)
                throw new ArgumentNullException();

            string[] textToParse = text.Split('×', StringSplitOptions.RemoveEmptyEntries);
            if (textToParse.Length != 3)
                throw new FormatException("text to long or short");

            double[] dimensions = ParseThreeNumbers(textToParse);
            UnitOfMeasure unit = ParseGetUnit(textToParse[0]);

            return new Pudelko(dimensions[0], dimensions[1], dimensions[2], unit);
        }

        private static double[] ParseThreeNumbers(string[] text)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            if (text[0] == null || text[1] == null || text[2] == null)
                throw new ArgumentNullException();

            double[] dimensions = { ParseNumber(text[0]), ParseNumber(text[1]), ParseNumber(text[2]) };
            return dimensions;
        }
        private static double ParseNumber(string text)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            // "2.500 m × 9.321 m × 0.100 m"
            text = text.Trim();
            if (text == null)
                throw new ArgumentNullException();

            string[] splitText = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!double.TryParse(splitText[0], out double dimension))
                throw new FormatException("dimension is incorrect");

            return dimension;
        }
        private static UnitOfMeasure ParseGetUnit(string text)
        {
            text = text.Trim();
            if (text == null)
                throw new ArgumentNullException(); 

            string[] splitText = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            switch(splitText[1])
            {
                case "m":
                    return UnitOfMeasure.meter;
                case "cm":
                    return UnitOfMeasure.centimeter;
                case "mm":
                    return UnitOfMeasure.milimeter;
                default:
                    throw new FormatException("unit is incorrect");
            }
        }
    }
}
