using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UFOAltitudeHUD : MonoBehaviour
{
    public Slider slider;
    public Text altitudeValueText;

    private float maxAltitude;                 //Максимальная высота

    private UFOController uFOController;
    private float altitude;
    // Start is called before the first frame update
    void Start()
    {
        uFOController = GetComponent<UFOController>();
        maxAltitude = uFOController.MaxAltitude;
        slider.maxValue = maxAltitude;
    }

    private void Update()
    {
        ShowCurrentAltitude();
        SetSliderValue();
    }

    /// <summary>
    /// Показ текущего значения высоты
    /// </summary>
    private void ShowCurrentAltitude()
    {
        altitudeValueText.text =  uFOController.GetCurrentAltitude() + " M";
    }

    /// <summary>
    /// Установка значения слайдера
    /// </summary>
    private void SetSliderValue()
    {
        slider.value = uFOController.GetCurrentAltitude();
    }
}
