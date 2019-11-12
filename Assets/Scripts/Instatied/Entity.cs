using System;
using Pool;
using UnityEngine;
using UnityEngine.AI;

namespace Instatied
{
    [RequireComponent(typeof(Rigidbody))]
    public class Entity : PooledObject
    {
        public Rigidbody Body { get ; private set; }
        
        public int ID;
        private void Awake()
        {
            Body = GetComponent<Rigidbody>();
            
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Body.AddForce(Vector3.up*20, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Entity>())
            {
                ObjectPool.SharedInstance.ReturnToPool(gameObject);
            }
        }
    }
}
