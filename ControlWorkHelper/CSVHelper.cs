using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ControlWorkHelper
{
    class CSVHelper
    {
        public static List<List<string>> ReadAll(string filename)
        {
            List<List<string>> res = new List<List<string>>();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    List<string> values = line.Split(';').ToList();
                    res.Add(values.ToList());
                }
            }
            return res;
        }
    }
}
