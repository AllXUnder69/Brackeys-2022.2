using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform headGunTip;
    [Space]
    [SerializeField] private Transform bullet;
    [SerializeField] private float bulletForce;

    [Space]
    [SerializeField] private Vector2 targetOffset;
    private Vector3 TargetPosition => Player.position + (Vector3)targetOffset;

    [Space]
    [SerializeField] private Enemy enemyPresets;

    [Space]
    [SerializeField] private Animation shootAnimation;

    Transform Player => GameObject.FindWithTag("Player").transform;

    float elapsedTime = 0f;
    bool initialShot = true;

    public virtual void Start()
    {
        bullet.GetComponent<Bullet>().BulletDamage = enemyPresets.damage;
    }
    public void Update()
    {
        GunLookAt();

        if (Vector2.Distance(Player.position, transform.position) < enemyPresets.triggerRange)
        {
            shootAnimation.Play("_Turret Body");

            if (elapsedTime < Time.time)
            {
                if (initialShot)
                {
                    initialShot = false;
                    elapsedTime = Time.time + enemyPresets.fireRate;
                }
                else
                    Shoot();
            }
        }
        else
        {
            shootAnimation.Stop("_Turret Body");

            initialShot = true;
        }
    }

    Vector2 lala;
    void GunLookAt()
    {
        lala = Player.position - head.position;

        if (Player.position.x < transform.position.x)
            head.localEulerAngles = Vector3.forward * Mathf.Atan(lala.y / lala.x) * Mathf.Rad2Deg;
        else if (Player.position.x > transform.position.x)
            head.localEulerAngles = Vector3.forward * (Mathf.Atan(lala.y / lala.x) - 91) * Mathf.Rad2Deg;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyPresets.triggerRange);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(TargetPosition, 0.2f);
    }

    public virtual void Shoot()
    {
        Rigidbody2D bulletRb = Instantiate(bullet, headGunTip.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        bulletRb.transform.localScale = Vector3.one / 2;

        Vector3 bulletTrajectory = -headGunTip.right * bulletForce * Vector2.Distance(Player.position, transform.position) / 5f;
        bulletRb.AddForce(bulletTrajectory, ForceMode2D.Impulse);

        elapsedTime = Time.time + enemyPresets.fireRate;

        Destroy(bulletRb.gameObject, 2f);
    }
}