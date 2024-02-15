using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

// queen movement is a combination of bishop and rook movement
// any space diagonally or vertically/horizontally

public class Queen : Piece
{
    public Queen(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Queen;
        Icon = GetColorPieceIcon(inColor);
    }
    
    public Queen(PieceColor inColor, (int row, int col) inPosition) : base(inColor, inPosition)
    {
        AssignIconByColor(inColor, PieceType.Queen);
        Type = PieceType.Queen;
    }
    
    public Queen(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
        bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened, (int row, int col) inPosition) : 
        base(inType, inColor, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened, inPosition) {}

    public override Piece Clone()
    {
        return new Queen(this.Type, this.Color, this.GetValidMoveList(), this.Icon, this.HasMoved, this.IsPinned,
            this.MoveCounter, this.IsThreatened, this.Position);
    }

    // TODO make methods into extensions
    public override void GenerateValidMoves(Board.ChessBoard inBoard)
    {
        ClearValidMoves();
        GenerateRookMoves(inBoard, this.Position.row, Position.col);
        // TODO: rook moves are being cleared from the list of valid moves or GenBishopMoves is overwriting them?
        // TODO: perhaps the deepcopy isn't working properly for the pieces?
        GenerateBishopMoves(inBoard, Position.row, Position.col);
    }
}