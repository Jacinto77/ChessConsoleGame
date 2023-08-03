namespace ChessMac;

// knight movement is in an L shape and is the only piece that can "jump" or "move through" other pieces
//
// using a 2 dimensional array for the board, movement for the knight could be thought of as:
// moving too indices in one direction (horizontal/vertical), then one index in the other direction
//
// something like all valid moves are currentpos[x, y] where x,y = x+/-2 && y+/-1
// 
// might be the easiest of them all to implement, however it must be remembered that 
// knights do not threaten each space in their movement path, only the exact position that
// they can move to
//

public class Knight : Piece
{
    public Knight(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }
}