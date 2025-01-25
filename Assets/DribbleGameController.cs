using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
 public class DribbleGameController : MonoBehaviour
{
    public SoundController soundController;
    public TextMeshProUGUI goalText;
    bool workOnce = false;
    int coin;
    [SerializeField] GameObject dribbleBall;
    
    private void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        soundController.PlayWhistle();
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
    }
    public void Goal()
    {
        if (workOnce)
            return;

        workOnce = true;
        goalText.gameObject.SetActive(true);
        soundController.GoalSound();
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