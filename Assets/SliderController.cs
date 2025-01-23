using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{
    [SerializeField] Slider slider;
    public float sliderIncreaseAmount = 0.1f;
   public bool startSlider = false;
   [SerializeField] bool sliderMoveRight = true;
    private void Start()
    {
        startSlider = false;
        slider.gameObject.SetActive(false);

    }
    public void StartSlider()
    {
        startSlider = true;
        slider.gameObject.SetActive(true);
    }
 
    private void Update()
    {
        if(startSlider)
        {
            HandleSlider();
        }
    }
    void HandleSlider()
    {
       if(sliderMoveRight)
        slider.value = slider.value + sliderIncreaseAmount;
       else
            slider.value = slider.value -sliderIncreaseAmount;


       if(slider.value==0)
            sliderMoveRight = true;
       if(slider.value==1)
            sliderMoveRight = false;
    }
    public void ResetSlider()
    {
        slider.value = 0;
        startSlider = false;
        slider.gameObject.SetActive(false);

    }
    public  float GetValue()
    {
        return slider.value;
    }
    public void ShootValueFeedBack()
    {
        Debug.Log("Tell users some good or bad stuff here");
    }
}
