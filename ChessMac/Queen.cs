namespace ChessMac;

// queen movement is a combination of bishop and rook movement
// any space diagonally or vertically/horizontally

public class Queen : Piece
{
    public Queen(PieceColor color)
    {
        this.Type = PieceType.Queen;
    }

    // TODO make methods into extensions
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        ValidMoves.Clear();
        base.GenerateRookMoves(inBoard, currentRow, currentCol);
        base.GenerateBishopMoves(inBoard, currentRow, currentCol);
    }
}