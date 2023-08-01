using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [SerializeField] private Transform bow;
    [SerializeField] private GameObject arrow;
    [SerializeField] private int count;
    [SerializeField] private List<GameObject> pool;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        for (int i = 0; i < count; i++)
        {
            GameObject a = Instantiate(arrow);
            a.SetActive(false);
            pool.Add(a);
        }
    }

    private GameObject PoolHandler()
    {
        for (int i = 0; i < count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    public void ArrowHandler()
    {
        if (playerController.TargetHandler.Target == null)
            return;
        GameObject a = PoolHandler();
        if (a != null)
        {
            a.transform.position = bow.transform.position;
            a.transform.rotation = bow.localRotation;
            a.SetActive(true);
            a.GetComponent<Arrow>().ArrowFiring(GetComponent<PlayerController>().TargetHandler.Target.transform);
        }
    }

    public void DoAShot()
    {
        ArrowHandler();
    }
}
