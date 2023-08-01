using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotMenuController : MonoBehaviour
{
    [SerializeField] private List<BotMenuItem> menus = new();
    [SerializeField] private List<Button> menuButtons = new();


    public void ButtonHandler(int index)
    {
        int lastIndex = 0;
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i].isActive)
            {
                lastIndex = i;
                break;
            }
        }
        if (lastIndex  == index) { return; }
        if (lastIndex > index)
        {
            menus[lastIndex].isActive = false;
            menus[index].GetComponent<RectTransform>().anchoredPosition = new Vector2 (-1080,0);
            menus[lastIndex].GetComponent<RectTransform>().DOAnchorPos(new Vector2(1080,0),.75f);
            menus[index].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), .75f);
            menus[index].isActive = true;
        }
        else
        {
            menus[lastIndex].isActive = false;
            menus[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(1080, 0);
            menus[lastIndex].GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1080, 0), .75f);
            menus[index].GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), .75f);
            menus[index].isActive = true;
        }

        menuButtons[index].GetComponent<RectTransform>().DOSizeDelta(new Vector2(200, 200), .75f);
        menuButtons[lastIndex].GetComponent<RectTransform>().DOSizeDelta(new Vector2(188,188), .75f);
    }
}
