using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Program
    {
        //@author: Austin Wang 11/14/2017
        //Performs the Knight's Tour using both Heuristic and Brute Force algorithms. 
        //Heuristic algorithm can be improved by maintaining heuristic board values as opposed to re-calculating them for each move
        //
        static void Main(string[] args)
        {
            
            //2 dimensional array that instantiates the chessboard
            // row 0 and column 0 are unused
            int[,] BOARD = new int[8, 8];
            int starti, startj;

            Console.Write("Enter a starting row(0-7): ");
            startj = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter a starting column(0-7): ");
            starti = Convert.ToInt32(Console.ReadLine());

            Knight white = new Knight(starti, startj);
            var watch = System.Diagnostics.Stopwatch.StartNew();



            runHeuristic(white, BOARD);
            //runBruteForce(white, BOARD, starti, startj);


            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("\nThe Knight's Tour finished in " + elapsedMs + " milliseconds");
            Console.ReadKey();
            
        }
        static void runHeuristic(Knight K, int[,] BOARD){
            bool complete = false;
            clearBoard(BOARD);
            BOARD[K.Loci, K.Locj] = 1;

            do
            {
                K.checkmov(BOARD);
                complete = K.moveK(BOARD);
            } while (complete);

            printArr(BOARD);
        }
        static void runBruteForce(Knight K, int[,] BOARD, int i, int j)
        {
            int count = 0;
            int[] record = new int[64];
            bool complete = false;

            do
            {
                //resets the knight's starting location and order to the one given by the user
                K.reset(i, j);
                clearBoard(BOARD);

                BOARD[K.Loci, K.Locj] = 1;

                do
                {
                    complete = K.randMoveK(BOARD);
                } while (complete);

                record[K.order - 2] += 1;
                count++;
            } while (K.order != 65);

            printArr(BOARD);
            Console.WriteLine("\nIt took " + count + " tours to find a complete path using Brute Force(random number generation)\n");
            printArr(record);

       
        }
        static void clearBoard(int[,] Arr)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Arr[i, j] = 0;
                }
            }
        }
        static void printArr(int[,] Arr)
        {
            Console.WriteLine();
            for(int j = 0; j<8; j++)
            {
                for(int i = 0; i<8; i++)
                {
                    Console.Write(Arr[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
        }
        static void printArr(int[] Arr)
        {
            Console.WriteLine("The number of attempts that ended at n moves, in order, are: ");
            for (int i = 0; i < 64; i++)
            {
                Console.Write(Arr[i]);
                Console.Write(", ");
            }
            Console.WriteLine("\n");
        }
    }
}
