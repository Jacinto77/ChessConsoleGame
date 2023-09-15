namespace ChessMac;

// generation of valid moves is restricted to one direction at a time, and all positions until
// the edge of the board or a piece is reached
//
// either validMoves is currentPos[x, y] = x+/-n or y+/-n
// 

public class Rook : Piece
{
    public Rook(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }

    public Rook(string color, string type) : base(color, type)
    {
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        base.GenerateRookMoves(inBoard);
    }
}