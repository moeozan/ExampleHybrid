using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using TMPro;

public class EnemyDamageText : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private List<TextMeshProUGUI> damageTexts;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(-mainCam.transform.position);
    }

    public void ShowDamage(string damage)
    {
        foreach (var dt in damageTexts)
        {
            if (!dt.gameObject.activeInHierarchy)
            {
                dt.gameObject.SetActive(true);
                if (PropertyManager.instance.IsDamageCritical)
                    dt.color = Color.yellow;

                else
                    dt.color = Color.white;

                dt.text = damage;
                return;
            }
        }
    }
}
