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

            //List<string> line = File.ReadAllLines(fileName).ToList();
            //foreach (string line2 in line)
            //{
            //    listT.Add(parserFunc(line2.Split(';')));
            //}

            var fileReader = new StreamReader(fileName);
            while (!fileReader.EndOfStream)
            {
                listT.Add(parserFunc(fileReader.ReadLine().Split(';')));
            }
            fileReader.Close();
            return listT;
        }
    }
}
