using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private Transform gunTip;
    [Space]
    [SerializeField] private Transform bullet;
    [SerializeField] private float bulletForce;

    [Space]
    [SerializeField] private Enemy enemy;

    Transform player;

    float elapsedTime = 0f;
    [SerializeField] bool initialShot = true;

    public virtual void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        bullet.GetComponent<Bullet>().bulletDamage = enemy.damage;
    }
    public virtual void Update()
    {
        gun.LookAt(player, Vector3.right);

        if (player.position.x < transform.position.x)
            transform.eulerAngles = Vector3.up * 0;
        else if (player.position.x > transform.position.x)
            transform.eulerAngles = Vector3.up * 180;

        if (Vector2.Distance(player.position, transform.position) < enemy.triggerRange)
        {
            if (elapsedTime < Time.time)
            {
                if (initialShot)
                {
                    initialShot = false;
                    elapsedTime = Time.time + enemy.fireRate;
                }
                else
                    Shoot();
            }
        }
        else
            initialShot = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemy.triggerRange);
    }

    public virtual void Shoot()
    {
        Rigidbody2D bulletRb = Instantiate(bullet, gunTip.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        bulletRb.transform.localScale = Vector3.one / 2;

        Vector3 bulletTrajectory = gun.forward * bulletForce * Vector2.Distance(player.position, transform.position) / 5f;
        bulletRb.AddForce(bulletTrajectory, ForceMode2D.Impulse);

        elapsedTime = Time.time + enemy.fireRate;

        Destroy(bulletRb.gameObject, 2f);
    }
}