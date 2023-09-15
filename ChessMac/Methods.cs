using System.Net.NetworkInformation;

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

    public static readonly Dictionary<string, string> DefWhiteStart = new()
    {
        {"whiteROOK_16", "H1"},
        {"whiteKNIGHT_15", "G1"},
        {"whiteBISHOP_14", "F1"},
        {"whiteKING_13", "E1"},
        {"whiteQUEEN_12", "D1"},
        {"whiteBISHOP_11", "C1"},
        {"whiteKNIGHT_10", "B1"},
        {"whiteROOK_9", "A1"},
        {"whitePAWN_8", "H2"},
        {"whitePAWN_7", "G2"},
        {"whitePAWN_6", "F2"},
        {"whitePAWN_5", "E2"},
        {"whitePAWN_4", "D2"},
        {"whitePAWN_3", "C2"},
        {"whitePAWN_2", "B2"},
        {"whitePAWN_1", "A2"}
        
    };

    public static readonly Dictionary<string, string> DefBlackStart = new()
    {
        {"blackROOK_16", "H8"},
        {"blackKNIGHT_15", "G8"},
        {"blackBISHOP_14", "F8"},
        {"blackKING_13", "E8"},
        {"blackQUEEN_12", "D8"},
        {"blackBISHOP_11", "C8"},
        {"blackKNIGHT_10", "B8"},
        {"blackROOK_9", "A8"},
        {"blackPAWN_8", "H7"},
        {"blackPAWN_7", "G7"},
        {"blackPAWN_6", "F7"},
        {"blackPAWN_5", "E7"},
        {"blackPAWN_4", "D7"},
        {"blackPAWN_3", "C7"},
        {"blackPAWN_2", "B7"},
        {"blackPAWN_1", "A7"}
    };

    const char emptySpaceIcon = '\u2610';

    public static void InitPieces(Piece[] inPieces, string color)
    {
        inPieces[0] = Piece.CreatePiece(color, "rook");
        inPieces[1] = Piece.CreatePiece(color, "knight");
        inPieces[2] = Piece.CreatePiece(color, "bishop");
        inPieces[3] = Piece.CreatePiece(color, "king");
        inPieces[4] = Piece.CreatePiece(color, "queen");
        inPieces[5] = Piece.CreatePiece(color, "bishop");
        inPieces[6] = Piece.CreatePiece(color, "knight");
        inPieces[7] = Piece.CreatePiece(color, "rook");
        for (int i = 8; i < 16; i++)
        {
            inPieces[i] = Piece.CreatePiece(color, "pawn");
        }
        Piece.ResetPieceCounter();
    }
    
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

    public static void PlacePieces(Piece[] pieces, ChessBoard inBoard)
    {
        foreach (var piece in pieces)
        {
            string? name = piece.Name;
            Space? space = null;
            if (name is null) return;

            if (piece.Color == "white")
                space = inBoard.GetSpace(DefWhiteStart[name]);
            if (piece.Color == "black")
                space = inBoard.GetSpace(DefBlackStart[name]);
            if (space is null)
            {
                return;
            }
            piece.PlacePiece(space, inBoard);
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

    public static void GeneratePieceMoves(Piece[] pieces, ChessBoard inBoard)
    {
        foreach (var piece in pieces)
        {
            piece.GenerateValidMoves(inBoard);
            piece.AddPieceToSpaceThreats();
        }
    }
}