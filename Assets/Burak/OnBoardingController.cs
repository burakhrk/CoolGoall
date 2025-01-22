using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OnBoardingController : MonoBehaviour
{
  [SerializeField]  GameController gameController;
    [SerializeField] GameObject AimText,airShotText;
    int step = 0;
     public void StartOnBoarding()
    {
        gameController.CanShoot = false;
        TeachAiming();
    }
   public void NextStep()
    {
      step++;
        if (step == 1)
            TeachOverShot();
        if(step == 2)
            FinishBoarding();
    }

    void TeachAiming()
    {
         AimText.gameObject.SetActive(true);
    }
  public  void TeachOverShot()
    {
         AimText.SetActive(false);
        airShotText.SetActive(true);    
    }
    void FinishBoarding()
    {
        airShotText.SetActive(false);
         gameController.OnBoardingDone(); 
    }
}
