using Needleman_Wunsch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Needleman_Wunsch
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = File.ReadAllLines("blosum62.txt");

            List<string[]> blosum = new List<string[]>();

            foreach(var line in file)
            {
                blosum.Add(line.Split(" ").Where(s => s != " " && s != "").ToArray());
            }

            var blosumMatrix = new int[20, 20];
            var labels = new string[20];

            for(int i = 1; i <= 20; i++)
                labels[i - 1] = blosum.ElementAt(0)[i];

            for (int i = 1; i < 21; i++)
            {
                for(int j = 1; j < 21; j++)
                {
                    blosumMatrix[i - 1, j - 1] = Int16.Parse(blosum.ElementAt(i)[j]);
                }
            }

            NeedlemanWunschMatrix matrix = new NeedlemanWunschMatrix("PLEASANTLY", "MEANLY", labels, blosumMatrix);
            matrix.PrintMatrix();
            matrix.PrintDirectionMatrix();
        }
    }
}
