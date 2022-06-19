using TMPro;
using UnityEngine;
using System;

public class CoinCell : MonoBehaviour
{
    [Tooltip("A text which show coin in cell.")]
    [SerializeField] private TextMeshProUGUI  _textCountCoin;
    
    public void AddCoin(int countCoin) {
        _textCountCoin.text = (Convert.ToInt32(_textCountCoin.text) + countCoin).ToString();
    }

    public void SetCoin(int countCoin) {
        _textCountCoin.text = countCoin.ToString();
    }
}
