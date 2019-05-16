using System;
using Pool;
using UnityEngine;
using UnityEngine.AI;

namespace Instacied
{
    [RequireComponent(typeof(Rigidbody),typeof(NavMeshAgent))]
    public class Entity : PooledObject
    {
        public Rigidbody Body { get ; private set; }
        public NavMeshAgent Agent { get; private set; }
        public Entity Target;
        public int ID;
        private void Awake()
        {
            Body = GetComponent<Rigidbody>();
            Agent = GetComponent<NavMeshAgent>();
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Target != null) Agent.SetDestination(Target.transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Entity>())
            {
                Target = null;
                ObjectPool.SharedInstance.ReturnToPool(gameObject);
            }
        }
    }
}
