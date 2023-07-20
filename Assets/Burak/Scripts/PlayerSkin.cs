using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerSkin : MonoBehaviour
{
    public bool Unlocked;
    public int Price;
    [SerializeField] Button unlockButton;
    [SerializeField] Button selectButton;
    ShopController shopController;
    CoinController coinController;
    private void Awake()
    {
      //  unlockButton = GetComponentInChildren<Button>();
       // selectButton = GetComponent<Button>();
        GetComponentInChildren<TextMeshProUGUI>().text = Price.ToString();

    }
    public void Init(ShopController shopController1, CoinController coinController1, bool unlocked)
    {
        shopController = shopController1;
        coinController = coinController1;
        Unlocked = unlocked;

        if (Unlocked)
            UnlockedItem();

        else
            SelectButton(false);


    }
 public   void UnlockedItem()
    {
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);
    }
    public void Unlock()
    {
        coinController.SpendCoin(Price);
        UnlockedItem();

    }
    void SelectButton(bool enable)
    {
        selectButton.interactable = enable;


    }
}
