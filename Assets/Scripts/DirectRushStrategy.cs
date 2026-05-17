using UnityEngine;

public class DirectRushStrategy : IEnemyMovement
{
    public void Move(Transform enemyTransform, Transform target, float speed)
    {
        Vector3 direction = (target.position - enemyTransform.position).normalized;
        enemyTransform.position += direction * speed * Time.deltaTime;
    }
}

public class ZigZagStrategy : IEnemyMovement
{
    private float frequency = 2f;
    private float magnitude = 1.5f;

    public void Move(Transform enemyTransform, Transform target, float speed)
    {
        Vector3 forwardDirection = (target.position - enemyTransform.position).normalized;

        Vector3 rightDirection = Vector3.Cross(forwardDirection, Vector3.up);
        Vector3 zigZagOffset = rightDirection * Mathf.Sin(Time.time * frequency) * magnitude;

        Vector3 moveDirection = (forwardDirection * speed) + zigZagOffset;
        enemyTransform.position += moveDirection * Time.deltaTime;
    }
}