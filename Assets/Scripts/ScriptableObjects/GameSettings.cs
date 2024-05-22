using UnityEngine;

namespace InAMinute
{

    [CreateAssetMenu(fileName = "GameSettings", menuName = "InAMinute/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Range(1, 10f)]
        public float difficulty; 
    }
}