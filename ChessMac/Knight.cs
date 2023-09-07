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

        Console.WriteLine("Before removing invalid moves");
        int counter3 = 1;
        foreach (var pos in validPositions)
        {
            Console.WriteLine(counter3);
            Console.WriteLine(ConvertIndicesToPos(inRow: pos.Row, inCol: pos.Col));
            Console.WriteLine();
            counter3 += 1;
        }


        List<int> invalidMoves = new List<int>();

        // check if generated move is out of bounds
        int counter = 0;
        foreach (var move in validPositions)
        {
            if (move.Row is > 7 or < 0)
                invalidMoves.Add(counter);
            if (move.Col is > 7 or < 0)
                invalidMoves.Add(counter);
            counter++;
        }

        // check if generated move's destination contains a piece of the same color
        int counter1 = 0;
        foreach (var move in validPositions)
        {
            Space destSpace = inBoard.BoardSpaces[move.Row, move.Col];
            
            if (destSpace.HasPiece == false) continue;
            if (destSpace.Piece?.Color == Color)
            {
                invalidMoves.Add(counter1);
            }

            counter1++;
        }

        for (int i = 0; i < invalidMoves.Count; i++)
        {
            validPositions[invalidMoves[i]] = new Space.Position(-1, -1);
        }

        
        foreach (var move in validPositions)
        {
            if (move.Row == -1 || move.Col == -1)
                continue;
            ValidMoves.Add(inBoard.BoardSpaces[move.Row, move.Col]);
        }
        
        Console.WriteLine("After removing invalid moves");
        foreach (var pos in ValidMoves)
        {
            Console.WriteLine("Col: " + pos.Col);
            Console.WriteLine("Row: " + pos.Row);
        }
    }
}