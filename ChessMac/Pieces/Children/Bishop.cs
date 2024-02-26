using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

public class Bishop : Piece
{
    // public Bishop(PieceColor inColor) : base(inColor)
    // {
    //     Type = PieceType.Bishop;
    //     SetIconByColorAndType(inColor);
    // }
    //
    // public Bishop(PieceColor inColor, (int row, int col) inPosition) : base(inColor, inPosition)
    // {
    //     Type = PieceType.Bishop;
    //     SetIconByColorAndType(inColor);
    // }

    // public Bishop(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
    //     bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened, (int row, int col) inPosition) : 
    //     base(inColor, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened, inPosition) {}

    public Bishop(
        PieceColor inColor = default,
        (int row, int col) inPosition = default,
        PieceType inType = PieceType.Bishop,
        List<(int row, int col)>? inValidMoves = default,
        char? inIcon = default,
        bool inHasMoved = default,
        bool inIsPinned = default,
        int inMoveCounter = default,
        bool inIsThreatened = default) 
        : base(inColor, inPosition, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened)
    {
    }
    
    public override Piece Clone()
    {
        return new Bishop(
            Color, 
            Position,
            Type, 
            GetValidMoveList(), 
            Icon, 
            HasMoved, 
            IsPinned,
            MoveCounter, 
            IsThreatened);
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        GenerateBishopMoves(inBoard, Position.row, Position.col);
    }
    
}