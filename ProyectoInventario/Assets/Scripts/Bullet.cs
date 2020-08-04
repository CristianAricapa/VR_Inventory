using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float Speed;

    public Player Player;
    private AudioManager _audioManager;


    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Room").GetComponent<Player>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        Speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Walls")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Suelo")
        {
            Destroy(gameObject);
        }

        if(other.tag == "Enemy")
        {
            Player.EnemyScore();
            GameManager.EnemyManager.TotalEnemies -= 1;
            _audioManager.PlayAudio(Constants.AudioFX.Enemy_Die);
            Destroy(other.gameObject);
        }
    }
}
