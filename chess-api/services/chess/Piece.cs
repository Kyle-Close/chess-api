namespace Chess
{
    public abstract class Piece
    {
        // ----- Properties -----
        public abstract PieceType PieceType { get; }

        public Color Color { get; }
        public bool HasMoved { get; set; }
        public int Index { get; set; }
        public List<MoveMetaData> ValidMoves { get; set; }

        // ----- Methods -----
        public abstract List<MoveMetaData> GetStandardMoves(Game game);
        public abstract char GetPieceChar();

        // ----- Constructors -----
        public Piece()
        {
            Index = 0;
            Color = Color.BLACK;
            HasMoved = false;
            ValidMoves = new List<MoveMetaData>();
        }
        public Piece(int posIndex, Color color)
        {
            Index = posIndex;
            Color = color;
            HasMoved = false;
            ValidMoves = new List<MoveMetaData>();
        }
        public Piece(int posIndex, Color color, bool hasMoved)
        {
            Index = posIndex;
            Color = color;
            HasMoved = hasMoved;
            ValidMoves = new List<MoveMetaData>();
        }
        public Piece(int posIndex, Color color, bool hasMoved, List<MoveMetaData> validMoves)
        {
            Index = posIndex;
            Color = color;
            HasMoved = hasMoved;
            ValidMoves = validMoves;
        }

        // Returns whether the passed in piece is being attacked by opponent.
        public static bool IsPieceBeingAttacked(Board board, int index)
        {
            if (!Board.IsValidSquareIndex(index))
            {
                throw new Exception("Passed invalid index to IsPieceBeingAttacked");
            }

            var piece = board.Squares[index].Piece;
            if (piece == null)
            {
                throw new Exception("There is no piece on this square.");
            }

            var scanner = new BoardScanner(board);
            var opponentColor = piece.Color == Color.WHITE ? Color.BLACK : Color.WHITE;

            // 1. Check for pawn attacks.
            var opponentPawns = board.GetPieces<Pawn>(opponentColor);
            var attackedIndexes = new List<int>();

            foreach (var pawn in opponentPawns)
            {
                var indexes = pawn.GetAttackIndexes(board);
                attackedIndexes.AddRange(indexes);
            }

            // 2. Check for knight attacks
            var opponentKnights = board.GetPieces<Knight>(opponentColor);

            foreach (var knight in opponentKnights)
            {
                var indexes = knight.GetUnfilteredMoveIndexes(knight.Index);
                attackedIndexes.AddRange(indexes);
            }

            // 3. Check for horizonal/vertical attacks
            var opponentRooks = board.GetPieces<Rook>(opponentColor);
            var opponentQueen = board.GetPieces<Queen>(opponentColor);

            foreach (var rook in opponentRooks)
            {
                var validMoves = scanner.EvaluateSlidingPieceMove(rook.Index, opponentColor);
                foreach (var move in validMoves)
                {
                    if (move.IsCapture)
                    {
                        attackedIndexes.Add(move.EndIndex);
                    }
                }
            }

            if (opponentQueen.Count > 0)
            {
                var validMoves = scanner.EvaluateSlidingPieceMove(opponentQueen[0].Index, opponentColor);
                foreach (var move in validMoves)
                {
                    if (move.IsCapture)
                    {
                        attackedIndexes.Add(move.EndIndex);
                    }
                }
            }

            // 4. Check for diagonal attacks
            var opponentBishops = board.GetPieces<Bishop>(opponentColor);

            foreach (var bishop in opponentBishops)
            {
                var validMoves = scanner.EvaluateDiagonalPieceMove(bishop.Index, opponentColor);
                foreach (var move in validMoves)
                {
                    if (move.IsCapture)
                    {
                        attackedIndexes.Add(move.EndIndex);
                    }
                }
            }

            if (opponentQueen.Count > 0)
            {
                var validMoves = scanner.EvaluateDiagonalPieceMove(opponentQueen[0].Index, opponentColor);
                foreach (var move in validMoves)
                {
                    if (move.IsCapture)
                    {
                        attackedIndexes.Add(move.EndIndex);
                    }
                }
            }

            // 5. Check for king attacks
            var opponentKing = board.GetPieces<King>(opponentColor);
            if (opponentKing.Count > 0)
            {
                var moves = scanner.EvaluateSurroundingPieceMove(opponentKing[0].Index, opponentColor);
                foreach (var move in moves)
                {
                    if (move.IsCapture)
                    {
                        attackedIndexes.Add(move.EndIndex);
                    }
                }
            }

            return attackedIndexes.Contains(index);
        }

        // ----- Methods -----
        public void UpdateValidMoves(Game game)
        {
            // 1. Get the unfiltered list of squares the piece can moved to based purely on how the piece can move/attack.
            List<MoveMetaData> validMoves = GetStandardMoves(game);

            // 2. Add en-passant captures (if applicable)
            if (this is Pawn pawn)
            {
                if (game.EnPassantIndex != null && pawn.IsAttackingEnPassantSquare(game.EnPassantIndex, game.Board))
                {
                    int index = game.EnPassantIndex.Value;
                    validMoves.Add(new ValidMove(pawn.Index, index, true));
                }
            }

            // 3. Add castle moves (if applicable)
            if (this is King king)
            {
                if (Color == Color.BLACK)
                {
                    if (game.BlackCastleRights.QueenSide && CastleRights.IsCastlePathClear(game.Board, CastlePaths.BLACK_QUEEN_SIDE))
                    {
                        validMoves.Add(new ValidMove(king.Index, 2, false));
                    }
                    if (game.BlackCastleRights.KingSide && CastleRights.IsCastlePathClear(game.Board, CastlePaths.BLACK_KING_SIDE))
                    {
                        validMoves.Add(new ValidMove(king.Index, 6, false));
                    }
                }
                else
                {
                    if (game.WhiteCastleRights.QueenSide && CastleRights.IsCastlePathClear(game.Board, CastlePaths.WHITE_QUEEN_SIDE))
                    {
                        validMoves.Add(new ValidMove(king.Index, 58, false));
                    }
                    if (game.WhiteCastleRights.KingSide && CastleRights.IsCastlePathClear(game.Board, CastlePaths.WHITE_KING_SIDE))
                    {
                        validMoves.Add(new ValidMove(king.Index, 62, false));
                    }
                }
            }

            // 4. Filter out any moves that would put player in check.
            validMoves = validMoves.Where(move =>
            {
                var newBoard = new Board(game.Board.BuildFen());
                newBoard.MovePiece(move.StartIndex, move.EndIndex);

                // If the new board state is missing a king, then this move is invalid.
                bool hasKing = newBoard.GetPieces<King>(Color.WHITE).Count > 0 && newBoard.GetPieces<King>(Color.BLACK).Count > 0;
                if (!hasKing) return false;

                var isCheckResults = newBoard.IsCheck();

                if (game.ActiveColor == Color.WHITE)
                    return !isCheckResults.WhiteInCheck;

                return !isCheckResults.BlackInCheck;
            }).ToList();

            ValidMoves = validMoves;
        }

        public static Piece ConvertCharToPiece(char letter, int index)
        {
            switch (letter)
            {
                case 'P':
                    return new Pawn(index, Color.WHITE);
                case 'p':
                    return new Pawn(index, Color.BLACK);
                case 'R':
                    return new Rook(index, Color.WHITE);
                case 'r':
                    return new Rook(index, Color.BLACK);
                case 'B':
                    return new Bishop(index, Color.WHITE);
                case 'b':
                    return new Bishop(index, Color.BLACK);
                case 'N':
                    return new Knight(index, Color.WHITE);
                case 'n':
                    return new Knight(index, Color.BLACK);
                case 'Q':
                    return new Queen(index, Color.WHITE);
                case 'q':
                    return new Queen(index, Color.BLACK);
                case 'K':
                    return new King(index, Color.WHITE);
                case 'k':
                    return new King(index, Color.BLACK);
                default:
                    throw new Exception($"Character {letter} is not a valid piece.");
            }
        }
    }
}
