using UnityEngine;

public class DribbleBoardingController : MonoBehaviour
{
    public GameObject ballParticle;
    public GameObject ballPickUpText;
    public GameObject shootText;
    public bool isBoarding;
    public void BallTaken()
    {
        ballParticle.SetActive(false);
        ballPickUpText.SetActive(false);
        TeachShooting();
    }
    public void StartDribbleBoarding()
    {
        isBoarding = true;
        ballParticle.SetActive(true);
        ballPickUpText.SetActive(true); 
    }
    void TeachShooting()
    {
        Time.timeScale = 0.5f;
        shootText.SetActive(true); 
    }
    public void ShootingBoardingDone()
    {
        shootText.SetActive(false);
        Time.timeScale = 1f; 
    }
}
