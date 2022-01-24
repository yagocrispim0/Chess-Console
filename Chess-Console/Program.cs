using System;
using Chess_Console;
using Chessboard;
using Chess;
using Chessboard.Exceptions;

try
{
    ChessMatch match = new ChessMatch();  
    
    while (!match.Finished)
    {
        // Showing the board
        Console.Clear();
        Screen.WriteBoard(match.Board);

        // Origin movement
        Console.WriteLine();
        Console.Write("Origin: ");
        Position origin = Screen.ReadChessPosition().ToPosition();
        bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();

        // Show possible positions
        Console.Clear();
        Screen.WriteBoard(match.Board, possiblePositions);

        // Destination movement
        Console.WriteLine();
        Console.Write("Destination: ");
        Position destination = Screen.ReadChessPosition().ToPosition();
        match.ExecuteMovement(origin, destination);
    }


    

}
catch (ChessboardException e)
{
    Console.WriteLine(e.Message);
}