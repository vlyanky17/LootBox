using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerPanel : MonoBehaviour
{
    [SerializeField] SpriteRenderer _winnerSprite;
    [SerializeField] TextMeshProUGUI _text;

    public void CallPanel(Item winner)
    {
        _winnerSprite.sprite = winner.Sprite.sprite;
        _winnerSprite.color = winner.Sprite.color;
        _text.text = winner.Name;
        transform.DOMoveY(0, 1.5f);
    }
}
