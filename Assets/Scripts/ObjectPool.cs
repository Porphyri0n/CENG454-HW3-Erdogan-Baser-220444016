using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefabToPool;
    [SerializeField] private int poolSize = 10;
    
    [Header("Auto Spawner Settings")]
    [SerializeField] private bool autoSpawn = false;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Transform spawnPoint;

    private Queue<GameObject> poolQueue;
    private Core gameCore;

    private void Awake()
    {
        poolQueue = new Queue<GameObject>();
        InitializePool();
    }

    private void OnEnable()
    {
        SurvivalTimer.OnTimeSurvived += StopSpawning;
    }

    private void OnDisable()
    {
        SurvivalTimer.OnTimeSurvived -= StopSpawning;
    }

    private void StopSpawning()
    {
        CancelInvoke(nameof(SpawnRoutine));
    }

    private void Start()
    {
        gameCore = FindObjectOfType<Core>();
        
        if (autoSpawn)
        {
            InvokeRepeating(nameof(SpawnRoutine), spawnInterval, spawnInterval);
        }
    }

    private void SpawnRoutine()
    {
        if (gameCore != null && gameCore.IsDead) return;

        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
        GameObject spawnedObj = GetFromPool(spawnPos, Quaternion.identity);

        Enemy enemyComponent = spawnedObj.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.myPool = this;
            if (gameCore != null)
            {
                enemyComponent.targetCore = gameCore.transform;
            }
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefabToPool, transform);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);

            IResettable resettable = obj.GetComponent<IResettable>();
            resettable?.ResetState();

            return obj;
        }

        GameObject newObj = Instantiate(prefabToPool, position, rotation, transform);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}
