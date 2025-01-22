using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CrazyGames;
public class PlayerSkin : MonoBehaviour
{
    public bool rewarded;
    public bool Unlocked;
    public int Price;
    [SerializeField] Button unlockButton;
    [SerializeField] Button selectButton;
    ShopController shopController;
    CoinController coinController;
  [SerializeField]  TextMeshProUGUI priceText;
    [SerializeField] GameObject adIcon;
    private void Awake()
    { 
        if (rewarded)
        {
            priceText.text = Price.ToString();
            adIcon.SetActive(true);
        }
        else 
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
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

        shopController.NewSkinUnlocked(this);
      //  SelectSkin();
    }
    public void UnlockAd()
    {
        RewardedShow();
    }
    public void Unlock()
    { 
        coinController.SpendCoin(Price);
        UnlockedItem();

    } 
    private void RewardEarned()
    {

        if (Unlocked)
            return;


        Debug.Log("unlocked");
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

        shopController.NewSkinUnlocked(this);
    }

     void RewardedShow()
    {
        CrazySDK.Ad.RequestAd(
                 CrazyAdType.Rewarded,
                 () =>
                 {
                     Debug.Log("Rewarded ad started");
                 },
                 (error) =>
                 {
                     Debug.Log("Rewarded ad error: " + error);
                 },
                 () =>
                 {
                     RewardedClosed();
                 }
             );
    }
    private void RewardedClosed()
    { 
         if (Unlocked)
            return;

            RewardEarned();
    }
 

    void SelectButton(bool enable)
    {
        selectButton.interactable = enable;
    }
    public void SelectSkin()
    {
        if(Unlocked)
        shopController.SkinSelected(this);
       
    }
}
