using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePowers : MonoBehaviour
{
    [SerializeField] private RectTransform attackPanel;
    [SerializeField] private RectTransform defensePanel;
    [SerializeField] private RectTransform utilityPanel;

    private void Awake()
    {
        AttackPanelHandler();
    }

    public void AttackPanelHandler()
    {
        attackPanel.DOAnchorPos(new Vector2(0,-5), .25f);
        defensePanel.DOAnchorPos(new Vector2(0, -540), .25f);
        utilityPanel.DOAnchorPos(new Vector2(0, -540), .25f);
    }

    public void DefensePanelHandler()
    {
        attackPanel.DOAnchorPos(new Vector2(0, -540), .25f);
        defensePanel.DOAnchorPos(new Vector2(0, -5), .25f);
        utilityPanel.DOAnchorPos(new Vector2(0, -540), .25f);
    }
    public void UtilityPanelHandler()
    {
        attackPanel.DOAnchorPos(new Vector2(0, -540), .25f);
        defensePanel.DOAnchorPos(new Vector2(0, -540), .25f);
        utilityPanel.DOAnchorPos(new Vector2(0, -5), .25f);
    }
}
