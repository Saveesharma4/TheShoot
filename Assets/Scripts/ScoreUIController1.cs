/*using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random; 


namespace InAMinute 
{ 
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI score;
        [SerializeField] TextMeshProUGUI counter;

        [SerializeField] GameState gameState; 

        private GameEventHandler _gameEventHandler;

        public void SetGameEventHandler(GameEventHandler gameEventHandler)
        {
            _gameEventHandler = gameEventHandler;
        }

        void Update()
        {
            score.text = "Score: "+gameState.currentPlayerScore.ToString("F0");
            counter.text = gameState.counter.ToString("F1");
        }
    }
}
*/
