using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

// TODO implement castling
public class Rook : Piece
{
    private (int row, int col) _castlePos;

    // public Rook(PieceColor inColor) : base(inColor)
    // {
    //     Type = PieceType.Rook;
    //     SetIconByColorAndType(inColor);
    // }
    //
    // public Rook(PieceColor inColor, (int row, int col) inPosition) : base(inColor, inPosition)
    // {
    //     Type = PieceType.Rook;
    //     SetIconByColorAndType(inColor);
    // }
    public Rook(
        PieceColor inColor = default,
        (int row, int col) inPosition = default,
        PieceType inType = PieceType.Rook,
        List<(int row, int col)>? inValidMoves = default,
        char? inIcon = default,
        bool inHasMoved = default,
        bool inIsPinned = default,
        int inMoveCounter = default,
        bool inIsThreatened = default) 
        : base(inColor, inPosition, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened)
    {
        if (inPosition != (-1, -1)) AssignCastlePos();
    }
    
    // public Rook(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
    //     bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened, (int row, int col) inPosition) : 
    //     base(inColor, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened, inPosition)
    // {
    // }

    public override Piece Clone()
    {
        var tempRook = new Rook(Color, Position, Type, GetValidMoveList(), Icon, HasMoved, IsPinned,
            MoveCounter, IsThreatened);
        tempRook._castlePos = _castlePos;

        return tempRook;
    }

    public void AssignCastlePos()
    {
        switch (this.Position)
        {
            case (0, 0): 
                _castlePos = (0, 3);
                break;
            case (0, 7): 
                _castlePos = (0, 5);
                break;
            case (7, 0): 
                _castlePos = (7, 3);
                break;
            case (7, 7):
                _castlePos = (7, 5);
                break;
            default:
                _castlePos = (-1, -1);
                break;

        }
    }

    public bool CanCastle()
    {
        return !HasMoved;
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        GenerateRookMoves(inBoard, Position.row, Position.col);
    }

    public override (int row, int col) GetCastlePos()
    {
        return _castlePos;
    }

    public override void PrintAttributes()
    {
        base.PrintAttributes();
        Console.WriteLine($"Castling Move Position: {_castlePos}");
        Console.WriteLine($"Can Castle: {CanCastle()}");
    }
}