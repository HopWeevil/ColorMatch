using UnityEngine;

namespace CodeBase.Logic.GameBasket
{
    public class BasketSpawnPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}