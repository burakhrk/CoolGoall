using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopController : MonoBehaviour
{
    [SerializeField] List<PlayerSkin> unlockedSkinList = new List<PlayerSkin>();
    [SerializeField] List<PlayerSkin> skinList = new List<PlayerSkin>();


    [SerializeField] List<BallSkin> unlockedBallSkinList = new List<BallSkin>();
    [SerializeField] List<BallSkin> BallskinList = new List<BallSkin>();


   public AdManager adManager;
    CoinController coinController;
    [SerializeField] GameObject formaPanel;
    [SerializeField] GameObject topPanel;
    [SerializeField] Image formaButton,topButton;
    [SerializeField] SkinController skinController;
    private void Awake()
    {
       
        for (int i = 0; i < skinList.Count; i++)
        {
            if (PlayerPrefs.GetInt("Unlock" + i, 0) == 1)
            {
                unlockedSkinList.Add(skinList[i]);
            }
        }


        for (int i = 0; i < BallskinList.Count; i++)
        {
            if (PlayerPrefs.GetInt("UnlockBall" + i, 0) == 1)
            {
                unlockedBallSkinList.Add(BallskinList[i]);
            }
        }


        coinController = GetComponent<CoinController>();

        topButton.color = Color.white;
        formaButton.color = Color.gray;

        InitButtons();
    }
    private void OnEnable()
    {
        coinController.onCoinChanged += CheckButtonAvailabity;
    }
    private void OnDisable()
    {
        coinController.onCoinChanged -= CheckButtonAvailabity;

    }
    void CheckButtonAvailabity(int a)
    {
        foreach (var item in skinList)
        {
            item.CheckMoney(a);
        }
    }
    public void NewSkinUnlocked(PlayerSkin playerSkin)
    {
        int a = skinList.IndexOf(playerSkin);
        PlayerPrefs.SetInt("Unlock" + a, 1);
    }
    public void NewBallSkinUnlocked(BallSkin ballSkin)
    {
        int a = BallskinList.IndexOf(ballSkin);
        PlayerPrefs.SetInt("UnlockBall" + a, 1);
    }
    void InitButtons()
    {
        foreach (var item in skinList)
        {
            item.Init(this, coinController, unlockedSkinList.Contains(item));
        }
        foreach (var item in BallskinList)
        {
            item.Init(this, coinController, unlockedBallSkinList.Contains(item));
        }
    }
    public void OpenFormaPanel()
    {
        topButton.color = Color.white;
        formaButton.color = Color.gray;
        topPanel.SetActive(false);
        formaPanel.SetActive(true);
    }
    public void OpenTopPanel()
    {
        topButton.color = Color.gray;
        formaButton.color = Color.white;
        topPanel.SetActive(true);
        formaPanel.SetActive(false);
    }
    public void SkinSelected(PlayerSkin a)
    {
        skinController.ChangeSkin(skinList.IndexOf(a));
    }

    public void SkinSelectedBall(BallSkin a)
    {
        skinController.ChangeBallSkin(BallskinList.IndexOf(a));

    }
}
