namespace Chess
{

    public enum PawnBlockStatus
    {
        NOT_BLOCKED,
        BLOCKED_ONE_RANK_AHEAD,
        BLOCKED_TWO_RANKS_AHEAD
    }

    public class Pawn : Piece
    {
        // ----- Properties -----
        public override PieceType PieceType { get; }

        // ----- Constructor -----
        public Pawn(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.PAWN;
        }

        // ----- Methods -----
        public bool IsAttackingEnPassantSquare(int? enPassantIndex, Board board)
        {
            if (enPassantIndex == null)
            {
                return false;
            }

            int index = enPassantIndex.Value;

            if (!Board.IsValidSquareIndex(index))
            {
                throw new Exception("Sent an invalid en passant target square");
            }

            var rank = Square.GetRank(index);
            if (rank != BoardRank.THREE && rank != BoardRank.SIX)
            {
                throw new Exception("En-passant target square should be on the 3rd or 6th rank only.");
            }

            var targetSquares = GetAttackIndexes(board);
            if (targetSquares.Contains(index))
            {
                return true;
            }

            return false;
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'P' : 'p';
        }

        // Gets the standard pawn moves that are possible. Does not consider en-passant.
        // 2 square moves, single square moves, capture (left & right) are considered.
        public override List<MoveMetaData> GetStandardMoves(Game game)
        {
            var piece = Board.ValidatePieceOnSquare<Pawn>(game.Board, Index);
            var moveIndexList = new List<MoveMetaData>();
            var blockStatus = IsBlocked(game.Board);

            if (blockStatus == PawnBlockStatus.NOT_BLOCKED)
            {
                if (!piece.HasMoved)
                {
                    if (piece.Color == Color.WHITE)
                    {
                        moveIndexList.Add(new MoveMetaData(Index, Index - 8));
                        moveIndexList.Add(new MoveMetaData(Index, Index - 16));
                    }
                    else
                    {
                        moveIndexList.Add(new MoveMetaData(Index, Index + 8));
                        moveIndexList.Add(new MoveMetaData(Index, Index + 16));
                    }
                }
                else
                {
                    if (piece.Color == Color.WHITE)
                        moveIndexList.Add(new MoveMetaData(Index, Index - 8));
                    else
                        moveIndexList.Add(new MoveMetaData(Index, Index + 8));
                }
            }
            else if (blockStatus == PawnBlockStatus.BLOCKED_TWO_RANKS_AHEAD)
            {
                if (piece.Color == Color.WHITE)
                    moveIndexList.Add(new MoveMetaData(Index, Index - 8));
                else
                    moveIndexList.Add(new MoveMetaData(Index, Index + 8));
            }

            // Get pawn capture info
            var attackIndexes = GetAttackIndexes(game.Board);
            foreach (var idx in attackIndexes)
            {
                Color enemyColor = piece.Color == Color.WHITE ? Color.BLACK : Color.WHITE;
                var isTargetIndex = game.IsEnemySquare(idx, enemyColor);
                if (isTargetIndex)
                {
                    moveIndexList.Add(new MoveMetaData(Index, idx, true));
                }
                ;
            }

            return moveIndexList;
        }

        // Returns a list of indexes that the pawn is actively attacking.
        // Only considered attacking if there is an enemy piece on that square.
        public List<int> GetAttackIndexes(Board board)
        {
            if (!Board.IsValidSquareIndex(Index))
            {
                throw new Exception("Cannot get pawn attacking indexes. Given index off the board.");
            }

            var pawn = Board.ValidatePieceOnSquare<Pawn>(board, Index);
            var file = Square.GetFile(Index);

            List<int> result = new List<int>();

            if (file == BoardFile.A)
            {
                if (pawn.Color == Color.WHITE)
                {
                    result.Add(Index - 7);
                    return result;
                }
                else
                {
                    result.Add(Index + 9);
                    return result;
                }
            }
            else if (file == BoardFile.H)
            {
                if (pawn.Color == Color.WHITE)
                {
                    result.Add(Index - 9);
                    return result;
                }
                else
                {
                    result.Add(Index + 7);
                    return result;
                }
            }

            // Getting here means we are in a center file and have 2 attack squares.
            if (pawn.Color == Color.WHITE)
            {
                result.Add(Index - 7); // Right
                result.Add(Index - 9); // Left
                return result;
            }
            else
            {
                result.Add(Index + 7); // Left
                result.Add(Index + 9); // Right
                return result;
            }
        }

        public PawnBlockStatus IsBlocked(Board board)
        {
            var piece = Board.ValidatePieceOnSquare<Pawn>(board, Index);

            int adder = piece.Color == Color.WHITE ? -8 : 8;
            int oneAhead = Index + adder;
            int twoAhead = oneAhead + adder;

            if (!Board.IsValidSquareIndex(oneAhead))
            {
                throw new Exception("Cannot check next pawn square as it's off the board.");
            }

            // Check square 1 rank ahead
            if (board.Squares[oneAhead].Piece != null)
            {
                return PawnBlockStatus.BLOCKED_ONE_RANK_AHEAD;
            }

            // Check square 2 ranks ahead
            if (Board.IsValidSquareIndex(twoAhead) && board.Squares[twoAhead].Piece != null)
            {
                return PawnBlockStatus.BLOCKED_TWO_RANKS_AHEAD;
            }

            return PawnBlockStatus.NOT_BLOCKED;
        }

        public bool IsInStartPosition()
        {
            // Assume we are sending the index of a pawn. No check here.
            BoardRank rank = Square.GetRank(Index);
            bool isWhite = Color == Color.WHITE;

            if (isWhite)
            {
                return rank == BoardRank.TWO ? true : false;
            }
            else
            {
                return rank == BoardRank.SEVEN ? true : false;
            }
        }
    }
}
