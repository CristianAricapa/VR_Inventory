using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAction : MonoBehaviour, InterfaceAction
{
    protected Item _item;
    protected InventoryItem _data;
    protected int _inventoryIndex = -1;

    protected AudioManager _audioManager;


    // Use this for initialization
    void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _data = GetComponent<InventoryItem>();
    }

    public void SetInventoryIndex(int index = -1)
    {
        _inventoryIndex = index;
    }


    virtual public void Action()
    {
        if (_data.cuantity == 1)
            _data.ResetCantidad();
        else if (_data.cuantity == 0)
        {
            GameManager.inventory.ResetItemSlot(_inventoryIndex);
            _data.ResetCantidad();
            Destroy(gameObject);
        }
    }

    public void SetItem(Item item, int index = -1)
    {
        _item = item;
        _inventoryIndex = index;
    }
}
