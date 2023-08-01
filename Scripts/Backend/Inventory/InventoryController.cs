using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private List<Slot> FakeSlots = new();
    public List<Sprite> resources = new();
    public List<Sprite> ItemSprites => resources;
    public Inventory DatabaseInventory { get; private set; }

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    private Transform informationPanelTransform;
    private Image itemImage;
    private Image informationPanel;
    private bool locked;
    private Slot choosenSlot;

    [SerializeField] private Image informationPanelPrefab;

    private void Awake()
    {
        InitializeSprites();
        FakeSlots = GetComponentsInChildren<Slot>().ToList();
        DatabaseInventory =DatabaseManager.instance.GetInventoryInformation();
        ItemInitializes();
    }

    private void Start()
    {
        raycaster = GetComponentInParent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        informationPanel = Instantiate<Image>(informationPanelPrefab, informationPanelTransform);
    }

    private void ItemInitializes()
    {
        for (int i = 0; i < DatabaseInventory.Slots.Length; i++)
        {
            if (DatabaseInventory.Slots[i].Item.ItemName != null && DatabaseInventory.Slots[i].Item.ItemName != string.Empty)
            {
                FakeSlots[i].Item = DatabaseInventory.Slots[i].Item;
                FakeSlots[i].ItemInitialize();
                FakeSlots[i].Index = i;
            }
            else
            {
                FakeSlots[i].IsEmpty = true;
                FakeSlots[i].Empty();
                FakeSlots[i].Index = i;
            }
        }
    }

    private void LateUpdate()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);
        if (results.Count < 1)
        {
            informationPanel.gameObject.SetActive(false);
            return;
        }
        else
        {
            foreach (RaycastResult result in results)
            {
                Slot temp = result.gameObject.GetComponent<Slot>();
                itemImage.transform.position = Input.mousePosition;
                if (locked && choosenSlot != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (result.gameObject.GetComponent<Trash>())
                        {
                            DeleteItemFromInventory(DatabaseInventory.Slots[choosenSlot.Index]);
                        }
                        else if (result.gameObject.GetComponent<Slot>())
                        {
                            ChangeItemPlace(DatabaseInventory.Slots[choosenSlot.Index], DatabaseInventory.Slots[temp.Index]);
                        }
                        choosenSlot.Choosen.gameObject.SetActive(false);
                        itemImage.gameObject.SetActive(false);
                        choosenSlot = null;
                        locked = false;
                        return;
                    }
                }

                if (!temp)
                    return;

                if (Input.GetMouseButtonDown(0))
                {
                    locked = true;
                    itemImage.gameObject.SetActive(true);
                    choosenSlot = temp;
                    itemImage.sprite = FindItemSprite(choosenSlot.Item.ItemName);
                    temp.Choosen.gameObject.SetActive(true);
                    return;
                }

                if (temp.IsEmpty)
                {
                    informationPanel.gameObject.SetActive(false);
                    return;
                }
                SetInformation(temp);
            }
        }
    }

    private void SetInformation(Slot slot)
    {
        if (slot == null)
            return;
        informationPanel.gameObject.SetActive(true);
        informationPanel.transform.position = slot.InformationPanelTransform.position;
        Information info = informationPanel.GetComponent<Information>();
        info.itemName.text = slot.Item.ItemName;
        info.itemDescription.text = slot.Item.Description;
    }

    public void InitializeSprites()
    {
        resources = Resources.LoadAll<Sprite>("Item").ToList();
    }

    public Sprite FindItemSprite(string itemName)
    {
        return resources.Where(x => x.name == itemName).FirstOrDefault();
    }

    public void DeleteItemFromInventory(InventorySlot slot)
    {
        slot.Item = new();
        DatabaseManager.instance.SetInventory(DatabaseInventory);
        ItemInitializes();
    }

    public void AddHandItemToInventory(Weapons typeOfWeapon, int attack, int defense)
    {
        ItemTypes type = new(typeOfWeapon);
        InventoryItem temp = new("Moe's " + typeOfWeapon.ToString(), type, attack, defense, "This is " + typeOfWeapon.ToString());
        InventorySlot choosen = new();

        for (int i = 0; i < DatabaseInventory.Slots.Length; i++)
        {
            if (DatabaseInventory.Slots[i].Item.ItemName == null)
            {
                DatabaseInventory.Slots[i].Item = temp;
                choosen = DatabaseInventory.Slots[i];
                break;
            }
        }
        DatabaseManager.instance.SetInventory(DatabaseInventory);
        ItemInitializes();
    }

    public void ChangeItemPlace(InventorySlot choosen, InventorySlot target)
    {
        if (target.Item == null)
        {
            target.Item = choosen.Item;
            choosen.Item = null;
        }
        else
        {
            InventoryItem temp = new();
            (choosen, target) = (target, choosen);
            //temp = choosen.Item;
            //choosen.Item = target.Item;
            //target.Item = temp;
        }
        DatabaseManager.instance.SetInventory(DatabaseInventory);
        ItemInitializes();
    }


    public void AddAxeItem()
    {
        AddHandItemToInventory(Weapons.Axe, 0, 50);
    }
    public void AddShieldItem()
    {
        AddHandItemToInventory(Weapons.Shield, 50, 0);
    }
    public void AddKnotItem()
    {
        AddHandItemToInventory(Weapons.Knot, 5, 0);
    }
    public void AddBowItem()
    {
        AddHandItemToInventory(Weapons.Bow, 0, 50);
    }
    public void AddSwordItem()
    {
        AddHandItemToInventory(Weapons.Sword, 0, 50);
    }

}
