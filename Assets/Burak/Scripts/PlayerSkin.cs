using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
        //  unlockButton = GetComponentInChildren<Button>();
        // selectButton = GetComponent<Button>();
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
        Debug.Log("unlocked");
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

        shopController.NewSkinUnlocked(this);

        SelectSkin();
    }
    public void Unlock()
    {
        if (rewarded)
        {
            UnlockRewarded();
            return;
        }
          

        coinController.SpendCoin(Price);
        UnlockedItem();

    }
    void UnlockRewarded()
    {
        /*
        adManager = shopController.adManager;
       if(adManager.RewardedAdManager.IsRewardedAdReady())
        {
            adManager.RewardedAdManager.RegisterOnAdClosedEvent(RewardedClosed);
            adManager.RewardedAdManager.RegisterOnAdShowFailedEvent(RewardedClosed);
            adManager.RewardedAdManager.RegisterOnUserEarnedRewarededEvent(RewardEarned);
            adManager.RewardedAdManager.ShowAd();
        }
        */
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

     
    private void RewardedClosedTwo()
    {
        /*
        adManager.RewardedAdManager.UnRegisterOnAdClosedEvent(RewardedClosed);
        adManager.RewardedAdManager.UnRegisterOnAdShowFailedEvent(RewardedClosed);
        adManager.RewardedAdManager.UnRegisterOnUserEarnedRewarededEvent(RewardEarned);


        */
         if (Unlocked)
            return;

            RewardEarned();
     }

    private void RewardedClosedOne()
    {
        /*
        adManager.RewardedAdManager.UnRegisterOnAdClosedEvent(RewardedClosed);
        adManager.RewardedAdManager.UnRegisterOnAdShowFailedEvent(RewardedClosed);
        adManager.RewardedAdManager.UnRegisterOnUserEarnedRewarededEvent(RewardEarned);
        */
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
        shopController.SkinSelected(this);
    }
}
