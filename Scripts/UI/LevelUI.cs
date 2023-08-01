using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private RectTransform currentLevelRect;
    [SerializeField] private RectTransform NextLevelRect;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private Image levelProgress;
    [SerializeField] private TextMeshProUGUI currentLevelExpInfo;

    private int currentLevel;
    private int nextLevel;

    private void Start()
    {
        currentLevelExpInfo.text = LevelManager.instance.LevelExperince.ToString();
    }

    public void LevelProgression()
    {
        currentLevel = LevelManager.instance.Level;
        nextLevel = currentLevel + 1;
        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = nextLevel.ToString();
        currentLevelExpInfo.text = LevelManager.instance.CurrentExperience.ToString();
        currentLevelRect.sizeDelta = new Vector2(150,150);
        currentLevelRect.DOSizeDelta(new Vector2(100,100), .7f).SetEase(Ease.InElastic).OnComplete(() => NextLevelRect.sizeDelta = new Vector2(150, 150));
        NextLevelRect.DOSizeDelta(new Vector2(100, 100), .7f).SetEase(Ease.InElastic).SetDelay(.7f);
    }

    public void IncreaseLevelExperience(float current, float max)
    {
        if (current >= max)
        {
            current -= max;
            LevelProgression();
        }
        levelProgress.DOFillAmount(current / max, .2f);
    }
}
