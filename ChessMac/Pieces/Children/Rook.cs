using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

// TODO implement castling
public class Rook : Piece
{
    public (int row, int col) CastlePos;

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
        return new Rook(Color, Position, Type, GetValidMoveList(), Icon, HasMoved, IsPinned,
            MoveCounter, IsThreatened);
    }

    public void AssignCastlePos()
    {
        switch (this.Position)
        {
            case (0, 0): 
                CastlePos = (0, 3);
                break;
            case (0, 7): 
                CastlePos = (0, 5);
                break;
            case (7, 0): 
                CastlePos = (7, 3);
                break;
            case (7, 7):
                CastlePos = (7, 5);
                break;
            default:
                CastlePos = (-1, -1);
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

    public override void PrintAttributes()
    {
        base.PrintAttributes();
        Console.WriteLine($"Castling Move Position: {CastlePos}");
        Console.WriteLine($"Can Castle: {CanCastle()}");
    }
}