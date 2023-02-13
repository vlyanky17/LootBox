using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LootBox : MonoBehaviour
{

    [SerializeField] private Button _button;
    [SerializeField] private WinnerPanel _winnerPanel;
    [SerializeField] private Scroller _scroller;
    void Awake()
    {
        _button.onClick.AddListener(SelectWinner);
    }
    private void SelectWinner()
    {
        var percents = _scroller.GetChancePercent();
        float percent = Random.Range(0, percents[percents.Length - 1]);
        int winnerNumber = 0;
        for (int i = 0; i < percents.Length; i++)
        {
            winnerNumber = i;
            if (percent < percents[i])
                break;
        }
        _scroller.GoScroll(winnerNumber);
    }

    public void SetWinner(Item winner)
    {
        _winnerPanel.CallPanel(winner);
        _scroller.gameObject.SetActive(false);
    }
}
