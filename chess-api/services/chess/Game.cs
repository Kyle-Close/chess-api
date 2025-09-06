namespace Chess
{
    public class Game
    {
        public string Id { get; set; }

        public Color ActiveColor { get; set; }

        public int? EnPassantIndex { get; set; }
        public bool IsCheck { get; set; }
        public bool IsCheckmate { get; set; }

        public int HalfMoves { get; set; }
        public int FullMoves { get; set; }

        public CastleRights WhiteCastleRights { get; set; }
        public CastleRights BlackCastleRights { get; set; }

        public List<string> FenHistory { get; set; }
        public List<string> MoveHistory { get; set; }
        public Board Board { get; set; }

        public Game()
        {
            Id = Guid.NewGuid().ToString();
            ActiveColor = Color.WHITE;
            HalfMoves = 0;
            FullMoves = 1;
            EnPassantIndex = null;
            WhiteCastleRights = new CastleRights();
            BlackCastleRights = new CastleRights();
            FenHistory = new List<string>();
            MoveHistory = new List<string>();
            Board = new Board();
            IsCheck = false;
            IsCheckmate = false;

            UpdateValidMoves();
        }

        public Game(string fen)
        {
            var fenHelper = new FenHelper(fen);

            Id = Guid.NewGuid().ToString();
            ActiveColor = fenHelper.ActiveColorSegment.ToUpper() == "W" ? Color.WHITE : Color.BLACK;
            HalfMoves = int.Parse(fenHelper.HalfMoveSegment);
            FullMoves = int.Parse(fenHelper.FullMoveSegment);
            EnPassantIndex = fenHelper.EnPassantIndex;
            FenHistory = new List<string>();
            MoveHistory = new List<string>();
            Board = new Board(fenHelper.BoardSegment);

            var crSeg = fenHelper.CastleRightsSegment;
            if (crSeg == "-")
            {
                WhiteCastleRights = new CastleRights(false, false);
                BlackCastleRights = new CastleRights(false, false);
            }
            else
            {
                bool whiteKing = false;
                bool whiteQueen = false;
                bool blackKing = false;
                bool blackQueen = false;

                if (crSeg.Contains("K"))
                {
                    whiteKing = true;
                }
                if (crSeg.Contains("k"))
                {
                    blackKing = true;
                }
                if (crSeg.Contains("Q"))
                {
                    whiteQueen = true;
                }
                if (crSeg.Contains("q"))
                {
                    blackQueen = true;
                }

                WhiteCastleRights = new CastleRights(whiteKing, whiteQueen);
                BlackCastleRights = new CastleRights(blackKing, blackQueen);
            }

            // Update the hasMoved property based on the state of the board.
            // Only care about pawns for double moves & king, rook for castle rights
            var pawns = Board.GetPieces<Pawn>();
            foreach (var pawn in pawns)
            {
                pawn.HasMoved = !pawn.IsInStartPosition();
            }

            var kings = Board.GetPieces<King>();
            foreach (var king in kings)
            {
                king.HasMoved = !king.IsInStartPosition();
            }

            var rooks = Board.GetPieces<Rook>();
            foreach (var rook in rooks)
            {
                rook.HasMoved = !rook.IsInStartPosition();
            }

            var isCheck = Board.IsCheck();
            if (isCheck.WhiteInCheck || isCheck.BlackInCheck)
            {
                IsCheck = true;
            }

            UpdateValidMoves();
        }

        // Returns a list of valid moves that the active player can make.
        public List<MoveMetaData> GetCurrentValidMoves()
        {
            var pieces = Board.GetPieces(ActiveColor);
            foreach (var piece in pieces)
            {
                piece.UpdateValidMoves(this);
            }
            return pieces.SelectMany(piece => piece.ValidMoves).ToList();
        }

        public void UpdateValidMoves()
        {
            var wPieces = Board.GetPieces(Color.WHITE);
            var bPieces = Board.GetPieces(Color.BLACK);

            foreach (var piece in wPieces)
            {
                piece.UpdateValidMoves(this);
            }

            foreach (var piece in bPieces)
            {
                piece.UpdateValidMoves(this);
            }
        }

        // Returns an array of squares of whatever file you provide.
        // Reads from [1-8] (ranks)
        public List<Square> GetCurrentBoardFile(BoardFile file)
        {
            switch (file)
            {
                case BoardFile.A:
                    var list = new List<int>() { 0, 8, 16, 24, 32, 40, 48, 56 };
                    return GetBoardSquares(list);
                case BoardFile.B:
                    list = new List<int>() { 1, 9, 17, 25, 33, 41, 49, 57 };
                    return GetBoardSquares(list);
                case BoardFile.C:
                    list = new List<int>() { 2, 10, 18, 26, 34, 42, 50, 58 };
                    return GetBoardSquares(list);
                case BoardFile.D:
                    list = new List<int>() { 3, 11, 19, 27, 35, 43, 51, 59 };
                    return GetBoardSquares(list);
                case BoardFile.E:
                    list = new List<int>() { 4, 12, 20, 28, 36, 44, 52, 60 };
                    return GetBoardSquares(list);
                case BoardFile.F:
                    list = new List<int>() { 5, 13, 21, 29, 37, 45, 53, 61 };
                    return GetBoardSquares(list);
                case BoardFile.G:
                    list = new List<int>() { 6, 14, 22, 30, 38, 46, 54, 62 };
                    return GetBoardSquares(list);
                case BoardFile.H:
                    list = new List<int>() { 7, 15, 23, 31, 39, 47, 55, 63 };
                    return GetBoardSquares(list);

                default:
                    throw new Exception("Not a valid board file.");
            }

        }

        // Returns an list of squares of whatever rank you provide.
        // Reads from [A-H] (files)
        public List<Square> GetCurrentBoardRank(BoardRank rank)
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
                    throw new Exception("Not a valid board file.");
            }

        }

        // Returns an list of squares of whatever rank you provide.
        // Reads from [A-H] (files)
        // public List<Square> GetCurrentTopLeftToBottomRightDiag(int index)
        // {
        //
        // }

        public Square GetBoardSquare(int index)
        {
            var square = Board.Squares[index];
            return square;
        }

        public List<Square> GetBoardSquares(List<int> indexes)
        {
            var result = new List<Square>();
            foreach (int index in indexes)
            {
                result.Add(GetBoardSquare(index));
            }

            return result;
        }

        public bool IsEnemySquare(int index, Color enemyColor)
        {
            if (!Board.IsValidSquareIndex(index))
            {
                throw new Exception("You sent an invalid square index");
            }

            var piece = Board.Squares[index].Piece;

            if (piece == null)
            {
                return false;
            }

            if (enemyColor == piece.Color)
            {
                return true;
            }

            return false;
        }

        public bool DoesMatchLatestFen(string fen)
        {
            var latest = FenHistory.Last();
            if (latest == null)
                return false;

            if (latest == fen)
                return true;

            return false;
        }

        public static Game? FindActiveGame(List<Game> activeGames, string id)
        {
            var game = activeGames.Find(game => game.Id == id);
            return game;
        }

        //TODO: lookup game in active games table


        public void ExecuteMove(int start, int end, PieceType? promotionPieceType = null)
        {
            Piece? selectedPiece = Board.Squares[start].Piece;

            if (selectedPiece == null)
            {
                throw new Exception("Attempted to execute a move but starting square '" + start.ToString() + "' has no piece.");
            }

            if (selectedPiece.Color != ActiveColor)
            {
                throw new Exception("Attempted to execute a move using inactive piece color.");
            }

            // Get most up to date list of valid moves on selected piece
            selectedPiece.UpdateValidMoves(this);

            // Check that the requested move is in the list of allowed moves
            MoveMetaData? move = selectedPiece.ValidMoves.Find(move => move.EndIndex == end);
            if (move == null)
            {
                throw new Exception("Attempted to execute an invalid move.");
            }

            Color opponentColor = ActiveColor == Color.WHITE ? Color.BLACK : Color.WHITE;

            if (move.IsPromotion)
            {
                if (promotionPieceType == null)
                {
                    throw new Exception("Promotion move attempted without specifying promotion piece type.");
                }

                switch (promotionPieceType)
                {
                    case PieceType.QUEEN:
                        Board.Squares[start].Piece = new Queen(start, ActiveColor);
                        break;
                    case PieceType.ROOK:
                        Board.Squares[start].Piece = new Rook(start, ActiveColor);
                        break;
                    case PieceType.BISHOP:
                        Board.Squares[start].Piece = new Bishop(start, ActiveColor);
                        break;
                    case PieceType.KNIGHT:
                        Board.Squares[start].Piece = new Knight(start, ActiveColor);
                        break;
                    default:
                        throw new Exception("Invalid promotion piece type specified.");
                }
            }

            Piece? capturedPiece = Board.Squares[end].Piece;

            // Move the piece
            Board.MovePiece(start, end);

            // If piece was a pawn that moved two squares, set en-passant index
            Piece? piece = Board.Squares[end].Piece;
            if (piece != null && piece.PieceType == PieceType.PAWN)
            {
                // If this was an en-passant capture, we need to capture the correct pawn
                if (move.IsEnPassantCapture && EnPassantIndex != null)
                {
                    if (opponentColor == Color.BLACK) // White's turn
                    {
                        int capturedPieceIndex = end + 8;
                        Board.Squares[capturedPieceIndex].Piece = null;
                    }
                    else
                    {
                        int capturedPieceIndex = end - 8;
                        Board.Squares[capturedPieceIndex].Piece = null;
                    }
                }

                BoardRank startRank = Square.GetRank(start);
                BoardRank endRank = Square.GetRank(end);

                if (opponentColor == Color.BLACK) // White's turn
                {
                    if (startRank == BoardRank.TWO && endRank == BoardRank.FOUR)
                    {
                        EnPassantIndex = end + 8;
                    }
                    else
                    {
                        EnPassantIndex = null;
                    }
                }
                else // Blacks's turn
                {
                    if (startRank == BoardRank.SEVEN && endRank == BoardRank.FIVE)
                    {
                        EnPassantIndex = end - 8;
                    }
                    else
                    {
                        EnPassantIndex = null;
                    }
                }


            }
            else // Moved piece other than pawn. Reset en-passant square
            {
                EnPassantIndex = null;
            }

            // If piece was the king, update castle rights
            if (piece != null && piece.PieceType == PieceType.KING)
            {
                if (opponentColor == Color.BLACK) // White's turn
                {
                    WhiteCastleRights.KingSide = false;
                    WhiteCastleRights.QueenSide = false;

                    if (start == 60 && end == 62)
                    {
                        Board.MovePiece(63, 61); // Castle king side - Move the rook
                    }
                    else if (start == 60 && end == 58)
                    {
                        Board.MovePiece(56, 59); // Castle queen side - Move the rook
                    }
                }
                else // Black's turn
                {
                    BlackCastleRights.KingSide = false;
                    BlackCastleRights.QueenSide = false;

                    if (start == 4 && end == 6)
                    {
                        Board.MovePiece(7, 5); // Castle king side - Move the rook
                    }
                    else if (start == 4 && end == 2)
                    {
                        Board.MovePiece(0, 3); // Castle queen side - Move the rook
                    }
                }
            }

            // If capturing a piece, check if capturing opponent rook. If so, update castle rights accordingly
            if (move.IsCapture && capturedPiece != null && capturedPiece.PieceType == PieceType.ROOK)
            {
                if (opponentColor == Color.BLACK) // White's turn
                {
                    if (move.EndIndex == 0) // Capturing black's queen side rook on starting pos
                    {
                        BlackCastleRights.QueenSide = false;
                    }
                    else if (move.EndIndex == 7) // Capturing black's king side rook on starting pos
                    {
                        BlackCastleRights.KingSide = false;
                    }
                }
                else // Blacks's turn
                {
                    if (move.EndIndex == 56) // Capturing white's queen side rook on starting pos
                    {
                        WhiteCastleRights.QueenSide = false;
                    }
                    else if (move.EndIndex == 63) // Capturing white's king side rook on starting pos
                    {
                        WhiteCastleRights.KingSide = false;
                    }
                }
            }

            // If the piece was a rook on it's starting square, update castle rights
            if (piece != null && piece.PieceType == PieceType.ROOK)
            {

                if (opponentColor == Color.BLACK) // White's turn
                {
                    if (start == 63)
                        WhiteCastleRights.KingSide = false;
                    else if (start == 56)
                        WhiteCastleRights.QueenSide = false;
                }
                else // White's turn
                {
                    if (start == 7)
                        BlackCastleRights.KingSide = false;
                    if (start == 0)
                        BlackCastleRights.QueenSide = false;
                }
            }

            // Check for checkmate before updating game state
            if (move.CausesCheck)
            {
                UpdateValidMoves();
                List<Piece> oPieces = Board.GetPieces(opponentColor);
                if (oPieces.All(piece => piece.ValidMoves.Count == 0))
                {
                    IsCheckmate = true;
                    FenHistory.Add(FenHelper.BuildFen(this, Board));
                    HalfMoves++;
                    return;
                }
            }

            // Update game state
            IsCheck = move.CausesCheck;

            if (opponentColor == Color.WHITE)
            {
                FullMoves++;
            }

            // Final check to see if the opponent is in check after all updates
            var isCheck = Board.IsCheck();
            if (isCheck.WhiteInCheck || isCheck.BlackInCheck)
            {
                IsCheck = true;
            }
            else
            {
                IsCheck = false;
            }

            // One last check for checkmate
            if (IsCheck)
            {
                UpdateValidMoves();
                List<Piece> pieces = Board.GetPieces(opponentColor);
                if (pieces.All(piece => piece.ValidMoves.Count == 0))
                {
                    IsCheckmate = true;
                    FenHistory.Add(FenHelper.BuildFen(this, Board));
                    HalfMoves++;
                    return;
                }
            }

            // Update the valid moves for all pieces
            UpdateValidMoves();

            HalfMoves++;
            ActiveColor = opponentColor;
            FenHistory.Add(FenHelper.BuildFen(this, Board));
            MoveHistory.Add(move.Notation);

            // If the move was a pawn move or a capture, reset the half-move clock
            if (selectedPiece.PieceType == PieceType.PAWN || move.IsCapture)
            {
                HalfMoves = 0;
            }


        }
    }
}
