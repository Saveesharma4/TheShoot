using UnityEngine;

namespace InAMinute
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] UIController uiController;
        [SerializeField] TargetController targetController;
        [SerializeField] PlayerController playerController;
        [SerializeField] GameObject staticGeom; 

        private GameEventHandler _gameEventHandler;  
    
        void Awake()
        {
            Debug.Log("GameManager Awake");

            _gameEventHandler = new GameEventHandler();         
            
            // Ensure the following references are assigned in the Inspector
            CheckInspectorAssignments();

            // Initialize GameEventHandler and set it for controllers
            InitializeGameEventHandler();

            // Subscribe to GameEventHandler events
            SubscribeToEvents();
        }

        private void CheckInspectorAssignments()
        {
            if (uiController == null)
            {
                Debug.LogError("UIController is not assigned in GameManager.");
            }

            if (targetController == null)
            {
                Debug.LogError("TargetController is not assigned in GameManager.");
            }

            if (playerController == null)
            {
                Debug.LogError("PlayerController is not assigned in GameManager.");
            }
        }

        private void InitializeGameEventHandler()
        {
            _gameEventHandler = new GameEventHandler();         
            
            // Ensure _gameEventHandler is not null after initialization
            if (_gameEventHandler == null)
            {
                Debug.LogError("GameEventHandler is null after initialization.");
            }

            // Set GameEventHandler for controllers
            if (uiController != null)
            {
                uiController.SetGameEventHandler(_gameEventHandler);
                Debug.Log("UIController SetGameEventHandler called.");
            }

            if (targetController != null)
            {
                targetController.SetGameEventHandler(_gameEventHandler);
                Debug.Log("TargetController SetGameEventHandler called.");
            }

            if (playerController != null)
            {
                playerController.SetGameEventHandler(_gameEventHandler);
                Debug.Log("PlayerController SetGameEventHandler called.");
            }
        }

        private void SubscribeToEvents()
        {
            if (_gameEventHandler != null)
            {
                _gameEventHandler.OnQuitGame += OnQuit;
                _gameEventHandler.OnStartGame += OnGameStarted; 
                _gameEventHandler.OnGameOver += OnGameFinished; 
            }
            else
            {
                Debug.LogError("GameEventHandler is null. Cannot subscribe to events.");
            }
        }

        private void OnQuit()
        {
#if !UNITY_EDITOR
            Application.Quit();
#endif
        }

        private void OnGameStarted()
        {
            Cursor.visible = false; 
            Cursor.lockState = CursorLockMode.Locked;
            staticGeom.SetActive(true);
        }

        private void OnGameFinished()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            staticGeom.SetActive(false);
        }
    }
}
