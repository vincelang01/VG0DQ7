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
            using (var fileReader = new StreamReader(fileName))
            {
                while (!fileReader.EndOfStream)
                {
                    listT.Add(parserFunc(fileReader.ReadLine().Split(';')));
                }
                fileReader.Close();
            }
            return listT;
        }
    }
}
