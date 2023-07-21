using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class GameController : MonoBehaviour
{
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

    CoinController coinController;
    private void Awake()
    {
        coinController = GetComponent<CoinController>();
    }
    private void Start()
    {

        levelController = GetComponent<LevelController>();
        kale = FindObjectOfType<Kale>().gameObject;
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
        OnGameEnd?.Invoke();
        cheerText.SetActive(true);
        player.Goal();
        Invoke("ActivateWinPanel", 2f);
    }
    public void Lose()
    {
        OnGameEnd?.Invoke();
        player.Fail();
        Invoke("ActivateLosePanel",2f);
    }
 void ActivateWinPanel()
    {
        PlayerPrefs.SetInt("Level", levelController.Level + 1);
        coinController.MakeCoin(100);
        winPanel.SetActive(true);
        shopPanel.SetActive(true);
    }
    void ActivateLosePanel()
    {
        coinController.MakeCoin(25);
        shopPanel.SetActive(true);

        losePanel.SetActive(true);
    }
}
