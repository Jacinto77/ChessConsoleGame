using System.Runtime.CompilerServices;

namespace ChessMac;
using static ChessBoard;

public class Pawn : Piece
{
    private readonly int direction;

    public Pawn(PieceColor color)
    {
        this.Type = PieceType.Pawn;
        if (color == PieceColor.Black) direction = 1;
        else direction = -1;
    }
    
    private void CheckAndAddDiagonal((int row, int col) diagonal, ChessBoard inBoard)
    {
        if (IsWithinBoard(diagonal.row, diagonal.col) && inBoard.BoardPieces[diagonal.row, diagonal.col] is not null
                                    && inBoard.BoardPieces[diagonal.row, diagonal.col]?.Color != Color)
        {
            ValidMoves.Add(diagonal);
        }
    }
    
    private void CheckEnPassant((int row, int col) horizontal, (int row, int col) diagonal, ChessBoard inBoard)
    {
        if (!IsWithinBoard(horizontal.row, horizontal.col)) return;
        
        Piece? horizPiece = inBoard.BoardPieces[horizontal.row, horizontal.col];
        Piece? diagPiece = inBoard.BoardPieces[diagonal.row, diagonal.col];
        
        if (horizPiece is null || horizPiece.Type != PieceType.Pawn) 
            return;
        if (horizPiece.MoveCounter != 1 || diagPiece is not null)
            return;

        ValidMoves.Add(horizontal);
        
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        base.GenerateValidMoves(inBoard, currentRow, currentCol);
        ValidMoves.Clear();

        (int row, int col) forwardOne = new (currentRow + (direction * 1), currentCol);
        (int row, int col) forwardTwo = new (currentRow + (direction * 2), currentCol);
        (int row, int col) diagPos = new (currentRow + (direction * 1),  currentCol + 1);
        (int row, int col) diagNeg = new ( currentRow + (direction * 1),  currentCol - 1);
        (int row, int col) horizPos = new (currentRow, currentCol + 1);
        (int row, int col) horizNeg = new (currentRow, currentCol - 1);
        
        if (IsWithinBoard(forwardOne.row, forwardOne.col) && inBoard.BoardPieces[forwardOne.row, forwardOne.col] is null)
        {
            ValidMoves.Add(forwardOne);

            if (IsWithinBoard(forwardTwo.row, forwardTwo.col) 
                && inBoard.BoardPieces[forwardTwo.row, forwardTwo.col] is null
                && this.HasMoved == false)
            {
                ValidMoves.Add(forwardTwo);
            }
        }
        // diagonal take
        CheckAndAddDiagonal(diagPos, inBoard);
        CheckAndAddDiagonal(diagNeg, inBoard);
        CheckEnPassant(horizPos, diagPos, inBoard);
        CheckEnPassant(horizNeg, diagNeg, inBoard);
    }
}