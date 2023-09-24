namespace ChessMac;
using static Methods;
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
        
        InitPieces(WhitePieces, Piece.PieceColor.White);
        InitPieces(BlackPieces, Piece.PieceColor.Black);
        PlacePieces(WhitePieces, this);
        PlacePieces(BlackPieces, this);
    }
    
    const int NumberOfPieces = 16;
    
    public Piece[] WhitePieces = new Piece[NumberOfPieces];
    public Piece[] BlackPieces = new Piece[NumberOfPieces];
    
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
                BoardSpaces[row, col] = new Space(new Tuple<int, int>(row, col));
            }
        }
    }
    
    public ChessBoard DeepCopy()
    {
        ChessBoard newBoard = new ChessBoard();
        for (int i = 0; i < this.WhitePieces.Length; i++)
        {
            newBoard.WhitePieces[i] = WhitePieces[i].DeepCopy();
            newBoard.BlackPieces[i] = BlackPieces[i].DeepCopy();
        }
        
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                if (BoardSpaces[row, col] is null)
                    throw new Exception("ChessBoard.DeepCopy() BoardSpaces space is null");
                
                newBoard.BoardSpaces[row, col] = BoardSpaces[row, col].DeepCopy();
            }
        }

        for (int i = 0; i < WhitePieces.Length; i++)
        {
            Piece tempPiece = newBoard.WhitePieces[i];
            tempPiece.PlacePiece(newBoard, new Tuple<int, int>(tempPiece.RowIndex, tempPiece.ColIndex));
        }
        for (int i = 0; i < BlackPieces.Length; i++)
        {
            Piece tempPiece = newBoard.BlackPieces[i];
            tempPiece.PlacePiece(newBoard, new Tuple<int, int>(tempPiece.RowIndex, tempPiece.ColIndex));
        }

        return newBoard;
    }
    
    // // debugging
    // public void PrintBoardSpacesConsole()
    // {
    //     for (int row = 0; row < 8; row++)
    //     {
    //         for (int col = 0; col < 8; col++)
    //         {
    //             // print indices
    //             Console.Write("Row: {0}; Col: {1} \t", row, col);
    //             
    //             // print readable col/row
    //             Console.Write(BoardSpaces[row, col].Col);
    //             Console.Write(BoardSpaces[row, col].Row + "\t");
    //             
    //             // print whether it has a piece or not
    //             Console.Write(BoardSpaces[row, col].HasPiece + "\n");
    //         }
    //     }
    // }
    
    // Output Board Display in ASCII to console
    public void OutputBoard()
    {
        // Black side label
        Console.WriteLine("\t\t\t\t BLACK\n");
        // column letter labels
        Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH");
        Console.WriteLine(@"      ______________________________________________________________");
        
        for (int row = 0; row < 8; row++)
        {
            // row number labels
            Console.Write(rowNums[row] + @"     |" + "\t");
            
            for (int col = 0; col < 8; col++)
            {
                Tuple<int, int> currentSpace = new Tuple<int, int>(row, col);
                Console.Write(GetSpace(currentSpace).Icon);
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

    public Space GetSpace(Tuple<int, int> inLocation)
    {
        if (inLocation.Item1 > 7 || inLocation.Item1 < 0 || inLocation.Item2 > 7 || inLocation.Item2 < 0)
        {
            throw new Exception("Chessboard.GetSpace() failed");
        }
        return BoardSpaces[inLocation.Item1, inLocation.Item2];
    }
    
    
    public Piece? GetPiece(Tuple<int, int> inPosition)
    {
        Space? tempSpace = BoardSpaces[inPosition.Item1, inPosition.Item2];
        if (tempSpace is null) return null;
        return tempSpace.HasPiece ? tempSpace.Piece : null;
    }
    
    // TODO: 
    // public void DisplayMoves(Piece? inPiece)
    // {
    //     for (int i = 0; i < inPiece.ValidMoves.Count; i++)
    //     {
    //         inPiece.ValidMoves[i].SetIconHighlight();
    //     }
    // }
    //
    // public void RemoveDisplayMoves(Piece? inPiece)
    // {
    //     for (int i = 0; i < inPiece.ValidMoves.Count; i++)
    //     {
    //         inPiece.ValidMoves[i].UnsetIconHighlight();
    //     }
    // }
}