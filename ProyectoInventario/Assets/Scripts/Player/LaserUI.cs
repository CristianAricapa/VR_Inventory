using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUI : MonoBehaviour
{
    public Material materialOut, materialIn;
    private LineRenderer _lineR;
    private RaycastHit _hitInfo;
    private LayerMask _layerUI;

    private InventoryItem _currentItem;


    // Use this for initialization
    void Start()
    {
        _lineR = GetComponent<LineRenderer>();
        _layerUI = LayerMask.GetMask("VR_UI");
    }

    // Update is called once per frame
    void Update()
    {
        _lineR.SetPosition(0, transform.position); // Direccion de laser
        Vector3 endPosition = transform.position + transform.forward * 10f; // Punto final laser

        Ray laserRay = new Ray(transform.position, transform.forward);
        _lineR.material = materialOut;

        if (Physics.Raycast(laserRay, out _hitInfo, 10f, _layerUI))
        {
            endPosition = _hitInfo.point;
            _lineR.material = materialIn;
            if (_currentItem != null)
            {
                _currentItem.SetUnselected();
            }
            _currentItem = _hitInfo.collider.GetComponent<InventoryItem>();
            _currentItem.SetSelected();
        }
        else
        {
            if (_currentItem != null)
            {
                _currentItem.SetUnselected();
                _currentItem = null;
            }
        }

        _lineR.SetPosition(1, endPosition);

        if(_currentItem != null && GameManager.input.triggerRightDown)
        {
            _currentItem.GetComponent<InterfaceAction>().Action();
        }

    }
}
