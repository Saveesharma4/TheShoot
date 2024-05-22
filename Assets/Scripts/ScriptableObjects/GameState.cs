using UnityEngine;

namespace InAMinute
{
    [CreateAssetMenu( fileName ="GameState", menuName ="InAMinute/Game State")]
    public class GameState : ScriptableObject
    {
        public float counter;
        public float currentPlayerScore;    
    }
}
