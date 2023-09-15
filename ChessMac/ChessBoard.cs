namespace ChessMac;

// creation of chessboard
// outputs chess board display
// modifies itself based on moves made
// outputs information about the state of the board
// holds all 64 Spaces

public class ChessBoard
{
    // initializes board with all spaces set to []
    public ChessBoard()
    {
        InitBoardSpaces();
    }
    
    // collection of all spaces in 8x8 array
    // first index is the row, second index is the column
    public Space[,] BoardSpaces = new Space[8, 8];
    
    // Sets each space to a default Space object
    public void InitBoardSpaces()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                BoardSpaces[row, col] = new Space(row, col);
            }
        }
    }
    
    // debugging
    public void PrintBoardSpacesConsole()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                // print indices
                Console.Write("Row: {0}; Col: {1} \t", row, col);
                
                // print readable col/row
                Console.Write(BoardSpaces[row, col].Col);
                Console.Write(BoardSpaces[row, col].Row + "\t");
                
                // print whether it has a piece or not
                Console.Write(BoardSpaces[row, col].HasPiece + "\n");
            }
        }
    }
    
    // Output Board Display in ASCII to console
    public void OutputBoard()
    {
        // Black side label
        Console.WriteLine("\t\t\t\t BLACK\n");
        // column letter labels
        Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH");
        Console.WriteLine(@"      ______________________________________________________________");
        //TODO: change i and j to row and column
        for (int row = 0; row < 8; row++)
        {
            // row number labels
            Console.Write(rowNums[row] + @"     |" + "\t");
            
            for (int col = 0; col < 8; col++)
            {
                Console.Write(GetSpace(row, col).Icon);
                // if (BoardSpaces[row, col].Piece == null)
                //     Console.Write(BoardSpaces[row, col].Icon);
                // else
                //     Console.Write(BoardSpaces[row, col].Piece.Icon);
                
                if (col < 7) Console.Write("\t");
                
                // skip to next line after reaching last item in row
                if (col == 7)
                {
                    // row number labels
                    Console.Write(@"  |" + "\t");
                    Console.Write(rowNums[row]);
                    Console.WriteLine();
                    if (row < 7)
                    {
                        Console.Write(@"      |" + "\t\t\t\t\t\t\t\t" + @"   |");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(@"      ______________________________________________________________");
                    }
                }
            }
        }
        // column letter labels
        Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH\n");
        // White side label
        Console.WriteLine("\t\t\t\t WHITE");
    }

    // array of numbers 1-8 to be used by OutputBoard()
    private int[] rowNums =
    {
        8, 7, 6, 5, 4, 3, 2, 1
    };
    
    // not used
    private char[] colChars =
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'
    };
    
    // not used
    private char[] pieces = 
    {
        'P', 'R', 'B', 'Q', 'N', 'K'
    };
    
    // unicode chars for piece display
    private char blackPawn = '\u2659';
    private char blackRook = '\u2656';
    private char blackKnight = '\u2658';
    private char blackBishop = '\u2657';
    private char blackQueen = '\u2655';
    private char blackKing = '\u2654';
    
    private char whitePawn = '\u265F';
    private char whiteRook = '\u265C';
    private char whiteKnight = '\u265E';
    private char whiteBishop = '\u265D';
    private char whiteQueen = '\u265B';
    private char whiteKing = '\u265A';

    private char emptySpace = '\u2610';
    
    // debugging
    public void PrintChars()
    {
        Console.WriteLine("WhitePawn = " + whitePawn);
        Console.WriteLine("BlackPawn = " + blackPawn);
    }

    // for fun
    public void PrintBoard_3()
    {
        Console.WriteLine(@"
                                       BLACK

              A       B       C       D       E       F       G       H
            ______________________________________________________________
        8   | ♖       ♘       ♗       ♔       ♕       ♗       ♘       ♖  |    8
            |                                                            |
        7   | ♙       ♙       ♙       ♙       ♙       ♙       ♙       ♙  |    7
            |                                                            |
        6   | ☐       ☐       ☐       ☐       ☐       ☐       ☐       ☐  |    6
            |                                                            |
        5   | ☐       ☐       ☐       ☐       ☐       ☐       ☐       ☐  |    5
            |                                                            |
        4   | ☐       ☐       ☐       ☐       ☐       ☐       ☐       ☐  |    4
            |                                                            |
        3   | ☐       ☐       ☐       ☐       ☐       ☐       ☐       ☐  |    3
            |                                                            |
        2   | ♟       ♟       ♟       ♟       ♟       ♟       ♟       ♟  |    2
            |                                                            |
        1   | ♜       ♞       ♝       ♚       ♛       ♝       ♞       ♜  |    1
            ______________________________________________________________
              A       B       C       D       E       F       G       H

                                       WHITE
        ");
    }
    
    // not used switch method to convert H3 input to [2, 7] output
    public Tuple<int, int> ConvertPosToIndices(Tuple<char, int> position)
    {
        int colIndex = -1;
        int rowIndex = -1;
        switch (position.Item1)
        {
            case 'A': colIndex = 0;
                break;
            case 'B': colIndex = 1;
                break;
            case 'C': colIndex = 2;
                break;
            case 'D': colIndex = 3;
                break;
            case 'E': colIndex = 4;
                break;
            case 'F': colIndex = 5;
                break;
            case 'G': colIndex = 6;
                break;
            case 'H': colIndex = 7;
                break;
        }

        switch (position.Item2)
        {
            case 1: rowIndex = 7;
                break;
            case 2: rowIndex = 6;
                break;
            case 3: rowIndex = 5;
                break;
            case 4: rowIndex = 4;
                break;
            case 5: rowIndex = 3;
                break;
            case 6: rowIndex = 2;
                break;
            case 7: rowIndex = 1;
                break;
            case 8: rowIndex = 0;
                break;
        }

        return new Tuple<int, int>(colIndex, rowIndex);
    }

    public Space GetSpace(int inRow, int inCol)
    {
        if (inRow > 7 || inRow < 0 || inCol > 7 || inCol < 0)
        {
            return new Space(-1, -1);
        }
        return BoardSpaces[inRow, inCol];
    }

    public Space GetSpace(Space.Position inPosition)
    {
        if (inPosition.RowIndex > 7 
            || inPosition.RowIndex < 0 
            || inPosition.ColIndex > 7 
            || inPosition.ColIndex < 0)
        {
            Space tempSpace = new Space(-1, -1);
            tempSpace.HasPiece = false;
            tempSpace.Piece = null;

            return tempSpace;
        }
        return BoardSpaces[inPosition.RowIndex, inPosition.ColIndex];
    }

    public Space GetSpace(string position)
    {
        Space.Position pos = Methods.ConvertPosToIndex(position);
        if (Space.IsWithinBoard(pos))
            return GetSpace(pos);

        return new Space(-1, -1);
    }
    
    public Piece? GetPiece(Space.Position inPosition)
    {
        Space tempSpace = BoardSpaces[inPosition.RowIndex, inPosition.ColIndex];
        if (tempSpace.HasPiece) return tempSpace.Piece;
        else return null;
    }
    
    public void DisplayMoves(Piece? inPiece)
    {
        for (int i = 0; i < inPiece.ValidMoves.Count; i++)
        {
            inPiece.ValidMoves[i].SetIconHighlight();
        }
    }

    public void RemoveDisplayMoves(Piece? inPiece)
    {
        for (int i = 0; i < inPiece.ValidMoves.Count; i++)
        {
            inPiece.ValidMoves[i].UnsetIconHighlight();
        }
    }
}