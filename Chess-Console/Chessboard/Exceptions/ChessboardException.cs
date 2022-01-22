using System;


namespace Chessboard.Exceptions
{
    internal class ChessboardException : ApplicationException 
    {
        public ChessboardException(string message) : base(message)
        {

        }
    }
}
