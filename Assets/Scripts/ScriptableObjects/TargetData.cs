using UnityEngine;

namespace InAMinute
{
    [CreateAssetMenu(fileName ="TargetData", menuName ="InAMinute/Target Data")]
    public class TargetData : ScriptableObject
    {
        public float targetSpeed;
        public float wingspeed;
        public float turnDelay; 
    }

}