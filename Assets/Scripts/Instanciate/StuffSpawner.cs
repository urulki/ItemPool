using System;
using System.Collections.Generic;
using Instacied;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Instanciate
{
    public class StuffSpawner : MonoBehaviour
    {
        public float SpawnInterval;
        public List<Stuff> StuffPrefabs = new List<Stuff>();
        private float _timer;
        public float Velocity;

        private void FixedUpdate()
        {
            _timer += Time.deltaTime;
            if (_timer>= SpawnInterval) Spawner();
        }

        private void Spawner()
        {
            _timer -= SpawnInterval;
            Stuff prefab = StuffPrefabs[Random.Range(0, StuffPrefabs.Count)];
            Stuff instance = prefab.GetPooledInstance<Stuff>();
            instance.Body.velocity = transform.up * Velocity;
        }
    }
}
