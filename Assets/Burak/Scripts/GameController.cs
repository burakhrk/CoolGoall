using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        player.Goal();
        ActivateWinPanel();
    }
    public void Lose()
    {
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
