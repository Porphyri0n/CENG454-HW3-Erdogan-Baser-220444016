using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IResettable
{
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int damageToCore = 10;

    private int currentHealth;
    public Transform targetCore;
    private IEnemyMovement movementStrategy;

    public ObjectPool myPool;

    public bool IsDead => currentHealth <= 0;

    private void Start()
    {
        ResetState();
        SetMovementStrategy(new DirectRushStrategy());
    }

    public void SetMovementStrategy(IEnemyMovement strategy)
    {
        movementStrategy = strategy;
    }

    public void ResetState()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (targetCore != null && movementStrategy != null && !IsDead)
        {
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
        if (myPool != null)
        {
            myPool.ReturnToPool(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Core"))
        {
            IDamageable coreDamageable = other.GetComponent<IDamageable>();
            if (coreDamageable != null)
            {
                coreDamageable.TakeDamage(damageToCore);
                Die();
            }
        }
    }
}
