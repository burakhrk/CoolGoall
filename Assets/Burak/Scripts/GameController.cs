using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Threading;
public class GameController : MonoBehaviour
{

    public AudioSource WinSound;
    public AudioSource LoseSound;
    
    GameObject kale;
    [SerializeField] GameObject top;

    [SerializeField] GameObject topPos;
    [SerializeField] GameObject kalePos;
    [SerializeField] Player player;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject shopPanel;

    LevelController levelController;
    [SerializeField] GameObject cheerText;
     public UnityAction OnGameEnd;
   public bool gameEnd=false;
    public bool CanShoot = true;
    CoinController coinController;

    [SerializeField] GameObject closeShopButton;
    [SerializeField] GameObject openShopButton;

    private void Awake()
    {
        CanShoot= false;
        coinController = GetComponent<CoinController>();
    }
    void AllowShoot()
    {
        CanShoot= true;
    }
    private void Start()
    {
        Invoke("AllowShoot", 1f);

        levelController = GetComponent<LevelController>();
        kale = FindFirstObjectByType<Kale>().gameObject;
        
        topPos.transform.position = top.transform.position;
        kalePos.transform.position = kale.transform.position;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Burak");
        }
    }
    public void Goal()
    {
        if (gameEnd)
            return;

        WinSound.Play();
        GameEnded();
        
        cheerText.SetActive(true);
        player.Goal();
        Invoke("ActivateWinPanel", 2f);
    }
    public void Lose()
    {
        if (gameEnd)
            return;

        GameEnded();
        LoseSound.Play();
        player.Fail();
        Invoke("ActivateLosePanel",2f);
    }

    private void GameEnded()
    {
        OnGameEnd?.Invoke();
        gameEnd = true;
    }
    void ActivateWinPanel()
    {
        PlayerPrefs.SetInt("Level", levelController.Level + 1);
        coinController.MakeCoin(100);
        winPanel.SetActive(true);
        shopPanel.SetActive(true);
        closeShopButton.SetActive(false);
        openShopButton.SetActive(false);
    }
    void ActivateLosePanel()
    {
        coinController.MakeCoin(25);
        shopPanel.SetActive(true);
        closeShopButton.SetActive(false);
        openShopButton.SetActive(false);

        losePanel.SetActive(true);
    }
    public void OpenShop()
    {
        CanShoot = false;
        closeShopButton.SetActive(true);
        shopPanel.SetActive(true);
    }
    public void CloseShop()
    {
        shopPanel.SetActive(false);
        closeShopButton.SetActive(false);

        Invoke("Asd", 0.3f);
    }
    void Asd()
    {
        CanShoot = true;

    }
}
