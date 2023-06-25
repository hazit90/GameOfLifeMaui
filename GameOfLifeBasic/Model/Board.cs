using System;
using System.Threading.Tasks;

namespace GameOfLifeBasic.Model
{
    public class Board
    {
        public readonly byte[,] Cells;
        public readonly int Columns;
        public readonly int Rows;

        public Board(int rows, int columns)
        {
            Cells = new byte[rows, columns];
            Columns = columns;
            Rows = rows;
            AddRandom(0.2);
        }

        public void AddRandom(double density)
        {
            Random rand = new();
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (rand.NextDouble() < density)
                        Cells[i, j] = 1;
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i, j] = 0;
                }
            }
        }

        public void Advance()
        {
            byte[,] newCells = new byte[Rows, Columns];

            // Calculate neighbors in parallel
            Parallel.For(0, Columns, x =>
            {
                for (int y = 0; y < Rows; y++)
                {
                    int xL = (x == 0) ? Columns - 1 : x - 1;
                    int xR = (x == Columns - 1) ? 0 : x + 1;
                    int yT = (y == 0) ? Rows - 1 : y - 1;
                    int yB = (y == Rows - 1) ? 0 : y + 1;

                    int liveNeighbors =
                        Cells[yT, xL] + Cells[yT, x] + Cells[yT, xR] +
                        Cells[y, xL] + Cells[y, xR] +
                        Cells[yB, xL] + Cells[yB, x] + Cells[yB, xR];

                    bool isAlive = Cells[y, x] == 1;

                    // Apply rules
                    if (isAlive && (liveNeighbors == 2 || liveNeighbors == 3))
                    {
                        newCells[y, x] = 1;
                    }
                    else if (!isAlive && liveNeighbors == 3)
                    {
                        newCells[y, x] = 1;
                    }
                    else
                    {
                        newCells[y, x] = 0;
                    }
                }
            });

            // Swap the new state into place
            Array.Copy(newCells, Cells, newCells.Length);
        }
    }
}
