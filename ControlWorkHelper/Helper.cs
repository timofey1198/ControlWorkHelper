using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorkHelper
{
    class Helper
    {
        public static int IntInput(string welcome)
        {
            int n = 0;
            Console.WriteLine(welcome);
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Ошибка. Повторите ввод:");
            }
            return n;
        }
    }
}
