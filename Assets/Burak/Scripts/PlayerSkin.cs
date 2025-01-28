using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CrazyGames;
using System.Net;
using NUnit.Framework.Constraints;
public class PlayerSkin : MonoBehaviour
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



    public string Name;
    public float speed;
    public float power;
    public float curve;

    Image image;
    private void Awake()
    {
        if (rewarded)
        {
            priceText.text = Price.ToString();
            adIcon.SetActive(true);
        }
        else
            priceText.text = Price.ToString();



        image = GetComponent<Image>();
    }
    public void Init(ShopController shopController1, CoinController coinController1, bool unlocked)
    {
        UnselectedColor();


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
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

        shopController.NewSkinUnlocked(this);
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

        if (Unlocked)
            return;


        Debug.Log("unlocked");
        Unlocked = true;
        unlockButton.gameObject.SetActive(false);
        SelectButton(true);

        shopController.NewSkinUnlocked(this);
        SelectSkin();
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

    public void Preview()
    {
        shopController.SkinPreview(this);
    }
    void SelectButton(bool enable)
    {
       // selectButton.interactable = enable;
    }
    public void SelectSkin()
    {
        if (Unlocked)
        {
            shopController.SkinSelected(this);
        }
        else
        {
            Preview();
        }
    }
    public void SelectedColor()
    {
        if (image)
            image.color = new Color(0, 185, 255, 255);
        else
        {
            image = GetComponent<Image>();
            image.color = new Color(0, 185, 255, 255);

        }
    }
    public void UnselectedColor()
    {

        if (image)
            image.color = Color.white;

        else
        {
            image = GetComponent<Image>();
            image.color = new Color(0, 185, 255, 255);

        }
    }
}
