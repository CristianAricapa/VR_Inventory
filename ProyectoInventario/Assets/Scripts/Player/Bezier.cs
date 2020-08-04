using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    [Range(2, 20)]
    public float distanciaMaxima = 5f;

    [Range(2, 20)]
    public float alturaControl = 3f;

    [Range(0, 20)]
    public float descensoPuntoFinal = 10f;

    public LayerMask layerTeleport, layerWalls;
    public Transform room;
    private AudioManager _audioManager;


    private const int NUMERO_MAXIMO_ESFERAS = 20;

    private RaycastHit _hitInfo;
    private LineRenderer lineRender;
    private Vector3 puntoFinal, puntoControl;
    private Vector3[] esferas;
    private GameObject _marker;

    private Gradient _gradientLine = new Gradient();
    private GradientColorKey[] _keyColors = new GradientColorKey[5];

    private float _gradientTimer;


    // Use this for initialization
    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _marker = transform.GetChild(0).gameObject;

        esferas = new Vector3[NUMERO_MAXIMO_ESFERAS];
        //for (int i = 0; i < NUMERO_MAXIMO_ESFERAS; i++)
        //{
        //    esferas[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere); // Instanciamos las esferas de Bezier
        //    esferas[i].transform.localScale = 0.1f * Vector3.one;

        //}

        _keyColors[0].color = Color.red;
        _keyColors[0].time = 0f;

        _keyColors[4].color = Color.red;
        _keyColors[4].time = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        CalculateCurve();
        GetCollision();
        ChangeGradient(Time.deltaTime);

        if (GameManager.input.triggerLeftDown && _marker.activeSelf)
        {
            _audioManager.audioManager.Stop();
            _audioManager.PlayAudio(Constants.AudioFX.Teleport);
            room.position = _marker.transform.position;
        }

        //if (Input.GetMouseButtonDown(0) && _marker.activeSelf)
        //{
        //    AudioS.Stop();
        //    AudioS.PlayOneShot(TpSound, 1f);
        //    room.position = _marker.transform.position;
        //}

    }

    private void GetCollision()
    {
        _marker.SetActive(false);
        _marker.transform.position = transform.position;
        for (int i = 0; i < NUMERO_MAXIMO_ESFERAS - 1; i++)
        {

            if (Physics.Linecast(esferas[i], esferas[i + 1], out _hitInfo, layerWalls))
            {
                _marker.SetActive(false);
                break;
            }

            if (Physics.Linecast(esferas[i], esferas[i + 1], out _hitInfo, layerTeleport))
            {
                _marker.SetActive(true);
                _marker.transform.position = _hitInfo.point;
                _marker.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
            }
        }
    }

    private void CalculateCurve()
    {
        lineRender.positionCount = NUMERO_MAXIMO_ESFERAS;

        for (int i = 0; i < NUMERO_MAXIMO_ESFERAS; i++) // Instance de esferas
        {
            float t = (float)i / (NUMERO_MAXIMO_ESFERAS - 1);

            puntoFinal = transform.position + transform.forward * distanciaMaxima;      //Asignamos punto final hacia adelante de nuestra posicion a distanciaMaxima
            puntoFinal.y = transform.position.y - descensoPuntoFinal;
            puntoControl = transform.position + (puntoFinal - transform.position) / 2;  //Establecemos punto intermedio entre mi posicion y posicion final
            puntoControl.y += alturaControl;                                            //Añadimos atura a punto control para que no sea recto
            esferas[i] = CalculateBezierPoint(t, transform.position, puntoControl, puntoFinal);

            lineRender.SetPosition(i, esferas[i]);
        }

    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) // Curva cuadrática de Bezier
    {
        //B(t) = (1-t) * (1-t) * p0 + 2 * t * (1-t) * p1 + t * t * p2
        //u = 1 - t;
        float u = 1 - t;
        //B(t) = u * u * p0 + 2 * t * u * p1 + t * t * p2
        Vector3 point = u * u * p0;
        point += 2 * t * u * p1;
        point += t * t * p2;
        return point;
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
        lineRender.colorGradient = _gradientLine;
    }

}
