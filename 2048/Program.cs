using System;

namespace _2048
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(args[0]);
            int[][] board = new int[size][];
            for (int i = 0; i < size; i++)
            {
                board[i] = new int[size];
                for (int j = 0; j < size; j++)
                    board[i][j] = 0;
            }

            Random rng = new Random();
            bool lose = false;
            int x = rng.Next(0, size);
            int y = rng.Next(0, size);
            board[x][y] = 2;
            //for (int i = 0; i < board.Length; i++)
            //    for(int j = 0; j < board.Length; j++)
            //          board[i][j] = 2;


            while (!lose)
            {
                DrawBoard(board);
                Move(Console.ReadKey().Key, board, rng);
                Console.WriteLine();
            }
            
        }

        static void DrawBoard(int[][] board)
        {
            Console.Clear();
            for (int i = 0; i < board.Length * 5; i++)
                Console.Write("_ ");
            Console.WriteLine("\n");

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (j == 0)
                        Console.Write("|\t");
                    if (board[i][j] == 0)
                        Console.ForegroundColor = ConsoleColor.Black;
                    else if (board[i][j] == 2)
                        Console.ForegroundColor = ConsoleColor.Gray;
                    else if (board[i][j] == 4)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    else if (board[i][j] == 8)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (board[i][j] == 16)
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else if (board[i][j] == 32)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (board[i][j] == 64)
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    else if (board[i][j] == 128)
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    else if (board[i][j] == 256)
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    else if (board[i][j] == 512)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (board[i][j] == 1024)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (board[i][j] == 2048)
                        Console.ForegroundColor = ConsoleColor.Green;

                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    
                    Console.Write(board[i][j] + "\t");
                    Console.ForegroundColor = ConsoleColor.White;

                    if (j == board.Length - 1)
                        Console.Write("|");
                }
                Console.WriteLine("\n");

            }
            for (int i = 0; i < board.Length * 5; i++)
                Console.Write("_ ");
            Console.WriteLine("\n");
        }

        static void PlacePiece(int[][] board, Random rng)
        {
            int drop = rng.Next(0, 100);
            if (drop > 75)
            {
                drop = 4;
            }
            else
                drop = 2;

            while(true)
            {
                int x = rng.Next(0, board[0].Length);
                int y = rng.Next(0, board.Length);

                if(board[x][y] == 0)
                {
                    board[x][y] = drop;
                    return;
                }
            }
        }

        static void Move(ConsoleKey key, int[][] board, Random rng)
        {
            bool validMove = false;
            if (key.ToString().Equals("RightArrow"))
            {
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = board[i].Length - 2; j >= 0; j--)
                    {
                        int barrier = -1;
                        if (board[i][j] != 0)
                        {
                            int curr = j;
                            while((curr < board.Length - 1) && (board[i][curr + 1] == 0 || board[i][curr + 1] == board[i][curr]))
                            {
                                if (board[i][curr + 1] == board[i][curr] /*&& barrier < curr*/)
                                {
                                    board[i][curr + 1] += board[i][curr];
                                    board[i][curr] = 0;
                                    barrier = curr  +1;
                                    validMove = true;
                                }
                                else if(board[i][curr + 1] == 0)
                                {
                                    board[i][curr + 1] = board[i][curr];
                                    board[i][curr] = 0;
                                    validMove = true;
                                }
                                curr++;
                            }
                        }
                    }
                }
                if(validMove)
                    PlacePiece(board, rng);
            }

            else if (key.ToString().Equals("UpArrow"))
            {
                for (int i = 0; i <board.Length ; i++)
                {
                    for (int j = 0; j < board.Length; j++)
                    {
                        if (board[i][j] != 0)
                        {
                            int curr = i;
                            while ((curr > 0) && (board[curr - 1][j] == 0 || board[curr - 1][j] == board[curr][j]))
                            {
                                if (board[curr][j] == board[curr - 1][j])
                                {
                                    board[curr - 1][j] += board[curr][j];
                                    board[curr][j] = 0;
                                    validMove = true;
                                }
                                else if (board[curr - 1][j] == 0)
                                {
                                    board[curr - 1][j] = board[curr][j];
                                    board[curr][j] = 0;
                                    validMove = true;
                                }
                                curr--;
                            }
                        }
                    }
                }
                if(validMove)
                   PlacePiece(board, rng);
            }
            else if (key.ToString().Equals("DownArrow"))
            {
                for (int i = board.Length - 1; i >= 0; i--)
                {
                    for (int j = 0; j < board.Length; j++)
                    {
                        if (board[i][j] != 0)
                        {
                            int curr = i;
                            while ((curr < board.Length - 1) && (board[curr + 1][j] == 0 || board[curr + 1][j] == board[curr][j]))
                            {
                                if (board[curr][j] == board[curr + 1][j])
                                {
                                    board[curr + 1][j] += board[curr][j];
                                    board[curr][j] = 0;
                                    validMove = true;
                                }
                                else if (board[curr + 1][j] == 0)
                                {
                                    board[curr + 1][j] = board[curr][j];
                                    board[curr][j] = 0;
                                    validMove = true;
                                }
                                curr++;
                            }
                        }
                    }
                }
                if (validMove)
                    PlacePiece(board, rng);
            }
            else if (key.ToString().Equals("LeftArrow"))
            {
                for (int i = 0; i < board.Length; i++)
                {
                    int barrier = int.MaxValue;
                    for (int j = 0; j < board.Length; j++)
                    {
                        
                        if (board[i][j] != 0)
                        {
                            int curr = j;
                            while ((curr > 0) && (board[i][curr - 1] == 0 || board[i][curr - 1] == board[i][curr]))
                            {
                                if (board[i][curr - 1] == board[i][curr] /*&& barrier > curr*/)
                                {
                                    validMove = true;
                                    board[i][curr - 1] += board[i][curr];
                                    board[i][curr] = 0;
                                    barrier = curr;
                                }
                                else if (board[i][curr - 1] == 0)
                                {
                                    validMove = true;
                                    board[i][curr - 1] = board[i][curr];
                                    board[i][curr] = 0;
                                }
                                curr--;
                            }
                        }
                    }
                }
                if(validMove)
                    PlacePiece(board, rng);
            }
        }
    }
}
