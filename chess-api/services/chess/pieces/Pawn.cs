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
        public Pawn(Color color, bool hasMoved)
          : base(PieceType.PAWN, color, hasMoved)
        {
        }

        // Gets the standard pawn moves that are possible. Does not consider en-passant.
        // 2 square moves, single square moves, capture (left & right) are considered.
        public static List<ValidMove> GetStandardMoveIndexes(Game game, int index)
        {
            var piece = Board.ValidatePieceOnSquare(game.Board, index, PieceType.PAWN);
            var moveIndexList = new List<ValidMove>();
            var blockStatus = IsBlocked(game.Board, index);

            if (blockStatus == PawnBlockStatus.NOT_BLOCKED)
            {
                if (!piece.HasMoved)
                {
                    if (piece.Color == Color.WHITE)
                    {
                        moveIndexList.Add(new ValidMove(index - 8, false));
                        moveIndexList.Add(new ValidMove(index - 16, false));
                    }
                    else
                    {
                        moveIndexList.Add(new ValidMove(index + 8, false));
                        moveIndexList.Add(new ValidMove(index + 16, false));
                    }
                }
                else
                {
                    if (piece.Color == Color.WHITE)
                        moveIndexList.Add(new ValidMove(index - 8, false));
                    else
                        moveIndexList.Add(new ValidMove(index + 8, false));
                }
            }
            else if (blockStatus == PawnBlockStatus.BLOCKED_TWO_RANKS_AHEAD)
            {
                if (piece.Color == Color.WHITE)
                    moveIndexList.Add(new ValidMove(index - 8, false));
                else
                    moveIndexList.Add(new ValidMove(index + 8, false));
            }

            // Get pawn capture info
            var attackIndexes = GetAttackIndexes(game.Board, index);
            foreach (var idx in attackIndexes)
            {
                Color enemyColor = game.ActiveColor == Color.WHITE ? Color.BLACK : Color.WHITE;
                var isTargetIndex = game.IsEnemySquare(idx, enemyColor);
                if (isTargetIndex)
                {
                    moveIndexList.Add(new ValidMove(idx, true));
                }
            }

            return moveIndexList;
        }

        // Returns a list of indexes that the pawn is actively attacking.
        // Only considered attacking if there is an enemy piece on that square.
        public static List<int> GetAttackIndexes(Board board, int index)
        {
            if (!Board.IsValidSquareIndex(index))
            {
                throw new Exception("Cannot get pawn attacking indexes. Given index off the board.");
            }

            var pawn = Board.ValidatePieceOnSquare(board, index, PieceType.PAWN);
            var file = Square.GetFile(index);

            List<int> result = new List<int>();

            if (file == BoardFile.A)
            {
                if (pawn.Color == Color.WHITE)
                {
                    result.Add(index - 7);
                    return result;
                }
                else
                {
                    result.Add(index + 9);
                    return result;
                }
            }
            else if (file == BoardFile.H)
            {
                if (pawn.Color == Color.WHITE)
                {
                    result.Add(index - 9);
                    return result;
                }
                else
                {
                    result.Add(index + 7);
                    return result;
                }
            }

            // Getting here means we are in a center file and have 2 attack squares.
            if (pawn.Color == Color.WHITE)
            {
                result.Add(index - 7); // Right
                result.Add(index - 9); // Left
                return result;
            }
            else
            {
                result.Add(index + 7); // Left
                result.Add(index + 9); // Right
                return result;
            }
        }

        public static PawnBlockStatus IsBlocked(Board board, int pawnIndex)
        {
            var piece = Board.ValidatePieceOnSquare(board, pawnIndex, PieceType.PAWN);

            int adder = piece.Color == Color.WHITE ? -8 : 8;
            int oneAhead = pawnIndex + adder;
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

        public static bool IsInStartPosition(int index, bool isWhite)
        {
            // Assume we are sending the index of a pawn. No check here.
            BoardRank rank = Square.GetRank(index);

            if (isWhite)
            {
                if (rank == BoardRank.TWO)
                    return false;

                return false;
            }
            else
            {
                if (rank == BoardRank.SEVEN)
                    return false;

                return true;
            }

        }
    }
}
