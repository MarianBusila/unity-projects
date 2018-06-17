using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f; // the distance wwhen we should start interact with our object

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
