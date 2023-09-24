namespace ChessMac;

//TODO implement castling
public class Rook : Piece
{
    public Rook(PieceColor color, string name, char icon, PieceType type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }

    public Rook(PieceColor color, PieceType type) : base(color, type)
    {
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        ValidMoves.Clear();
        base.GenerateRookMoves(inBoard);
    }
}