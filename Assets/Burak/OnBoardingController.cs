using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class OnBoardingController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject AimText, airShotText;
    
    // Instead of direct reference, we will listen to input
    // But since we need to check IsAiming, finding PlayerController is easiest
    [SerializeField] PlayerController playerController;

    private int step = 0;
    private float aimTimer = 0f;
    private bool stateComplete = false;
    private Sequence handSequence;
    private Sequence textPulseSequence;

    private void Awake()
    {
        if (playerController == null)
            playerController = FindFirstObjectByType<PlayerController>();
    }

    private void OnDestroy()
    {
        if (handSequence != null) handSequence.Kill();
        if (textPulseSequence != null) textPulseSequence.Kill();
    }

    public void StartOnBoarding()
    {
        gameController.CanShoot = false;
        step = 0;
        TeachAiming();
    }

    private void Update()
    {
        if (step == 0) // Aiming Step
        {
            if (playerController.IsAiming)
            {
                aimTimer += Time.deltaTime;
                if (aimTimer > 2.0f && !stateComplete)
                {
                    stateComplete = true;
                    // Success feedback can go here
                    NextStep();
                }
            }
            else
            {
                aimTimer = 0f;
            }
        }
        else if (step == 1) // OverShot / AirShot Step
        {
             // For the second step, we also wait for user to hold aim a bit to "understand"
            if (playerController.IsAiming)
            {
                aimTimer += Time.deltaTime;
                 if (aimTimer > 2.0f && !stateComplete)
                {
                     stateComplete = true;
                    NextStep();
                }
            }
             else
            {
                aimTimer = 0f;
            }
        }
    }

    public void NextStep()
    {
        step++;
        aimTimer = 0;
        stateComplete = false;

        if (step == 1)
            TeachOverShot();
        else if (step == 2)
            FinishBoarding();
    }

    void TeachAiming()
    {
        AimText.SetActive(true);
        hand.SetActive(true);

        // Animate Hand Left-Right to simulate aiming
        float initialX = hand.transform.position.x;
        handSequence = DOTween.Sequence();
        handSequence.Append(hand.transform.DOMoveX(initialX + 2f, 1f).SetEase(Ease.InOutSine));
        handSequence.Append(hand.transform.DOMoveX(initialX - 2f, 1f).SetEase(Ease.InOutSine));
        handSequence.SetLoops(-1, LoopType.Yoyo);

        // Animate Text Pulse
        textPulseSequence = DOTween.Sequence();
        textPulseSequence.Append(AimText.transform.DOScale(1.1f, 0.5f));
        textPulseSequence.Append(AimText.transform.DOScale(1.0f, 0.5f));
        textPulseSequence.SetLoops(-1, LoopType.Restart);
    }

    public void TeachOverShot()
    {
        // Cleanup previous step
        AimText.SetActive(false);
        if (textPulseSequence != null) textPulseSequence.Kill();

        airShotText.SetActive(true);
        // Keep hand active but maybe change animation? 
        // For now, let's keep the hand animation running as it demonstrates aiming generally
        
        // Pulse new text
        textPulseSequence = DOTween.Sequence();
        textPulseSequence.Append(airShotText.transform.DOScale(1.1f, 0.5f));
        textPulseSequence.Append(airShotText.transform.DOScale(1.0f, 0.5f));
        textPulseSequence.SetLoops(-1, LoopType.Restart);
    }

    void FinishBoarding()
    {
        // Cleanup
        airShotText.SetActive(false);
        hand.SetActive(false);
        
        if (handSequence != null) handSequence.Kill();
        if (textPulseSequence != null) textPulseSequence.Kill();

        gameController.OnBoardingDone();
    }
}
