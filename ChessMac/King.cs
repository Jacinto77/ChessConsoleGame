namespace ChessMac;
using static ChessBoard;
// TODO implement castling

public class King : Piece
{
    public King(PieceColor inColor) : base(inColor)
    {
        this.Type = PieceType.King;
        this.Icon = GetColorPieceIcon(inColor);
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        ClearValidMoves();
        
        List<(int row, int col)> tempMoves = CreateList(
            (currentRow + 1, currentCol), 
            (currentRow + 1, currentCol + 1), 
            (currentRow + 1, currentCol - 1), 
            (currentRow, currentCol + 1), 
            (currentRow, currentCol - 1), 
            (currentRow - 1, currentCol),
            (currentRow - 1, currentCol + 1),
            (currentRow - 1, currentCol - 1)
        );

        foreach (var move in tempMoves)
        {
            if (IsWithinBoard(move.row, move.col) == false) continue;

            Piece? destSpace = inBoard.BoardPieces[move.row, move.col];
            if (destSpace is not null)
            {
                if (destSpace.Color == this.Color)
                {
                    continue;
                }
            }
            AddValidMove(move);
        }
    }
}