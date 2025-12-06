using UnityEngine;
using DG.Tweening;

public class DribbleBoardingController : MonoBehaviour
{
    public GameObject ballParticle;
    public GameObject ballPickUpText;
    public GameObject shootText;
    public bool isBoarding;

    private Sequence textPulseSequence;

    private void OnDestroy()
    {
        if (textPulseSequence != null) textPulseSequence.Kill();
    }

    public void BallTaken()
    {
        ballParticle.SetActive(false);
        ballPickUpText.SetActive(false);
        
        // Stop previous tween if any
        if (textPulseSequence != null) textPulseSequence.Kill();

        TeachShooting();
    }

    public void StartDribbleBoarding()
    {
        isBoarding = true;
        ballParticle.SetActive(true);
        ballPickUpText.SetActive(true);
        
        // Pulse Pickup Text
        PulseText(ballPickUpText);
    }

    void TeachShooting()
    {
        // Removed Time.timeScale manipulation for smoother flow
        shootText.SetActive(true);
        
        // Pulse Shoot Text
        PulseText(shootText);
    }

    void PulseText(GameObject textObj)
    {
        if (textPulseSequence != null) textPulseSequence.Kill();
        
        textPulseSequence = DOTween.Sequence();
        textPulseSequence.Append(textObj.transform.DOScale(1.1f, 0.5f));
        textPulseSequence.Append(textObj.transform.DOScale(1.0f, 0.5f));
        textPulseSequence.SetLoops(-1, LoopType.Restart);
    }

    public void ShootingBoardingDone()
    {
        shootText.SetActive(false);
        if (textPulseSequence != null) textPulseSequence.Kill();
        
        // Ensure time scale is correct just in case (though we removed the modification)
        Time.timeScale = 1f; 
    }
}
