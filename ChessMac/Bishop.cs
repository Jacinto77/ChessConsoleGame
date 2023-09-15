namespace ChessMac;

public class Bishop : Piece
{
    public Bishop(string color, string name, char icon, string type) 
        : base(color, name, icon)
    {
        this.Type = type;
    }

    public Bishop(string color, string type) : base(color, type)
    {
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        base.GenerateBishopMoves(inBoard);
    }
}