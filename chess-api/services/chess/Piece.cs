namespace Chess
{
    public abstract class Piece
    {
        // ----- Properties -----
        public abstract PieceType PieceType { get; }

        public Color Color { get; }
        public bool HasMoved { get; set; }
        public int Index { get; set; }
        public List<ValidMove> ValidMoves { get; set; }

        // ----- Methods -----
        public abstract List<ValidMove> GetStandardMoves(Game game);
        public abstract char GetPieceChar();

        // ----- Constructors -----
        public Piece(int posIndex, Color color)
        {
            Index = posIndex;
            Color = color;
            HasMoved = false;
            ValidMoves = new List<ValidMove>();
        }
        public Piece(int posIndex, Color color, bool hasMoved)
        {
            Index = posIndex;
            Color = color;
            HasMoved = hasMoved;
            ValidMoves = new List<ValidMove>();
        }
        public Piece(int posIndex, Color color, bool hasMoved, List<ValidMove> validMoves)
        {
            Index = posIndex;
            Color = color;
            HasMoved = hasMoved;
            ValidMoves = validMoves;
        }

        // ----- Methods -----
        public void UpdateValidMoves(Game game)
        {
            // 1. Get the unfiltered list of squares the piece can moved to based purely on how the piece can move.
            List<ValidMove> validMoves = GetStandardMoves(game);
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
