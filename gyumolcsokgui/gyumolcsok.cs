using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace gyumolcsokgui
{
    internal class gyumolcsok
    {
        int id;
        string nev;
        double egyegar;
        double menyiseg;

        public int Id { get => id; set => id = value; }
        public string Nev { get => nev; set => nev = value; }
        public double Egyegar { get => egyegar; set => egyegar = value; }
        public double Menyiseg { get => menyiseg; set => menyiseg = value; }

        public gyumolcsok(int id, string nev, double egyegar, double menyiseg)
        {
            Id = id;
            Nev = nev;
            Egyegar = egyegar;
            Menyiseg = menyiseg;
        }
        public override string ToString()
        {
            return nev + "( " + egyegar +" Ft/Kg )" + menyiseg ;
        }
    }
}
