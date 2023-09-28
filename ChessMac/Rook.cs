namespace ChessMac;

//TODO implement castling
public class Rook : Piece
{
    public Rook(PieceColor color) : base()
    {
        this.Type = PieceType.Rook;
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        ValidMoves.Clear();
        base.GenerateRookMoves(inBoard, currentRow, currentCol);
    }
}