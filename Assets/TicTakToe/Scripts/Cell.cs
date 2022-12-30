using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public enum CellContent
    { 
        Empty,
        PlayerTurnMark,
        ComputerTurnMark
    }
    public class Cell : MonoBehaviour
    {
        [Header("Assign row and column of this cell")]
        [SerializeField]
        int _rowIndex;
        public int Row => _rowIndex;

        [SerializeField]
        int _colIndex;
        public int Column => _colIndex;

        [SerializeField]
        Image PlayerTurnMark;
        
        [SerializeField]
        Image ComputerTurnMark;

        Button btn;

        [Header("Visible for debug purposes")]
        [SerializeField]
        CellContent _contentId;

        public CellContent ContentId { get => _contentId; }
        public void SetComputerTurnMark()
        {
            Debug.Log($"called SetComputerTurnMark for cell ({Row},{Column}) ");
            if (ContentId != CellContent.Empty)
                Debug.LogError($"Cell {Row}, {Column} is already contains {ContentId}");
            else
            {
                _contentId = CellContent.ComputerTurnMark;
                Debug.Log($"SetComputerTurnMark: Placed {_contentId} at Cell {Row}, {Column} ");
                UpdateMark();
            }
        }

        public void SetEmpty()
        {
            _contentId = CellContent.Empty;
            UpdateMark();
        }


// Start is called before the first frame update
        TikTacToeGame gameManager;

        public void SetManager(TikTacToeGame newGameManager)
        {
            gameManager = newGameManager;
        }


        private void Awake()
        {
            if(btn == null)
                btn = GetComponent<Button>();
            btn.onClick.AddListener(OnCellClicked);
            _contentId = CellContent.Empty;
        }

        public void UpdateMark()
        {
            PlayerTurnMark.gameObject.SetActive(_contentId == CellContent.PlayerTurnMark);
            ComputerTurnMark.gameObject.SetActive(_contentId == CellContent.ComputerTurnMark);
        }

        void OnCellClicked()
        {
            if (_contentId == CellContent.Empty && gameManager.IsRunning)
            {
                _contentId = CellContent.PlayerTurnMark;
                UpdateMark();
                gameManager.OnNewPlayerTurn(Row,Column);
            }
        }

        void Start()
        {
            UpdateMark();
        }

    }
}
