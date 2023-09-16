namespace ChessMac;

public class Bishop : Piece
{
    public Bishop(PieceColor color, string name, char icon, PieceType type) 
        : base(color, name, icon)
    {
        this.Type = type;
    }

    public Bishop(PieceColor color, PieceType type) : base(color, type)
    {
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        ValidMoves.Clear();
        base.GenerateBishopMoves(inBoard);
    }
}