using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    public string Name;
    public string Descripteion;
    public SpriteRenderer Sprite;

    [Tooltip("Chance in %")]
    public float Chance;

    [SerializeField] Scroller _scroller;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _scroller.ItemOnField();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _scroller.ItemOutField(this);
    }
}
