using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

// TODO implement castling
public class Rook : Piece
{
    public (int row, int col) CastlePos;

    public Rook(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Rook;
        Icon = GetColorPieceIcon(inColor);
    }
    
    public Rook(PieceColor inColor, (int row, int col) inPosition) : base(inColor, inPosition)
    {
        AssignIconByColor(inColor, PieceType.Rook);   
    }
    
    public Rook(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
        bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened, (int row, int col) inPosition) : 
        base(inType, inColor, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened, inPosition)
    {
    }

    public override Piece Clone()
    {
        return new Rook(this.Type, this.Color, this.GetValidMoveList(), this.Icon, this.HasMoved, this.IsPinned,
            this.MoveCounter, this.IsThreatened, this.Position);
    }

    public void AssignCastlePos((int row, int col) rookPos)
    {
        CastlePos = rookPos;
    }

    public bool CanCastle(PieceColor inColor, Board.ChessBoard inBoard)
    {
        return !HasMoved;
    }

    public override void GenerateValidMoves(Board.ChessBoard inBoard)
    {
        GenerateRookMoves(inBoard, Position.row, Position.col);
    }
}