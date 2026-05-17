using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int damageToCore = 10;

    private int currentHealth;
    public Transform targetCore;

    // Strateji arayüzü
    private IEnemyMovement movementStrategy;

    public bool IsDead => currentHealth <= 0;

    private void Start()
    {
        currentHealth = maxHealth;

        // Þimdilik varsayýlan bir strateji atýyoruz. Aþama 3'te Spawner üzerinden atanacak.
        SetMovementStrategy(new DirectRushStrategy());
    }

    // Strateji çalýþma zamanýnda (Runtime) dýþarýdan deðiþtirilebilir
    public void SetMovementStrategy(IEnemyMovement strategy)
    {
        movementStrategy = strategy;
    }

    private void Update()
    {
        if (targetCore != null && movementStrategy != null && !IsDead)
        {
            // Düþman nasýl hareket ettiðini bilmez, strateji sýnýfý bu iþi çözer.
            movementStrategy.Move(transform, targetCore, moveSpeed);
        }
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        currentHealth -= amount;
        if (IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        // Þimdilik nesneyi yok ediyoruz, Aþama 3'te Object Pool'a (Havuza) geri göndereceðiz.
        Destroy(gameObject);
    }

    // Çekirdeðe temas ettiðinde hasar ver
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Core"))
        {
            IDamageable coreDamageable = other.GetComponent<IDamageable>();
            if (coreDamageable != null)
            {
                coreDamageable.TakeDamage(damageToCore);
                Die(); // Kamikaze mantýðý, hasar verince ölür
            }
        }
    }
}