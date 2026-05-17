using UnityEngine;

public class DirectRushStrategy : IEnemyMovement
{
    public void Move(Transform enemyTransform, Transform target, float speed)
    {
        Vector3 direction = (target.position - enemyTransform.position).normalized;
        enemyTransform.position += direction * speed * Time.deltaTime;
    }
}