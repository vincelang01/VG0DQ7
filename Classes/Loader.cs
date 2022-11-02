using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VG0DQ7.Classes
{
    internal class Loader<T>
    {
        public List<T> LoadFromFile(string fileName, Func<string[], T> parserFunc)
        {
            List<T> listT = new List<T>();
            List<string> lineIn = File.ReadAllLines(fileName).ToList();
            foreach(string line in lineIn)
            {
                listT.Add(parserFunc(line.Split(';')));
            }
            return listT;
        }
    }
}
