namespace Chess
{
    public abstract class Piece
    {
        // ----- Properties -----
        public abstract PieceType PieceType { get; }

        public Color Color { get; }
        public bool HasMoved { get; set; }
        public int PosIndex { get; set; }

        // ----- Methods -----
        public abstract List<ValidMove> GetStandardMoveIndexes(Game game);
        public abstract char GetPieceChar();

        // ----- Constructors -----
        public Piece(int posIndex, Color color)
        {
            PosIndex = posIndex;
            Color = color;
            HasMoved = false;
        }
        public Piece(int posIndex, Color color, bool hasMoved)
        {
            PosIndex = posIndex;
            Color = color;
            HasMoved = hasMoved;
        }

        // ----- Methods -----
        public static Piece ConvertCharToPiece(char letter, int index)
        {
            switch (letter)
            {
                case 'P':
                    return new Pawn(Color.WHITE, index);
                case 'p':
                    return new Pawn(Color.BLACK, index);
                default:
                    throw new Exception($"Character {letter} is not a valid piece.");
            }
        }
    }
}
