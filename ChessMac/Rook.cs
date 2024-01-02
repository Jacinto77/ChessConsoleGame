namespace ChessMac;

// TODO implement castling
public class Rook : Piece
{
    public (int row, int col) CastlePos;

    public Rook(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Rook;
        Icon = GetColorPieceIcon(inColor);
    }

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
        GenerateRookMoves(inBoard, currentRow, currentCol);
    }
}