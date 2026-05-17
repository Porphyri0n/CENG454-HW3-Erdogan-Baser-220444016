using UnityEngine;

public interface IEnemyMovement
{
    // Hareket stratejisi için ortak sözleþme
    void Move(Transform enemyTransform, Transform target, float speed);
}