using Instatied;
using UnityEngine;

namespace Management
{
    public class Spawner : MonoBehaviour
    {
        // Start is called before the first frame update
        public Entity LinkedEntity;

        private void Update()
        {
            if (LinkedEntity != null && !LinkedEntity.gameObject.activeInHierarchy) LinkedEntity = null;
        }
    }
}
