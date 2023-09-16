namespace ChessMac;

// generation of valid moves is restricted to one direction at a time, and all positions until
// the edge of the board or a piece is reached
//
// either validMoves is currentPos[x, y] = x+/-n or y+/-n
// 

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