using Xunit;
namespace Chess;

public class CastlingTests
{
    [Fact]
    public void PathIsAttacked_ExpectNoCastleRights()
    {
        var game = new Game("rn1qk2r/pppb1ppp/4pn2/1B1p4/2Q1P3/2N2N2/P1PP1PPP/b1B1R1K1 w kq - 0 1");
        game.ExecuteMove(34, 33); // White queen to b4 to attack black king side castle path

        var bKing = game.Board.Squares[4].Piece; // Black king
        int bKingCastleIndex = 6; // g8

        Assert.DoesNotContain(bKing.ValidMoves, m => m.EndIndex == bKingCastleIndex || m.EndIndex == 5 || m.EndIndex == 12);
    }
}
