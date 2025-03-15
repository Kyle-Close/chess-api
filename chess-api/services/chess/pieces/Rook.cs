namespace Chess
{
    public class Rook : Piece
    {
        public override PieceType PieceType { get; }

        public Rook(int squareIndex, Color color) : base(squareIndex, color)
        {
            PieceType = PieceType.ROOK;
        }


        public override char GetPieceChar()
        {
            return Color == Color.WHITE ? 'R' : 'r';
        }

        public override List<ValidMove> GetStandardMoves(Game game)
        {
            var result = new List<ValidMove>();

            // Scan file
            var file = Square.GetFile(PosIndex);
            var rank = Square.GetRank(PosIndex);
            var scanner = new BoardScanner(game.Board);
            var scannedFile = scanner.GetFile(file);
            var index = scannedFile.FindIndex(square => square.Index == PosIndex);

            // Count up from rookSquare index to end. From lower rank to higher
            // Empty = add, enemy = capture & stop, ally = blocked & stop
            if(index < scannedFile.Count - 1)
            {
                for (int i = index + 1; i < scannedFile.Count; i++)
                {
                    var piece = scannedFile[i].Piece;
                    if (piece == null)
                    {
                        result.Add(new ValidMove(scannedFile[i].Index, false));
                    }
                    else if (piece.Color != game.ActiveColor)
                    {
                        result.Add(new ValidMove(piece.PosIndex, true));
                        break;
                    }
                    else if (piece.Color == game.ActiveColor)
                    {
                        break;
                    }
                }
            }


            // Count down from rookSquare index to end. From higher rank to lower
            // Empty = add, enemy = capture & stop, ally = blocked & stop
            if (index > 0)
            {
                for (int i = index - 1; i >= 0; i--)
                {
                    var piece = scannedFile[i].Piece;
                    if (piece == null)
                    {
                        result.Add(new ValidMove(scannedFile[i].Index, false));
                    }
                    else if (piece.Color != game.ActiveColor)
                    {
                        result.Add(new ValidMove(piece.PosIndex, true));
                        break;
                    }
                    else if (piece.Color == game.ActiveColor)
                    {
                        break;
                    }
                }
            }


            // Scan rank
            rank = Square.GetRank(PosIndex);
            var scannedRank = scanner.GetRank(rank);
            index = scannedRank.FindIndex(square => square.Index == PosIndex);


            // Count up from rookSquare index to end. From A-H direction
            // Empty = add, enemy = capture & stop, ally = blocked & stop
            if(index < scannedRank.Count - 1)
            {
                for (int i = index + 1; i < scannedRank.Count; i++)
                {
                    var piece = scannedRank[i].Piece;
                    if (piece == null)
                    {
                        result.Add(new ValidMove(scannedRank[i].Index, false));
                    }
                    else if (piece.Color != game.ActiveColor)
                    {
                        result.Add(new ValidMove(piece.PosIndex, true));
                        break;
                    }
                    else if (piece.Color == game.ActiveColor)
                    {
                        break;
                    }
                }
            }


            // Count down from rookSquare index to start. From H-A direction
            // Empty = add, enemy = capture & stop, ally = blocked & stop
            if(index > 0)
            {
                for (int i = index - 1; i >= 0; i--)
                {
                    var piece = scannedRank[i].Piece;
                    if (piece == null)
                    {
                        result.Add(new ValidMove(scannedRank[i].Index, false));
                    }
                    else if (piece.Color != game.ActiveColor)
                    {
                        result.Add(new ValidMove(piece.PosIndex, true));
                        break;
                    }
                    else if (piece.Color == game.ActiveColor)
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }
}
