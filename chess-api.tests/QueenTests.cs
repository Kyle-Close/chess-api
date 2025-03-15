namespace Chess
{
    public class QueenTests
    {
        [Fact]
        public void GetStandardMoves_StartingPos_ExpectNoMoves()
        {
            var game = new Game();
            var queen = new Queen(59, Color.WHITE);
            var res = queen.GetStandardMoves(game);

            Assert.True(res.Count == 0);
        }

        [Fact]
        public void GetStandardMoves_BlackCheck1_ExpectCorrectMoves()
        {
            var game = new Game();
            game.ActiveColor = Color.BLACK;
            game.Board = new Board("rnb1kbnr/ppp1pppp/3pq3/8/2P3Q1/1P2P3/P2P1PPP/RNB1KBNR");
            var queen = new Queen(20, Color.BLACK);
            var res = queen.GetStandardMoves(game);

            Assert.True(res.Count == 11);
            Assert.Equal(3, res.Count(res => res.IsCapture == true));
        }

        [Fact]
        public void GetStandardMoves_BlackCheck2_ExpectCorrectMoves()
        {
            var game = new Game();
            game.ActiveColor = Color.BLACK;
            game.Board = new Board("rnb1k1nr/p3pppp/1bp5/1pqp4/1PP3Q1/4P3/P2P1PPP/RNB1KBNR");
            var queen = new Queen(26, Color.BLACK);
            var res = queen.GetStandardMoves(game);

            Assert.True(res.Count == 5);
            Assert.Equal(3, res.Count(res => res.IsCapture == true));
        }

        [Fact]
        public void GetStandardMoves_WhiteCheck1_ExpectCorrectMoves()
        {
            var game = new Game();
            game.Board = new Board("rnb1k1nr/p3pppp/1bp5/1pq5/1PPp3Q/4P2P/P2P1PP1/RNB1KBNR");
            var queen = new Queen(39, Color.WHITE);
            var res = queen.GetStandardMoves(game);

            Assert.True(res.Count == 11);
            Assert.Equal(3, res.Count(res => res.IsCapture == true));
        }
    }
}
