using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InAMinute
{
    public class GameEventHandler
    {


    [SerializeField] ScoreUIController scoreUIController;

        private PlayerControls _playerControls;  

        public GameEventHandler()
        {

            _playerControls = new PlayerControls();
            _playerControls.Enable(); 
            _playerControls.Player.Fire.performed += OnPlayerFired; 
            _playerControls.Player.Look.performed += OnPlayerLook; 
        }

        


        #region INPUT_EVENTS

        public event Action OnFire; 

        private void OnPlayerFired(InputAction.CallbackContext inputContext)
        {
            float fired = inputContext.ReadValue<float>();

            if(fired == 1.0f)
            {
                OnFire?.Invoke(); 
            }
        }

        public event Action<Vector2> OnLook; 
        private void OnPlayerLook(InputAction.CallbackContext obj)
        {
            Vector2 pointerDelta = obj.ReadValue<Vector2>();
            OnLook?.Invoke(pointerDelta); 
        }
public void OnGameEnd()
    {
        scoreUIController.UpdateFinalScore();
        
        // Other game end logic...
    }
        #endregion

        #region GAME_EVENTS
        public Action OnShowMainMenu;
        public Action OnStartGame;
        public Action OnGameOver;
        public Action OnQuitGame; 
        #endregion
    }
}
