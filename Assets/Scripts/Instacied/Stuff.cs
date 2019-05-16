using System;
using Pool;
using UnityEngine;

namespace Instacied
{
    [RequireComponent(typeof(Rigidbody))]
    public class Stuff : PooledObject
    {
        public Rigidbody Body { get ; private set; }
        private void Awake()
        {
            Body = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name=="DeadZone") ReturnToPool();
        }
    }
}
