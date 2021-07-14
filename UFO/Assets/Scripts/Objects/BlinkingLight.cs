using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    public GameObject gameObjectLight;     //Объект, к которому присоединен источник света

    private Light blinkingLight;    //Источник света

    public float intesiveLight;     //Текущее значение интенсивности света

    void Start()
    {
        //Получение компонента light
        blinkingLight = gameObjectLight.GetComponentInChildren<Light>();
        //Получеине текущего значения интенсивности источника света
        intesiveLight = blinkingLight.intensity;
        
        #region Удалить после настройки второго уровня
        //Установка масштаба времени 1
        Time.timeScale = 1f;
        //Запуск корутины по изменению состояния света
        StartCoroutine(DoBlink());
        #endregion
    }

    public void StartBlinkingLight()
    {
        StartCoroutine(DoBlink());
    }

    IEnumerator DoBlink()
    {
        //Пауза перед деактивацией источника света
        yield return new WaitForSeconds(Random.value);
        //Деактиватия источника света
        //blinkingLight.enabled = false;
        gameObjectLight.SetActive(false);

        //Пауза перед активацией источника света
        yield return new WaitForSeconds(Random.value);
        //Активатия источника света
        gameObjectLight.SetActive(true);
        //blinkingLight.enabled = true;
        //Установка интенсивности света в зависимости от рандомного значения
        blinkingLight.intensity = intesiveLight * Random.value;
        //Перезапуск корутины
        StartCoroutine(DoBlink());
    }
}
