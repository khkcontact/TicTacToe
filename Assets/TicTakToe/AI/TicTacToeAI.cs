using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe;
using System.Linq;

namespace TicTacToeAI
{
    struct Move
    {
        public int index;
        public int score;
    }
    
    public class Board
    {
        public static int GridSize => TikTacToeGame.GridSize;
        CellContent[] cells = new CellContent[GridSize * GridSize];

        public CellContent GetCellByIndex(int index) => cells[index];
        public void ClearCell(int index)
        {
            cells[index] = CellContent.Empty;
        }
        public bool MakeMove(int cellIndex, CellContent player )
        {
        //    List<int> emptyCellsIndicies = GetEmptyCellsIdicies();
            if (cells[cellIndex] == CellContent.Empty)
            {
                cells[cellIndex] = player;
                return true;
            }
            else
            {
                Debug.LogError($" at place [{cellIndex}] already stored {cells[cellIndex]} but it must be empty");
                return false;
            }
        }
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

        // return  indicies of empty cells
        public List<int> GetEmptyCellsIdicies()
        {
            List<int> ret = new List<int>();
            for (int i = 0; i < cells.Length; i++)
                if (cells[i] == CellContent.Empty)
                    ret.Add(i);
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

        public Vector2Int GetRowCol(int index)
        {
            int row = index / GridSize;
            int cols = index % GridSize;
            return new Vector2Int(row, cols);
        }

        public bool IsWin(CellContent player)
        {
           // Debug.Log("called IsWin");
            // check rows
            for (int row = 0; row < GridSize; row++)
            {
                bool isRowWin = true;
                for (int col = 0; col < GridSize; col++)
                    isRowWin = isRowWin && cells[row * GridSize + col] == player;

                if (isRowWin)
                {
                   // Debug.Log($"IsWin: win row{row}");
                    return true;
                }
            }

            // check cols
            // check rows
            for (int col = 0; col < GridSize; col++)
            {
                bool isColumnWin = true;
                for (int row = 0; row < GridSize; row++)
                    isColumnWin = isColumnWin && cells[row * GridSize + col] == player;

                if (isColumnWin)
                {
                   // Debug.Log($"IsWin: win column {col} ");
                    return true;
                }
            }

            // check diagin1
            bool isDiagonLTRBWin = true;
            for (int row = 0; row < GridSize; row++)
            {
                isDiagonLTRBWin = isDiagonLTRBWin && (cells[row * GridSize + row] == player);
            }
            if (isDiagonLTRBWin)
            {
               // Debug.Log($"IsWin: win diagon1");
                return true;
            }


            bool isDiagonRTLBWin = true;
            for (int row = 0; row < GridSize; row++)
            {
                 int col = GridSize - row - 1;
                 int cellIdx = row * GridSize + col;
                 bool isPlayerTurn = (cells[cellIdx] == player);
                isDiagonRTLBWin = isDiagonRTLBWin && isPlayerTurn;
               
                /*
                 Debug.Log($"isDiagonLTRBWin=={isDiagonRTLBWin} based on {cells[cellIdx]} == {player} {cells[cellIdx] == player} isPlayerTurn {isPlayerTurn} ");
                 if (isDiagonRTLBWin)
                 {
                     Debug.Log($"isDiagonLTRBWin=={isDiagonRTLBWin} is equal {cells[cellIdx] == player} for cell {cells[cellIdx]} and {player} isPlayerTurn {isPlayerTurn} at row={row}, col={col}, cellIdx = {cellIdx}");
                 }
                */
            }
            if (isDiagonRTLBWin)
            {
               // Debug.Log($"IsWin: isDiagonLTRBWin win diagon2");
                return true;
            }
            //Debug.Log($"IsWin: isDiagonLTRBWin NOT win diagon2");
            return false;

        }
        public bool isFinished() => !isEmptyCellsAvailable();

        public void Test()
        {

            Debug.Log("Started TEST()");
            /*
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
                { CellContent.Empty, CellContent.Empty,CellContent.Empty},
                { CellContent.ComputerTurnMark, CellContent.ComputerTurnMark,CellContent.ComputerTurnMark},
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
            */
            
            CellContent[,] diag2win =
            {
                { CellContent.Empty             , CellContent.Empty            ,CellContent.ComputerTurnMark                   },
                { CellContent.Empty             , CellContent.ComputerTurnMark ,CellContent.Empty},
                { CellContent.ComputerTurnMark   , CellContent.Empty            ,CellContent.Empty}
            };
            InputFromGrid(diag2win);
            Debug.Log($"----------  test diag2win computer: {IsWin(CellContent.ComputerTurnMark)}");

        }
    }
    
    public class TicTacToeAIObject //: MonoBehaviour
    {
        static int GridSize => TikTacToeGame.GridSize;
      //  CellContent[,] board = new CellContent[GridSize,GridSize];

        Board aiBoard = new Board();


        Move minimax(Board board, CellContent player)
        {
            Move ret = new Move();
            List<int> emptyCellsIndicies = board.GetEmptyCellsIdicies();

            if (board.IsWin(CellContent.PlayerTurnMark))
                return new Move() { index = -1, score = -10};
            else if (board.IsWin(CellContent.ComputerTurnMark))
                return new Move() { index = -1, score = 10 };
            else if (!board.isEmptyCellsAvailable())
                return new Move() { index = -1, score = 0 };

            List<Move> moves = new List<Move>();
            for (int i = 0; i < emptyCellsIndicies.Count; i++)
            {
                Move move = new Move();
                int curCell = emptyCellsIndicies[i];
                move.index = curCell;
                board.MakeMove(curCell, player );

                if (player == CellContent.ComputerTurnMark)
                {
                    var res = minimax(board, CellContent.PlayerTurnMark);
                    move.score = res.score;
                }
                else
                {
                    var res = minimax(board, CellContent.ComputerTurnMark);
                    move.score = res.score;
                }
                board.ClearCell(move.index);
                moves.Add(move);
            }
         
            int bestMoveScore;
            // If this is the computer turn, we need in move with highest score,
            // otherwise it must be lowest score
            Move bestMove = new Move();
            if (player == CellContent.ComputerTurnMark)
            {
                bestMoveScore = int.MinValue;
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].score > bestMoveScore)
                    {
                        bestMoveScore = moves[i].score;
                        bestMove.score = bestMoveScore;
                        bestMove.index = moves[i].index;
                    }
                }
            }
            else
            {
                bestMoveScore = int.MaxValue;
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].score < bestMoveScore)
                    {
                        bestMoveScore = moves[i].score;
                        bestMove.score = bestMoveScore;
                        bestMove.index = moves[i].index;
                    }
                }
            }
              
            return bestMove;
        }
        // Start is called before the first frame update
        void Start()
        {
            //CellContent
        }

        public void Test()
        {  
            //aiBoard.Test();
            CellContent[,] row0win =
                {
                { CellContent.PlayerTurnMark, CellContent.PlayerTurnMark,CellContent.Empty},
                { CellContent.Empty, CellContent.ComputerTurnMark,CellContent.Empty},
                { CellContent.Empty, CellContent.Empty,CellContent.Empty}
            };
            aiBoard.InputFromGrid(row0win);
            
            var move= minimax(aiBoard, CellContent.ComputerTurnMark);
            Debug.Log($" minimax: index {move.index} row col {aiBoard.GetRowCol(move.index)}  ");
            
        }

        public  Vector2Int GetComputerMove(CellContent[,] newBoardData)
        {
            Reset(newBoardData);
            var move = minimax(aiBoard, CellContent.ComputerTurnMark);
            Vector2Int ret = aiBoard.GetRowCol(move.index);
            return ret;
        }
        public void Reset(CellContent[,] newBoardData)
        {
            /*
            for (int row = 0; row < GridSize; row++)
                for (int col = 0; col < GridSize; col++)
                    board[row, col] = newBoardData[row,col];
            */
            aiBoard.InputFromGrid(newBoardData);
        }

        public Vector2Int CalculateTurn(Vector2Int place)
        {
            Vector2Int ret = place;
            return ret;
        }

    }
}
