namespace Chess
{
    public class ValidMove
    {
        public int Index { get; set; }
        public bool IsCapture { get; set; }

        public ValidMove(int index, bool isCapture)
        {
            Index = index;
            IsCapture = isCapture;
        }
    }

    public class MoveMetaData
    {
        public int StartIndex { get; set; }
        public bool IsCapture { get; set; }
        public List<ValidMove> ValidMoves { get; set; } // List of squares the piece can move to. The index

        public int? EndIndex { get; set; }
        public string? Notation { get; set; }

        public MoveMetaData(Game game, int start) // Generate a list of moves.
        {
            StartIndex = start;
            IsCapture = false;

            List<ValidMove> validMoves = new List<ValidMove>();
            // 1. Get the unfiltered list of squares the piece can moved to based purely on how the piece can move.
            //a

            ValidMoves = validMoves;
        }

        /*public MoveMetaData(Game game, int start, int end) // Execute the move.*/
        /*{*/
        /*    StartIndex = start;*/
        /*    EndIndex = end;*/
        /**/
        /*    // 1.*/
        /**/
        /**/
        /*    Notation = ""; // TODO: build the notation string of the move*/
        /*}*/
    }

    public class Move
    {
        public string[] GenerateValidMoves(Game game)
        {

            return [""];
        }
    }
}
