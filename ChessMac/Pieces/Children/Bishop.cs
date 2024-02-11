using ChessMac.Board;
using ChessMac.ChessBoard;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

public class Bishop : Piece
{
    public Bishop(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Bishop;
        Icon = GetColorPieceIcon(inColor);
    }

    public Bishop(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
        bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened) : 
        base(inType, inColor, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened) {}

    public override Piece Clone()
    {
        return new Bishop(this.Type, this.Color, this.GetValidMoveList(), this.Icon, this.HasMoved, this.IsPinned,
            this.MoveCounter, this.IsThreatened);
    }
    
    public override void GenerateValidMoves(Board.ChessBoard inBoard, int currentRow, int currentCol)
    {
        GenerateBishopMoves(inBoard, currentRow, currentCol);
    }
}