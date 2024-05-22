using System;
using System.Collections.Generic;
using UnityEngine;

namespace InAMinute
{
    [Serializable]
    public class ScoreInstance
    {
        public int score;
        //TODO: add time stamp for when excatly the score achieved. 
    }

    [CreateAssetMenu(fileName ="PlayerInfo", menuName ="InAMinute/Player Info")]
    public class PlayerInfo : ScriptableObject
    {
        public string username; 
        public string displayName;
        public string password; 

        [SerializeReference] public List<ScoreInstance> scores; 
    }

}
