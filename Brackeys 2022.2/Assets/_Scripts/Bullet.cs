using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private UnityEngine.Events.UnityEvent OnCollisionEvent;

    public int BulletDamage { get; set; }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        print(health != null);

        if (health != null)
            health.TakeDamage(BulletDamage);

        Destroy(gameObject);
    }
}