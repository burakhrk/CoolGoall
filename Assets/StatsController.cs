using UnityEngine;
using TMPro;
public class StatsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI  powerText,speedText,curveText;
    [SerializeField] int speed, curve, power;
    [SerializeField] float speedUp, curveUp, powerUp;

    [SerializeField] Ball ball;
    private void Awake()
    {
      //  GetPrefs();
        ApplyStats();
    }
    public void SetPrefs()
    {
        PlayerPrefs.SetInt("Power",power);
        PlayerPrefs.SetInt("Speed", speed);
        PlayerPrefs.SetInt("Curve", curve);

    }
    public void GetPrefs()
    { 
       power= PlayerPrefs.GetInt("Power", power);
       speed= PlayerPrefs.GetInt("Speed", speed);
       curve= PlayerPrefs.GetInt("Curve", curve);
    }
    public void ApplyStats()
    {
        ball.SetSpeed(speed);
        SetPrefs();
    }
    public void UpdateStats(float power, float speed , float curve)
    {
        
        powerText.text = power.ToString();
        speedText.text = speed.ToString();  
        curveText.text = curve.ToString();

        powerUp = power;
        speedUp=speed;
        curveUp=curve;

     }
    public void UpdateStatsBall(float power, float speed, float curve)
    {
        powerText.text = power.ToString();
        speedText.text = speed.ToString();
        curveText.text = curve.ToString();

        powerUp = power;
        speedUp = speed;
        curveUp = curve;

     }
}
