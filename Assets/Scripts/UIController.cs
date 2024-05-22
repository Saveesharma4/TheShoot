using UnityEngine;
using UnityEngine.UI;

namespace InAMinute
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject gameScreen;
        [SerializeField] private GameObject resultScreenObject;
        

        [Header("Game Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        
        private GameEventHandler _gameEventHandler; 

        public void SetGameEventHandler(GameEventHandler gameEventHandler)
        {
            _gameEventHandler = gameEventHandler;
            RegisterCallbacks();
            resultScreenObject.GetComponent<FinalResultScreenController>().SetGameEventHandler(gameEventHandler);
        }

        private void ShowFinalScores()
        {
            gameScreen.SetActive(false);
            resultScreenObject.SetActive(true);
        }

        private void Awake()
        {
            gameScreen.SetActive(false);
            mainMenu.SetActive(true);
            resultScreenObject.SetActive(false);    

            playButton.onClick.AddListener(() =>
            {
                _gameEventHandler.OnStartGame?.Invoke();
            });
            quitButton.onClick.AddListener(() => { 
                _gameEventHandler.OnQuitGame?.Invoke(); 
            }); 

        }

        private void RegisterCallbacks()
        {
            _gameEventHandler.OnShowMainMenu += ShowMainMenu; 
            _gameEventHandler.OnStartGame += ShowGameScreen;
            _gameEventHandler.OnGameOver += ShowFinalScores;
        }

        private void ShowMainMenu()
        {
            mainMenu.SetActive(true); 
            resultScreenObject.SetActive(false); 
            gameScreen.SetActive(false);
        }

        private void ShowGameScreen()
        {
            gameScreen.SetActive(true); 
            mainMenu.SetActive(false); 
        }

        private void UnRegisterCallakcs()
        {
            _gameEventHandler.OnShowMainMenu -= ShowMainMenu; 
            _gameEventHandler.OnStartGame -= ShowGameScreen; 
        }

        private void OnDestroy()
        {
            UnRegisterCallakcs(); 
        }
    }
}