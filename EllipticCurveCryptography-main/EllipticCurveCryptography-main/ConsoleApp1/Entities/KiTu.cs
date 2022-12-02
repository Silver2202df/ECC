using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Entities
{
    class KiTu
    {
        private char Charactr;
        private ToaDo toado;

        public KiTu()
        {

        }
        
        public KiTu(char Charactr, ToaDo toado)
        {
            this.Charactr = Charactr;
            this.toado = toado;
        }
        
        public void setCharactr(char Charactr)
        {
            this.Charactr = Charactr;
        }
        public char getCharctr()
        {
            return this.Charactr;
        }

        public void setToado(ToaDo toado)
        {
            this.toado = toado;
        }
        public ToaDo getToado()
        {
            return this.toado;
        }

        public override String ToString()
        {
            return "KiTu [" + Charactr + "] - [" + toado + "]";
        }
    }
}
