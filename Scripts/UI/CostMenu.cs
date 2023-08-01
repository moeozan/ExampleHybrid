using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CostMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    public void SetGoldText(float amount)
    {
        goldText.text = amount.ToString();
    }
}
