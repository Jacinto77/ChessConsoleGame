namespace ChessMac;

public class Bishop : Piece
{
    public Bishop(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Bishop;
        Icon = GetColorPieceIcon(inColor);
    }

    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        GenerateBishopMoves(inBoard, currentRow, currentCol);
    }
}