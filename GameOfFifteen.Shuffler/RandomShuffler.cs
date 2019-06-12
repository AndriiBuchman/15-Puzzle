using System;
using System.Collections.Generic;

namespace GameOfFifteen.Shuffler
{
    class RandomShuffler
    {
        public int[,] defaultMatrix = {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 },
            { 9, 10, 11, 12 },
            { 13, 14, 15, 0 }
        };

        public int[,] Shuffler(int n)
        {
            int[] indexOfZero;
            for (int i = 0; i < n; i++)
            {
                indexOfZero = SearchZero(defaultMatrix);

                List<int> DirectionList = new List<int>() { 1, 2, 3, 4 };

                if (indexOfZero[0] == 3) DirectionList.Remove(2);

                if (indexOfZero[0] == 0) DirectionList.Remove(1);

                if (indexOfZero[1] == 3) DirectionList.Remove(3);

                if (indexOfZero[1] == 0) DirectionList.Remove(4);

                Random random = new Random();

                int move = random.Next(0, DirectionList.Count);
               
                int[] indexesOfElNearZero = elNearZero(DirectionList[move], indexOfZero);
                defaultMatrix = Swap(defaultMatrix, indexesOfElNearZero, indexOfZero);
            }
            WriteReadFile.WriteMatrixToFile(defaultMatrix);
           
            return defaultMatrix;
        }
        public static int[] SearchZero(int[,] matrix)
        {
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] == 0) return new int[] { i, j };
                }
            }
            return new int[] { };
        }

        public int[] elNearZero(int n, int[] zeroIndex)
        {
            if (n == 1) return new int[] { zeroIndex[0] - 1, zeroIndex[1] };

            if (n == 2) return new int[] { zeroIndex[0] + 1, zeroIndex[1] };

            if (n == 3) return new int[] { zeroIndex[0], zeroIndex[1] + 1 };

            return new int[] { zeroIndex[0], zeroIndex[1] - 1 };

        }

        public int[,] Swap(int[,] matrix, int[] index, int[]zeroIndex)
        {
            matrix[zeroIndex[0], zeroIndex[1]] = matrix[index[0], index[1]];
            matrix[index[0], index[1]] = 0;
            return matrix;
        }
    }
}
