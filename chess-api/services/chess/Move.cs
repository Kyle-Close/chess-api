namespace Chess;

public static class Move
{
    private static MoveMetaData ValidateMove(Game game, int start, int end)
    {
        Piece? selectedPiece = game.Board.Squares[start].Piece;

        if (selectedPiece == null)
        {
            throw new Exception("Attempted to execute a move but starting square '" + start.ToString() + "' has no piece.");
        }

        if (selectedPiece.Color != game.ActiveColor)
        {
            throw new Exception("Attempted to execute a move using inactive piece color.");
        }

        // Get most up to date list of valid moves on selected piece
        selectedPiece.UpdateValidMoves(game, game.ActiveColor);

        // Check that the requested move is in the list of allowed moves
        MoveMetaData? move = selectedPiece.ValidMoves.Find(move => move.EndIndex == end);

        if (move == null)
        {
            throw new Exception("Attempted to execute an invalid move.");
        }

        return move;
    }

    private static void HandlePawnPromotion(Game game, int start, PieceType? promotionPieceType)
    {
        if (promotionPieceType == null)
        {
            throw new Exception("Promotion move attempted without specifying promotion piece type.");
        }

        switch (promotionPieceType)
        {
            case PieceType.QUEEN:
                game.Board.Squares[start].Piece = new Queen(PieceType.QUEEN, start, game.ActiveColor);
                break;
            case PieceType.ROOK:
                game.Board.Squares[start].Piece = new Rook(PieceType.ROOK, start, game.ActiveColor);
                break;
            case PieceType.BISHOP:
                game.Board.Squares[start].Piece = new Bishop(PieceType.BISHOP, start, game.ActiveColor);
                break;
            case PieceType.KNIGHT:
                game.Board.Squares[start].Piece = new Knight(PieceType.KNIGHT, start, game.ActiveColor);
                break;
            default:
                throw new Exception("Invalid promotion piece type specified.");
        }
    }

    private static void HandleEnPassantCapture(Game game, Color opponentColor, int end)
    {
        // If this was an en-passant capture, we need to capture the correct pawn
        if (opponentColor == Color.BLACK) // White's turn
        {
            int capturedPieceIndex = end + 8;
            game.Board.Squares[capturedPieceIndex].Piece = null;
        }
        else
        {
            int capturedPieceIndex = end - 8;
            game.Board.Squares[capturedPieceIndex].Piece = null;
        }
    }

    private static void SetEnPassantIndex(Game game, Piece piece, int start, int end, Color opponentColor)
    {
        BoardRank startRank = Square.GetRank(start);
        BoardRank endRank = Square.GetRank(end);

        if (piece != null && piece.PieceType == PieceType.PAWN)
        {
            if (opponentColor == Color.BLACK) // White's turn
            {
                if (startRank == BoardRank.TWO && endRank == BoardRank.FOUR)
                {
                    game.EnPassantIndex = end + 8;
                }
                else
                {
                    game.EnPassantIndex = null;
                }
            }
            else // Blacks's turn
            {
                if (startRank == BoardRank.SEVEN && endRank == BoardRank.FIVE)
                {
                    game.EnPassantIndex = end - 8;
                }
                else
                {
                    game.EnPassantIndex = null;
                }
            }
        }
        else
        {
            game.EnPassantIndex = null; // Moving piece other than pawn. Reset en-passant square
        }
    }

    private static void Castle(Game game, int start, int end, Color opponentColor)
    {
        if (opponentColor == Color.BLACK) // White's turn
        {
            if (start == 60 && end == 62)
            {
                game.Board.MovePiece(63, 61); // Castle king side - Move the rook
            }
            else if (start == 60 && end == 58)
            {
                game.Board.MovePiece(56, 59); // Castle queen side - Move the rook
            }
        }
        else // Black's turn
        {
            if (start == 4 && end == 6)
            {
                game.Board.MovePiece(7, 5); // Castle king side - Move the rook
            }
            else if (start == 4 && end == 2)
            {
                game.Board.MovePiece(0, 3); // Castle queen side - Move the rook
            }
        }
    }

    private static void HandleKingMove(Game game, int start, int end, Color opponentColor, MoveMetaData move)
    {
        if (move.IsCastle)
        {
            Castle(game, start, end, opponentColor);
        }

        // Update castle rights. Since king was moved that player no longer has any castle rights
        if (opponentColor == Color.BLACK)
        {
            game.WhiteCastleRights.KingSide = false;
            game.WhiteCastleRights.QueenSide = false;
        }
        else
        {
            game.BlackCastleRights.KingSide = false;
            game.BlackCastleRights.QueenSide = false;
        }
    }

    private static void HandleCapture(Game game, MoveMetaData move, Piece capturedPiece, Color opponentColor)
    {
        // If capturing a rook, castle rights may need to be updated
        if (capturedPiece.PieceType == PieceType.ROOK)
        {
            if (opponentColor == Color.BLACK) // White's turn
            {
                if (move.EndIndex == 0) // Capturing black's queen side rook on starting pos
                {
                    game.BlackCastleRights.QueenSide = false;
                }
                else if (move.EndIndex == 7) // Capturing black's king side rook on starting pos
                {
                    game.BlackCastleRights.KingSide = false;
                }
            }
            else // Blacks's turn
            {
                if (move.EndIndex == 56) // Capturing white's queen side rook on starting pos
                {
                    game.WhiteCastleRights.QueenSide = false;
                }
                else if (move.EndIndex == 63) // Capturing white's king side rook on starting pos
                {
                    game.WhiteCastleRights.KingSide = false;
                }
            }
        }
    }

    private static void HandleRookMove(Game game, int start, int end, Color opponentColor)
    {
        // If the rook was on it's starting square, update castle rights
        if (opponentColor == Color.BLACK) // White's turn
        {
            if (start == 63)
            {
                game.WhiteCastleRights.KingSide = false;
            }
            else if (start == 56)
            {
                game.WhiteCastleRights.QueenSide = false;
            }
        }
        else // White's turn
        {
            if (start == 7)
            {
                game.BlackCastleRights.KingSide = false;
            }
            if (start == 0)
            {
                game.BlackCastleRights.QueenSide = false;
            }
        }
    }

    private static bool IsCheckmate(Game game, MoveMetaData move, Color opponentColor)
    {
        if (move.CausesCheck)
        {
            game.UpdateValidMoves(opponentColor);
            List<Piece> oPieces = game.Board.GetPieces(opponentColor);
            if (oPieces.All(piece => piece.ValidMoves.Count == 0))
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsStalemate(Game game, Color opponentColor)
    {
        if (game.Status == GameStatus.CHECKMATE) return false;

        // Assuming moves have already been updated
        var oPieces = game.Board.GetPieces(opponentColor);
        if (oPieces.Any(piece => piece.ValidMoves.Count > 0))
        {
            return false;
        }

        return true;
    }

    private static bool IsInsufficientMaterial(Game game)
    {
        var wPieces = game.Board.GetPieces(Color.WHITE);
        var bPieces = game.Board.GetPieces(Color.BLACK);

        // King vs King
        if (wPieces.Count == 1 && bPieces.Count == 1)
        {
            return true;
        }

        // King vs Bishop
        if (wPieces.Any(piece => piece.PieceType == PieceType.BISHOP) && wPieces.Count == 2 && bPieces.Count == 1)
        {
            return true;
        }
        if (bPieces.Any(piece => piece.PieceType == PieceType.BISHOP) && bPieces.Count == 2 && wPieces.Count == 1)
        {
            return true;
        }

        // King vs Knight
        if (wPieces.Any(piece => piece.PieceType == PieceType.KNIGHT) && wPieces.Count == 2 && bPieces.Count == 1)
        {
            return true;
        }
        if (bPieces.Any(piece => piece.PieceType == PieceType.KNIGHT) && bPieces.Count == 2 && wPieces.Count == 1)
        {
            return true;
        }

        return false;
    }



    public static void ExecuteMove(Game game, int start, int end, PieceType? promotionPieceType = null)
    {
        MoveMetaData move = ValidateMove(game, start, end);

        Color opponentColor = game.ActiveColor == Color.WHITE ? Color.BLACK : Color.WHITE;
        Piece? capturedPiece = game.Board.Squares[end].Piece;
        Piece? piece = game.Board.Squares[start].Piece;

        if (piece == null)
        {
            throw new Exception("Attempted to execute move on square with no piece.");
        }

        if (move.IsPromotion)
        {
            HandlePawnPromotion(game, start, promotionPieceType);

            // Update piece reference to newly promoted piece
            piece = game.Board.Squares[start].Piece;

            // Need to update move notation to include promotion piece
            move.Notation = move.Notation.Replace('?', piece.GetPieceChar());
        }

        game.UpdateRemainingTime(game.ActiveColor);

        if (game.WhiteRemainingTime <= 0)
        {
            game.EndGame(GameStatus.TIMEOUT, Color.BLACK);
            return;
        }
        if (game.BlackRemainingTime <= 0)
        {
            game.EndGame(GameStatus.TIMEOUT, Color.WHITE);
            return;
        }

        // Move is valid, update the board to move the piece to it's target square
        game.Board.MovePiece(start, end);

        // Set the last move
        game.LastMoveMetaData = move;

        // If this was a promotion, the previously calculated valid moves won't know if it causes check since we don't know what piece it will be promoted to
        if (move.IsPromotion)
        {
            var opponentKing = game.Board.GetPieces<King>(opponentColor).FirstOrDefault();
            if (opponentKing != null && opponentKing.IsInCheck(game.Board))
            {
                move.CausesCheck = true;
            }
            else
            {
                move.CausesCheck = false;
            }
        }

        if (piece.PieceType == PieceType.PAWN)
        {
            if (move.IsEnPassantCapture && game.EnPassantIndex != null)
            {
                HandleEnPassantCapture(game, opponentColor, end);
                if (opponentColor == Color.BLACK)
                {
                    game.WhiteCapturedPieces.Add(PieceType.PAWN);
                }
                else
                {
                    game.BlackCapturedPieces.Add(PieceType.PAWN);
                }
            }
        }

        SetEnPassantIndex(game, piece, start, end, opponentColor);

        if (piece.PieceType == PieceType.KING)
        {
            HandleKingMove(game, start, end, opponentColor, move);
        }

        // If capturing a piece, check if capturing opponent rook. If so, update castle rights accordingly
        if (move.IsCapture && capturedPiece != null)
        {
            HandleCapture(game, move, capturedPiece, opponentColor);
            if (opponentColor == Color.BLACK)
            {
                game.WhiteCapturedPieces.Add(capturedPiece.PieceType);
            }
            else
            {
                game.BlackCapturedPieces.Add(capturedPiece.PieceType);
            }
        }

        if (piece.PieceType == PieceType.ROOK)
        {
            HandleRookMove(game, start, end, opponentColor);
        }

        game.Status = GameStatus.ONGOING;

        // Update game states
        if (move.CausesCheck)
        {
            game.Status = GameStatus.IN_CHECK;
        }

        game.HalfMoves++;
        game.FenHistory.Add(FenHelper.BuildFen(game, game.Board));
        game.MoveHistory.Add(move.Notation);

        if (piece.PieceType == PieceType.PAWN || move.IsCapture)
        {
            game.HalfMoves = 0;
        }

        if (opponentColor == Color.WHITE)
        {
            game.FullMoves++;
        }

        if (IsCheckmate(game, move, opponentColor))
        {
            game.EndGame(GameStatus.CHECKMATE, game.ActiveColor);
        }

        if (game.HalfMoves == 100)
        {
            game.EndGame(GameStatus.DRAW_FIFTY_MOVE_RULE, null);
        }

        game.ActiveColor = opponentColor;
        game.UpdateValidMoves(opponentColor); // Update all valid moves for the next player

        if (IsStalemate(game, opponentColor))
        {
            game.EndGame(GameStatus.DRAW_STALEMATE, null);
        }

        if (IsInsufficientMaterial(game))
        {
            game.EndGame(GameStatus.DRAW_INSUFFICIENT_MATERIAL, null);
        }

        // Update material values
        game.WhiteMaterialValue = game.Board.TotalPieceValue(Color.WHITE);
        game.BlackMaterialValue = game.Board.TotalPieceValue(Color.BLACK);

        // Update last move timestamp
        game.LastMoveTimeStamp = DateTime.UtcNow;
    }
}
