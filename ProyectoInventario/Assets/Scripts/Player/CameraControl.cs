using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float angularSpeed;

    private Transform _camera;
    private Vector2 _oldMousePos;


    // Use this for initialization
    void Start()
    {
        _camera = Camera.main.transform;
        _oldMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = new Vector3(Input.mousePosition.y - _oldMousePos.y, -Input.mousePosition.x + _oldMousePos.x, 0); //Calculamos la distancia recorrida, final - inicio

        difference *= angularSpeed;
        _camera.rotation = Quaternion.Euler(_camera.rotation.eulerAngles + difference);

        _oldMousePos = Input.mousePosition;
    }

}
