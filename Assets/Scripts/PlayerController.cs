using System;
using UnityEngine;

namespace InAMinute
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform canon;
        [SerializeField] private GameObject bulletObject;
        [SerializeField] private float bulletSpeed = 50;

        [SerializeField, Range(10, 100)] private float cameraSensitivity = 50;
        [SerializeField] private Transform firedBullets;


        #region CAMERA_VARIABLES

        public float movmentSpeed = 1; 
        private const float ROTATION_DELTA_MODIFIEER = 0.1f;
        private const float SCREEN_MULTIPLIER_OFFSET = 1000;

        public float minClampDeltaX = -85;
        public float maxClampDeltaX = 85;
        public float minClampDeltaY = -180;
        public float maxClampDeltaY = 180;
        private Vector2 _inputDelta; 

        private Vector2 _screenSize = Vector2.zero;
        #endregion


        private GameEventHandler _gameEventHandler;
        private ObjectPool _bulletPool;
    
        private void Awake()
        {
            _bulletPool = new ObjectPool(bulletObject, 10);
            _screenSize = new Vector2(Screen.width, Screen.height); 
        }
    
        public void SetGameEventHandler(GameEventHandler gameEventHandler)
        {
            _gameEventHandler = gameEventHandler;

            _gameEventHandler.OnStartGame += ActivatePlayer;
            _gameEventHandler.OnGameOver += DeactivatePlayer; 
        }

        private void DeactivatePlayer()
        {
            _gameEventHandler.OnFire -= Fire;
            _gameEventHandler.OnLook -= OnLook;
        }

        private void ActivatePlayer()
        {
            _gameEventHandler.OnFire += Fire;
            _gameEventHandler.OnLook += OnLook;
        }

        private void Fire()
        {
            GameObject bullet = _bulletPool.GetGameObject();
    
            RaycastHit hit; 
    
            if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 1004f))
            {
                bullet.transform.position = canon.position + 3f * cameraTransform.forward;
                Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                bulletComponent.OnHitSomething = ReturnBullet; 
                Vector3 direction = hit.point - bullet.transform.position; 
                rigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode.Impulse);
                bullet.transform.SetParent(firedBullets.transform);
            }
        }
    
        private void ReturnBullet(Bullet bullet)
        {
            bullet.OnHitSomething = null;
            bullet.transform.rotation = Quaternion.identity;    
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero; 
            _bulletPool.RePoolGameObject(bullet.gameObject); 
        }

        private void OnLook(Vector2 inputDelta)
        {
            _inputDelta = inputDelta; 

            Vector3 rotation = cameraTransform.localEulerAngles;

            _inputDelta.x = (SCREEN_MULTIPLIER_OFFSET / _screenSize.x) * _inputDelta.x;
            _inputDelta.y = (SCREEN_MULTIPLIER_OFFSET / _screenSize.y) * _inputDelta.y;

            Vector3 rotationDelta = (_inputDelta * movmentSpeed * ROTATION_DELTA_MODIFIEER);
            rotation += new Vector3(rotationDelta.y, -rotationDelta.x);

            cameraTransform.localEulerAngles = ClampCameraRotation(rotation);
        }

        private Vector3 ClampCameraRotation(Vector3 rotation)
        {
            rotation.x = ClampAngle(rotation.x, minClampDeltaX, maxClampDeltaX);
            rotation.y = ClampAngle(rotation.y, minClampDeltaY, maxClampDeltaY);
            return rotation;
        }


        public float ClampAngle(float rotateAngle, float minClampValue, float maxClampValue)
        {

            if(rotateAngle > 180f)
            {
                rotateAngle -= 360f; 
            }

            return Mathf.Min(maxClampValue, Mathf.Max(minClampValue, rotateAngle));

        }
    }
    
}