namespace ChessMac;

public class King : Piece
{
    public King(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }

    public King(string color, string type) : base(color, type)
    {
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        int currentRow = this.GetPosition().RowIndex;
        int currentCol = this.GetPosition().ColIndex;
        
        List<Space.Position> tempMoves = new List<Space.Position>
        {
            new(currentRow + 1, currentCol),
            new(currentRow + 1, currentCol + 1),
            new(currentRow + 1, currentCol - 1),
            new(currentRow, currentCol + 1),
            new(currentRow, currentCol - 1),
            new(currentRow - 1, currentCol),
            new(currentRow - 1, currentCol + 1),
            new(currentRow - 1, currentCol - 1)
        };

        foreach (var move in tempMoves)
        {
            if (Space.IsWithinBoard(move) == false) continue;

            Space destSpace = inBoard.GetSpace(move.RowIndex, move.ColIndex);
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