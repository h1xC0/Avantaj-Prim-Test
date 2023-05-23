using BaseInfrastructure;
using UnityEngine;

namespace MainComponents.Customers
{
    public class CustomerSpawnPoint : BaseView
    {
        private bool _isEmpty = true;
        public bool IsEmpty => _isEmpty;
        public Vector3 SpawnPosition => transform.position;

        public void SetEmptyState(bool isEmpty)
        {
            _isEmpty = isEmpty;
        }
    }
}