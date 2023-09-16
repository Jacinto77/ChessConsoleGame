using System.Reflection.Metadata.Ecma335;

namespace ChessMac;
using static ChessMac.Program;

public class Knight : Piece
{
    public Knight(PieceColor color, string name, char icon, PieceType type) 
        : base(color, name, icon)
    {
        this.Type = type;
    }

    public Knight(PieceColor color, PieceType type) : base(color, type)
    {
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        ValidMoves.Clear();
        int currentCol = this.GetPosition().ColIndex;
        int currentRow = this.GetPosition().RowIndex;
        
        //Console.WriteLine("Current Column:" + currentCol);
        //Console.WriteLine("Current Row: " + currentRow);

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
        
        foreach (var move in validPositions)
        {
            if (Space.IsWithinBoard(move) == false)
            {
                continue;
            }
            
            // check if move's dest space has a piece of the same color
            Space destSpace = inBoard.GetSpace(move);
            // Console.WriteLine(destSpace.HasPiece);
            if (destSpace.HasPiece == true)
            {
                if (destSpace.Piece?.Color == this.Color)
                {
                    destSpace.AddPieceToThreats(this);
                    continue;
                }
            }
            
            ValidMoves.Add(destSpace);
            destSpace.AddPieceToThreats(this);
        }
    }
}