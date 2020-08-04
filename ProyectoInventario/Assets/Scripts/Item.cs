using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Items
{
    public List<Item> items;
}

[System.Serializable]
public class Item
{
    public enum Type { Garbage = 0, Weapon, Health_Potion }

    public int      id;
    public string   name;
    public string   description;
    public bool     stackable;
    public string   pathImage;
    public int      damage;
    public Type     type;

    public Item(int id = -1)
    {
        this.id = id;
    }

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>(pathImage);
    }

}

