using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InAMinute
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private Transform wings;
        [SerializeField] private TargetData targetData;  // Serialized field to adjust anchor reposition time
        [SerializeField] private Vector3 anchorPosition;

        private Vector3 originalPosition;
        private float timeSinceLastReposition;
        public Action<Target> OnShotDown;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Bullet")
            { 
               
                OnShotDown?.Invoke(this);
            }
        }


        private void Start()
        {
            originalPosition = transform.position;
            RespositionAnchor();
        }

        private void Update()
        {
           Maneuver();
          
        }

        private void Maneuver()
        {
            timeSinceLastReposition += Time.deltaTime;

            // Reposition the anchor after a specified time interval
            if (timeSinceLastReposition >= targetData.turnDelay)
            {
                RespositionAnchor();
                timeSinceLastReposition = 0f;
            }

            // Calculate the direction from the current position to the anchor
            Vector3 directionToAnchor = (anchorPosition - transform.position).normalized;

            // Calculate the lerped direction between current forward and direction to the anchor
            Vector3 lerpedDirection = Vector3.Lerp(transform.forward, directionToAnchor, Time.deltaTime * targetData.targetSpeed);
            transform.forward = lerpedDirection;

            transform.position += transform.forward * (Time.deltaTime * targetData.targetSpeed);
        }

        private void RespositionAnchor()
        {
            // Randomly set the anchor position within a sphere around the original position
            Vector3 anchorPosition = originalPosition + Random.insideUnitSphere * 5f;
            Vector3 newAnchorPosition = new Vector3(anchorPosition.x, anchorPosition.y, originalPosition.z);
            this.anchorPosition = newAnchorPosition;
        }
    }
}