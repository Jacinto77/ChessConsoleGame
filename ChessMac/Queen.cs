namespace ChessMac;

// queen movement is a combination of bishop and rook movement
// any space diagonally or vertically/horizontally

public class Queen : Piece
{
    public Queen(PieceColor color, string name, char icon, PieceType type) 
        : base(color, name, icon)
    {
        this.Type = type;
    }

    public Queen(PieceColor color, PieceType type) : base(color, type)
    {
    }

    // TODO make methods into extensions
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        ValidMoves.Clear();
        base.GenerateRookMoves(inBoard);
        base.GenerateBishopMoves(inBoard);
    }
}