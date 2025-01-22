using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyGames;
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
    void RewardedClosed()
    { 
        RewardEarned();
    }
    public void UnlockedItem()
    {
        Debug.Log("unlocked");
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

       shopController.NewBallSkinUnlocked(this);
        SelectSkin();

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
        Debug.Log("unlocked");
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

        shopController.NewBallSkinUnlocked(this);
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

