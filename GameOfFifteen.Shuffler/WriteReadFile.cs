using Puzzle15;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace GameOfFifteen.Shuffler
{
    public static class WriteReadFile
    {
        public static int [,] ReadMatrix()
        {
            int k = 0;
            int[,] matrix = new int[4, 4];
            using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/Matrix.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] arr = line.Split('\t');
                    for (int i = 0; i < 4; i++)
                    {
                        matrix[k, i] = Convert.ToInt32( arr[i].ToString());
                    }
                    k++;
                }
                return matrix;
            }
        }

        public static void WriteMovesToFile(LinkedList<State> moves)
        {
            List<int[,]> matrices = new List<int[,]>();
            foreach (State s in moves)
            {
                matrices.Add(((Puzzle15State)s).Square);                
            }

            using (StreamWriter file = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Moves.txt"))
            {
                for (int k = 1; k < matrices.Count; k++)
                {
                    int[] firstZero = RandomShuffler.SearchZero(matrices[k - 1]);
                    int[] secondZero = RandomShuffler.SearchZero(matrices[k]);
                    string move = "";
                    int ele = 0;
                    if (firstZero[0] > secondZero[0]) { move = "↓"; ele = matrices[k][firstZero[0], firstZero[1]]; }
                    else if (firstZero[0] < secondZero[0]) { move = "↑"; ele = matrices[k][firstZero[0], firstZero[1]]; }
                    else if (firstZero[1] < secondZero[1]) { move = "←"; ele = matrices[k][firstZero[0], firstZero[1]]; }
                    else if (firstZero[1] > secondZero[1]) { move = "→"; ele = matrices[k][firstZero[0], firstZero[1]]; }
                    string line = $"{ele}, {move}";
                    file.WriteLine(line);
                }
            }
            
        }
        public static void WriteMatrixToFile(int[,] matrix)
        {
            using (StreamWriter file = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Matrix.txt"))
            {
                string line = "";
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        line += matrix[i, j] + "\t";
                    }
                    file.WriteLine(line);

                    line = "";
                }
            }
        }
    }
}
