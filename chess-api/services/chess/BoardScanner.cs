namespace Chess
{
    public enum Diagonal
    {
        TOP_LEFT_TO_BOTTOM_RIGHT,
        BOTTOM_LEFT_TO_TOP_RIGHT
    }

    public class BoardScanner
    {
        private Board Board { get; set; }
        public BoardScanner(Board board)
        {
            Board = board;
        }
        // Returns an array of squares of whatever file you provide.
        // Reads from [1-8] (ranks)
        public List<Square> GetFile(BoardFile file)
        {
            switch (file)
            {
                case BoardFile.A:
                    var list = new List<int>() { 0, 8, 16, 24, 32, 40, 48, 56 };
                    return GetBoardSquares(list);

                case BoardFile.B:
                    list = new List<int>() { 1, 9, 17, 25, 33, 41, 49, 57 };
                    return GetBoardSquares(list);

                case BoardFile.C:
                    list = new List<int>() { 2, 10, 18, 26, 34, 42, 50, 58 };
                    return GetBoardSquares(list);

                case BoardFile.D:
                    list = new List<int>() { 3, 11, 19, 27, 35, 43, 51, 59 };
                    return GetBoardSquares(list);

                case BoardFile.E:
                    list = new List<int>() { 4, 12, 20, 28, 36, 44, 52, 60 };
                    return GetBoardSquares(list);

                case BoardFile.F:
                    list = new List<int>() { 5, 13, 21, 29, 37, 45, 53, 61 };
                    return GetBoardSquares(list);

                case BoardFile.G:
                    list = new List<int>() { 6, 14, 22, 30, 38, 46, 54, 62 };
                    return GetBoardSquares(list);

                case BoardFile.H:
                    list = new List<int>() { 7, 15, 23, 31, 39, 47, 55, 63 };
                    return GetBoardSquares(list);

                default:
                    throw new Exception("Not a valid Board file.");
            }
        }

        // Returns an list of squares of whatever rank you provide.
        // Reads from [A-H] (files)
        public List<Square> GetRank(BoardRank rank)
        {
            switch (rank)
            {
                case BoardRank.ONE:
                    var list = new List<int>() { 56, 57, 58, 59, 60, 61, 62, 63 };
                    return GetBoardSquares(list);
                case BoardRank.TWO:
                    list = new List<int>() { 48, 49, 50, 51, 52, 53, 54, 55 };
                    return GetBoardSquares(list);

                case BoardRank.THREE:
                    list = new List<int>() { 40, 41, 42, 43, 44, 45, 46, 47 };
                    return GetBoardSquares(list);

                case BoardRank.FOUR:
                    list = new List<int>() { 32, 33, 34, 35, 36, 37, 38, 39 };
                    return GetBoardSquares(list);

                case BoardRank.FIVE:
                    list = new List<int>() { 24, 25, 26, 27, 28, 29, 30, 31 };
                    return GetBoardSquares(list);

                case BoardRank.SIX:
                    list = new List<int>() { 16, 17, 18, 19, 20, 21, 22, 23 };
                    return GetBoardSquares(list);

                case BoardRank.SEVEN:
                    list = new List<int>() { 8, 9, 10, 11, 12, 13, 14, 15 };
                    return GetBoardSquares(list);

                case BoardRank.EIGHT:
                    list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
                    return GetBoardSquares(list);

                default:
                    throw new Exception("Not a valid Board file.");
            }
        }


        public List<Square> GetDiagonal(int index, Diagonal diagonal)
        {
            // for TOP_LEFT_TO_BOTTOM_RIGHT
            // subtract 9 till on a file or 8th rank
            // add 9 till on h file or 1st rank
            //
            // for BOTTOM_LEFT_TO_TOP_RIGHT
            // add 7 till on a file or 1st rank
            // subtract 7 till on h file or 8th rank
            //

            if (!Board.IsValidSquareIndex(index))
            {
                throw new Exception("Sent invalid index to GetDiagonal");
            }

            var result = new List<Square>();
            var file = Square.GetFile(index);
            var rank = Square.GetRank(index);

            if (diagonal == Diagonal.BOTTOM_LEFT_TO_TOP_RIGHT)
            {

            }

            return new List<Square>();
        }

        public Square GetSquare(int index)
        {
            if (index < 0 || index > 63)
            {
                throw new Exception("Cannot get square as index is out of bounds.");
            }

            var square = Board.Squares[index];
            return square;
        }

        public List<Square> GetBoardSquares(List<int> indexes)
        {
            var result = new List<Square>();
            foreach (int index in indexes)
            {
                result.Add(GetSquare(index));
            }

            return result;
        }
    }
}
