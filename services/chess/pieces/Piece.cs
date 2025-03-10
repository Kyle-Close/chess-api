namespace Chess
{
    public class Piece
    {
        public PieceType PieceType { get; }
        public Color Color { get; }
        public bool HasMoved { get; set; }

        public Piece(PieceType pieceType, Color color, bool hasMoved)
        {
            PieceType = pieceType;
            Color = color;
            HasMoved = hasMoved;
        }

        public static char ConvertPieceTypeToChar(PieceType type, bool isWhite)
        {
            switch (type)
            {
                case PieceType.PAWN:
                    return isWhite ? 'P' : 'p';
                case PieceType.ROOK:
                    return isWhite ? 'R' : 'r';
                case PieceType.KNIGHT:
                    return isWhite ? 'N' : 'n';
                case PieceType.BISHOP:
                    return isWhite ? 'B' : 'b';
                case PieceType.QUEEN:
                    return isWhite ? 'Q' : 'q';
                case PieceType.KING:
                    return isWhite ? 'K' : 'k';

                default:
                    throw new Exception("Invalid PieceType cannot convert to char.");
            }
        }

        public static Piece ConvertCharToPiece(char letter, int index)
        {

            switch (letter)
            {
                case 'P':
                    return new Piece(PieceType.PAWN, Color.WHITE, Pawn.IsInStartPosition(index, true));
                case 'p':
                    return new Piece(PieceType.PAWN, Color.BLACK, Pawn.IsInStartPosition(index, false));
                case 'R':
                    return new Piece(PieceType.ROOK, Color.WHITE, false);
                case 'r':
                    return new Piece(PieceType.ROOK, Color.BLACK, false);
                case 'N':
                    return new Piece(PieceType.KNIGHT, Color.WHITE, false);
                case 'n':
                    return new Piece(PieceType.KNIGHT, Color.BLACK, false);
                case 'B':
                    return new Piece(PieceType.BISHOP, Color.WHITE, false);
                case 'b':
                    return new Piece(PieceType.BISHOP, Color.BLACK, false);
                case 'Q':
                    return new Piece(PieceType.QUEEN, Color.WHITE, false);
                case 'q':
                    return new Piece(PieceType.QUEEN, Color.BLACK, false);
                case 'K':
                    return new Piece(PieceType.KING, Color.WHITE, false);
                case 'k':
                    return new Piece(PieceType.KING, Color.BLACK, false);

                default:
                    throw new Exception($"Character {letter} is not a valid piece.");
            }
        }
    }
}
