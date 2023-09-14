using static ChessMac.Program;
using static ChessMac.Space;
using static ChessMac.Methods;
namespace ChessMac;

/*
 * TODO: create move generation functionality work to identify and tag pieces as being threatened, as well as what piece is threatening
 * TODO: implement pinned piece recognition, for when king would be threatened if a piece wasn't in the way
 * TODO: implement piece blocking checking for when King is in check
 * TODO: add secondary move generation method that scans all possible movements as if there were no other pieces, tags them and assigns a score based on how many pieces are in the way
 */

public class Piece
{
    protected Piece(string color, string name, char icon)
    {
        Color = color;
        Name = name;
        Icon = icon;
    }
    
  
    public Space.Position PiecePosition = new();
    // human readable position of piece
    public string? Pos { get; set; }
    
    public string? Type { get; set; }
    public string? Color { get; set; }
    public string? Name { get; set; }
    public char? Icon { get; set; }
    
    public bool IsFirstMove { get; set; }
    public bool HasMoved { get; set; }

    // all valid moves for the piece
    public List<Space> ValidMoves = new();
    
    // generates all valid moves
    public virtual void GenerateValidMoves(ChessBoard inBoard)
    {
    }
    
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

    public void GenerateRookMoves(ChessBoard inBoard)
    {
        // scan spaces between startSpace and possible destinations
        // walk spaces from start to edges of board
        // if space.HasPiece == True, stop

        int currentCol = GetPosition().Col;
        int currentRow = GetPosition().Row;

        // subtract current position from 7 (last index of board array) to find boundaries
        int rangeLeft = currentCol;
        int rangeRight = 7 - currentCol;
        int rangeUp = currentRow;
        int rangeDown = 7 - currentRow;

        //scan up
        for (int i = 1; i < rangeUp; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        //scan down
        for (int i = 1; i < rangeDown; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        //scan left
        for (int i = 1; i < rangeLeft; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow, currentCol - i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        //scan right
        for (int i = 1; i < rangeRight; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow, currentCol + i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }

        int counter = 1;
        foreach (var move in ValidMoves)
        {
            Console.WriteLine(counter);
            Console.WriteLine(move.Col + move.Row.ToString());
            counter++;
        }
    }

    public void GenerateBishopMoves(ChessBoard inBoard)
    {
        int currentCol = GetPosition().Col;
        int currentRow = GetPosition().Row;

        Tuple<int, int> rangeUpLeft = new Tuple<int, int>(currentRow, currentCol);
        Tuple<int, int> rangeDownRight = new Tuple<int, int>(7 - currentRow, 7 - currentCol);
        Tuple<int, int> rangeUpRight = new Tuple<int, int>(currentRow, 7 - currentCol);
        Tuple<int, int> rangeDownLeft = new Tuple<int, int>(7 - currentRow, currentCol);
        
        // scan up-left
        for (int i = 1; i < rangeUpLeft.Item1 || i < rangeUpLeft.Item2; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol - i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        // scan up-right
        for (int i = 1; i < rangeUpRight.Item1 || i < rangeUpRight.Item2; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol + i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        // scan down-right
        for (int i = 1; i < rangeDownRight.Item1 || i < rangeDownRight.Item2; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol + i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        // scan down-left
        for (int i = 1; i < rangeDownLeft.Item1 || i < rangeDownLeft.Item2; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol - i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        int counter = 1;
        foreach (var move in ValidMoves)
        {
            Console.WriteLine(counter);
            Console.WriteLine(move.Col + move.Row.ToString());
            counter++;
        }
    }
    
    public void ScanHorizVert(Space currentSpace, int boundary)
    {
        
    }

    public void ScanDiagonal(Space currentSpace)
    {
        
    }
}