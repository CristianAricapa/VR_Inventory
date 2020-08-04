using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicRenderer : MonoBehaviour
{

    private LineRenderer _lineR;

    private Gradient _gradientLine = new Gradient();
    private GradientColorKey[] _keyColors = new GradientColorKey[5];

    private float _gradientTimer;

    // Use this for initialization
    void Start()
    {
        _lineR = GetComponent<LineRenderer>();
        _keyColors[0].color = Color.red;
        _keyColors[0].time = 0f;

        _keyColors[4].color = Color.red;
        _keyColors[4].time = 1f;

    }

    private void ChangeGradient(float t)
    {
        _gradientTimer = (_gradientTimer + t) % (1 - 0.2f);

        _keyColors[1].color = Color.red;
        _keyColors[1].time = _gradientTimer;

        _keyColors[2].color = Color.white;
        _keyColors[2].time = _gradientTimer + 0.1f;

        _keyColors[3].color = Color.red;
        _keyColors[3].time = _gradientTimer + 0.2f;
        
        _gradientLine.colorKeys = _keyColors;
        _lineR.colorGradient = _gradientLine;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeGradient(Time.deltaTime);
    }
}
