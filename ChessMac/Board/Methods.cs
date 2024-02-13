using ChessMac.Pieces.Base;

namespace ChessMac.Board;

// TODO: get rid of magic numbers

public static class Methods
{
    public static string GetPlayerInput()
    {
        while (true)
        {
            Console.Write(
                @"Move input must be in format:
                'A4 A5' or enter ! to see valid moves            
                >  ");
            var playerInput = Console.ReadLine();
            //Console.WriteLine(playerInput);
            // adding move checking logic
            // TODO
            if (playerInput == "!")
            {
                Console.WriteLine("Enter the piece you want to see the moves for:\n>  ");
                playerInput = Console.ReadLine();
                if (playerInput?.Length != 2) continue;
                return playerInput;
            }

            if (playerInput?.Length != 5) continue;

            if (!playerInput.Contains(' ')) continue;

            return playerInput;
        }

        return Console.ReadLine();
    }

    // TODO: check input w/ regex and known words instead
    public static (string pieceToMove, string moveDestination) ParseInput(string? input)
    {
        (string pieceToMove, string moveDestination) pieceMove;
        if (input != null)
        {
            pieceMove.pieceToMove = input[..2];
            pieceMove.moveDestination = input[3..5];
        }
        //TODO undetectable bug, nice
        else
        {
            Console.WriteLine("Error in input validation in: ParseInput()");
            return new ("A1", "A7");
        }

        return pieceMove;
    }

    public static string ConvertIndexToPos((int row, int col) inIndex)
    {
        string? row = null;
        string? column = null;
        row = inIndex.row switch
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
        column = inIndex.Item2 switch
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

    public static (string pieceToMove, string moveDestination) GetPlayerMove()
    {
        var playerInput = GetPlayerInput();
        var pieceDest = ParseInput(playerInput);
        return ParseInput(playerInput);
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
            if (choice is <= 4 and >= 1) return choice;

            Console.WriteLine("Input must be 1-4");
        }
    }
    
    public static bool HasPlayerSelectedCorrectColorPiece(Piece? activePiece, Piece.PieceColor colorToMove)
    {
        if (activePiece?.Color != colorToMove)
        {
            Console.WriteLine("That ain't your piece");
            return false;
        }
        return true;
    }
}