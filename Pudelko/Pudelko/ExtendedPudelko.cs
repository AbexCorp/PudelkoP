using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PudelkoL;
using PudelkoL.Enums;

namespace PudelkoLExtended
{
    public static class ExtendedPudelko
    {
        public static Pudelko Kompresuj(this Pudelko box)
        {
            double side = Math.Cbrt(box.Objetosc);
            Pudelko newBox = new Pudelko(side, side, side, UnitOfMeasure.meter);
            return newBox;
        }

        public static int ComparePudelko(Pudelko boxA, Pudelko boxB)
        {
            int result = (boxA.Objetosc).CompareTo(boxB.Objetosc);
            if(result != 0)
                return result;

            result = (boxA.Pole).CompareTo(boxB.Pole);
            if (result != 0)
                return result;

            result = (boxA.A + boxA.B + boxA.C).CompareTo(boxB.A + boxB.B + boxB.C);
            return result;
        }
    }
}
