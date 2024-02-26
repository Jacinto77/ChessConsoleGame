using System.Data;
using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

using static ChessBoard;

// TODO implement castling

public class King : Piece
{
    private (int row, int col) _kingSideCastlePos;
    private (int row, int col) _queenSideCastlePos;
    
    private (int row, int col) _kingSideRookPos;
    private (int row, int col) _queenSideRookPos;

    private List<(int row, int col)> _kingSpacesCheckToCastle = new();
    private List<(int row, int col)> _queenSpacesCheckToCastle = new();

    private bool _canCastleKingSide;
    private bool _canCastleQueenSide;

    // public King(PieceColor inColor) : base(inColor)
    // {
    //     Type = PieceType.King;
    //     SetIconByColorAndType(inColor);
    //     SetCastlePositions(inColor);
    // }
    //
    // public King(PieceColor inColor, (int row, int col) inPosition) : base(inColor, inPosition)
    // {
    //     Type = PieceType.King;
    //     SetIconByColorAndType(inColor);
    //     SetCastlePositions(inColor);
    // }

    public King(
        PieceColor inColor = default,
        (int row, int col) inPosition = default,
        PieceType inType = PieceType.King,
        List<(int row, int col)>? inValidMoves = default,
        char? inIcon = default,
        bool inHasMoved = default,
        bool inIsPinned = default,
        int inMoveCounter = default,
        bool inIsThreatened = default
        ) 
        : base(inColor, inPosition, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened)
    {
        SetCastlePositions();
        SetRookPositions();
        AddSpacesToCheckForCastle();
    }
    
    // public King(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
    //     bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened, (int row, int col) inPosition) : 
    //     base(inColor, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened, inPosition)
    // {
    //     SetCastlePositions(inColor);
    // }

    public override Piece Clone()
    {
        var tempPiece = new King(Color, Position, Type, GetValidMoveList(), Icon, HasMoved, IsPinned,
            MoveCounter, IsThreatened)
        {
            _kingSideCastlePos = _kingSideCastlePos,
            _queenSideCastlePos = _queenSideCastlePos,
            _canCastleKingSide = _canCastleKingSide,
            _canCastleQueenSide = _canCastleQueenSide,
            _kingSpacesCheckToCastle = _kingSpacesCheckToCastle,
            _queenSpacesCheckToCastle = _queenSpacesCheckToCastle,
            _kingSideRookPos = _kingSideRookPos,
            _queenSideRookPos = _queenSideRookPos
        };

        return tempPiece;
    }

    private void SetCastlePositions()
    {
        if (Color == PieceColor.White)
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

    private void SetRookPositions()
    {
        if (Color == PieceColor.White)
        {
            _kingSideRookPos = (7, 7);
            _queenSideRookPos = (7, 0);
        }
        else
        {
            _kingSideRookPos = (0, 7);
            _queenSideRookPos = (0, 0);
        }
    }

    // returns the spaces to be checked for threat to validate castle
    private void AddKingSideSpaces()
    {
        if (Color == PieceColor.White)
        {
            _kingSpacesCheckToCastle.Add((7, 1));
            _kingSpacesCheckToCastle.Add((7, 2));
        }
        else
        {
            _kingSpacesCheckToCastle.Add((0, 1));
            _kingSpacesCheckToCastle.Add((0, 2));
        }
    }

    // returns the spaces to be checked for threat to validate castle
    private void AddQueenSideSpaces()
    {
        if (Color == PieceColor.White)
        {
            _queenSpacesCheckToCastle.Add((7, 4));
            _queenSpacesCheckToCastle.Add((7, 5));
            _queenSpacesCheckToCastle.Add((7, 6));
        }
        else
        {
            _queenSpacesCheckToCastle.Add((0, 4));
            _queenSpacesCheckToCastle.Add((0, 5));
            _queenSpacesCheckToCastle.Add((0, 6));
        }
    }

    private void AddSpacesToCheckForCastle()
    {
        _queenSpacesCheckToCastle.Clear();
        AddQueenSideSpaces();
        
        _kingSpacesCheckToCastle.Clear();
        AddKingSideSpaces();
    }

    // public void CheckAndAddCastle(ChessBoard inBoard)
    // {
    //     var threats = Color == PieceColor.Black ? inBoard.WhiteThreats : inBoard.BlackThreats;
    //     if (HasMoved) _canCastleKingSide = false;
    //     
    //     var kingSideRook = inBoard.GetPieceByIndex(_kingSideRookPos);
    //     var queenSideRook = inBoard.GetPieceByIndex(_queenSideRookPos);
    //
    //     if (kingSideRook is not null && kingSideRook is { Type: PieceType.Rook, HasMoved: false })
    //     {
    //         foreach (var space in _kingSpacesCheckToCastle)
    //         {
    //             if (threats[space].Count > 0 || inBoard.GetPieceByIndex(space) is not null)
    //                 break;
    //         }
    //         
    //     }
    //         
    //     
    //     if (queenSideRook is not null && queenSideRook is { Type: PieceType.Rook, HasMoved: false })
    //         foreach (var space in _queenSpacesCheckToCastle)
    //         {
    //             if (threats[space].Count > 0)
    //                 _canCastleQueenSide = false;
    //             if (inBoard.GetPieceByIndex(space) is not null)
    //                 _canCastleQueenSide = false;
    //         }
    // }
    
    // checks if queen side rook is a valid castle
    // returns bool
    // used by main checkCastle function
    // public bool CheckQueenSideCastle(ChessBoard inBoard)
    // {
    //     var threats = Color == PieceColor.Black ? inBoard.WhiteThreats : inBoard.BlackThreats;
    //     var queenSideRook = inBoard.GetPieceByIndex(_queenSideRookPos);
    //     if (queenSideRook?.Type != PieceType.Rook || queenSideRook.HasMoved) 
    //         return false;
    //     
    //     foreach (var space in _queenSpacesCheckToCastle)
    //     {
    //         var tempPiece = inBoard.GetPieceByIndex(space);
    //         if (threats[space].Count > 0)
    //             return false;
    //         if (tempPiece is not null)
    //             return false;
    //     }
    //
    //     return true;
    // }

    private bool CheckCanCastle(PieceType pieceType, ChessBoard inBoard)
    {
        if (HasMoved == true || MoveCounter > 0) return false;
        Piece? rook;
        List<(int row, int col)> spacesToCheck;
        
        if (pieceType == PieceType.King)
        {
            rook = inBoard.GetPieceByIndex(_kingSideRookPos);
            spacesToCheck = _kingSpacesCheckToCastle;
        }
        else
        {
            rook = inBoard.GetPieceByIndex(_queenSideRookPos);
            spacesToCheck = _kingSpacesCheckToCastle;
        }
        
        if (rook?.Type != PieceType.Rook || rook.HasMoved) 
            return false;
        
        var threats = Color == PieceColor.Black ? inBoard.WhiteThreats : inBoard.BlackThreats;
        foreach (var space in spacesToCheck)
        {
            //var tempPiece = inBoard.GetPieceByIndex(space);
            if (threats.TryGetValue(space, out List<Piece>? tempPiece))
                return false;
            if (tempPiece is not null)
                return false;
        }

        return true;
    }


    // checks if king side rook is a valid castle
    // returns bool
    // used by main checkCastle function
    // public bool CheckKingSideCastle(PieceColor inColor, ChessBoard inBoard)
    // {
    //     var kingSideRookPos = GetRookPos(inColor, inBoard)[0];
    //     var kingSideRook = inBoard.BoardPieces[kingSideRookPos.row, kingSideRookPos.col];
    //     if (kingSideRook?.Type != PieceType.Rook || kingSideRook.HasMoved) return false;
    //
    //     var kingSpacesToCheck = GetKingSideSpaces();
    //     foreach (var space in kingSpacesToCheck)
    //     {
    //         var tempPiece = inBoard.BoardPieces[space.row, space.col];
    //         if (tempPiece?.Icon != EmptySpaceIcon || tempPiece.IsThreatened) return false;
    //     }
    //
    //     return true;
    // }

    // gets k and q side rooks positions based on color
    // used by k+q side castle checking
    // public List<(int row, int col)> GetRookPos(PieceColor inColor, ChessBoard inBoard)
    // {
    //     (int row, int col) kingSideRookPos;
    //     (int row, int col) queenSideRookPos;
    //
    //     if (inColor == PieceColor.White)
    //     {
    //         kingSideRookPos = (7, 7);
    //         queenSideRookPos = (7, 0);
    //     }
    //     else
    //     {
    //         kingSideRookPos = (0, 7);
    //         queenSideRookPos = (0, 0);
    //     }
    //
    //     return new List<(int row, int col)> { kingSideRookPos, queenSideRookPos };
    // }

    // checks if king can castle both to king and queen side rooks
    // returns pair of bools
    // to be used by generate valid moves to add the castle movements
    // public (bool king, bool queen) CheckCastle(PieceColor inColor, ChessBoard inBoard)
    // {
    //     if (HasMoved) return (false, false);
    //     
    //     var canKingCastle = CheckKingSideCastle(inColor, inBoard);
    //     var canQueenCastle = CheckQueenSideCastle(inColor, inBoard);
    //
    //     return (canKingCastle, canQueenCastle);
    // }
    
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        List<(int row, int col)> tempMoves = new List<(int row, int col)>
        {
            (Position.row + 1, Position.col),
            (Position.row + 1, Position.col + 1),
            (Position.row + 1, Position.col - 1),
            (Position.row,     Position.col + 1),
            (Position.row,     Position.col - 1),
            (Position.row - 1, Position.col),
            (Position.row - 1, Position.col + 1),
            (Position.row - 1, Position.col - 1)
        };

        foreach (var move in tempMoves)
        {
            if (IsWithinBoard(move) == false) continue;

            var destSpace = inBoard.BoardSpaces[move.row, move.col];
            if (destSpace is not null)
                if (destSpace.Color == Color)
                    continue;
            if (destSpace is { IsThreatened: true }) continue;

            AddValidMove(move);
        }

        _canCastleKingSide = CheckCanCastle(PieceType.King, inBoard);
        _canCastleQueenSide = CheckCanCastle(PieceType.Queen, inBoard);

        if (_canCastleKingSide) AddValidMove(_kingSideCastlePos);
        if (_canCastleQueenSide) AddValidMove(_queenSideCastlePos);
    }

    public override void PrintAttributes()
    {
        base.PrintAttributes();
        Console.WriteLine($"Castle Position king side: {_kingSideCastlePos}");
        Console.WriteLine($"Castle Position queen side: {_queenSideCastlePos}");
        Console.WriteLine($"Rook king side Position: {_kingSideRookPos}");
        Console.WriteLine($"Rook queen side Position: {_queenSideRookPos}");
        Console.WriteLine($"Can castle king side: {_canCastleKingSide}");
        Console.WriteLine($"Can castle queen side: {_canCastleQueenSide}");
        Console.WriteLine($"King side spaces to check: ");
        foreach (var space in _kingSpacesCheckToCastle)
        {
            Console.WriteLine(space);
        }
        Console.WriteLine($"Queen side spaces to check: ");
        foreach (var space in _queenSpacesCheckToCastle)
        {
            Console.WriteLine(space);
        }
        
    }

    public override void Move((int row, int col) inPosition)
    {
        base.Move(inPosition);
        _canCastleKingSide = false;
        _canCastleQueenSide = false;
    }
}