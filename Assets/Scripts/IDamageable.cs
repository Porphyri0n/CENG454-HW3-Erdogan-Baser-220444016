public interface IDamageable
{
    // Nesnenin hasar alma sözleþmesi
    void TakeDamage(int amount);

    // Nesnenin ölüp ölmediðini belirten durum
    bool IsDead { get; }
}