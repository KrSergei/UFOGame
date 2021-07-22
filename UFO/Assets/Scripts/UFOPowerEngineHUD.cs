using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UFOPowerEngineHUD : MonoBehaviour
{

    public Slider rightEngineSlider;
    public Slider leftEngineSlider;

    public Text rightEnginePowerText;
    public Text leftEnginePowerText;

    private UFOController getLeftPowerEngineValue;
    private UFOController getRightPowerEngineValue;


    // Start is called before the first frame update
    void Start()
    {
        //ѕолучение сслыки на метод получени€ значени€ мощности левого двигател€
        getLeftPowerEngineValue = GetComponent<UFOController>();
        //ѕолучение сслыки на метод получени€ значени€ мощности правого двигател€
        getRightPowerEngineValue = GetComponent<UFOController>();

        //”становка масимального значени€ дл€ слайдера правого двигател€
        rightEngineSlider.maxValue = GetComponent<UFOController>().Power;
        //”становка масимального значени€ дл€ слайдера левого двигател€
        leftEngineSlider.maxValue = GetComponent<UFOController>().Power;
    }

    // Update is called once per frame
    void Update()
    {
        SetSliderValue(leftEngineSlider, getLeftPowerEngineValue.GetLeftEnginePower());
        SetSliderValue(rightEngineSlider, getRightPowerEngineValue.GetRightEnginePower());

        ShowCurrentPowerEngine(leftEnginePowerText, getLeftPowerEngineValue.GetLeftEnginePower());
        ShowCurrentPowerEngine(rightEnginePowerText, getRightPowerEngineValue.GetRightEnginePower());
    }

    /// <summary>
    /// ”становка значени€ слайдера, переданного в параметр
    /// </summary>
    /// <param name="slider">—лайдер</param>
    private void SetSliderValue(Slider slider, float powerValue)
    {
        slider.value = powerValue;
    }

    private void ShowCurrentPowerEngine(Text text, float powerValue)
    {
        text.text = powerValue + " Wt";
    }
}
