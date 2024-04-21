using System.Text.RegularExpressions;
using ChessMac.Pieces.Base;

namespace ChessMac.Board;

// TODO: get rid of magic numbers

public static class Methods
{
    private static Dictionary<int, string> _dictErrorCodeMessages = new Dictionary<int, string>()
    {
        {1, "activePiece was null"},
        {2, "Invalid color"},
        {3, "Move entered was not found as a valid move"},
        {4, "Not implemented yet -- TODO"}
    };

    private static List<string> _keywords = new List<string>()
    {
        // FINISH
        "clear",
        "restart",
    };

    public static void ExecuteKeyword(string inKeyword)
    {
        // TODO
        // pass
    }
    
    public static string GetAndValidateFormatPlayerMove()
    {
        while (true)
        {
            Console.Write("> ");
            var playerInput = Console.ReadLine();
            if (playerInput is null)
            {
                Console.WriteLine("Input was null: GetAndValidateFormatPlayerMove()");
                continue;
            }
            
            var cleanedInput = playerInput.ToLower().Trim();
            if (_keywords.Contains(cleanedInput) || false) // TODO
            {
                ExecuteKeyword(cleanedInput);
            }
            if (!CheckIsInputValidRegex(cleanedInput))
            {
                Console.WriteLine("Input was not valid. Please use format 'A2 A4'");
                continue;
            }

            if (playerInput == "/h")
            {
                // TODO: help menu
                Console.WriteLine("Not yet implemented");
                continue;
            }
            
            return playerInput;
        }
    }
    
    public static string[] ConvertInputStringToValueTuple(string? input)
    {
        if (input is null)
        {
            throw new Exception();
        }

        return input.Split();
    }

    public static string ConvertIndexToPos((int row, int col)? inIndex)
    {
        if (!inIndex.HasValue)
            return "null";
        var index = inIndex.Value;
        
        string row = "";
        string column = "";
        row = index.row switch
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
        column = index.Item2 switch
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
        return column + row;
    }

    public static (int row, int col) ConvertPosToIndex(string input)
    {
        var columnChar = input[0];
        var rowChar = input[1];

        var tempCol = -1;
        var tempRow = -1;
        tempCol = columnChar switch
        {
            'A' => 0,
            'B' => 1,
            'C' => 2,
            'D' => 3,
            'E' => 4,
            'F' => 5,
            'G' => 6,
            'H' => 7,
            _ => tempCol
        };

        tempRow = rowChar switch
        {
            '1' => 7,
            '2' => 6,
            '3' => 5,
            '4' => 4,
            '5' => 3,
            '6' => 2,
            '7' => 1,
            '8' => 0,
            _ => tempRow
        };

        return (tempRow, tempCol);
    }

    public static string[] GetPlayerMove()
    {
        // TODO: take care of input
        // will handle the following input options
        /*
         * A2 B3, conventional notation specifying space to space
         * knight to pawn, piece to piece notation, will need to check for ambiguity and confirm if found
         * knight g4, piece to space
         * a2-b3
         * a2 pawn
         *
         * for options/views:
         *  a2 --options
         *  a2 -o
         *  a2 --piece
         *  a2 -p
         *  a2 --threat
         *  a2 -t
         *  a2 --moves
         *  a2 -m
         * 
         * for stats:
         *  --stats
         *  -s
         * 
         *  for help:
         *  a2 /?
         *  a2?
         *  /?
         *  /help
         *  /h
         * 
         */
        return ConvertInputStringToValueTuple(GetAndValidateFormatPlayerMove());
    }

    public static void CheckAndPromotePawn(Piece pieceBeingMoved, Board.ChessBoard inBoard, int currentRow)
    {
        if (!pieceBeingMoved.IsPawnPromotionSpace(currentRow))
            return;
        pieceBeingMoved.PromotePawn(inBoard);
    }

    public static int? GetPlayerTypeInt()
    {
        while (true)
        {
            var choice = Console.Read();
            Console.WriteLine($"Input: {choice}");
            if (choice is <= 4 and >= 1) return choice;
            else Console.WriteLine("Input must be 1-4");
        }
    }
    
    public static bool IsPieceCorrectColor(Piece? activePiece, Piece.PieceColor colorToMove)
    {
        if (activePiece?.Color == colorToMove) return true;
        Console.WriteLine("That ain't your piece");
        return false;
    }

    public static bool CheckIsInputValidRegex(string inInput)
    {
        const string pattern = @"^[a-h][1-8]\s[a-h][1-8]$";
        return Regex.IsMatch(inInput, pattern);
    }

    public static Piece.PieceColor GetColorToMove(int inMoveCounter)
    {
        return inMoveCounter % 2 != 0 ? Piece.PieceColor.White : Piece.PieceColor.Black;
    }

    public static void UpdateScreen(ChessBoard inBoard, int inMoveCounter, Piece.PieceColor inColor, List<int> errorCodes)
    {
        inBoard.OutputBoard(inMoveCounter);
        Console.WriteLine($"Turn: {inColor.ToString().ToUpper()} to move");
        Console.WriteLine("/h for help");
        PrintErrorMessages(errorCodes);
    }

    public static void PrintErrorMessages(List<int> inErrorCodes)
    {
        if (inErrorCodes.Count == 0) return;
        foreach (var errorCode in inErrorCodes)
        {
            Console.WriteLine($"{_dictErrorCodeMessages[errorCode]}");
        }
    }

    public static void TestMoveSequence()
    {
        Dictionary<int, string> moveSequence = new Dictionary<int, string>
        {
            { 1, "D2 D4" },
            { 2, ""},
            { 3, ""},
            { 4, ""},
            { 5, ""},
            { 6, ""},
            { 7, ""},
            { 8, ""},
            { 9, ""}
        };
    }
}