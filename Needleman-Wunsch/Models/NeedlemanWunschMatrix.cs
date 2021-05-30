using System;
using System.Collections.Generic;
using System.Linq;

namespace Needleman_Wunsch.Models
{
    public class NeedlemanWunschMatrix : DinamicProgrammingMatrix
    {
        private const int GAP = -5;
        public string[,] DirectionalMatrix { get; set; }
        public int[,] BlosumMatrix { get; set; }
        public List<string> Labels { get; set; }

        public NeedlemanWunschMatrix(string sequenceA, string sequenceB, string[] labels, int[,] blosumMatrix) 
            : base(sequenceA, sequenceB)
        {
            Matrix = new int[sequenceA.Length + 1, sequenceB.Length + 1];
            DirectionalMatrix = new string[sequenceA.Length + 1, sequenceB.Length + 1];
            Labels = labels.ToList();
            BlosumMatrix = blosumMatrix;

            InitializeMatrix(sequenceA, sequenceB);
            CompleteMatrixCalculations();
        }

        public void PrintMatrix()
        {
            Console.WriteLine("\nScore Matrix\n");
            Console.Write($"");
            foreach (var label in LabelsCols)
                Console.Write($"{label}\t");

            Console.WriteLine();
            for (int i = 0; i < GetLinesCount(); i++)
            {
                Console.Write($"{LabelsRows[i]} ");
                for (int j = 0; j < GetColsCount(); j++)
                {
                    Console.Write($"{ Matrix[i, j] }\t");
                }
                Console.WriteLine();
            }
        }

        public void PrintDirectionMatrix()
        {
            Console.WriteLine("\nDirectional Matrix\n");
            Console.Write($"");
            foreach (var label in LabelsCols)
                Console.Write($"{label}\t");

            Console.WriteLine();
            for (int i = 0; i < GetLinesCount(); i++)
            {
                Console.Write($"{LabelsRows[i]} ");
                for (int j = 0; j < GetColsCount(); j++)
                {
                    Console.Write($"{ DirectionalMatrix[i, j] ?? "None"}\t");
                }
                Console.WriteLine();
            }
        }

        public void CompleteMatrixCalculations()
        {
            for(int i = 1; i < GetLinesCount(); i++)
            {
                for(int j = 1; j < GetColsCount(); j++)
                {
                    var left = Matrix[i, j - 1] + GAP;
                    var diagonal = Matrix[i - 1, j - 1] + CompareChars(i, j);
                    var up = Matrix[i - 1, j] + GAP;

                    Matrix[i, j] = GetMaxValue(i, j);

                    if(Matrix[i, j] == left)
                        DirectionalMatrix[i, j] = "Left";
                    else if(Matrix[i, j] == diagonal)
                        DirectionalMatrix[i, j] = "Diag";
                    else if(Matrix[i, j] == up)
                        DirectionalMatrix[i, j] = "Up";
                }
            }
        }

        private int GetMaxValue(int i, int j)
        {
            return Math.Max(Matrix[i, j - 1] + GAP, Math.Max(Matrix[i - 1, j - 1] + CompareChars(i, j), Matrix[i - 1, j] + GAP));
        }

        private int CompareChars(int i, int j)
        {
            var rowSymbol = LabelsRows[i];
            var colSymbol = LabelsCols[j];

            var indexRow = Labels.IndexOf($"{rowSymbol}");
            var indexCol = Labels.IndexOf($"{colSymbol}");

            return BlosumMatrix[indexRow, indexCol];
        }

        public int GetLinesCount()
        {
            return LabelsRows.Length;
        }

        public int GetColsCount()
        {
            return LabelsCols.Length;
        }

        public void InitializeMatrix(string sequenceA, string sequenceB)
        {
            LabelsRows = $"e{sequenceA}".ToCharArray();
            LabelsCols = $"e{sequenceB}".ToCharArray();

            for (int i = 0; i < LabelsRows.Length; i++)
            {
                for (int j = 0; j < LabelsCols.Length; j++)
                {
                    if (i == 0)
                    {
                        Matrix[i, j] = GAP * j;

                        if(j == 0)
                            DirectionalMatrix[i, j] = "None";
                        else
                            DirectionalMatrix[i, j] = "Left";
                    }
                    else
                    {
                        if (i > 0 && j < 1)
                        {
                            Matrix[i, j] = GAP * i;
                            DirectionalMatrix[i, j] = "Up";
                        }
                    }
                }
            }
        }
    }
}
