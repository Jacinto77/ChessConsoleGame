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

    public bool CheckCastle(PieceColor inColor, ChessBoard inBoard)
    {
        if (HasMoved) return false;
        
        (int row, int col) KingSideRookPos;
        (int row, int col) QueenSideRookPos;

        if (inColor == PieceColor.White)
        {
            KingSideRookPos = (7, 7);
            QueenSideRookPos = (7, 0);
        }
        else
        {
            KingSideRookPos = (0, 7);
            QueenSideRookPos = (0, 0);
        }

        Piece? kingSideRook = inBoard.BoardPieces[KingSideRookPos.row, KingSideRookPos.col];
        Piece? queenSideRook = inBoard.BoardPieces[QueenSideRookPos.row, QueenSideRookPos.col];
        
        if (kingSideRook?.Type == PieceType.Rook && kingSideRook.HasMoved == false)
        {
            // if (inBoard.BoardPieces[KingSideRookPos.row, KingSideRookPos.col - 1]?.Icon == EmptySpaceIcon
            //     &&)
        }
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
            if (destSpace?.Icon != EmptySpaceIcon)
            {
                if (destSpace?.Color == this.Color)
                {
                    continue;
                }
            }
            if (destSpace is { IsThreatened: true }) continue;
            
            AddValidMove(move);
        }
    }
}