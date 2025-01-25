using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelController : MonoBehaviour
{
    GameController gameController;
    [SerializeField] GameObject[] levels;
    [SerializeField] bool playSpecificLevel = false;
    public int Level;
    GameObject activeLevel;
    public TextMeshProUGUI Leveltext;

  [SerializeField]  OnBoardingController boardingController;
    private void Awake()
    {
        gameController = GetComponent<GameController>();
        if (playSpecificLevel)
        {
            ActivateLevel();
            return;
        }


        if (PlayerPrefs.HasKey("Level"))
        {
            Level = PlayerPrefs.GetInt("Level", 1);
        }
        else
            Level = 1;

        if (Level == 1)
        {
            boardingController.StartOnBoarding();
            gameController.OnBoarding();
        }

        ActivateLevel();
       
    }

    public int levelIndex;
    void ActivateLevel()
    {
        string a = " " + Level.ToString();
        string b = Leveltext.text;
        Leveltext.text = b + a;

        levelIndex = Level % (levels.Length + 1);


        if (Level <= levels.Length)
        {
            var go = Instantiate(levels[levelIndex - 1]);
            go.transform.parent = transform;
            go.SetActive(true);
            //  levels[levelIndex - 1].SetActive(true);
            activeLevel = go;


        }

        else
        {
            var go = Instantiate(levels[levelIndex]);
            go.transform.parent = transform;

            go.SetActive(true);
            activeLevel = go;

        }
    }

    public GameObject GetActiveLevel()
    {
        return activeLevel;
    }
    public void NextLevel()
    {
        Level = PlayerPrefs.GetInt("Level", 1);
      // if (levels[Level].GetComponent<>)
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
 

}
