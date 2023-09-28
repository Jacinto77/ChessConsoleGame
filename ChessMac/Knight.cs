using System.Reflection.Metadata.Ecma335;

namespace ChessMac;
using static ChessBoard;

public class Knight : Piece
{
    public Knight(PieceColor inColor) : base(inColor)
    {
        this.Type = PieceType.Knight;
        this.Icon = GetColorPieceIcon(inColor);
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        ClearValidMoves();

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
            if (IsWithinBoard(row, col) == false)
            {
                continue;
            }
            
            // check if move's dest space has a piece of the same color
            Piece? destSpace = inBoard.BoardPieces[row, col];
            // Console.WriteLine(destSpace.HasPiece);
            if (destSpace is not null)
            {
                if (destSpace.Color == this.Color)
                {
                    continue;
                }
            }
            
            AddValidMove((row, col));
        }
    }
}