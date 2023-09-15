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

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        base.GenerateValidMoves(inBoard);
        // scan spaces between startSpace and possible destinations
        // walk spaces from start to edges of board
        // if space.HasPiece == True, stop

        int currentCol = GetPosition().ColIndex;
        int currentRow = GetPosition().RowIndex;

        // subtract current position from 7 (last index of board array) to find boundaries
        int rangeLeft = currentCol + 1;
        int rangeRight = 7 - currentCol + 1;
        int rangeUp = currentRow + 1;
        int rangeDown = 7 - currentRow + 1;

        //scan up
        for (int i = 1; i < rangeUp; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        //scan down
        for (int i = 1; i < rangeDown; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        //scan left
        for (int i = 1; i < rangeLeft; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow, currentCol - i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        //scan right
        for (int i = 1; i < rangeRight; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow, currentCol + i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }

        int counter = 1;
        foreach (var move in ValidMoves)
        {
            Console.WriteLine(counter);
            Console.WriteLine(move.Col + move.Row.ToString());
            counter++;
        }
    }
}