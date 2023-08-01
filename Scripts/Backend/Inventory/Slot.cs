using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public bool IsEmpty { get; set; }
    public InventoryItem Item { get; set; }
    public int Index { get; set; }

    [SerializeField] private Sprite emptySlotSprite;
    [Header("Children")]
    [SerializeField] private Image choosen;
    [SerializeField] private Transform informationPanelTransform;

    public Transform InformationPanelTransform => informationPanelTransform;
    public Image Choosen => choosen;

    private void Awake()
    {
        choosen.gameObject.SetActive(false);
    }

    public void ItemInitialize()
    {
        GetComponent<Image>().sprite = GetComponentInParent<InventoryController>().FindItemSprite(Item.ItemName);
        IsEmpty = false;
    }

    public void Empty()
    {
        GetComponent<Image>().sprite = emptySlotSprite;
    }
}
