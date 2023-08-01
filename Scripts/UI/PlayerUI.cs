using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthInfo;
    [SerializeField] private Image manaBar;
    [SerializeField] private TextMeshProUGUI manaInfo;

    public void SetHealthUI(float current, float max)
    {

        healthBar.fillAmount = current / max;
        healthInfo.text = current.ToString("F2") + " / " + max;
    }

    public void SetManaUI(float current, float max)
    {
        manaBar.fillAmount = current / max;
        manaInfo.text = current.ToString("F2") + " / " + max;
    }
}
