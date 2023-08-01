using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private int maxInstantiateAmount;
    private List<GameObject> coins = new();

    public static CoinPool instance;
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < maxInstantiateAmount; i++)
        {
            GameObject c = Instantiate(coin, Vector3.zero, coin.transform.rotation);
            c.SetActive(false);
            coins.Add(c);
        }
    }

    public void InitializeCoin(Vector3 pos)
    {
        GameObject c = coins.Where(x => !x.activeInHierarchy).FirstOrDefault();
        c.transform.position = pos;
        c.SetActive(true);
    }
}
