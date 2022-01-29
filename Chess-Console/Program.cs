using System;
using Chess_Console;
using Chessboard;
using Chess;
using Chessboard.Exceptions;
using Chess.Exceptions;

try
{
    ChessMatch match = new ChessMatch();

    while (!match.Finished)
    {
        try
        {
            // Showing the board
            Console.Clear();
            Screen.WriteMatch(match);

            // Origin movement
            Console.WriteLine();
            Console.Write("Origin: ");
            try
            {
                Position origin = Screen.ReadChessPosition().ToPosition();
                match.ValidateOriginPosition(origin);


                // Show possible positions
                bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();
                Console.Clear();
                Screen.WriteBoard(match.Board, possiblePositions);

                // Destination movement
                Console.WriteLine();
                Console.Write("Destination: ");
                Position destination = Screen.ReadChessPosition().ToPosition();
                match.ValidateDestinationPosition(origin, destination);

                match.PerformPlay(origin, destination);
            }
            catch (InputException e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.WriteLine("Press 'ENTER' to continue");
                Console.ReadLine();
            }
        }
        catch (ChessboardException e)
        {
            Console.WriteLine();
            Console.WriteLine(e.Message);
            Console.WriteLine("Press 'ENTER' to continue");
            Console.ReadLine();
        }
    }
}
catch (ChessboardException e)
{
    Console.WriteLine(e.Message);
}