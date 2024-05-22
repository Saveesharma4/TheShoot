using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InAMinute
{
    public class FinalResultScreenController : MonoBehaviour
    {
        [SerializeField] Button backButton;
        [SerializeField] TextMeshProUGUI score;
        private GameEventHandler _gameEventHandler;

        public void SetGameEventHandler(GameEventHandler gameEventHandler)
        {
            _gameEventHandler = gameEventHandler;
            Debug.Log("GameEventHandler set.");
            RegisterCallbacks();
        }

        private void RegisterCallbacks()
        {
            if (backButton == null)
            {
                Debug.LogError("backButton is not assigned.");
                return;
            }

            if (_gameEventHandler == null)
            {
                Debug.LogError("GameEventHandler is not assigned.");
                return;
            }

            backButton.onClick.AddListener(() => { 
                Debug.Log("Back button clicked.");
                _gameEventHandler.OnShowMainMenu?.Invoke(); 
            });
        }
    }
}
