using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<GameObject> _slots = new List<GameObject>();
    private List<Item> _items = new List<Item>();
    private int amountSlots = 15;
    public GameObject panelSlots;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < amountSlots; i++)
        {
            _slots.Add(Instantiate(GameManager.inventorySlot, panelSlots.transform));
            _items.Add(new Item());
        }
    }

    public void AddItem(int id)
    {
        Item itemToAdd = GameManager.dataBaseItems.FetchItem(id);
        int index = ChechItemInventory(id);

        if (itemToAdd.stackable && index > -1) //Si positivo lo ha encontrado y se puede agrupar
        {
            _slots[index].GetComponentInChildren<InventoryItem>().AddCantidad();
        }
        else
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].id == -1)
                {
                    _items[i] = itemToAdd;
                    GameObject obj = Instantiate(GameManager.inventoryItem, _slots[i].transform);
                    ItemAction action = null;

                    switch (itemToAdd.type)
                    {
                        case Item.Type.Garbage:
                            action = obj.AddComponent<ItemAction>();
                            
                            break;
                        case Item.Type.Weapon:
                            action = obj.AddComponent<ItemActionWeapon>();
                            break;
                        case Item.Type.Health_Potion:
                            action = obj.AddComponent<ItemActionHealthPotion>();
                            break;
                        default:
                            break;
                    }
                    if (action != null)
                    {
                        action.SetItem(itemToAdd, i);
                        obj.name = itemToAdd.name;
                        obj.GetComponent<InventoryItem>().SetSprite(itemToAdd.GetSprite());
                    }

                    break;
                }
            }
        }
    }

    public int ChechItemInventory(int id)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (id == _items[i].id)
                return i;
        }

        return -1;
    }

    public void ResetItemSlot(int i)
    {
        _items[i] = new Item();
    }
}
