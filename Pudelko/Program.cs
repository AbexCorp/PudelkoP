using System;
using PudelkoL.Enums;
using PudelkoLExtended;
using System.Collections.Generic;

namespace PudelkoL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("---------Kompresuj---------");
            Console.WriteLine("");
            Pudelko p1 = new Pudelko(2,2,2, UnitOfMeasure.meter); //2x2x2
            Console.WriteLine($"{p1} --> {p1.Kompresuj()}");
            Pudelko p2 = new Pudelko(2, 4, 8, UnitOfMeasure.meter); //4x4x4
            Console.WriteLine($"{p2} --> {p2.Kompresuj()}");
            Pudelko p3 = new Pudelko(3.24, 7.678, 5.553, UnitOfMeasure.meter); //?x?x?
            Console.WriteLine($"{p3} --> {p3.Kompresuj()}");


            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("---------Sortuj---------");
            Console.WriteLine("");
            List<Pudelko> boxList = new List<Pudelko>();
            boxList.Add(p1);
            boxList.Add(p2);
            boxList.Add(p1);
            boxList.Add(p2);
            boxList.Add(new Pudelko(6.267,1));
            boxList.Add(new Pudelko(594,10,500, UnitOfMeasure.centimeter));
            boxList.Add(new Pudelko());
            boxList.Add(new Pudelko(1,2,3, UnitOfMeasure.milimeter));
            boxList.Add(new Pudelko(8,5));
            boxList.Add(new Pudelko(10,10,10));
            boxList.Add(new Pudelko());
            boxList.Add(new Pudelko(6,7,8));

            boxList.Sort(ExtendedPudelko.ComparePudelko);

            for(int i = 0; i < boxList.Count; i++)
            {
                Console.WriteLine($"[{i}] {boxList[i].ToString()}   -  V: {boxList[i].Objetosc}  P: {boxList[i].Pole}  Obw: {boxList[i].A + boxList[i].B + boxList[i].C}");
            }
        }
    }
}
