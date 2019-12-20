using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadow
{
    class Program
    {
        static void Main(string[] args)
        {
            MainForm window = new MainForm(1000, 600);
            window.ShowDialog();
        }
    }
}
