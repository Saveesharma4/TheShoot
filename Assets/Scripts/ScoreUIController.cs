using TMPro;
using UnityEngine;

namespace InAMinute 
{ 
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI score;
         [SerializeField] TextMeshProUGUI score1;
        [SerializeField] TextMeshProUGUI counter;
        [SerializeField] TextMeshProUGUI finalScore;

        [SerializeField] GameState gameState; 

        private GameEventHandler _gameEventHandler;

        public void SetGameEventHandler(GameEventHandler gameEventHandler)
        {
            _gameEventHandler = gameEventHandler;
        }

        void Update()
        {
            score.text = "Score: " + gameState.currentPlayerScore.ToString("F0");
            score1.text = "Final Score: " + gameState.currentPlayerScore.ToString("F0");
            counter.text = gameState.counter.ToString("F1");
        }

        public void UpdateFinalScore()
        {
            finalScore.text = "Final Score: " + gameState.currentPlayerScore.ToString("F0");
        }
    }
}