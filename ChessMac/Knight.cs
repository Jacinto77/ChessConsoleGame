using System.Reflection.Metadata.Ecma335;

namespace ChessMac;
using static ChessMac.Program;
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
        : base(color, name, icon)
    {
        this.Type = type;
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        base.GenerateValidMoves(inBoard);
        /* Gen possible moves
         * check if move is out of bounds
         * check if move is to a piece with the same color
         * update valid moves with remaining moves
         */
        
        
        int currentCol = this.GetPosition().Col;
        int currentRow = this.GetPosition().Row;
        
        Console.WriteLine("Current Column:" + currentCol);
        Console.WriteLine("Current Row: " + currentRow);

        List<Space.Position> validPositions = new List<Space.Position>
        {
            new Space.Position(currentRow + 2, currentCol + 1),
            new Space.Position(currentRow + 2, currentCol - 1),
            new Space.Position(currentRow - 2, currentCol + 1),
            new Space.Position(currentRow - 2, currentCol - 1),
            new Space.Position(currentRow + 1, currentCol + 2),
            new Space.Position(currentRow + 1, currentCol - 2),
            new Space.Position(currentRow - 1, currentCol + 2),
            new Space.Position(currentRow - 1, currentCol - 2)
        };

        // check if generated move's destination contains a piece of the same color
        int counter1 = 0;
        foreach (var move in validPositions)
        {
            if (Space.IsValidPosition(move) == false)
            {
                continue;
            }
            
            // check if move's dest space has a piece of the same color
            Space destSpace = inBoard.BoardSpaces[move.Row, move.Col];
            // Console.WriteLine(destSpace.HasPiece);
            if (destSpace.HasPiece == true)
            {
                if (destSpace.Piece?.Color == Color)
                {
                    continue;
                }
            }
            
            ValidMoves.Add(destSpace);
        }
    }
}