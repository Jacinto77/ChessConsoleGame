namespace ChessMac;

public class Piece
{
    protected Piece(string color, string name, char icon)
    {
        Color = color;
        Name = name;
        Icon = icon;
    }
    
    public Space.Position PiecePosition = new();
    
    public string? Type { get; set; }
    public string? Color { get; set; }
    public string? Name { get; set; }
    public char? Icon { get; set; }

    public bool IsFirstMove { get; set; }
    public bool HasMoved { get; set; }

    public List<Space.Position> ValidMoves = new();

    public virtual void GenerateValidMoves()
    {
    }

    public void PrintValidMoves()
    {
        foreach (Space.Position position in ValidMoves)
        {
            Console.Write(position.Row + position.Col + "\t");
        }
    }
    
    
    public void SetPosition(Space.Position inPosition)
    {
        PiecePosition.Row = inPosition.Row;
        PiecePosition.Col = inPosition.Col;
    }

    public Space.Position GetPosition()
    {
        return new Space.Position()
        {
            Row = PiecePosition.Row,
            Col = PiecePosition.Col
        };
    }
    
    
    
    
    
    
}