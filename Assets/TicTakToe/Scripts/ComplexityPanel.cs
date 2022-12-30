using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TicTacToe
{
    public enum Complexity
    { 
        Easy,
        Middle,
        Hard
    }
    
    public class ComplexityPanel : MonoBehaviour
    {
        [SerializeField]
        Toggle Easy;

        [SerializeField]
        Toggle Middle;

        [SerializeField]
        Toggle Hard;

        [SerializeField]
        Button startButton;

        [Header("Visible for debug purposes")]
        [SerializeField]
        Complexity currentComplexity = Complexity.Easy;

        System.Action onApply;
        TikTacToeGame game;
        public void Initialize(TikTacToeGame gameManager, System.Action onApplyHandler = null)
        {
            game = gameManager;
            onApply = onApplyHandler;
        }
        

        private void Start()
        {
                    
            Easy.onValueChanged.AddListener(isOn => OnToggle(Complexity.Easy, isOn));
            Middle.onValueChanged.AddListener(isOn => OnToggle(Complexity.Middle, isOn));
            Hard.onValueChanged.AddListener(isOn => OnToggle(Complexity.Hard, isOn));
            startButton.onClick.AddListener(OnStartGame);
        }

      
        public void Show(bool isShown = true)
        {
            gameObject.SetActive(isShown);
        }

        public void Hide()
        {
            Show(false);
        }
        void OnToggle(Complexity level, bool isOn)
        {
           // Debug.Log($"Chosen {level}, {isOn} ");
            if (isOn)
            {
                currentComplexity = level;
                Debug.Log($"new complexity {currentComplexity}");
            }
        }

        void OnStartGame()
        {
            Debug.Log("called OnStartGame()");
            Hide();
            game.SetComplexity(currentComplexity);
            onApply?.Invoke();
        }
    }
}
