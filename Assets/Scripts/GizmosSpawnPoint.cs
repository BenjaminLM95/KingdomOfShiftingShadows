using UnityEngine;

public class GizmosSpawnPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {

        // Draw a sphere at the spawn point
        Gizmos.color = Color.cyan;   // You can change the color here
        Gizmos.DrawWireSphere(transform.position, 0.5f); // Draw a sphere at the spawn point location with radius 0.5


    }
}
