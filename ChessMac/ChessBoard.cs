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

    public Piece[,] BoardPieces = new Piece[8, 8];
    
    // collection of all spaces in 8x8 array
    // first index is the row, second index is the column
    public Space[,] BoardSpaces = new Space[8, 8];
    
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

    //TODO finish transitioning from using Space class to only Piece class
    public void InitBoardPieces()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                BoardPieces[row, col] = new Piece(Piece.PieceColor.Null, Piece.PieceType.Null);
            }
        }
    }
    
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
        colIndex = position.Item1 switch
        {
            'A' => 0,
            'B' => 1,
            'C' => 2,
            'D' => 3,
            'E' => 4,
            'F' => 5,
            'G' => 6,
            'H' => 7,
            _ => colIndex
        };

        rowIndex = position.Item2 switch
        {
            1 => 7,
            2 => 6,
            3 => 5,
            4 => 4,
            5 => 3,
            6 => 2,
            7 => 1,
            8 => 0,
            _ => rowIndex
        };

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