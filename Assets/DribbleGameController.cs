using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
     using Unity;
using UnityEngine.Events;
public class DribbleGameController : MonoBehaviour
{
    public SoundController soundController;
    public TextMeshProUGUI goalText;
    bool workOnce = false;
    int coin;
    [SerializeField] GameObject dribbleBall;
    [SerializeField] LevelController levelController; 
    DribbleBoardingController DribbleBoarding;
    public bool GameEnd = false;
    public GameObject winPanel,LosePanel;
    public UnityAction OnGameStart;

    private void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        soundController.PlayWhistle();
        OnGameStart?.Invoke();
    }
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetInt("Coin");
        }
        else
        {
            coin = 0;
            PlayerPrefs.SetInt("Coin", coin);
        }
    }
    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BurakBallDragging");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
           levelController.IntersitialClosed();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
           SpawnBall();
        }
    }
    public void Goal()
    {
        if (workOnce)
            return;

        workOnce = true;
        goalText.gameObject.SetActive(true);
        soundController.GoalSound();
        Invoke("DisableGoalText", 1.2f);
        Win();
    }
  public  void Win()
    {
        if (GameEnd)
            return;

        GameEnd = true; 
        winPanel.SetActive(true);
    }
  public  void Lose()
    {
        if (GameEnd)
            return;

        GameEnd = true;
        LosePanel.SetActive(true);
        
    }
    void DisableGoalText()
    {
        goalText.gameObject.SetActive(false);   
    }
    public void GoldCollected(int plus)
    {
        coin = coin + plus;
        PlayerPrefs.SetInt("Coin", coin);
    }
    public void SpawnBall()
    {
        GameObject go;
      go= Instantiate(dribbleBall);
        go.transform.position=Vector3.zero;
        
    }
}