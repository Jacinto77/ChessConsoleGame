namespace ChessMac;

using static ChessBoard;

public class Knight : Piece
{
    public Knight(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Knight;
        Icon = GetColorPieceIcon(inColor);
    }

    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        List<(int row, int col)> tempMoves = CreateList(
            (currentRow + 2, currentCol + 1),
            (currentRow + 2, currentCol - 1),
            (currentRow - 2, currentCol + 1),
            (currentRow - 2, currentCol - 1),
            (currentRow + 1, currentCol + 2),
            (currentRow + 1, currentCol - 2),
            (currentRow - 1, currentCol + 2),
            (currentRow - 1, currentCol - 2)
        );

        foreach (var (row, col) in tempMoves)
        {
            if (IsWithinBoard(row, col) == false) continue;

            var destSpace = inBoard.BoardPieces[row, col];

            if (destSpace?.Icon != EmptySpaceIcon)
                if (destSpace?.Color == Color)
                    continue;

            AddValidMove((row, col));
        }
    }
}