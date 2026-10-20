using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > 0.02f)
        {
            Instantiate(Fractures, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    public GameObject Fractures;
}
