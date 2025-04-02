using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTrasform = collision.transform;
        if (hitTrasform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            hitTrasform.GetComponent<PlayerHealth>().TakeDamage(10);
        }
        Destroy(gameObject);

    }
}
