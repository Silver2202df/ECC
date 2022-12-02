using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Entities
{
    class ToaDoMa
    {
        private ToaDo C1;
        private ToaDo C2;

        public ToaDoMa()
        {

        }
        public ToaDoMa(ToaDo C1, ToaDo C2)
        {
            this.C1 = C1;
            this.C2 = C2;
        }
        public void setC1(ToaDo C1)
        {
            this.C1 = C1;

        }

        public void setC2(ToaDo C2)
        {
            this.C2 = C2;

        }

        public ToaDo getC1()
        {
            return this.C1;

        }

        public ToaDo getC2()
        {
            return this.C2;

        }

        public override String ToString()
        {
            return  C1 +" "+  C2 ;
        }
    }
}
