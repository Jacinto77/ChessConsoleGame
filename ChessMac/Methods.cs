namespace ChessMac;

// TODO: get rid of magic numbers

public static class Methods
{
    public static readonly Dictionary<string, char> BlackIcons = new()
    {
        { "PawnIcon", '\u2659' },
        { "RookIcon", '\u2656' },
        { "KnightIcon", '\u2658' },
        { "BishopIcon",'\u2657' },
        { "QueenIcon", '\u2655' },
        { "KingIcon", '\u2654' },
    };

    public static readonly Dictionary<string, char> WhiteIcons = new()
    {
        { "PawnIcon", '\u265F' },
        { "RookIcon", '\u265C' },
        { "KnightIcon", '\u265E' },
        { "BishopIcon", '\u265D' },
        { "QueenIcon", '\u265B' },
        { "KingIcon", '\u265A' },
    };
    
    const char emptySpaceIcon = '\u2610';
    
    public static void InitializePieces(Piece[] inPieces, string color, Dictionary<string, char> inIcons)
    {
        // pieces
        inPieces[0] = new Rook
            (color: color, 
            name: color + "Rook" + 1, 
            icon: inIcons["RookIcon"], 
            type: "rook");
        inPieces[1] = new Knight
        (color: color, 
            name: color + "Knight" + 1, 
            icon: inIcons["KnightIcon"], 
            type: "knight");
        inPieces[2] = new Bishop
            (color: color, 
            name: color + "Bishop" + 1, 
            icon: inIcons["BishopIcon"], 
            type: "bishop");
        inPieces[3] = new King
        (color: color, 
            name: color + "King", 
            icon: inIcons["KingIcon"], 
            type: "king");
        inPieces[4] = new Queen
        (color: color, 
            name: color + "Queen", 
            icon: inIcons["QueenIcon"], 
            type: "queen");
        inPieces[5] = new Bishop
            (color: color, 
            name: color + "Bishop" + 2, 
            icon: inIcons["BishopIcon"], 
            type: "bishop");
        inPieces[6] = new Knight
        (color: color, 
            name: color + "Knight" + 2, 
            icon: inIcons["KnightIcon"], 
            type: "knight");
        inPieces[7] = new Rook
        (color: color, 
            name: color + "Rook" + 2, 
            icon: inIcons["RookIcon"], 
            type: "rook");
        
        // pawns
        for (int i = 8; i < 16; i++)
        {
            inPieces[i] = new Pawn
            (color: color, 
                name: color + "Pawn" + (i - 7), 
                icon: inIcons["PawnIcon"], 
                type: "pawn");
        }
    }

    public static void PlacePieces
        (Piece[] whitePieces, Piece[] blackPieces, ChessBoard inBoard)
    {
        int whitePieceCounter = 0;
        int blackPieceCounter = 0;
        
        // black pieces
        for (int row = 0; row < 2; row++)
        {
            for (int col = 7; col > -1; col--)
            {
                Space tempSpace = inBoard.BoardSpaces[row, col];
                tempSpace.PlacePiece(blackPieces[blackPieceCounter]);
                blackPieceCounter++;
            }
        }
        
        // white pieces
        for (int row = 7; row > 5; row--)
        {
            for (int col = 7; col > -1; col--)
            {
                Space tempSpace = inBoard.BoardSpaces[row, col];
                tempSpace.PlacePiece(whitePieces[whitePieceCounter]);
                whitePieceCounter++;
            }
        }
    }

    // TODO finish this function and test
    // TODO: function should take the board, startSpace, and destSpace as arguments
    // needs to reset the space's Piece reference to default icon and 
    // set destPosition space to inputPosition space.Piece so that the
    // icon is updated and piece is now referenced by a new position
    public static void MovePiece(ChessBoard inBoard)
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (input != null)
            {
                Tuple<string, string> positions = ParseInput(input);
                
                Space.Position startPosition = ConvertPosToIndex(positions.Item1);
                Space.Position destPosition = ConvertPosToIndex(positions.Item2);
                Space startSpace = inBoard.BoardSpaces[startPosition.RowIndex, startPosition.ColIndex];
                Space destSpace = inBoard.BoardSpaces[destPosition.RowIndex, destPosition.ColIndex];
                
                // TODO: move this check somehwere else
                if (startSpace.HasPiece == false)
                {
                    Console.WriteLine("That space doesn't have a piece on it!");
                }
                
                else
                {
                    foreach (Space space in startSpace.Piece.ValidMoves)
                    {
                        if (destSpace == space)
                        {
                            destSpace.PlacePiece(startSpace.Piece);
                            startSpace.ClearSpace();
                            return;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("invalid input");
            }
        }
    }

    // TODO: check input w/ regex and known words instead
    public static Tuple<string, string> ParseInput(string input)
    {
        string piece;
        string dest;

        if (input != null)
        {
            piece = input[0..2];
            dest = input[3..5];
        }
        //undetectable bug, nice
        else return new Tuple<string, string>("A1", "A8");
        return new Tuple<string, string>(piece, dest);
    }

    public static string ConvertIndexToPos(int inRow, int inCol)
    {
        string column = "";
        string row = "";

        column = inCol switch
        {
            0 => "A",
            1 => "B",
            2 => "C",
            3 => "D",
            4 => "E",
            5 => "F",
            6 => "G",
            7 => "H",
            _ => column
        };

        row = inRow switch
        {
            0 => "8",
            1 => "7",
            2 => "6",
            3 => "5",
            4 => "4",
            5 => "3",
            6 => "2",
            7 => "1",
            _ => row
        };
        
        return column + row;
    }
    
    public static string ConvertIndexToPos(Space.Position inPosition)
    {
        string column = "";
        string row = "";

        column = inPosition.ColIndex switch
        {
            0 => "A",
            1 => "B",
            2 => "C",
            3 => "D",
            4 => "E",
            5 => "F",
            6 => "G",
            7 => "H",
            _ => column
        };

        row = inPosition.RowIndex switch
        {
            0 => "8",
            1 => "7",
            2 => "6",
            3 => "5",
            4 => "4",
            5 => "3",
            6 => "2",
            7 => "1",
            _ => row
        };
        
        return column + row;
    }
    
    public static Space.Position ConvertPosToIndex(string input)
    {
        var column = input[0];
        var row = input[1];

        Space.Position position = new Space.Position();
        position.ColIndex = column switch
        {
            'A' => 0,
            'B' => 1,
            'C' => 2,
            'D' => 3,
            'E' => 4,
            'F' => 5,
            'G' => 6,
            'H' => 7,
            _ => position.ColIndex
        };

        position.RowIndex = row switch
        {
            '1' => 7,
            '2' => 6,
            '3' => 5,
            '4' => 4,
            '5' => 3,
            '6' => 2,
            '7' => 1,
            '8' => 0,
            _ => position.RowIndex
        };

        return position;
    }
}