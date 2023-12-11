using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linear_1
{
    internal class Program           //Ahmed Ayman Mahmoud
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter the number of rows : ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Please enter the number of columns : ");
            int columns = int.Parse(Console.ReadLine());

            double[,] matrix = new double[rows, columns];
            Console.WriteLine("Please enter the elements of the matrix : ");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("Matrix("+ (i + 1) + "," + (j + 1) + ") : ");
                    matrix[i, j] = double.Parse(Console.ReadLine());
                }
            }
            Console.WriteLine("\nMatrix : ");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(matrix[i, j]);
                    if (j < columns - 1)
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Solving the matrix by row echelon form : \n");
            solve_rowEchelonForm(matrix);

            Console.WriteLine("\nThe final matrix : ");
            print_matrix(matrix);

            get_values(matrix);
            Console.WriteLine("\nSolution : ");
            print_solution(matrix);

            Console.ReadKey();
        }

        static void solve_rowEchelonForm(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                if (matrix[i, i] == 0)
                {
                    for (int j = i + 1; j < rows; j++)
                    {
                        if (matrix[j, i] != 0)
                        {
                            swap_rows(matrix, i, j);
                            Console.WriteLine( "Swapped row " + (i + 1) + " with row " + (j + 1) );
                            break;
                        }
                    }
                }
                if (matrix[i, i] == 0)
                {
                    continue;
                }
                double factor = matrix[i, i];
                Console.WriteLine("Dividing row " + (i + 1) + " by " + (factor) );
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] /= factor;
                }
                for (int j = i + 1; j < rows; j++)
                {
                    factor = matrix[j, i];
                    Console.WriteLine("Subtracting "+ factor + " times row " + (i + 1) + " from row " + (j + 1) );
                    for (int k = 0; k < columns; k++)
                    {
                        matrix[j, k] -= (factor * matrix[i, k]);
                    }
                }

                Console.WriteLine("Result after step "+ (i + 1) + " : ");
                print_matrix(matrix);
                Console.WriteLine();
            }
            if (zero_row(matrix, rows - 1))
            {
                Console.WriteLine("The matrix has infinite solutions.");
            }
            else if (inconsistent(matrix))
            {
                Console.WriteLine("The matrix has no solution.");
            }
        }
        static void swap_rows(double[,] matrix, int row1, int row2)
        {
            int columns = matrix.GetLength(1);
            for (int i = 0; i < columns; i++)
            {
                double temp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = temp;
            }
        }
        static bool zero_row(double[,] matrix, int row)
        {
            int columns = matrix.GetLength(1);
            for (int i = 0; i < columns; i++)
            {
                if (matrix[row, i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        static bool inconsistent(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                if (matrix[i, i] == 0 && matrix[i, columns - 1] != 0)
                {
                    return true;
                }
            }
            return false;
        }
        static void get_values(double[,] matrix2)
        {
            int rows = matrix2.GetLength(0);
            int columns = matrix2.GetLength(1);
            for (int k = 0; k < rows; k++)
            {
                int maxRow = k;
                double maxVal = matrix2[k, k];
                for (int i = k + 1; i < rows; i++)
                {
                    if (Math.Abs(matrix2[i, k]) > Math.Abs(maxVal))
                    {
                        maxRow = i;
                        maxVal = matrix2[i, k];
                    }
                }
                if (maxRow != k)
                {
                    for (int j = k; j < columns; j++)
                    {
                        double temp = matrix2[k, j];
                        matrix2[k, j] = matrix2[maxRow, j];
                        matrix2[maxRow, j] = temp;
                    }
                }
                for (int i = k + 1; i < rows; i++)
                {
                    double factor = matrix2[i, k] / matrix2[k, k];
                    for (int j = k; j < columns; j++)
                    {
                        matrix2[i, j] -= factor * matrix2[k, j];
                    }
                }
            }
            for (int k = rows - 1; k >= 0; k--)
            {
                double sum = 0;
                for (int j = k + 1; j < columns - 1; j++)
                {
                    sum += matrix2[k, j] * matrix2[j, columns - 1];
                }
                matrix2[k, columns - 1] = (matrix2[k, columns - 1] - sum) / matrix2[k, k];
                matrix2[k, k] = 1;
            }
        }
        static void print_matrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }

                Console.WriteLine();
            }
        }

        static void print_solution(double[,] matrix2)
        {
            int rows = matrix2.GetLength(0);
            int columns = matrix2.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("x{0} = {1}", i + 1, matrix2[i, columns - 1]);
            }
        }

    }
}
