namespace Needleman_Wunsch.Models
{
    public class DinamicProgrammingMatrix
    {
        public int[,] Matrix { get; set; }
        public char[] LabelsRows { get; set; }
        public char[] LabelsCols { get; set; }

        public DinamicProgrammingMatrix(string sequenceA, string sequenceB)
        {

        }


    }
}
