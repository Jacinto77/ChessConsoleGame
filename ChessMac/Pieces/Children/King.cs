using System.Data;
using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

using static ChessBoard;

// TODO finish testing castling
// TODO Add check for move generation for if the move is threatened or not (? maybe not though as it could get confusing to sync move generation with counting up threatened spaces pre-move)

public class King : Piece
{
    private (int row, int col) _kingSideCastlePos;
    private (int row, int col) _queenSideCastlePos;

    public (int row, int col) KingSideRookPos { get; private set; }
    public (int row, int col) QueenSideRookPos { get; private set; }

    private List<(int row, int col)> _kingSpacesCheckToCastle = new();
    private List<(int row, int col)> _queenSpacesCheckToCastle = new();

    private bool _canCastleKingSide = false;
    private bool _canCastleQueenSide = false;

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

    public override King Clone()
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
            KingSideRookPos = KingSideRookPos,
            QueenSideRookPos = QueenSideRookPos
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
            KingSideRookPos = (7, 7);
            QueenSideRookPos = (7, 0);
        }
        else
        {
            KingSideRookPos = (0, 7);
            QueenSideRookPos = (0, 0);
        }
    }

    // returns the spaces to be checked for threat to validate castle
    private void AddKingSideSpaces()
    {
        if (Color == PieceColor.White)
        {
            _kingSpacesCheckToCastle.Add((7, 5));
            _kingSpacesCheckToCastle.Add((7, 6));
        }
        else
        {
            _kingSpacesCheckToCastle.Add((0, 5));
            _kingSpacesCheckToCastle.Add((0, 6));
        }
    }

    // returns the spaces to be checked for threat to validate castle
    private void AddQueenSideSpaces()
    {
        if (Color == PieceColor.White)
        {
            _queenSpacesCheckToCastle.Add((7, 1));
            _queenSpacesCheckToCastle.Add((7, 2));
            _queenSpacesCheckToCastle.Add((7, 3));
        }
        else
        {
            _queenSpacesCheckToCastle.Add((0, 1));
            _queenSpacesCheckToCastle.Add((0, 2));
            _queenSpacesCheckToCastle.Add((0, 3));
        }
    }

    private void AddSpacesToCheckForCastle()
    {
        _queenSpacesCheckToCastle.Clear();
        AddQueenSideSpaces();
        
        _kingSpacesCheckToCastle.Clear();
        AddKingSideSpaces();
    }

    private bool CheckCanCastle(PieceType pieceType, ChessBoard inBoard)
    {
        if (HasMoved == true || MoveCounter > 0) return false;
        
        Piece? rook;
        List<(int row, int col)> spacesToCheck;
        if (pieceType == PieceType.King)
        {
            rook = inBoard.GetPieceByIndex(KingSideRookPos);
            spacesToCheck = _kingSpacesCheckToCastle;
        }
        else
        {
            rook = inBoard.GetPieceByIndex(QueenSideRookPos);
            spacesToCheck = _queenSpacesCheckToCastle;
        }
        
        if (rook?.Type != PieceType.Rook || rook.HasMoved) 
            return false;

        var threats = inBoard.ThreatenedSpaces;
        //var threats = Color == PieceColor.Black ? inBoard.WhiteThreats : inBoard.BlackThreats;
        foreach (var space in spacesToCheck)
        {
            //var tempPiece = inBoard.GetPieceByIndex(space);
            if (threats.TryGetValue(space, out List<Piece>? threateningPieces))
                return false;
            if (inBoard.PiecePositions.TryGetValue(space, out var blockingPiece))
                return false;
        }

        return true;
    }
    
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
        Console.WriteLine($"Rook king side Position: {KingSideRookPos}");
        Console.WriteLine($"Rook queen side Position: {QueenSideRookPos}");
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

    public override (int row, int col) GetRookPos(PieceType inType)
    {
        switch (inType)
        {
            case PieceType.King:
                return KingSideRookPos;
            case PieceType.Queen:
                return QueenSideRookPos;
            default:
                return (-1, -1);
        }
    }
}