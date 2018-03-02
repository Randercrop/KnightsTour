using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Knight
    {
        //using the array provided in 304
        //int[] KTMOVi = new int[8] { -2, -1, 1, 2, 2, 1, -1, -2 };
        //int[] KTMOVj = new int[8] { 1, 2, 2, 1, -1, -2, -2, -1 };

        //using the order given in the picture
        //int[] KTMOVi = new int[8] { 1, 2, 2, 1, -1, -2, -2, -1 };
        //int[] KTMOVj = new int[8] { -2, -1, 1, 2, 2, 1, -1, -2 };

        //using the order in Deitel's Java how to Program
        int[] KTMOVi = new int[8] {  2,  1, -1, -2, -2, -1, 1, 2};
        int[] KTMOVj = new int[8] { -1, -2, -2, -1,  1,  2, 2, 1};
        
        //Holds how many movement options are avaiable from the current location
        //A zero represents that location is outside the borders or has been previously traveled to
        int[] mov1 = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

        int loci;
        int locj;
        public int order;
        Random rnd;

        public Knight()
        {
            rnd = new Random();
            loci = rnd.Next(0,8);
            locj = rnd.Next(0,8);
            order = 2;
        }
        public Knight(int i, int j)
        {
            rnd = new Random();
            loci = i;
            locj = j;
            order = 2;
        }
        public bool reset(int i, int j){
            loci = i;
            locj = j;
            order = 2;
            return true;
        }
        public int Loci
        {
            get { return loci; }
            set { loci = value; }
        }
        public int Locj
        {
            get { return locj; }
            set { locj = value; }
        }
        //Checks the next 8 moves from the current location to see if it's out of bounds or has already been visited
        //for all next valid spaces, we check the 8 moves from that space for how many moves from there are valid
        public void checkmov(int[,] board)
        {
            for(int ind = 0; ind<8; ind++)
            {
                if(((loci+KTMOVi[ind])>=0) && ((loci + KTMOVi[ind])<8) && ((locj+KTMOVj[ind]) >=0) && ((locj + KTMOVj[ind]) <8) )
                {
                    if(board[(loci+KTMOVi[ind]),(locj+KTMOVj[ind])] == 0)
                    {
                        mov1[ind] = checknext(board, (loci + KTMOVi[ind]), (locj + KTMOVj[ind]))+1;
                    }
                }
            }
        }
         
        //checks the next iteration of 8 moves from the given position and returns how many moves from this position are valid
        private int checknext(int[,] board, int ii, int jj)
        {
            int count = 0;
            for (int next = 0; next < 8; next++)
            {
                if (((ii+KTMOVi[next]) >= 0) && ((ii +KTMOVi[next]) < 8) && ((jj + KTMOVj[next]) >= 0) && ((jj + KTMOVj[next]) < 8))
                {
                    if (board[(ii + KTMOVi[next]), (jj + KTMOVj[next])] == 0)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public bool randMoveK(int[,] board)
        {
            int move;
            int tempi, tempj;
            for (int i = 0; i < 8; i++)
            {
                mov1[i] = 0;
            }

            //assigns moves that place the knight outside the board or onto a previously visited space -1 indicating invalid
            for (int i = 0; i < 8; i++)
            {
                tempi = loci + KTMOVi[i];
                tempj = locj + KTMOVj[i];
                if (tempi < 0 || tempi >= 8 || tempj < 0 || tempj >= 8 || board[tempi, tempj] != 0)
                {
                    mov1[i] = -1;
                }
            }

            //if all possible moves are invalid, return false to exit
            if (Array.IndexOf(mov1, 0) == -1)
                return false;
            else
            {
                do
                {
                   move = rnd.Next(0, 8);
                } while (mov1[move] != 0);
                loci = loci + KTMOVi[move];
                locj = locj + KTMOVj[move];
                board[loci, locj] = order++;
                return true;
            }

        }
        //moves the knight to the position with least number of exits 
        public bool moveK(int[,] board)
        {
            int low = 999;
            int lowind = 999;

            // finds the lowest non-zero value in the array mov1 and 
            for (int i = 0; i < 8; i++)
            {
                if ((mov1[i] > 1) && (mov1[i] <= low))
                {
                    lowind = i;
                    low = mov1[i];
                }
            }
            //ensures that we travel to the final open position
            if(lowind==999)
            {
                for(int i = 0; i< 8; i++)
                {
                    if(mov1[i]==1)
                    {
                        lowind = i;
                    }
                }
            }

            
            // if there are no possible movement options left, return false, exiting the loop
            if(lowind ==999)   
            {
                return false;
            }

            //moves the knight, incrementing order to put into the next position
            loci = loci + KTMOVi[lowind];
            locj = locj + KTMOVj[lowind];
            board[loci, locj] = order;
            order++;
            
            //resets the mov1 array
            for (int i = 0; i< 8; i++)
            {
                mov1[i] = 0;
            }
            return true;
        }
        ~Knight() { }
    }
}
