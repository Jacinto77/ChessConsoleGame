using ChessMac.Board;
using ChessMac.ChessBoard;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

using static Board.ChessBoard;

// TODO implement castling

public class King : Piece
{
    private (int row, int col) _kingSideCastlePos;
    private (int row, int col) _queenSideCastlePos;

    public King(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.King;
        Icon = GetColorPieceIcon(inColor);
        SetCastlePositions(inColor);
    }

    public King(PieceColor inColor, PieceType inType) : base(inColor, inType)
    {
        SetCastlePositions(inColor);
    }

    public King(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
        bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened) : 
        base(inType, inColor, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened)
    {
        SetCastlePositions(inColor);
    }

    public override Piece Clone()
    {
        return new King(this.Type, this.Color, this.GetValidMoveList(), this.Icon, this.HasMoved, this.IsPinned,
            this.MoveCounter, this.IsThreatened);
    }
    /*
     * Check if king has moved, if yes, no castle
     * check if rook has moved, if yes no castle
     * check if pieces between have threat, if yes no castle
     * check if king is threatened, if yes no castle
     */

    public void SetCastlePositions(PieceColor inColor)
    {
        if (inColor == PieceColor.White)
        {
            _kingSideCastlePos = (7, 6);
            _queenSideCastlePos = (7, 2);
        }
        else
        {
            _kingSideCastlePos = (0, 6);
            _queenSideCastlePos = (0, 2);
        }
    }

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
    public bool CheckQueenSideCastle(PieceColor inColor, Board.ChessBoard inBoard)
    {
        var queenSideRookPos = GetRookPos(inColor, inBoard)[1];
        var queenSideRook = inBoard.BoardPieces[queenSideRookPos.row, queenSideRookPos.col];
        if (queenSideRook?.Type != PieceType.Rook || queenSideRook.HasMoved) return false;

        var queenSpacesToCheck = GetQueenSideSpaces();
        foreach (var space in queenSpacesToCheck)
        {
            var tempPiece = inBoard.BoardPieces[space.row, space.col];
            if (tempPiece is null) continue;
            if (tempPiece.IsThreatened) return false;
        }

        return true;
    }


    // checks if king side rook is a valid castle
    // returns bool
    // used by main checkCastle function
    public bool CheckKingSideCastle(PieceColor inColor, Board.ChessBoard inBoard)
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
    public List<(int row, int col)> GetRookPos(PieceColor inColor, Board.ChessBoard inBoard)
    {
        (int row, int col) kingSideRookPos;
        (int row, int col) queenSideRookPos;

        if (inColor == PieceColor.White)
        {
            kingSideRookPos = (7, 7);
            queenSideRookPos = (7, 0);
        }
        else
        {
            kingSideRookPos = (0, 7);
            queenSideRookPos = (0, 0);
        }

        return new List<(int row, int col)> { kingSideRookPos, queenSideRookPos };
    }

    // checks if king can castle both to king and queen side rooks
    // returns pair of bools
    // to be used by generate valid moves to add the castle movements
    public (bool king, bool queen) CheckCastle(PieceColor inColor, Board.ChessBoard inBoard)
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
    
    public override void GenerateValidMoves(Board.ChessBoard inBoard, int currentRow, int currentCol)
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
            if (IsWithinBoard(move) == false) continue;

            var destSpace = inBoard.BoardPieces[move.row, move.col];
            if (destSpace?.Icon != EmptySpaceIcon)
                if (destSpace?.Color == Color)
                    continue;
            if (destSpace is { IsThreatened: true }) continue;

            AddValidMove(move);
        }

        (bool kingSide, bool QueenSide) canCastle = CheckCastle(Color, inBoard);

        if (canCastle.kingSide) AddValidMove(_kingSideCastlePos);
        if (canCastle.QueenSide) AddValidMove(_queenSideCastlePos);
    }
}