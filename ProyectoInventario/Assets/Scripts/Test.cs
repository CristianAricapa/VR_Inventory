using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.input.touchpadLeftDown)
        {
            GameManager.inventory.AddItem(0);
        }
        if (GameManager.input.touchpadRightDown)
        {
            GameManager.inventory.AddItem(1);
        }
    }
}
