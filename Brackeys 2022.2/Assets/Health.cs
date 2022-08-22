using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    [Space]
    [SerializeField] private UnityEngine.Events.UnityEvent OnDeathEvent;

    void Awake()
    {
        currentHealth = maxHealth;   
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth < 0f)
        {
            OnDeathEvent?.Invoke();
            print("You dead bro");
            //Destroy(gameObject);
        }
    }
}