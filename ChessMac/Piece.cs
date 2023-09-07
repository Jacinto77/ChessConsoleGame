using static ChessMac.Program;
using static ChessMac.Space;
namespace ChessMac;

public class Piece
{
    protected Piece(string color, string name, char icon)
    {
        Color = color;
        Name = name;
        Icon = icon;
    }
    
    // current position of piece
    public Space.Position PiecePosition = new();
    
    // type of piece; pawn, rook, etc
    public string? Type { get; set; }
    // color of piece
    public string? Color { get; set; }
    // name of piece = color + type
    public string? Name { get; set; }
    // icon to display the piece in console
    public char? Icon { get; set; }
    
    // human readable position of piece
    public string? Pos { get; set; }
    
    public bool IsFirstMove { get; set; }
    // flag for if the piece has moved or not
    public bool HasMoved { get; set; }

    // all valid moves for the piece
    public List<Space> ValidMoves = new();
    
    // generates all valid moves
    public virtual void GenerateValidMoves(ChessBoard inBoard)
    {
    }
    
    // prints all valid moves
    public void PrintValidMoves()
    {
        foreach (Space position in ValidMoves)
        {
            Console.Write(position.Row + position.Col + "\t");
        }
    }
    
    
    public void SetPosition(Position inPosition)
    {
        PiecePosition.Row = inPosition.Row;
        PiecePosition.Col = inPosition.Col;
        
        Pos = ConvertIndicesToPos(inPosition.Row, inPosition.Col);
    }

    public Position GetPosition()
    {
        return new Position()
        {
            Row = PiecePosition.Row,
            Col = PiecePosition.Col
        };
    }

    public void ScanHorizVert(Space currentSpace, int boundary)
    {
        
    }

    public void ScanDiagonal(Space currentSpace)
    {
        
    }
}