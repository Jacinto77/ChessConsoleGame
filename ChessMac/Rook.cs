namespace ChessMac;

// TODO implement castling
public class Rook : Piece
{
    public Rook(PieceColor inColor) : base(inColor)
    {
        this.Type = PieceType.Rook;
        this.Icon = GetColorPieceIcon(inColor);
    }

    public (int row, int col) CastlePos;
    
    public void AssignCastlePos((int row, int col) rookPos)
    {
        CastlePos = rookPos;
    }

    public bool CanCastle(PieceColor inColor, ChessBoard inBoard)
    {
        return !HasMoved;
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        base.GenerateRookMoves(inBoard, currentRow, currentCol);
    }
}