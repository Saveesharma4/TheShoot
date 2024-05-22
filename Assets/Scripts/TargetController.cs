using UnityEngine;

namespace InAMinute
{
    public class TargetController : MonoBehaviour
    {
        private GameEventHandler _gameEventHandler;
    
        [SerializeField] private GameState gameState;
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private GameObject type1Target;
        public float maxRadius = 25f; // Radius of Sphere A
        public float minRadius = 15f; // Radius of Sphere B

        private ObjectPool _type1TargetPool;
        private bool _gameStarted; 


        private void Awake()
        {
            _type1TargetPool = new ObjectPool(type1Target, 30); 
        }


        public void SetGameEventHandler(GameEventHandler gameEventHandler)
        {
            _gameEventHandler = gameEventHandler;
    
            _gameEventHandler.OnStartGame += GameStarted;
            _gameEventHandler.OnQuitGame += GameStopped; 
        }
    
        private void GameStopped()
        {
            enabled = false;
            _gameStarted = false; 
            gameState.counter = 60;
            gameState.currentPlayerScore = 0; 
        }
    
        private void GameStarted()
        {
            enabled = true;
            _gameStarted = true; 
            gameState.counter = 60;
            gameState.currentPlayerScore = 0; 
            GenerateTargets();
        }


        public void GenerateTargets()
        {
            for (int i = 0; i < 30; i++)
            { 
                GameObject targetObject = _type1TargetPool.GetGameObject(GenerateRandomPoint(), Quaternion.identity);
                Target target = targetObject.GetComponent<Target>();
                target.OnShotDown = OnTargetShotDonw; 
            }
        }

        private void OnTargetShotDonw(Target target)
        {
            target.OnShotDown = null; 
            _type1TargetPool.RePoolGameObject(target.gameObject);
            gameState.currentPlayerScore++; 
        }

        void Update()
        {
            if(!_gameStarted)
            {
                return; 
            }
            gameState.counter -= Time.deltaTime;

            if(gameState.counter <=0)
            {
                enabled = false; 
                gameState.counter = 1; 
                _gameEventHandler.OnGameOver?.Invoke(); 
            }
        }


        Vector3 GenerateRandomPoint()
        {
            // Randomize x, y, and z components within a specified range
            float x = Random.Range(-100f, 100f);
            float y = Random.Range(-100f, 100f);
            float z = Random.Range(-100f, 100f);

            // Create a random direction vector
            Vector3 randomDirection = new Vector3(x, y, z).normalized;

            // Generate a random distance within the specified range
            float randomDistance = Random.Range(minRadius, maxRadius);

            // Calculate the final random point
            Vector3 randomPoint = randomDirection.normalized * randomDistance;

            return randomPoint;
        }


    }

}