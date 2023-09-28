namespace ChessMac;

public class Bishop : Piece
{
    public Bishop(PieceColor inColor) : base(inColor)
    {
        this.Type = PieceType.Bishop;
        this.Icon = GetColorPieceIcon(inColor);
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        ClearValidMoves();
        base.GenerateBishopMoves(inBoard, currentRow, currentCol);
    }
}