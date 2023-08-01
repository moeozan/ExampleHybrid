using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    [SerializeField] private EnemyPooling enemyPool;
    private List<GameObject> enemiesInRange = new();
    public GameObject Target { get; private set; }
    private EnemyDetector enemyDetector;
    private GameObject[] potentialTargets;
    private void Awake()
    {
        enemyDetector = GetComponent<EnemyDetector>();
    }

    public void Update()
    {
        if (Target != null && Target.GetComponent<PoolEnemy>().CurrentHealth <= 0)
        {
            Debug.Log("Removed: " + Target.name);
            enemiesInRange.Remove(Target);
            Target = null;
        }
        ChooseTarget();
    }

    private void ChooseTarget()
    {
        foreach (GameObject pt in enemyPool.EnemiesInPool)
        {
            if (enemiesInRange.Contains(pt))
                continue;
            if (!pt.activeInHierarchy)
                continue;
            if (pt.GetComponent<PoolEnemy>().CurrentHealth <= 0)
                continue;
            if (Vector3.Distance(pt.transform.position, Vector3.zero) < enemyDetector.XRadius)
            {
                enemiesInRange.Add(pt);
                Debug.Log("Enemy Added: " + pt.name);
            }
        }
        GameObject temp = null;
        if (enemiesInRange.Count == 1)
        {
            temp = enemiesInRange[0];
        }
        else if (enemiesInRange.Count > 1)
        {
            for (int i = 1; i < enemiesInRange.Count; i++)
            {
                if (Vector3.Distance(enemiesInRange[i].transform.position, transform.position) < Vector3.Distance(enemiesInRange[i - 1].transform.position, transform.position))
                {
                    temp = enemiesInRange[i];
                }
                else
                {
                    temp = enemiesInRange[i - 1];
                }
            }
        }
        Target = temp;
    }
}
