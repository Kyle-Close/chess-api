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
                    var list = new List<int>() { 56, 48, 40, 32, 24, 16, 8, 0 };
                    return GetBoardSquares(list);

                case BoardFile.B:
                    list = new List<int>() { 57, 49, 41, 33, 25, 17, 9, 1 };
                    return GetBoardSquares(list);

                case BoardFile.C:
                    list = new List<int>() { 58, 50, 42, 34, 26, 18, 10, 2 };
                    return GetBoardSquares(list);

                case BoardFile.D:
                    list = new List<int>() { 59, 51, 43, 35, 27, 19, 11, 3 };
                    return GetBoardSquares(list);

                case BoardFile.E:
                    list = new List<int>() { 60, 52, 44, 36, 28, 20, 12, 4 };
                    return GetBoardSquares(list);

                case BoardFile.F:
                    list = new List<int>() { 61, 53, 45, 37, 29, 21, 13, 5 };
                    return GetBoardSquares(list);

                case BoardFile.G:
                    list = new List<int>() { 62, 54, 46, 38, 30, 22, 14, 6 };
                    return GetBoardSquares(list);

                case BoardFile.H:
                    list = new List<int>() { 63, 55, 47, 39, 31, 23, 15, 7 };
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
            if (!Board.IsValidSquareIndex(index))
            {
                throw new Exception("Sent invalid index to GetDiagonal");
            }

            var result = new List<Square>();
            var file = Square.GetFile(index);
            var rank = Square.GetRank(index);

            if (diagonal == Diagonal.BOTTOM_LEFT_TO_TOP_RIGHT)
            {
                while (file != BoardFile.A && rank != BoardRank.ONE)
                {
                    index += 7;

                    file = Square.GetFile(index);
                    rank = Square.GetRank(index);
                }

                // Push index square as this is the start
                result.Add(Board.Squares[index]);

                while (file != BoardFile.H && rank != BoardRank.EIGHT)
                {
                    index -= 7;
                    result.Add(Board.Squares[index]);

                    file = Square.GetFile(index);
                    rank = Square.GetRank(index);
                }

                return result;
            }
            else
            {
                while (file != BoardFile.A && rank != BoardRank.EIGHT)
                {
                    index -= 9;

                    file = Square.GetFile(index);
                    rank = Square.GetRank(index);
                }


                result.Add(Board.Squares[index]);

                while (file != BoardFile.H && rank != BoardRank.ONE)
                {
                    index += 9;
                    result.Add(Board.Squares[index]);

                    file = Square.GetFile(index);
                    rank = Square.GetRank(index);
                }

                return result;
            }
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
