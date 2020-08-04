using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    private float StartCount = 0;
    private int Count;
    public GameObject StartUI;
    public GameObject FinishUI;
    public Text CountText;
    public Text TotalEnemies_Text;
    public int TotalEnemies = 12;

    public bool EnemyCanMove = false;
    // Use this for initialization
    void Start()
    {

        Count = 20;
        FinishUI.SetActive(false);
        StartUI.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        TotalEnemies_Text.text = TotalEnemies.ToString();

        if (Count > 0)
        {
            StartCount += Time.deltaTime;
            CountText.text = Count.ToString();
            if (StartCount >= 1)
            {
                Count -= 1;
                StartCount = 0;
            }
        }

        if (Count <= 0)
        {
            StartUI.SetActive(false);
            EnemyCanMove = true;
        }

        if(TotalEnemies == 0)
        {
            FinishUI.SetActive(true);
        }


    }
}
