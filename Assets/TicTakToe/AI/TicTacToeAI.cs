using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe;

namespace TicTacToeAI
{
    public class Board
    {
        public static int GridSize => TikTacToeGame.GridSize;
        CellContent[] cells = new CellContent[GridSize * GridSize];

        public Board()
        {
            for (int i = 0; i < cells.Length; i++)
                cells[i] = CellContent.Empty;
        }

        public void InputFromGrid(CellContent[,] grid)
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                    cells[GridSize * row + col] = grid[row,col];
            }
        }

        public void OutputToGrid(CellContent[,] grid)
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                   grid[row, col] = cells[GridSize * row + col];
            }
        }

        List<CellContent> GetEmptyCells()
        {
            List<CellContent> ret = new List<CellContent>();
            for (int i = 0; i < cells.Length; i++)
                if (cells[i] == CellContent.Empty)
                    ret.Add(cells[i]);
            return ret;
        }

        public bool isEmptyCellsAvailable()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == CellContent.Empty)
                    return true;
            }
            return false;
        }

        public bool IsWin(CellContent player)
        {
           // check rows
            for (int row = 0; row < GridSize; row++)
            {
                bool isRowWin = true;
                for (int col = 0; col < GridSize; col++)
                    isRowWin = isRowWin && cells[row * GridSize + col] == player;

                if (isRowWin)
                {
                    Debug.Log($"win row{row}");
                    return true;
                }
            }

            // check cols
            // check rows
            for (int col = 0; col < GridSize; col++)
            {
                bool isColumnWin = true;
                for (int row = 0; row < GridSize; col++)
                    isColumnWin = isColumnWin && cells[row * GridSize + col] == player;

                if (isColumnWin)
                {
                    Debug.Log($"win column {col}");
                    return true;
                }
            }

            // check diagin1
            bool isDiagonLTRBWin = true;
            for (int row = 0; row < GridSize; row++)
            {
                isDiagonLTRBWin = isDiagonLTRBWin && cells[row * GridSize + row] == player;
            }
            if (isDiagonLTRBWin)
            {
                Debug.Log($"win diagon1");
                return true;
            }


            bool isDiagonRTLBWin = true;
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = GridSize-1; col >=0; col--)
                    isDiagonLTRBWin = isDiagonLTRBWin && cells[row * GridSize + col] == player;
            }
            if (isDiagonRTLBWin)
            {
                Debug.Log($"win diagon2");
                return true;
            }

            return false;

        }
        public bool isFinished() => !isEmptyCellsAvailable();

        public void Test()
        {

            Debug.Log("Started TEST()");
            CellContent[,] row0win =
            {
                { CellContent.PlayerTurnMark, CellContent.PlayerTurnMark,CellContent.PlayerTurnMark},
                { CellContent.Empty, CellContent.Empty,CellContent.Empty},
                { CellContent.Empty, CellContent.Empty,CellContent.Empty}
            };
            
            InputFromGrid(row0win);
            Debug.Log($"----------  test row0 player: {IsWin(CellContent.PlayerTurnMark)}");
            
            CellContent[,] row1win =
            {
                { CellContent.ComputerTurnMark, CellContent.ComputerTurnMark,CellContent.ComputerTurnMark},
                { CellContent.Empty, CellContent.Empty,CellContent.Empty},
                { CellContent.Empty, CellContent.Empty,CellContent.Empty}
            };
            InputFromGrid(row1win);
            Debug.Log($"----------  test row1 computer: {IsWin(CellContent.ComputerTurnMark)}");

            CellContent[,] col2win =
            {
                { CellContent.Empty, CellContent.PlayerTurnMark,CellContent.Empty},
                { CellContent.Empty, CellContent.PlayerTurnMark,CellContent.Empty},
                { CellContent.Empty, CellContent.PlayerTurnMark,CellContent.Empty}
            };

            InputFromGrid(col2win);
            Debug.Log($"----------  test col2 player: {IsWin(CellContent.PlayerTurnMark)}");


            CellContent[,] diag1win =
            {
                { CellContent.PlayerTurnMark, CellContent.Empty         ,CellContent.Empty},
                { CellContent.Empty         , CellContent.PlayerTurnMark,CellContent.Empty},
                { CellContent.Empty         ,CellContent.Empty          ,CellContent.PlayerTurnMark}
            };
            InputFromGrid(diag1win);
            Debug.Log($"----------  test diag1win player: {IsWin(CellContent.PlayerTurnMark)}");
            
            
            CellContent[,] diag2win =
            {
                { CellContent.Empty             , CellContent.Empty            ,CellContent.ComputerTurnMark                   },
                { CellContent.Empty             , CellContent.ComputerTurnMark ,CellContent.Empty},
                { CellContent.ComputerTurnMark  , CellContent.Empty            ,CellContent.Empty}
            };
            InputFromGrid(diag2win);
            Debug.Log($"----------  test diag1win computer: {IsWin(CellContent.ComputerTurnMark)}");

        }
    }
    
    public class TicTacToeAIObject //: MonoBehaviour
    {
        static int GridSize => TikTacToeGame.GridSize;
        CellContent[,] board = new CellContent[GridSize,GridSize];

        Board aiBoard = new Board();
        // Start is called before the first frame update
        void Start()
        {
            //CellContent
        }

        public void Test() => aiBoard.Test();
        public void Reset(CellContent[,] newBoardData)
        {
            for (int row = 0; row < GridSize; row++)
                for (int col = 0; col < GridSize; col++)
                    board[row, col] = newBoardData[row,col];
        }

        public Vector2Int CalculateTurn(Vector2Int place)
        {
            Vector2Int ret = place;
            return ret;
        }

    }
}
