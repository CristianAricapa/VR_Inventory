using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AudioManager _audioManager;
    public int health = 100;

    public int EnemyCount;
    public Text EnemyCount_Text;

    public GameObject teleport, laserUI, bullet, shootPointLeft, shootPointRight;
    public Transform canvasInventario;
    public HealthSlider sliderHealth;

    public GameObject[] WeaponRight, WeaponLeft;
    private int _currentWeaponLeft = -1, _currentWeaponRight = -1;
    public int lefthand { get { return _currentWeaponLeft; } }
    public int righthand { get { return _currentWeaponRight; } }

    private Vector3 defaultScaleCanvas;

    private void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        Invoke("AddItems", 1f);
        defaultScaleCanvas = canvasInventario.localScale;
        ResetWeapons();

        EnemyCount_Text.text = "0";
        SetHealth(100);
    }

    public void ResetWeapons()
    {
        for (int i = 0; i < WeaponRight.Length; i++)
        {
            WeaponRight[i].SetActive(false);
        }
        for (int i = 0; i < WeaponLeft.Length; i++)
        {
            WeaponLeft[i].SetActive(false);
        }
    }

    public void SetWeaponLeft(int index)
    {
        if (_currentWeaponLeft > -1)
            WeaponLeft[_currentWeaponLeft].SetActive(!WeaponLeft[_currentWeaponLeft].activeSelf);

        if (_currentWeaponLeft != index)
        {
            _currentWeaponLeft = index;
            WeaponLeft[_currentWeaponLeft].SetActive(!WeaponLeft[_currentWeaponLeft].activeSelf);
        }

        if (WeaponLeft[_currentWeaponLeft].activeSelf == false)
            _currentWeaponLeft = -1;
    }

    public void SetWeaponRight(int index)
    {
        if (_currentWeaponRight > -1)
            WeaponRight[_currentWeaponRight].SetActive(!WeaponRight[_currentWeaponRight].activeSelf);

        if (_currentWeaponRight != index)
        {
            _currentWeaponRight = index;
            WeaponRight[_currentWeaponRight].SetActive(!WeaponRight[_currentWeaponRight].activeSelf);
        }

        if (WeaponRight[_currentWeaponRight].activeSelf == false)
            _currentWeaponRight = -1;

    }

    private void AddItems()
    {
        GameManager.inventory.AddItem(1);
        GameManager.inventory.AddItem(1);
        GameManager.inventory.AddItem(1);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCount_Text.text = EnemyCount.ToString() + "  Slimes";
        teleport.SetActive(GameManager.input.touchpadLeft);
        laserUI.SetActive(GameManager.input.touchpadRight);


        if (GameManager.input.touchpadRight)
        {
            canvasInventario.localScale = defaultScaleCanvas;
        }
        else
        {
            canvasInventario.localScale = Vector3.zero;
        }

        if (!GameManager.input.touchpadLeft && !GameManager.input.touchpadRight)
        {
            if (GameManager.input.triggerLeftDown && _currentWeaponLeft != -1)
            {
                //Disparo arma izquierda
                _audioManager.PlayAudio(Constants.AudioFX.GunShot);
                Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation);
                //WeaponLeft[_currentWeaponLeft].GetComponent<Renderer>().material.color = Color.green;
            }

            if (GameManager.input.triggerRightDown && _currentWeaponRight != -1)
            {
                //Disparo arma derecha
                _audioManager.PlayAudio(Constants.AudioFX.GunShot);
                Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation);
                //WeaponRight[_currentWeaponRight].GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }

    public void EnemyScore(int num = 1)
    {
        EnemyCount += num;
    }

    public void SetHealth(int health)
    {
        this.health = health;
        sliderHealth.SetHealth((float)health / Constants.MAX_HEALTH);
    }

    public void Heal(int healing)
    {
        health += healing;
        if (healing > Constants.MAX_HEALTH)
            health = Constants.MAX_HEALTH;

        sliderHealth.SetHealth((float)health / Constants.MAX_HEALTH);
        Debug.Log(health);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Potion")
        {
            GameManager.inventory.AddItem(1);
            _audioManager.PlayAudio(Constants.AudioFX.PickUp_Pot);
            Destroy(col.gameObject);
        }
        if(col.tag == "MHI")
        {
            GameManager.inventory.AddItem(0);
            _audioManager.PlayAudio(Constants.AudioFX.PickUp_Gun);
            Destroy(col.gameObject);
        }
        if(col.tag == "Revolver")
        {
            GameManager.inventory.AddItem(20);
            _audioManager.PlayAudio(Constants.AudioFX.PickUp_Gun);
            Destroy(col.gameObject);
        }

        if(col.tag == "Enemy")
        {
            Heal(-2);
            GameManager.EnemyManager.TotalEnemies -= 1;
            _audioManager.PlayAudio(Constants.AudioFX.Hurt);
            Destroy(col.gameObject);
        }
    }
}
