using System;
using Chess_Console;
using Chessboard;
using Chess;
using Chessboard.Exceptions;

try
{
    ChessMatch match = new ChessMatch();  

    Screen.WriteBoard(match.Board);

}
catch (ChessboardException e)
{
    Console.WriteLine(e.Message);
}