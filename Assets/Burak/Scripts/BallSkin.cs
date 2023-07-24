using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class BallSkin : MonoBehaviour
{
    public bool rewarded;
    public bool Unlocked;
    public int Price;
    [SerializeField] Button unlockButton;
    [SerializeField] Button selectButton;
    ShopController shopController;
    CoinController coinController;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] GameObject adIcon;
    private void Awake()
    {
        //  unlockButton = GetComponentInChildren<Button>();
        // selectButton = GetComponent<Button>();
        if (rewarded)
        {
            priceText.text = " ";
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

    public void UnlockedItem()
    {
        Debug.Log("unlocked");
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

       shopController.NewBallSkinUnlocked(this);
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
    AdManager adManager;
    void UnlockRewarded()
    {
        adManager = shopController.adManager;
        if (adManager.RewardedAdManager.IsRewardedAdReady())
        {
            adManager.RewardedAdManager.RegisterOnAdClosedEvent(RewardedClosed);
            adManager.RewardedAdManager.RegisterOnAdShowFailedEvent(RewardedClosed);
            adManager.RewardedAdManager.RegisterOnUserEarnedRewarededEvent(RewardEarned);
            adManager.RewardedAdManager.ShowAd();
        }
    }

    private void RewardEarned(IronSourcePlacement arg1, IronSourceAdInfo arg2)
    {
#if CRAZY_GSDK
        if (Unlocked)
            return;
 
#endif

        Debug.Log("unlocked");
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

        shopController.NewBallSkinUnlocked(this);
    }


    private void RewardedClosed(IronSourceError arg1, IronSourceAdInfo arg2)
    {
        adManager.RewardedAdManager.UnRegisterOnAdClosedEvent(RewardedClosed);
        adManager.RewardedAdManager.UnRegisterOnAdShowFailedEvent(RewardedClosed);
        adManager.RewardedAdManager.UnRegisterOnUserEarnedRewarededEvent(RewardEarned);



#if CRAZY_GSDK
        if (Unlocked)
            return;

        RewardEarned(null, null);
#endif
    }

    private void RewardedClosed(IronSourceAdInfo obj)
    {
        adManager.RewardedAdManager.UnRegisterOnAdClosedEvent(RewardedClosed);
        adManager.RewardedAdManager.UnRegisterOnAdShowFailedEvent(RewardedClosed);
        adManager.RewardedAdManager.UnRegisterOnUserEarnedRewarededEvent(RewardEarned);
    }

    void SelectButton(bool enable)
    {
        selectButton.interactable = enable;

    }
    public void SelectSkin()
    {
       shopController.SkinSelectedBall(this);
    }
}

