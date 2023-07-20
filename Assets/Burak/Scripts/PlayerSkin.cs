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
  [SerializeField]  TextMeshProUGUI priceText;
    private void Awake()
    {
        //  unlockButton = GetComponentInChildren<Button>();
        // selectButton = GetComponent<Button>();
        priceText.text = Price.ToString();

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
    public void CheckMoney(int a)
    {
        if (a < Price)
            unlockButton.interactable = false;
        else
            unlockButton.interactable = true;

    }

    public   void UnlockedItem()
    {
        Debug.Log("unlocked");
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

        shopController.NewSkinUnlocked(this);
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
    public void SelectSkin()
    {
        shopController.SkinSelected(this);
    }
}
