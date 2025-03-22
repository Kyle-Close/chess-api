using System.Text.RegularExpressions;

namespace Chess
{
    public class Square
    {
        public int Index { get; set; }
        public Piece? Piece { get; set; }
        public BoardFile File { get; set; }
        public BoardRank Rank { get; set; }

        public Square(int index, Piece piece, BoardFile file, BoardRank rank)
        {
            Index = index;
            Piece = piece;
            File = file;
            Rank = rank;
        }

        public Square(int index, BoardFile file, BoardRank rank)
        {
            Index = index;
            File = file;
            Rank = rank;
        }

        public static int GetSquareIndex(BoardFile file, BoardRank rank)
        {
            int fileIndex = (int)file - 1;
            int rankIndex = ConvertRankToIndex(rank);

            return fileIndex + rankIndex;
        }

        public static int GetSquareIndex(char file, char rank)
        {
            Regex regex = new Regex(@"[A-H]", RegexOptions.IgnoreCase);
            Match match = regex.Match(file.ToString());

            if (!match.Success)
            {
                throw new Exception("File must be between A-H.");
            }

            int rankNumber;
            if (!int.TryParse(rank.ToString(), out rankNumber) || rankNumber < 1 || rankNumber > 8)
            {
                throw new Exception("Rank must be a number between 1-8");
            }

            var rankFinal = (BoardRank)rankNumber;
            BoardFile fileFinal;

            switch (file.ToString().ToUpper())
            {
                case "A":
                    fileFinal = BoardFile.A;
                    break;
                case "B":
                    fileFinal = BoardFile.B;
                    break;
                case "C":
                    fileFinal = BoardFile.C;
                    break;
                case "D":
                    fileFinal = BoardFile.D;
                    break;
                case "E":
                    fileFinal = BoardFile.E;
                    break;
                case "F":
                    fileFinal = BoardFile.F;
                    break;
                case "G":
                    fileFinal = BoardFile.G;
                    break;
                case "H":
                    fileFinal = BoardFile.H;
                    break;
                default:
                    throw new Exception("Provided invalid file letter.");
            }

            return GetSquareIndex(fileFinal, rankFinal);

        }

        private static int ConvertRankToIndex(BoardRank rank)
        {
            // Converts a rank into the value we need to index a square in the board array
            switch (rank)
            {
                case BoardRank.ONE:
                    return 56;
                case BoardRank.TWO:
                    return 48;
                case BoardRank.THREE:
                    return 40;
                case BoardRank.FOUR:
                    return 32;
                case BoardRank.FIVE:
                    return 24;
                case BoardRank.SIX:
                    return 16;
                case BoardRank.SEVEN:
                    return 8;
                case BoardRank.EIGHT:
                    return 0;
                default:
                    throw new Exception("Invalid rank cannot convert.");
            }
        }

        public static BoardFile GetFile(int index)
        {
            if (index % 8 == 0 || index == 0) return BoardFile.A;
            else if ((index - 1) % 8 == 0 || index == 1) return BoardFile.B;
            else if ((index - 2) % 8 == 0 || index == 2) return BoardFile.C;
            else if ((index - 3) % 8 == 0 || index == 3) return BoardFile.D;
            else if ((index - 4) % 8 == 0 || index == 4) return BoardFile.E;
            else if ((index - 5) % 8 == 0 || index == 5) return BoardFile.F;
            else if ((index - 6) % 8 == 0 || index == 6) return BoardFile.G;
            else if ((index - 7) % 8 == 0 || index == 7) return BoardFile.H;

            throw new Exception("Invalid file index. Cannot determine BoardFile.");
        }

        public static BoardRank GetRank(int index)
        {
            if (index < 8) return BoardRank.EIGHT;
            else if (index >= 8 && index < 16) return BoardRank.SEVEN;
            else if (index >= 16 && index < 24) return BoardRank.SIX;
            else if (index >= 24 && index < 32) return BoardRank.FIVE;
            else if (index >= 32 && index < 40) return BoardRank.FOUR;
            else if (index >= 40 && index < 48) return BoardRank.THREE;
            else if (index >= 48 && index < 56) return BoardRank.TWO;
            else if (index >= 56 && index < 64) return BoardRank.ONE;

            throw new Exception("Invalid index passed to getSquareRank: " + index);
        }
    }
}
