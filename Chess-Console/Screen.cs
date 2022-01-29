using System;
using Chessboard;
using Chess;
using Chess.Exceptions;

namespace Chess_Console
{
    internal class Screen
    {
        public static void WriteMatch(ChessMatch match)
        {
            WriteBoard(match.Board);
            Console.WriteLine();
            WriteCapturedPieces(match);
            Console.WriteLine();
            if (!match.Finished)
            {
                Console.Write("Turn: " + match.Turn + " | Current player: ");
                if (match.CurrentPlayer == Color.Black)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(match.CurrentPlayer);
                    Console.ForegroundColor = aux;
                }
                else
                {
                    Console.WriteLine(match.CurrentPlayer);
                }

                if (match.Check)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine(match.CurrentPlayer + " Wins!");
            }
           
        }

        // Printing the board on the console.
        public static void WriteBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                ConsoleColor auxColorOne = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = auxColorOne;
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }

                Console.WriteLine();
            }
            ConsoleColor auxColorTwo = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("  A B C D E F G H");
            Console.ForegroundColor = auxColorTwo;
        }

        // Prints the board with possible movements after user's origin input.
        public static void WriteBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                ConsoleColor auxColorOne = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = auxColorOne;
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = newBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            ConsoleColor auxColorTwo = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("  A B C D E F G H");
            Console.ForegroundColor = auxColorTwo;

        }

        // Outputs the match status (captured pieces)
        public static void WriteCapturedPieces(ChessMatch match)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            PrintCollection(match.CapturedPieces(Color.White));
            Console.Write("Black: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintCollection(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
        }
        public static void PrintCollection(HashSet<Piece> collection)
        {
            Console.Write("[ ");
            foreach (Piece x in collection)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine("]");
        }

        // Reads a user input to set the position of the next movement. 
        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            
            if (s.Length > 2 || s.Length < 2)
            {
                throw new InputException("Invalid input");
            }

            if (!Char.IsLetter(s,0))
            {
                throw new InputException("The first digit must be a letter!");
            }
            
            if (!Char.IsNumber(s, 1))
            {
                throw new InputException("The second digit must be a number!");
            }
            char column = s[0];
            int line = int.Parse(s[1] + "");

            if (Char.IsUpper(column))
            {
                column = Char.ToLower(column);
            }



            return new ChessPosition(column, line);
        }
        
        // Modify color of pieces. 
        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece + " ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece + " ");
                    Console.ForegroundColor = aux;
                }
            }
            
        }
    }
}
