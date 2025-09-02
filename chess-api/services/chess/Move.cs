namespace Chess
{
    // Contains all information about a move
    public class MoveMetaData
    {
        public int StartIndex { get; set; } // The pieces' starting square index
        public int EndIndex { get; set; }   // The pieces' new square index after move is executed

        public bool IsCapture { get; set; }
        public bool IsEnPassantCapture { get; set; }
        public bool IsCastle { get; set; }
        public bool IsPromotion { get; set; }

        public string Notation { get; set; }

        public MoveMetaData(int start, int end, bool isCapture = false, bool isEnPassantCapture = false, bool isCastle = false, bool isPromotion = false)
        {
            StartIndex = start;
            EndIndex = end;

            IsEnPassantCapture = isEnPassantCapture;
            IsCapture = isCapture;
            IsCastle = isCastle;
            IsPromotion = isPromotion;

            // TODO: Auto set the move notation Generate
            Notation = "";
        }
    }
}
