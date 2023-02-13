using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private LootBox _lootBox;
    [SerializeField] private Item[] _items;
    [SerializeField] private RectTransform _rTanform;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private SpriteRenderer _bar;

    private Vector3 _leftPos;
    private Vector3 _rightPos;
    private int _count = 0;
    private Item _winner;

    public float[] GetChancePercent()
    {
        float[] percents = new float[_items.Length];
        float count = 0;
        for (int i = 0; i < _items.Length; i++)
        {
            count = count + _items[i].Chance;
            percents[i] = count;
        }
        return percents;
    }

    private void SendNext()
    {
        if (_count == _items.Length)
            _count = 0;
        var item = _items[_count];
        if (item.transform.parent == _rTanform.transform)
        {
            ItemOutField(item);
        }
        item.gameObject.SetActive(true);
        item.transform.SetParent(_rTanform.transform);
        _count++;
    }


    private void Awake()
    {
        GetRightLeftPos();
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].transform.localPosition = _rightPos + new Vector3(170, 0, 0);
        }
        _bar.transform.localScale = new Vector2(_rightPos.x - _leftPos.x + 50, 100);
    }
    public void ItemOnField()
    {
        SendNext();
    }

    public void ItemOutField(Item item)
    {
        item.transform.SetParent(transform);
        item.transform.localPosition = _rightPos + new Vector3(170, 0, 0);
        item.gameObject.SetActive(false);
    }

    public void GoScroll(int winnerNumber)
    {
        var swapItem = _items[_items.Length - 1];
        _items[_items.Length - 1] = _items[winnerNumber];
        _items[winnerNumber] = swapItem;
        _collider.isTrigger = true;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(_rTanform.transform.DOLocalMoveX(-170 * _items.Length * 3 - 70, _items.Length * 2));
        mySequence.AppendCallback(ScrollEnd);
        SendNext();
    }

    private void ScrollEnd()
    {
        _winner = _items[_items.Length - 1];
        for (int i = 0; i < _items.Length - 1; i++)
        {
            _items[i].gameObject.SetActive(true);
            _items[i].transform.SetParent(_winner.transform);
        }
        _winner.transform.SetParent(transform);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(_winner.transform.DOLocalMoveX(0, 0.5f));
        mySequence.AppendCallback(() => { _lootBox.SetWinner(_winner); });
    }


    private void GetRightLeftPos()
    {
        _rTanform.anchorMin = new Vector2(0, 0.5f);
        _rTanform.anchorMax = new Vector2(0, 0.5f);
        _leftPos = _rTanform.transform.localPosition;
        _rTanform.anchorMin = new Vector2(1, 0.5f);
        _rTanform.anchorMax = new Vector2(1, 0.5f);
        _rightPos = _rTanform.transform.localPosition;
    }
}
