using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

using static Board.ChessBoard;

public class Knight : Piece
{
    public Knight(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Knight;
        Icon = GetColorPieceIcon(inColor);
    }
    
    public Knight(PieceColor inColor, (int row, int col) inPosition) : base(inColor, inPosition)
    {
        AssignIconByColor(inColor, PieceType.Knight);
    }
    
    public Knight(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
        bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened, (int row, int col) inPosition) : 
        base(inType, inColor, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened, inPosition)
    {
    }

    public override Piece Clone()
    {
        return new Knight(this.Type, this.Color, this.GetValidMoveList(), this.Icon, this.HasMoved, this.IsPinned,
            this.MoveCounter, this.IsThreatened, this.Position);
    }

    public override void GenerateValidMoves(Board.ChessBoard inBoard)
    {
        // List<(int row, int col)> tempMoves = CreateList(
        //     (currentRow + 2, currentCol + 1),
        //     (currentRow + 2, currentCol - 1),
        //     (currentRow - 2, currentCol + 1),
        //     (currentRow - 2, currentCol - 1),
        //     (currentRow + 1, currentCol + 2),
        //     (currentRow + 1, currentCol - 2),
        //     (currentRow - 1, currentCol + 2),
        //     (currentRow - 1, currentCol - 2)
        // );
        List<(int row, int col)> tempMoves = new List<(int row, int col)>
        { 
            (Position.row + 2, Position.col + 1), 
            (Position.row + 2, Position.col - 1),
            (Position.row - 2, Position.col + 1),
            (Position.row - 2, Position.col - 1),
            (Position.row + 1, Position.col + 2),
            (Position.row + 1, Position.col - 2),
            (Position.row - 1, Position.col + 2),
            (Position.row - 1, Position.col - 2)
        };

        foreach (var possiblePosition in tempMoves)
        {
            if (IsWithinBoard(possiblePosition) == false) continue;

            var destSpace = inBoard.BoardPieces[possiblePosition.row, possiblePosition.col];

            if (destSpace is not null)
                if (destSpace?.Color == Color)
                    continue;

            AddValidMove(possiblePosition);
        }
    }
}