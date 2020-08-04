using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManagerMonoBehaviour : MonoBehaviour
{
    [Header("Inventario prefabs")]
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    [Header("Inventario references")]
    public GameObject slotsParent;


    [Header("Inputs")]
    public KeyCode touchpadLeftKey;
    public KeyCode touchpadRightKey;


    [Header("Player")]
    public Player player;



    // Use this for initialization
    void Awake()
    {
        GameManager.inventorySlot = inventorySlot;
        GameManager.inventoryItem = inventoryItem;

        GameObject go = new GameObject("DataBaseManager");
        GameManager.dataBaseItems = go.AddComponent<DataBaseManager>();
        go.transform.parent = transform;

        GameManager.dataBaseItems.LoadData();

        go = new GameObject("InputManager");
        GameManager.input = go.AddComponent<InputManager>();
        go.transform.parent = transform;

        GameManager.input.touchpadLeftKey = touchpadLeftKey;
        GameManager.input.touchpadRightKey = touchpadRightKey;

        go = new GameObject("Inventory");
        GameManager.inventory = go.AddComponent<Inventory>();
        GameManager.inventory.panelSlots = slotsParent;

        go.transform.parent = transform;

        GameManager.player = player;

        GameManager.EnemyManager = go.AddComponent<EnemyManager>();
        GameManager.EnemyManager.StartUI = GameObject.Find("StartUI");
        GameManager.EnemyManager.FinishUI = GameObject.Find("FinishUI");
        GameManager.EnemyManager.CountText = GameObject.Find("CountTex").GetComponent<Text>();
        GameManager.EnemyManager.TotalEnemies_Text = GameObject.Find("TotalEnemiesText").GetComponent<Text>();


        //GameManager.Weapon_1 = Weapon_1;
        //GameManager.Weapon_1.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

public static class GameManager
{
    public static GameObject inventorySlot;
    public static GameObject inventoryItem;

    public static DataBaseManager dataBaseItems;
    public static InputManager input;

    public static Inventory inventory;

    public static Player player;

    public static EnemyManager EnemyManager;
}
