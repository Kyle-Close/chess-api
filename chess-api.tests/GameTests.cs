using Chess;
namespace chess_api.tests;

public class GameTests
{
    [Fact]
    public void IsEnemySquare_SendInvalidIndex_ExpectException()
    {
        var game = new Game();
        Assert.Throws<Exception>(() => game.IsEnemySquare(-1, Color.WHITE));
    }
    [Fact]
    public void IsEnemySquare_WhiteTurnTargetWhite_ExpectFalse()
    {
        var game = new Game();
        var res = game.IsEnemySquare(52, Color.BLACK);

        Assert.False(res);
    }
    [Fact]
    public void IsEnemySquare_WhiteTurnTargetEmpty_ExpectFalse()
    {
        var game = new Game();
        var res = game.IsEnemySquare(36, Color.BLACK);

        Assert.False(res);
    }
    [Fact]
    public void IsEnemySquare_WhiteTurnTargetBlack_ExpectTrue()
    {
        var game = new Game();
        var res = game.IsEnemySquare(12, Color.BLACK);

        Assert.True(res);
    }
    [Fact]
    public void IsEnemySquare_BlackTurnTargetBlack_ExpectFalse()
    {
        var game = new Game();
        var res = game.IsEnemySquare(12, Color.WHITE);

        Assert.False(res);
    }
    [Fact]
    public void IsEnemySquare_BlackTurnTargetEmpty_ExpectFalse()
    {
        var game = new Game();
        var res = game.IsEnemySquare(27, Color.WHITE);

        Assert.False(res);
    }
    [Fact]
    public void IsEnemySquare_BlackTurnTargetWhite_ExpectTrue()
    {
        var game = new Game();
        var res = game.IsEnemySquare(52, Color.WHITE);

        Assert.True(res);
    }
}
