using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelController : MonoBehaviour
{
   [SerializeField] GameController gameController;
    [SerializeField] GameObject[] levels;
    [SerializeField] bool playSpecificLevel = false;
    public int Level;
    GameObject activeLevel;
    public TextMeshProUGUI Leveltext;
    [SerializeField] DribbleGameController dribbleGameController;
  [SerializeField]  OnBoardingController boardingController;
    [SerializeField] DribbleBoardingController dribbleBoardingController;
    private void Awake()
    {
        
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

        ActivateLevel();
    }

    public int levelIndex;
    void ActivateLevel()
    {
        string a = " " + Level.ToString();
        string b = Leveltext.text;
        Leveltext.text = b + a;

        levelIndex = Level % (levels.Length + 1);
        GameObject go;

        if (Level <= levels.Length)
        {
              go = Instantiate(levels[levelIndex - 1]);
            go.transform.parent = transform;
            go.SetActive(true);
            //  levels[levelIndex - 1].SetActive(true);
            activeLevel = go;


        }
        else
        {
              go = Instantiate(levels[levelIndex]);
            go.transform.parent = transform;

            go.SetActive(true);
            activeLevel = go;

        }
        
        Level level;
        level=go.GetComponent<Level>();
        if(level.isDribbleLevel&& SceneManager.GetActiveScene().buildIndex==0)
        {
            
            SceneManager.LoadScene(1);
            return;
        }
        if (level.isTutorial)
        {
            boardingController.StartOnBoarding();
            gameController.OnBoarding();
        }
        else if (level.isDribblingTutorial)
        {
            if(dribbleBoardingController)
                dribbleBoardingController=FindFirstObjectByType<DribbleBoardingController>();

            dribbleBoardingController.StartDribbleBoarding();
         }
         
    }

    public GameObject GetActiveLevel()
    {
        return activeLevel;
    }
    public void NextLevel()
    {
        Level = PlayerPrefs.GetInt("Level", 1);
         if (levels[Level-1].GetComponent<Level>().isDribbleLevel)
        {
            SceneManager.LoadScene(1); 
        }
        else
        {
            SceneManager.LoadScene(0); 
        } 
    }
    public void Restart()
    {
        if (levels[Level].GetComponent<Level>().isDribbleLevel)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
 

}
