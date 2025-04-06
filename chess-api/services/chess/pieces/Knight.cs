namespace Chess
{
    public class Knight : Piece
    {
        public override PieceType PieceType { get; }

        public Knight(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.KNIGHT;
        }

        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'N' : 'n';
        }

        // Gets the standard knight moves that are possible.
        // considers being blocked and ensuring not to go off board.
        // Returns valid open squares and squares to capture on
        public override List<ValidMove> GetStandardMoves(Game game)
        {
            var unfiltered = GetUnfilteredMoveIndexes(Index);
            var result = new List<ValidMove>();

            foreach (var index in unfiltered)
            {
                var piece = game.Board.Squares[index].Piece;
                if (piece == null)
                {
                    result.Add(new ValidMove(Index, index, false));
                }
                else if (piece.Color != Color)
                {
                    result.Add(new ValidMove(Index, index, true));
                }
            }

            return result;
        }

        // Does not consider if the square is already occupied.
        // It will get only valid moves. Cross-board moves are considered and prevented.
        public List<int> GetUnfilteredMoveIndexes(int index)
        {
            const int NORTH_LEFT = -17;
            const int NORTH_RIGHT = -15;

            const int EAST_TOP = -6;
            const int EAST_BOTTOM = 10;

            const int SOUTH_RIGHT = 17;
            const int SOUTH_LEFT = 15;

            const int WEST_BOTTOM = 6;
            const int WEST_TOP = -10;

            // --- Special cases ---
            //
            // A file: exclude WEST & NORTH_LEFT & SOUTH_LEFT
            // B file: exclude WEST
            // G file: exclude EAST
            // H file: exclude EAST & NORTH_RIGHT & SOUTH_RIGHT
            //
            // 1st rank: exclude SOUTH & EAST_BOTTOM & WEST_BOTTOM
            // 2nd rank: exclude SOUTH
            // 7th rank: exclude NORTH
            // 8th rank: exclude NORTH & EAST_TOP & WEST_TOP

            var res = new List<int>() { NORTH_LEFT, NORTH_RIGHT, EAST_TOP, EAST_BOTTOM, SOUTH_RIGHT, SOUTH_LEFT, WEST_BOTTOM, WEST_TOP };
            var file = Square.GetFile(index);
            var rank = Square.GetRank(index);

            if (file == BoardFile.A)
            {
                res.Remove(WEST_TOP);
                res.Remove(WEST_BOTTOM);
                res.Remove(NORTH_LEFT);
                res.Remove(SOUTH_LEFT);
            }
            else if (file == BoardFile.B)
            {
                res.Remove(WEST_TOP);
                res.Remove(WEST_BOTTOM);
            }
            else if (file == BoardFile.G)
            {
                res.Remove(EAST_TOP);
                res.Remove(EAST_BOTTOM);
            }
            else if (file == BoardFile.H)
            {
                res.Remove(EAST_TOP);
                res.Remove(EAST_BOTTOM);
                res.Remove(NORTH_RIGHT);
                res.Remove(SOUTH_RIGHT);
            }

            if (rank == BoardRank.ONE)
            {
                res.Remove(SOUTH_LEFT);
                res.Remove(SOUTH_RIGHT);
                res.Remove(EAST_BOTTOM);
                res.Remove(WEST_BOTTOM);
            }
            else if (rank == BoardRank.TWO)
            {
                res.Remove(SOUTH_LEFT);
                res.Remove(SOUTH_RIGHT);
            }
            else if (rank == BoardRank.SEVEN)
            {
                res.Remove(NORTH_LEFT);
                res.Remove(NORTH_RIGHT);
            }
            else if (rank == BoardRank.EIGHT)
            {
                res.Remove(NORTH_LEFT);
                res.Remove(NORTH_RIGHT);
                res.Remove(EAST_TOP);
                res.Remove(WEST_TOP);
            }

            res = res.Select(num => index + num).ToList();

            return res;
        }
    }
}
