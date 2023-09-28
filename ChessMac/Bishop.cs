namespace ChessMac;

public class Bishop : Piece
{
    public Bishop(PieceColor inColor)
    {
        this.Type = PieceType.Bishop;
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        ValidMoves.Clear();
        base.GenerateBishopMoves(inBoard, currentRow, currentCol);
    }
}