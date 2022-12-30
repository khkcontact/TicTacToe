using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToeAI;
namespace TicTacToe
{
    public class TikTacToeGame : MonoBehaviour
    {
       /// <summary>
       /// Lenght of table side
       /// </summary>
        public const int GridSize = 3;
        [SerializeField]
        Transform tableRoot;

        [SerializeField]
        VictoryPanel victoryPanel;
        
        Cell[,] cells = new Cell[GridSize, GridSize];

        public bool IsRunning { get; private set; }

        TicTacToeAIObject ai = new TicTacToeAIObject();

        // Start is called before the first frame update
        void Start()
        {
           ai.Test();

            victoryPanel.SetGameManager(this);
            victoryPanel.Hide();
            
            if (cells == null)
                cells = new Cell[3, 3];

            int rowIndex = 0;
            List<Cell> cellsOfRow = new List<Cell>();
            
            foreach (Transform row in tableRoot)
            {
               
                Debug.Log($"row index = {rowIndex} ");
                
                row.GetComponentsInChildren<Cell>(cellsOfRow);
                for (int i = 0; i < GridSize; i++)
                {
                    cells[rowIndex, i] = cellsOfRow[i];
                    cells[rowIndex, i].SetManager(this);
                }
                rowIndex = rowIndex + 1;
            }

            IsRunning = true;
           // printCells();
        }

        public void Restart()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    cells[row, col].SetEmpty();
                }
            }
            IsRunning = true;
        }
        /// <summary>
        /// for debug purposes
        /// </summary>
        void printCells()
        {
            System.Text.StringBuilder sb = new StringBuilder();
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {

                    sb.Append($"row={cells[row, col].Row}, col={cells[row, col].Column}");
                }
                sb.Append("\n");
            }

            Debug.Log(sb.ToString());
        }

        public void OnNewPlayerTurn(int row,int col)
        {
            Debug.Log($"Done player turn row={row}, column={col}, newval= {cells[row,col]} ");


            if (CheckWinner(CellContent.PlayerTurnMark))
                OnGameEnd(CellContent.PlayerTurnMark);
            else
            {
                if (!CheckFreeCell())
                {
                    OnGameEnd(CellContent.Empty);
                    return;
                }
                SmartComputerTurn();
                //StartCoroutine(RandomComputerTurn());
            }

            if (CheckWinner(CellContent.ComputerTurnMark))
                OnGameEnd(CellContent.ComputerTurnMark);
        }

        void SmartComputerTurn()
        {
            Debug.Log("called TikTacToeGame::SmartComputerTurn()");
            if (!CheckFreeCell())
            {
                OnGameEnd(CellContent.Empty);
                return;
            }

            //ai.Reset(PrepareBoardData());
            Vector2Int selected = ai.GetComputerMove(PrepareBoardData());
            Debug.Log($" TikTacToeGame::SmartComputerTurn selected cell {selected}");
            cells[selected.x, selected.y].SetComputerTurnMark();
        }

        CellContent[,] board = new CellContent[GridSize, GridSize];
        CellContent[,]  PrepareBoardData()
        {
            Debug.Log("TikTacToeGame::PrepareBoardData() called ");
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                    board[row, col] = cells[row, col].ContentId;
            }
            return board;
        }

        bool SelectRandomCell()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                    if (cells[row, col].ContentId == CellContent.Empty)
                    {
                        if (Random.Range(0, 10) == 0)
                        {
                            cells[row, col].SetComputerTurnMark();
                            return true;
                        }
                                  
                    }
            }
            return false;
        }

        IEnumerator RandomComputerTurn()
        {
            yield return null;
            if (!CheckFreeCell())
            {
                OnGameEnd(CellContent.Empty);
                yield break;
            }

            for (int i = 0; i < 10; i++)
            {
                Debug.Log($"Selection started step  {i}");
                if (SelectRandomCell())
                {
                    Debug.Log($"Computer turn done at step {i}");
                    yield break;
                }
                yield return new WaitForSeconds(0.05f);
            }

            if (CheckWinner(CellContent.ComputerTurnMark))
                OnGameEnd(CellContent.ComputerTurnMark);
            Debug.Log("Failed random selection ");
        }

        bool CheckFreeCell()
        {
            for (int row = 0; row < GridSize; row++)
                for (int col = 0; col < GridSize; col++)
                    if (cells[row, col].ContentId == CellContent.Empty)
                        return true;
            return false;
        }

        void OnGameEnd(CellContent winner)
        {
            IsRunning = false;
            victoryPanel.Show(winner);
        }
        // check if mark placed to diagonal, horizontal or vertical eow.
        public bool CheckWinner(CellContent mark)
        {

            Debug.Log($"Started CheckWinner {mark} ");
            bool isWin = true;
            int col = 0, row = 0;

            // check diagonale 1
            for ( row = 0; row < GridSize; row++)
            {
                isWin = isWin && cells[row, row].ContentId == mark;
            }
            if (isWin)
            {
                Debug.Log($"CheckWinner: Win {mark}, диагональ 1 ");
                return true;
            }


            // check diagonale 2
            isWin = true;
            for ( row = 0, col = GridSize - 1; row < GridSize; row++, col--)
            {
                isWin = isWin && cells[row, col].ContentId == mark;
              //  Debug.Log($"diagonale 2:row={row}.col={col}, {cells[row, col].ContentId} isWin={isWin}");
            }

            if (isWin)
            {
                Debug.Log($"CheckWinner: Win {mark}, диагональ 2 ");
                return true;
            }

            // check rows, horizontal
            isWin = true;
            for (row = 0; row < GridSize; row++)
            {
                isWin = true;
                for (col = 0; col < GridSize; col++)
                {
                    isWin = isWin && cells[row, col].ContentId == mark;
                }
                if (isWin)
                {
                    Debug.Log($"CheckWinner: Win {mark}, строка {row}");
                    return true;
                }
            }

            // check columns, horizontal
            isWin = true;
            for (col = 0; col < GridSize; col++)
            {
                isWin = true;
                for (row = 0; row < GridSize; row++)
                {
                    isWin = isWin && cells[row, col].ContentId == mark;
                }
                if (isWin)
                {
                    Debug.Log($"CheckWinner: Win {mark}, столбец {col}");
                    return true;
                }
            }

            return false;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
