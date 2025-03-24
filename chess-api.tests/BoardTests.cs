using Chess;
namespace chess_api.tests;

public class BoardTests
{
    [Fact]
    public void AreSquaresEmpty_All_Empty()
    {
        var game = new Game("r3kbnr/1pp1qppp/p1npb3/3Pp3/8/8/PPP1PPPP/RNBQKBNR w KQkq - 0 1");
        var isEmpty = game.Board.AreSquaresEmpty([1, 2, 3]);

        Assert.True(isEmpty);
    }

    [Fact]
    public void AreSquaresEmpty_All_Empty2()
    {
        var game = new Game("r3kbnr/1pp1qppp/p1npb3/3Pp3/8/5B1N/PPP1PPPP/RNBQK2R w KQkq - 0 1");
        var isEmpty = game.Board.AreSquaresEmpty([61, 62]);

        Assert.True(isEmpty);
    }

    [Fact]
    public void AreSquaresEmpty_All_Empty3()
    {
        var game = new Game("r3kbnr/1pp1qppp/p1npb3/3Pp3/8/5B1N/PPP1PPPP/RNBQK2R w KQkq - 0 1");
        var isEmpty = game.Board.AreSquaresEmpty([21, 22, 23, 24, 25, 26]);

        Assert.True(isEmpty);
    }

    [Fact]
    public void AreSquaresEmpty_Some_Empty()
    {
        var game = new Game("r3kb1r/1pp1qppp/p1npbn2/3Pp3/8/5B1N/PPP1PPPP/RNBQK2R w KQkq - 0 1");
        var isEmpty = game.Board.AreSquaresEmpty([5, 6]);

        Assert.False(isEmpty);
    }

    [Fact]
    public void AreSquaresEmpty_None_Empty()
    {
        var game = new Game("r3kb1r/1pp1qppp/p1npbn2/3Pp3/8/5B1N/PPP1PPPP/RNBQK2R w KQkq - 0 1");
        var isEmpty = game.Board.AreSquaresEmpty([47, 48, 49, 50]);

        Assert.False(isEmpty);
    }
}
