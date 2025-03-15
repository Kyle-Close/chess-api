namespace Chess
{
    public class BishopTests
    {
        [Fact]
        public void GetStandardMoves_WhiteStartPos_ExpectNoMoves()
        {
            var game = new Game();
            var bishop = new Bishop(58, Color.WHITE);

            var res = bishop.GetStandardMoves(game);

            Assert.True(res.Count == 0);
        }

        [Fact]
        public void GetStandardMoves_WhitePos1_ExpectCorrectMoves()
        {
            var game = new Game();
            game.Board = new Board("rnb1k1nr/p1p1pppp/1b6/1pqB4/1PPp3Q/4P2P/P2P1PP1/RN2KBNR");
            var bishop = new Bishop(27, Color.WHITE);
            var res = bishop.GetStandardMoves(game);

            Assert.True(res.Count == 7);
        }
    }
}
