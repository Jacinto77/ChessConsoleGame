namespace ChessMac;

// TODO implement castling

public class King : Piece
{
    public King(PieceColor color, string name, char icon, PieceType type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }

    public King(PieceColor color, PieceType type) : base(color, type)
    {
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        ValidMoves.Clear();
        int currentRow = RowIndex;
        int currentCol = ColIndex;
        
        List<Tuple<int, int>> tempMoves = new List<Tuple<int, int>>
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

            Space? destSpace = inBoard.GetSpace(move);
            if (destSpace.HasPiece == true)
            {
                if (destSpace.Piece?.Color == Color)
                {
                    continue;
                }
            }
            
            ValidMoves.Add(move);
        }

        // // reset pinned pieces before regenerating new
        // foreach (var pinnedPiece in PinnedPieces)
        // {
        //     pinnedPiece.IsPinned = false;
        // }
        // PinnedPieces.Clear();
        // ScanForPinnedPiece(inBoard);
    }
}