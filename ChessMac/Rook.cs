namespace ChessMac;

// TODO implement castling
public class Rook : Piece
{
    public Rook(PieceColor inColor) : base(inColor)
    {
        this.Type = PieceType.Rook;
        this.Icon = GetColorPieceIcon(inColor);
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        ClearValidMoves();
        base.GenerateRookMoves(inBoard, currentRow, currentCol);
    }
}