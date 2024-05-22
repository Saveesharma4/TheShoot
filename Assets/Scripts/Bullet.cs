using System;
using UnityEngine;

namespace InAMinute
{
    public class Bullet : MonoBehaviour
    {
        public Action<Bullet> OnHitSomething;

        private void OnCollisionEnter(Collision collision)
        {
            OnHitSomething?.Invoke(this); 
        }
    }
}