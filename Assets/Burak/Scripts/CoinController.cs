using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class CoinController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    public UnityAction<int> onCoinChanged;
    public int coin;
    private void Awake()
    {
        if(PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetInt("Coin");
        }
        else
        {
            coin = 0;
            PlayerPrefs.SetInt("Coin",coin);
        }
        updateText();
    }
    void updateText()
    {
        coinText.text = coin.ToString();
    }
    public void MakeCoin(int plus)
    {
        coin = coin +plus;
        PlayerPrefs.SetInt("Coin", coin);
        updateText();

        onCoinChanged?.Invoke(coin);
        if (coin < 0)
            Debug.LogError("bug");
    }
    public void SpendCoin(int minus)
    {
       

        coin = coin - minus;
        PlayerPrefs.SetInt("Coin", coin);
        updateText();

        onCoinChanged?.Invoke(coin);
        if (coin < 0)
            Debug.LogError("bug");
    }
}
