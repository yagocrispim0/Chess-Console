using System;
using Chessboard;
using Chess;

namespace Chess_Console
{
    internal class Screen
    {

        // Printing the board on the console.
        public static void WriteBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    
                    if (board.Piece(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Screen.PrintPiece(board.Piece(i,j));
                        Console.Write(" ");
                    }
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");


            }

        // Reads a user input to set the position of the next movement. 

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }
        // Modify color of pieces. 
        public static void PrintPiece(Piece piece)
        {
            if (piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
