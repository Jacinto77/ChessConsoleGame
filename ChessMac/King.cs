namespace ChessMac;

using static ChessBoard;

// TODO implement castling

public class King : Piece
{
    private readonly (int row, int col) KingSideCastlePos;
    private readonly (int row, int col) QueenSideCastlePos;

    public King(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.King;
        Icon = GetColorPieceIcon(inColor);
        if (inColor == PieceColor.White)
        {
            KingSideCastlePos = (7, 6);
            QueenSideCastlePos = (7, 2);
        }
        else
        {
            KingSideCastlePos = (0, 6);
            QueenSideCastlePos = (0, 2);
        }
    }

    /*
     * Check if king has moved, if yes, no castle
     * check if rook has moved, if yes no castle
     * check if pieces between have threat, if yes no castle
     * check if king is threatened, if yes no castle
     */

    // returns the spaces to be checked for threat to validate castle
    public List<(int row, int col)> GetKingSideSpaces()
    {
        List<(int row, int col)> spacesToCheck;
        if (Color == PieceColor.White)
            spacesToCheck = new List<(int row, int col)>
            {
                (7, 1),
                (7, 2)
            };
        else
            spacesToCheck = new List<(int row, int col)>
            {
                (0, 1),
                (0, 2)
            };

        return spacesToCheck;
    }

    // returns the spaces to be checked for threat to validate castle
    public List<(int row, int col)> GetQueenSideSpaces()
    {
        List<(int row, int col)> spacesToCheck;
        if (Color == PieceColor.White)
            spacesToCheck = new List<(int row, int col)>
            {
                (7, 4),
                (7, 5),
                (7, 6)
            };
        else
            spacesToCheck = new List<(int row, int col)>
            {
                (0, 4),
                (0, 5),
                (0, 6)
            };

        return spacesToCheck;
    }

    // checks if queen side rook is a valid castle
    // returns bool
    // used by main checkCastle function
    public bool CheckQueenSideCastle(PieceColor inColor, ChessBoard inBoard)
    {
        var QueenSideRookPos = GetRookPos(inColor, inBoard)[1];
        var queenSideRook = inBoard.BoardPieces[QueenSideRookPos.row, QueenSideRookPos.col];
        if (queenSideRook?.Type != PieceType.Rook || queenSideRook.HasMoved) return false;

        var queenSpacesToCheck = GetQueenSideSpaces();
        foreach (var space in queenSpacesToCheck)
        {
            var tempPiece = inBoard.BoardPieces[space.row, space.col];
            if (tempPiece?.Icon != EmptySpaceIcon || tempPiece.IsThreatened) return false;
        }

        return true;
    }


    // checks if king side rook is a valid castle
    // returns bool
    // used by main checkCastle function
    public bool CheckKingSideCastle(PieceColor inColor, ChessBoard inBoard)
    {
        var kingSideRookPos = GetRookPos(inColor, inBoard)[0];
        var kingSideRook = inBoard.BoardPieces[kingSideRookPos.row, kingSideRookPos.col];
        if (kingSideRook?.Type != PieceType.Rook || kingSideRook.HasMoved) return false;

        var kingSpacesToCheck = GetKingSideSpaces();
        foreach (var space in kingSpacesToCheck)
        {
            var tempPiece = inBoard.BoardPieces[space.row, space.col];
            if (tempPiece?.Icon != EmptySpaceIcon || tempPiece.IsThreatened) return false;
        }

        return true;
    }

    // gets k and q side rooks positions based on color
    // used by k+q side castle checking
    public List<(int row, int col)> GetRookPos(PieceColor inColor, ChessBoard inBoard)
    {
        (int row, int col) KingSideRookPos;
        (int row, int col) QueenSideRookPos;

        if (inColor == PieceColor.White)
        {
            KingSideRookPos = (7, 7);
            QueenSideRookPos = (7, 0);
        }
        else
        {
            KingSideRookPos = (0, 7);
            QueenSideRookPos = (0, 0);
        }

        return new List<(int row, int col)> { KingSideRookPos, QueenSideRookPos };
    }

    // checks if king can castle both to king and queen side rooks
    // returns pair of bools
    // to be used by generate valid moves to add the castle movements
    public (bool king, bool queen) CheckCastle(PieceColor inColor, ChessBoard inBoard)
    {
        var canKingCastle = false;
        var canQueenCastle = false;
        // checks if king has moved
        if (HasMoved) return (false, false);

        // checks if kingside rook is a valid castle
        if (CheckKingSideCastle(inColor, inBoard)) canKingCastle = true;

        // checks if queenside rook is a valid castle
        if (CheckQueenSideCastle(inColor, inBoard)) canQueenCastle = true;

        return (canKingCastle, canQueenCastle);
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        List<(int row, int col)> tempMoves = CreateList(
            (currentRow + 1, currentCol),
            (currentRow + 1, currentCol + 1),
            (currentRow + 1, currentCol - 1),
            (currentRow, currentCol + 1),
            (currentRow, currentCol - 1),
            (currentRow - 1, currentCol),
            (currentRow - 1, currentCol + 1),
            (currentRow - 1, currentCol - 1)
        );

        foreach (var move in tempMoves)
        {
            if (IsWithinBoard(move.row, move.col) == false) continue;

            var destSpace = inBoard.BoardPieces[move.row, move.col];
            if (destSpace?.Icon != EmptySpaceIcon)
                if (destSpace?.Color == Color)
                    continue;
            if (destSpace is { IsThreatened: true }) continue;

            AddValidMove(move);
        }

        (bool kingSide, bool QueenSide) canCastle = CheckCastle(Color, inBoard);

        if (canCastle.kingSide) AddValidMove(KingSideCastlePos);
        if (canCastle.QueenSide) AddValidMove(QueenSideCastlePos);
    }
}