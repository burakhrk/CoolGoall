using UnityEngine;
using TMPro;
public class StatsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI  powerText,speedText,curveText;
    [SerializeField] TextMeshProUGUI powerTextBall, speedTextBall, curveTextBall;

    [SerializeField] int  speedX, curveX, powerX;
    [SerializeField] float speedB, curveB, powerB;
    [SerializeField] Bezier bezier;
    [SerializeField] TrailTest trail;
    // [SerializeField] float speedUp, curveUp, powerUp;

    [SerializeField] Ball ball; 
    private void Start()
    {
        GetPrefs();
        ApplyStats(); 
    }
    public void SetPrefs()
    {
        PlayerPrefs.SetInt("Power",powerX);
        PlayerPrefs.SetInt("Speed", speedX);
        PlayerPrefs.SetInt("Curve", curveX);
        PlayerPrefs.SetInt("PowerBall",(int) powerB);
        PlayerPrefs.SetInt("SpeedBall", (int)speedB);
        PlayerPrefs.SetInt("CurveBall", (int)curveB);

    }
    public void GetPrefs()
    { 
       powerX= PlayerPrefs.GetInt("Power", powerX);
       speedX= PlayerPrefs.GetInt("Speed", speedX);
       curveX= PlayerPrefs.GetInt("Curve", curveX);
        powerB = PlayerPrefs.GetInt("PowerBall",(int) powerB);
        speedB = PlayerPrefs.GetInt("SpeedBall", (int) speedB);
        curveB = PlayerPrefs.GetInt("CurveBall", (int) curveB);
    }
    private void OnDisable()
    {
        SetPrefs();
    }
    public void ApplyStats()
    {
        float speedLast=speedX+speedB;
        float curveLast=curveX+curveB;
        float powerLast=powerX+powerB;
        if(speedLast <= 89)
        {
            ball.SetSpeed(175);
            trail.SetParticleLevel1();
        }
       else if (speedLast <= 99)
        {
            ball.SetSpeed(255);
            trail.SetParticleLevel2(); 
        }
        else if (speedLast <= 119)
        {
            ball.SetSpeed(355);
            trail.SetParticleLevel3(); 
        }
        else
        {
            ball.SetSpeed(400);
            trail.SetParticleLevel4();
        }

        if (curveLast <= 89)
        {
            bezier.SetCurveStrength(0.9f);
        }
        else if (curveLast <= 99)
        {
            bezier.SetCurveStrength(1f);
        }
        else if (curveLast <= 119)
        {
            bezier.SetCurveStrength(1.1f);
        }
        else
        {
            bezier.SetCurveStrength(1.15f); 
        }
        SetPrefs();
        UpdateTexts();
    }
    public void UpdateStats(float power, float speed , float curve)
    {
         
        powerX = (int)power;
        speedX= (int)speed;
        curveX= (int)curve;
        UpdateTexts();
     }
    public void UpdateStatsBall(float power, float speed, float curve)
    { 
        powerB = power;
        speedB = speed;
        curveB = curve; 
        UpdateTexts();
     }
   public void UpdateTexts()
    {
        powerText.text = powerX.ToString();
        speedText.text = speedX.ToString();
        curveText.text = curveX.ToString();
        powerTextBall.text = "+ " + powerB.ToString();
        speedTextBall.text = "+ " + speedB.ToString();
        curveTextBall.text = "+ " + curveB.ToString();

    }
}
