using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemy = new();
    [SerializeField] private int firstWaveCount;
    [SerializeField] private int lastWaveCount;
    [SerializeField] private EnemyDetector enemyDetector;
    [SerializeField] private TargetHandler targetHandler;

    private List<GameObject> enemiesInPool = new();
    public List<GameObject> EnemiesInPool => enemiesInPool; 

    public static EnemyPooling instance;

    private int counter;

    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        for (int i = 0; i < lastWaveCount; i++)
        {
            GameObject e = Instantiate(enemy[0]);
            enemiesInPool.Add(e);
            e.SetActive(false);
            if (i >= firstWaveCount)
                { continue; }
            counter++;
            EnemyHandler(e);
        }
    }

    public void EnemyHandler(GameObject temp)
    {
        StartCoroutine(Spawner(temp));
    }

    private IEnumerator Spawner(GameObject temp)
    {
        float x = UnityEngine.Random.Range(-enemyDetector.XRadius - 7, enemyDetector.XRadius + 7);
        float z = UnityEngine.Random.Range(-enemyDetector.XRadius - 7, enemyDetector.XRadius + 7);
        if (MathF.Abs(x) < enemyDetector.XRadius)
        {
            if (x > 0)
                x += enemyDetector.XRadius;

            else
                x += -enemyDetector.XRadius;
        }
        if (MathF.Abs(z) < enemyDetector.XRadius)
        {
            if (z > 0)
                z += enemyDetector.XRadius;

            else
                z += -enemyDetector.XRadius;
        }


        Vector3 pos = new(x, 0.5f, z);
        Vector3.Max(pos, transform.position);
        temp.transform.position = pos;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0,3f));
        temp.SetActive(true);
        Debug.Log(temp.name);
    }

    public void IncreaseEnemyAmount(int increaseAmount)
    {
        for (int i = 0; i < increaseAmount; i++)
        {
            enemiesInPool[counter].SetActive(true);
            counter++;
        }
    }
}
