//The Knight's Tour is a chessboard challenge to move a knight to all spaces on the board without visiting a square twice.
//1)	Place a knight in the corner of a chessboard.
//2)	Mark the starting square as "visited."
//3)	Move to another square and mark it.
//4)	Continue moving until you have visited all squares on the board exactly once.

//Here is the algorithm:
//1)	Visit square(0,0) and mark it as "visited." Theoretically, a piece can start on any square, but most squares require immense amounts of time to solve.
//2)	Choose the first of the possible legal moves.
//3)	If there are no legal moves, then move backward and recursively try the next legal move.
//4)	Repeat #3 until either a solution is found or until all possible moves have been attempted.


using Microsoft.VisualBasic;
using System.Collections;

class Program
{
    static int BoardSize = 8;
    static int attemptedMoves = 0;
    /*xMove[] and yMove[] define next move of Knight.
     * xMove[] is for the next value of x coordinate.
     * yMove[] is for tht next value of y coordinate.*/
    static int[] xMove = { 2, 1, -1, -2, -2, -1, 1, 2 };
    static int[] yMove = { 1, 2, 2, 1, -1, -2, -2, -1 };

    //boardGrid is an 8x8 array that contains -1 for an unvisited square of a move number between 0-63.
    static int[,] boardGrid = new int[BoardSize, BoardSize];

    static void Main(string[] args)
    {
        solveKnightsTour();
    }

    /* 
     * Funtion to solve the Knight's Tour using backtracking.
     */
    private static void solveKnightsTour()
    {
        //Initialization of solution matrix. Value of -1 means "not visited yet"
        for (int x = 0; x < BoardSize; x++)
        {
            for (int y = 0; y < BoardSize; y++)
            {
                boardGrid[x, y] = -1;
            }
        }
        int startX = 7;
        int startY = 0;
        //set starting position for the knight
        boardGrid[startX, startY] = 0;

        //count the total number of guesses
        attemptedMoves = 0;

        //explore all tours using solveKnightsTourUtil()
        if (!solveKnightsTourUtil(startX, startY, 1))
        {
            Console.WriteLine($"Solution does not exit for {startX} {startY}");
        }
        else
        {
            printSolution(boardGrid);
            Console.WriteLine($"Total attempted moves {attemptedMoves}");
        }
    }

    //utility function to print solution matrix
    private static void printSolution(int[,] solution)
    {
        for (int x = 0; x < BoardSize; x++)
        {
            for (int y = 0; y < BoardSize; y++)
            {
                Console.Write(solution[x, y] + " ");
            }
            Console.WriteLine();
        }
    }

    //recursive utility function to solve the problem
    private static bool solveKnightsTourUtil(int startX, int startY, int moveCount)
    {
        attemptedMoves++;
        if (attemptedMoves % 1_000_000 == 0)
        {
            Console.WriteLine($"Attempts: {attemptedMoves}");
        }
        int next_x, next_y;

        //check to see if we have a solution
        if (moveCount == BoardSize * BoardSize) return true;

        //array stores number of visited neighbors and the corrdinates where it is at
        int[][] visitedNeighbors = new int[8][];

        //try all next moves from the current coordinate
        for (int i = 0; i < visitedNeighbors.Length; i++)
        {
            next_x = startX + xMove[i];
            next_y = startY + yMove[i];
            if (isSquareSafe(next_x, next_y))
            {    //add the number of visited neighbors to the array and then the coordinates
                visitedNeighbors[i] = new int[3] { CountVisitedNeighbors(next_x, next_y), next_x, next_y };
                
            }
            ////else number of that location is -1 which will be removed later
            ///maybe we can leave the index as null
            //else visitedNeighbors[i][0] = -1;
        }
        //remove nulls
        visitedNeighbors = visitedNeighbors.Where(v => v != null).ToArray();

        //sort the array by visited neighbors
        //got this from https://stackoverflow.com/questions/36746218/sorting-jagged-array
        Array.Sort(visitedNeighbors, (x, y) => y[0].CompareTo(x[0]));

        for (int i = 0; i < visitedNeighbors.Length; i++)
        {
            if (visitedNeighbors[i] != null)
            {
                boardGrid[visitedNeighbors[i][1], visitedNeighbors[i][2]] = moveCount;
                if (solveKnightsTourUtil(visitedNeighbors[i][1], visitedNeighbors[i][2], moveCount + 1)) return true;
                else
                    //backtrack, reset the square's move count to -1 which means unvisited
                    boardGrid[visitedNeighbors[i][1], visitedNeighbors[i][2]] = -1;
            }
        }
        
        return false;
    }

    //utility function to check if next_x, next_y are valid indexes for N*N chessboard
    //and checks to make sure that the square has not been visited (== -1)
    private static bool isSquareSafe(int next_x, int next_y)
    {
        return (next_x >= 0 && next_x < BoardSize &&
                next_y >= 0 && next_y < BoardSize &&
                boardGrid[next_x,next_y] == -1);
    }

    //Warnsdorff's addition: helps to choose next move that has the least number of unvisited 
    //arguments are the coordinates of the next possible moves
    private static int CountVisitedNeighbors(int x, int y)
    {
        int countVisitedNeighbors = 0;
        for (int i = y - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (isSquareSafe(i, j))
                {
                    if (boardGrid[i, j] != -1) countVisitedNeighbors++; //keep track of visited neighbors
                }
            }
        }
        return countVisitedNeighbors;
    }
}