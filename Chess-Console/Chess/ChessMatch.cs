using System;
using System.Collections.Generic;
using Chess;
using Chessboard;
using Chessboard.Exceptions;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        public bool Check { get; private set; }
        private HashSet<Piece> PiecesCollection;
        private HashSet<Piece> CapturedCollection;

        // Initialize board
        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            PiecesCollection = new HashSet<Piece>();
            CapturedCollection = new HashSet<Piece>();
            PutPieces();

        }

        // Executes a movement in the game;
        public Piece ExecuteMovement(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovement();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.InsertPiece(p, destination);
            if (capturedPiece != null)
            {
                CapturedCollection.Add(capturedPiece);
            }
            return capturedPiece;
        }

        // Undo a movement in the game;
        public void UndoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destination);
            p.DecreaseMovement();
            if (capturedPiece != null)
            {
                Board.InsertPiece(capturedPiece, destination);
                CapturedCollection.Remove(capturedPiece);
            }
            Board.InsertPiece(p, origin);
        }

        // Perform a full play, including movement.
        public void PerformPlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMovement(origin, destination);
            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new ChessboardException("You can't move into check!");
            }

            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsCheckMate(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                SwitchPlayer();
            }
        }

        // Validates origin position
        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new ChessboardException("The square is empty.");
            }
            if (CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new ChessboardException("The choosen piece is not yours.");
            }
            if (!Board.Piece(pos).IsTherePossibleMoviment())
            {
                throw new ChessboardException("There are no moves available for the chosen piece.");
            }
        }

        // Validates destination position
        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveTo(destination))
            {
                throw new ChessboardException("Invalid destination position.");
            }

        }

        // Identifies opponent
        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        // CHECK!

        private Piece King (Color color)
        {
            foreach (Piece x in PiecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece K = King(color);
            if (K == null)
            {
                throw new ChessboardException("There isn't a king of this color on the board");
            }

            foreach (Piece x in PiecesInGame(Opponent(color)))
            {
                bool[,] matrix = x.PossibleMovements();
                if (matrix[K.Position.Line, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        // CHECKMATE!

        public bool IsCheckMate(Color color)
        {
            foreach (Piece x in PiecesInGame(color))
            {
                bool[,] matrix = x.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (matrix[i, j])
                        {
                            Position origin = x.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, destination);
                            bool checkTest = IsInCheck(color);
                            UndoMovement(origin, destination, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }  
            }
            return true;
        }
        // Switches players
        private void SwitchPlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        // Returns all the captured pieces filtered by color
        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in CapturedCollection)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        // Returns all the pieces in the game
        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in PiecesCollection)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        // Method that inserts a new piece into the game
        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            PiecesCollection.Add(piece);
        }

        // Aux method to insert pieces
        private void PutPieces()
        {
            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            InsertNewPiece('b', 1, new Knight(Board, Color.White));
            InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White));
            InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            InsertNewPiece('g', 1, new Knight(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));
            InsertNewPiece('a', 2, new Pawn(Board, Color.White));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White));



            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('b', 8, new Knight(Board, Color.Black));
            InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black));
            InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('g', 8, new Knight(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            InsertNewPiece('a', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black));

        }
    }
}
