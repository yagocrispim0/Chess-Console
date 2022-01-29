using System;
using System.Collections.Generic;
using Chess;
using Chess.Exceptions;
using Chessboard;
using Chessboard.Exceptions;
using Chess.Exceptions;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }
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
            VulnerableEnPassant = null;
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

            // Castling short

            if (p is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestination = new Position(origin.Line, origin.Column + 1);
                Piece r = Board.RemovePiece(rookOrigin);
                r.IncreaseMovement();
                Board.InsertPiece(r, rookDestination);
            }

            // Castling long
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestination = new Position(origin.Line, origin.Column - 1);
                Piece r = Board.RemovePiece(rookOrigin);
                r.IncreaseMovement();
                Board.InsertPiece(r, rookDestination);
            }

            // En Passant

            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position pawnPos;
                    if (p.Color == Color.White)
                    {
                        pawnPos = new Position(destination.Line + 1, destination.Column);
                    }
                    else
                    {
                        pawnPos = new Position(destination.Line - 1, destination.Column);
                    }

                    capturedPiece = Board.RemovePiece(pawnPos);
                    CapturedCollection.Add(capturedPiece);
                }
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

            // Castling short
            if (p is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestination = new Position(origin.Line, origin.Column + 1);
                Piece r = Board.RemovePiece(rookDestination);
                r.IncreaseMovement();
                Board.InsertPiece(r, rookOrigin);
            }

            // Castling long
            if (p is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestination = new Position(origin.Line, origin.Column - 1);
                Piece r = Board.RemovePiece(rookDestination);
                r.IncreaseMovement();
                Board.InsertPiece(r, rookOrigin);
            }

            // En Passant

            if (p is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position pawnPos;
                    if (p.Color == Color.White)
                    {
                        pawnPos = new Position(3, destination.Column);
                    }
                    else
                    {
                        pawnPos = new Position(4, destination.Column);
                    }

                    Board.InsertPiece(pawn, pawnPos);
                }
            }


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
            Piece p = Board.Piece(destination);
            // Pawn promotion
            if (p is Pawn)
            {
                if ((p.Color == Color.White && destination.Line == 0) || p.Color == Color.Black && destination.Line == 7)
                {
                    try
                    {
                        Char newPiece = InputNewPiece();

                        p = Board.RemovePiece(destination);
                        PiecesCollection.Remove(p);
                        PawnPromotion(p, newPiece);
                        Piece piece = PawnPromotion(p, newPiece);
                        Board.InsertPiece(piece, destination);
                        PiecesCollection.Add(piece);
                    }catch (InputException e)
                    {
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Press 'ENTER' to continue");
                        Console.ReadLine();
                        UndoMovement(origin, destination, capturedPiece);
                    }
                }
            }

            // Testing Check
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

            // EnPassant

            if (p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
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

        private Piece King(Color color)
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

        // Pawn Promotion user input

        private static char InputNewPiece()
        {

            Console.Write("What piece would you like? ");
            string aux = Console.ReadLine();
            if (aux.Length != 1)
            {
                throw new InputException("Invalid input.");
            }
            char p = char.Parse(aux);

            if (Char.IsUpper(p))
            {
                p = Char.ToLower(p);
            }

            if (p != 'q' || p != 'n' || p != 'r' || p != 'b')
            {
                throw new InputException("Invalid piece.");
            }
            return p;
        }

        private Piece PawnPromotion(Piece p, Char newPiece)
        {
            if (newPiece == 'q')
            {
                p = new Queen(Board, p.Color);
            }
            if (newPiece == 'n')
            {
                p = new Knight(Board, p.Color);
            }
            if (newPiece == 'b')
            {
                p = new Bishop(Board, p.Color);
            }
            if (newPiece == 'r')
            {
                p = new Rook(Board, p.Color);
            }
            return p;

        }
        // Switches players
        public void SwitchPlayer()
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
            InsertNewPiece('e', 1, new King(Board, Color.White, this));
            InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            InsertNewPiece('g', 1, new Knight(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));
            InsertNewPiece('a', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White, this));



            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('b', 8, new Knight(Board, Color.Black));
            InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black, this));
            InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('g', 8, new Knight(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            InsertNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black, this));

        }
    }
}
