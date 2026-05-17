using UnityEngine;

// Strateji 1: Hedefe düz, agresif bir þekilde koþan hareket
public class DirectRushStrategy : IEnemyMovement
{
    public void Move(Transform enemyTransform, Transform target, float speed)
    {
        Vector3 direction = (target.position - enemyTransform.position).normalized;
        enemyTransform.position += direction * speed * Time.deltaTime;
    }
}

// Strateji 2: Hedefe zikzak çizerek yaklaþan kaçýnmacý hareket
public class ZigZagStrategy : IEnemyMovement
{
    private float frequency = 2f;
    private float magnitude = 1.5f;

    public void Move(Transform enemyTransform, Transform target, float speed)
    {
        // Hedefe doðru ana yön
        Vector3 forwardDirection = (target.position - enemyTransform.position).normalized;

        // Yöne dik bir vektör alýp sinüs dalgasý ile zikzak yaratýyoruz
        Vector3 rightDirection = Vector3.Cross(forwardDirection, Vector3.up);
        Vector3 zigZagOffset = rightDirection * Mathf.Sin(Time.time * frequency) * magnitude;

        // Hem ileri hem de zikzak hareketini birleþtirerek uyguluyoruz
        Vector3 moveDirection = (forwardDirection * speed) + zigZagOffset;
        enemyTransform.position += moveDirection * Time.deltaTime;
    }
}