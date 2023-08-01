using TMPro;
using UnityEngine;

public class UserCosts : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI experienceText;
    public void ExperienceHandler(float amount)
    {
        experienceText.text = amount.ToString();
    }
    public void GoldHandler(float amount)
    {
        goldText.text = amount.ToString();
    }
}
