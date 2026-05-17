using System;
using UnityEngine;

public class Core : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public bool IsDead => currentHealth <= 0;

    public static event Action<int, int> OnCoreHealthChanged;
    public static event Action OnCoreDestroyed;

    private void Start()
    {
        currentHealth = maxHealth;
        OnCoreHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0); 
        OnCoreHealthChanged?.Invoke(currentHealth, maxHealth);

        if (IsDead)
        {
            Debug.Log("Core Destroyed! Game Over.");
            OnCoreDestroyed?.Invoke();
        }
    }
}
