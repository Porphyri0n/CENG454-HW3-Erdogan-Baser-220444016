using System;
using UnityEngine;

public class Core : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public bool IsDead => currentHealth <= 0;

    // Olay (Event) Tanýmlamalarý - Observer Deseni
    // Diðer sistemler (UI, Ses vb.) bu olaylara abone olacak
    public static event Action<int, int> OnCoreHealthChanged; // Mevcut Can, Maksimum Can
    public static event Action OnCoreDestroyed;

    private void Start()
    {
        currentHealth = maxHealth;
        // Baþlangýçta UI'ýn güncellenmesi için bir kez tetikliyoruz
        OnCoreHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0); // Canýn eksiye düþmesini engelliyoruz

        // Sadece olayý yayýnlýyoruz (Publish). UI veya Audio'yu direkt çaðýrmýyoruz.
        OnCoreHealthChanged?.Invoke(currentHealth, maxHealth);

        if (IsDead)
        {
            Debug.Log("Çekirdek Yýkýldý! Oyun Kaybedildi.");
            OnCoreDestroyed?.Invoke();
        }
    }
}