using System.Collections.Generic;
using Instacied;
using UnityEngine;

namespace Management.S.O
{
    [CreateAssetMenu(fileName = "newSpawnPref", menuName = "Pref/SpawnPref")]
    public class SpawnPref : ScriptableObject
    {
        public List<Entity> ObjectsToPool = new List<Entity>();
        public int AmountOfInstance;
    }
}
