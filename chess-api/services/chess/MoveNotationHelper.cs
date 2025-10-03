namespace Chess;
using System.Text.RegularExpressions;

public static class MoveNotationHelper
{
    public static MoveMetaData? FindMove(string notation, Game game)
    {
        var notationMatch = game.Board.GetPieces(game.ActiveColor).SelectMany(p => p.ValidMoves).FirstOrDefault(m => m.Notation == notation);
        if (notationMatch != null) return notationMatch;

        // Engine specifies the start pos of pawn moves but I don't
        string pattern = @"^[a-h]\d[a-h]\d";
        var regex = new Regex(pattern);
        if (regex.IsMatch(notation))
        {
            string startNotation = notation.Substring(0, 2);
            string endNotation = notation.Substring(2);

            var startIndex = Square.GetSquareIndex(Square.GetBoardFileFromChar(startNotation[0]), Square.GetBoardRankFromChar(startNotation[1]));
            var endIndex = Square.GetSquareIndex(Square.GetBoardFileFromChar(endNotation[0]), Square.GetBoardRankFromChar(endNotation[1]));

            notationMatch = game.Board.GetPieces(game.ActiveColor).SelectMany(p => p.ValidMoves).FirstOrDefault(m => m.StartIndex == startIndex && m.EndIndex == endIndex);
            if (notationMatch != null) return notationMatch;
        }

        return null;
    }
}
