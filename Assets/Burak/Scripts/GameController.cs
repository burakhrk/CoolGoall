using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class GameController : MonoBehaviour
{
    [SerializeField] GameObject kale;
    [SerializeField] GameObject top;

    [SerializeField] GameObject topPos;
    [SerializeField] GameObject kalePos;
    [SerializeField] Player player;

    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    LevelController levelController;
    [SerializeField] GameObject cheerText;
    Bezier bezier;
    public UnityAction OnGameEnd;
    private void Awake()
    {
        levelController = GetComponent<LevelController>();
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
        Invoke("ActivateLosePanel",2f);
    }
 void ActivateWinPanel()
    {
        PlayerPrefs.SetInt("Level", levelController.Level + 1);
        winPanel.SetActive(true);

    }
    void ActivateLosePanel()
    {
        losePanel.SetActive(true);
    }
}
