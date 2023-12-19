namespace ChessMac;

// queen movement is a combination of bishop and rook movement
// any space diagonally or vertically/horizontally

public class Queen : Piece
{
    public Queen(PieceColor inColor) : base(inColor)
    {
        this.Type = PieceType.Queen;
        this.Icon = GetColorPieceIcon(inColor);
    }

    // TODO make methods into extensions
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        ClearValidMoves();
        base.GenerateRookMoves(inBoard, currentRow, currentCol);
        // TODO: rook moves are being cleared from the list of valid moves or GenBishopMoves is overwriting them?
        // TODO: perhaps the deepcopy isn't working properly for the pieces?
        base.GenerateBishopMoves(inBoard, currentRow, currentCol);
    }
}