namespace Chess
{
    public class RookTests
    {
        [Fact]
        public void GetStandardMoves_WhiteStartingPosition_ExpectNoMoves()
        {
            var game = new Game();
            var rook = new Rook(56, Color.WHITE);

            var res = rook.GetStandardMoves(game);
            Assert.True(res.Count == 0);
        }

        [Fact]
        public void GetStandardMoves_BlackStartingPosition_ExpectNoMoves()
        {
            var game = new Game();
            game.ActiveColor = Color.BLACK;
            var rook = new Rook(0, Color.BLACK);

            var res = rook.GetStandardMoves(game);
            Assert.True(res.Count == 0);
        }

        [Fact]
        public void GetStandardMoves_BlackHFileOpen_Expect7Moves()
        {
            var game = new Game();
            game.ActiveColor = Color.BLACK;
            game.Board = new Board("rnbqkbnr/ppppppp1/6p1/8/8/5RP1/PPPPPPP1/RNBQKBN1");
            var rook = new Rook(7, Color.BLACK);

            var res = rook.GetStandardMoves(game);
            Assert.True(res.Count == 7);

            Assert.Contains(res, move => move.Index == 15 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 23 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 31 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 39 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 47 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 55 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 63 && move.IsCapture == false);
        }

        [Fact]
        public void GetStandardMoves_HFileCaptureOn5thRank_Expect7Moves()
        {
            var game = new Game();
            game.Board = new Board("rnbqkbnr/ppppppp1/8/7p/8/6P1/PPPPPPP1/RNBQKBNR");
            var rook = new Rook(63, Color.WHITE);

            var res = rook.GetStandardMoves(game);
            Assert.True(res.Count == 4);

            Assert.Contains(res, move => move.Index == 31 && move.IsCapture == true);
            Assert.Contains(res, move => move.Index == 39 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 47 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 55 && move.IsCapture == false);
        }

        [Fact]
        public void GetStandardMoves_Black8thRankOpen_Expect7Moves()
        {
            var game = new Game();
            game.ActiveColor = Color.BLACK;
            game.Board = new Board("r7/pppppppp/4kbnr/2nbq3/8/8/PPPPPPPP/RNBQKBNR");
            var rook = new Rook(0, Color.BLACK);

            var res = rook.GetStandardMoves(game);
            Assert.True(res.Count == 7);

            Assert.Contains(res, move => move.Index == 1 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 2 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 3 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 4 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 5 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 6 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 7 && move.IsCapture == false);
        }

        [Fact]
        public void GetStandardMoves_WhiteCaptureOnFileAndOpenRank_ExpectCorrectMoves()
        {
            var game = new Game();
            game.Board = new Board("rnbqkb1r/1p1ppppp/1pp5/4nP2/3PPNP1/R7/PPP4P/1NBQKB1R");
            var rook = new Rook(40, Color.WHITE);

            var res = rook.GetStandardMoves(game);
            Assert.True(res.Count == 12);

            Assert.Contains(res, move => move.Index == 32 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 24 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 16 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 8 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 0 && move.IsCapture == true);

            Assert.Contains(res, move => move.Index == 41 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 42 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 43 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 44 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 45 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 46 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 47 && move.IsCapture == false);
        }

        [Fact]
        public void GetStandardMoves_BlackBoxedInWith2Captures_Expect2Moves()
        {
            var game = new Game();
            game.ActiveColor = Color.BLACK;
            game.Board = new Board("1nbqkbnr/1p4pp/2pp1p2/2Prp3/1P1P4/R7/4PPPP/1NBQKBNR");
            var rook = new Rook(27, Color.BLACK);

            var res = rook.GetStandardMoves(game);
            Assert.True(res.Count == 2);

            Assert.Contains(res, move => move.Index == 26 && move.IsCapture == true);
            Assert.Contains(res, move => move.Index == 35 && move.IsCapture == true);
        }

        [Fact]
        public void GetStandardMoves_WhiteAllDirections_ExpectCorrectMoves()
        {
            var game = new Game();
            game.Board = new Board("1nbqkbnr/1p4pp/2pp1p2/2Prp1R1/1P1P2P1/8/4PP1P/1NBQKBNR");
            var rook = new Rook(30, Color.WHITE);

            var res = rook.GetStandardMoves(game);
            Assert.True(res.Count == 5);

            // captures
            Assert.Contains(res, move => move.Index == 28 && move.IsCapture == true);
            Assert.Contains(res, move => move.Index == 14 && move.IsCapture == true);

            Assert.Contains(res, move => move.Index == 22 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 31 && move.IsCapture == false);
            Assert.Contains(res, move => move.Index == 29 && move.IsCapture == false);
        }
    }
}
