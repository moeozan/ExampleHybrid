using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageText : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private TextMeshProUGUI m_text;
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        m_RectTransform.DOAnchorPosY(.5f, .5f).SetEase(Ease.Flash).OnComplete(() => gameObject.SetActive(false));
        m_text.DOFade(0,.5f);
    }

    private void OnDisable()
    {
        m_text.color = new Color(255, 255, 255, 1);
        m_RectTransform.anchoredPosition = Vector2.zero;
    }
}
