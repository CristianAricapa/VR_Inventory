using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public Color selectedColor;

    private Color _defaultColor;
    private Image _parent;
    private Image _img;
    private Text _text;
    private int cantidad;

    public int cuantity{ get { return cantidad; } }

    // Use this for initialization
    void Awake()
    {
        _img = GetComponent<Image>();
        _text = GetComponentInChildren<Text>();

        _parent = transform.parent.GetComponent<Image>();
        _defaultColor = _parent.color;

        ResetCantidad();
    }

    public void ResetCantidad()
    {
        this.cantidad = 1;
        _text.gameObject.SetActive(false);
        SetUnselected();
    }

    public void AddCantidad(int cantidad = 1)
    {
        if (this.cantidad + cantidad > 99)
            return;

        this.cantidad = this.cantidad + cantidad;
        _text.text = this.cantidad.ToString();
        _text.gameObject.SetActive(this.cantidad > 1);
    }

    public void SubstractCantidad(int cantidad = 1)
    {
        if (this.cantidad - cantidad < 0)
            return;

        this.cantidad = this.cantidad - cantidad;
        _text.text = this.cantidad.ToString();
        _text.gameObject.SetActive(this.cantidad > 1);
    }

    public void SetSprite(Sprite sprite)
    {
        _img.sprite = sprite;
    }


    public void SetSelected()
    {
        _parent.color = selectedColor;
    }

    public void SetUnselected()
    {
        _parent.color = _defaultColor;
    }


}
