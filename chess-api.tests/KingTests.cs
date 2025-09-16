namespace Chess
{
    public class KingTests
    {
        [Fact]
        public void StartingPosition_ExpectNoMoves()
        {
            var game = new Game();
            var wKing = game.Board.GetPieces<King>(Color.WHITE).FirstOrDefault();
            var bKing = game.Board.GetPieces<King>(Color.BLACK).FirstOrDefault();

            Assert.True(wKing?.ValidMoves.Count == 0);
            Assert.True(bKing?.ValidMoves.Count == 0);
        }

        [Fact]
        public void RandomPosition_1_ExpectCorrectMoves()
        {
            var game = new Game("rnbqkbnr/ppp1p1pp/8/3pPp2/8/8/PPPP1PPP/RNBQKBNR b KQkq f6 0 1");
            var bKing = game.Board.GetPieces<King>(Color.BLACK).FirstOrDefault();

            Assert.NotNull(bKing);
            Assert.True(bKing.ValidMoves.Count == 2);

            Assert.Contains(bKing.ValidMoves, move => move.EndIndex == 11 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(bKing.ValidMoves, move => move.EndIndex == 13 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
        }

        [Fact]
        public void RandomPosition_2_ExpectCorrectMoves()
        {
            var game = new Game("r2q1rk1/pp1nbppp/2p1pn2/3p4/3P1B2/2N1PN2/PPQ2PPP/R3KB1R w KQ - 0 1");
            var wKing = game.Board.GetPieces<King>(Color.WHITE).FirstOrDefault();

            Assert.NotNull(wKing);
            Assert.True(wKing.ValidMoves.Count == 4);

            Assert.Contains(wKing.ValidMoves, move => move.EndIndex == 51 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(wKing.ValidMoves, move => move.EndIndex == 52 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(wKing.ValidMoves, move => move.EndIndex == 58 && !move.IsCapture && move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(wKing.ValidMoves, move => move.EndIndex == 59 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
        }

        [Fact]
        public void RandomPosition_3_ExpectCorrectMoves()
        {
            var game = new Game("4rrk1/1pp2ppp/p1n2q2/3p4/3P4/2P2N2/PP1Q1PPP/3RR1K1 w - - 0 1");
            var wKing = game.Board.GetPieces<King>(Color.WHITE).FirstOrDefault();

            Assert.NotNull(wKing);
            Assert.True(wKing.ValidMoves.Count == 2);

            Assert.Contains(wKing.ValidMoves, move => move.EndIndex == 61 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(wKing.ValidMoves, move => move.EndIndex == 63 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
        }

        [Fact]
        public void RandomPosition_4_ExpectCorrectMoves()
        {
            var game = new Game("rnbq1bnr/pppp1ppp/4pk2/8/2B1P3/8/PPPP1PPP/RNBQK1NR b KQ - 0 1");
            var bKing = game.Board.GetPieces<King>(Color.BLACK).FirstOrDefault();

            Assert.NotNull(bKing);
            Assert.True(bKing.ValidMoves.Count == 4);

            Assert.Contains(bKing.ValidMoves, move => move.EndIndex == 12 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(bKing.ValidMoves, move => move.EndIndex == 22 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(bKing.ValidMoves, move => move.EndIndex == 28 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(bKing.ValidMoves, move => move.EndIndex == 30 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
        }

        [Fact]
        public void RandomPosition_5_ExpectCorrectMoves()
        {
            var game = new Game("r1bqk2r/pppp1ppp/2n2n2/2b1p3/4P3/2NP1N2/PPP2PPP/R1BQKB1R b KQkq - 0 1");
            var bKing = game.Board.GetPieces<King>(Color.BLACK).FirstOrDefault();

            Assert.NotNull(bKing);
            Assert.True(bKing.ValidMoves.Count == 3);

            Assert.Contains(bKing.ValidMoves, move => move.EndIndex == 5 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(bKing.ValidMoves, move => move.EndIndex == 6 && !move.IsCapture && move.IsCastle && !move.IsEnPassantCapture);
            Assert.Contains(bKing.ValidMoves, move => move.EndIndex == 12 && !move.IsCapture && !move.IsCastle && !move.IsEnPassantCapture);
        }
    }
}
