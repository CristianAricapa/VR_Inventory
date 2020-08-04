using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public Items data;

    public void LoadData()
    {
        data = JsonUtility.FromJson<Items>(Resources.Load<TextAsset>("Files/Items").text);
    }

    public Item FetchItem(int id)
    {
        for (int i = 0; i < data.items.Count; i++)
        {
            if (id == data.items[i].id)
                return data.items[i];
        }
        return null;
    }
}
