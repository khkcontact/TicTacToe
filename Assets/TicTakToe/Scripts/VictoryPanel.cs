using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TicTacToe
{
    public class VictoryPanel : MonoBehaviour
    {
        [SerializeField]
        GameObject ComputerWinObject;
        
        [SerializeField]
        GameObject HumanWinObject;

        [SerializeField]
        GameObject NobodyWinObject;

        [SerializeField]
        Button ButtonOK;

        TikTacToeGame game;
        public void SetGameManager(TikTacToeGame gameManager) => game = gameManager;

        // Start is called before the first frame update
        void Start()
        {
            ButtonOK.onClick.AddListener(ResetGame);
        }

        public void Show(CellContent winner)
        {
            ComputerWinObject.SetActive(winner == CellContent.ComputerTurnMark);
            HumanWinObject.SetActive(winner == CellContent.PlayerTurnMark);
            NobodyWinObject.SetActive(winner == CellContent.Empty);
            gameObject.SetActive(true);
        }

        void ResetGame()
        {
            game.Restart();
            Hide();
        }

        public void Hide()
        {
            Show(CellContent.Empty);
            gameObject.SetActive(false);
        }


    }
}
