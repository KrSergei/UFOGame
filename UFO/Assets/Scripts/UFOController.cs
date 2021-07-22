using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UFOController : MonoBehaviour
{
    public Rigidbody LeftLegRB, RightLegRB;    
    public UIManager UIManager;
    public WorldBulder WorldBulder;
    public float rotationMultipler = 0.8f;          //Показатель уменьшения силы двигателя при повороте       
    public float MaxAltitude;                       //Максимальная высота, на которой отключаются двигатели
    public float AltitudeDecreasePowerEngine = 5f;  //Высота, с которой начинается уменьшение коэффициента мощности двигателя

    [SerializeField]
    private float power = 12f;                      //Сила воздействия
    private float curretnRatioPower = 1f;           //Текущий коэффициент мощности
    private Vector3 currentPowerLeftEngine;         //Текущая мощность левого двигателя
    private Vector3 currentPowerRightEngine;        //Текущая мощность правого двигателя
    private SceneLoader SceneLoader;

    public float Power { get => power; set => power = value; }

    void Start()
    {
        SceneLoader = FindObjectOfType<SceneLoader>();
        UIManager = FindObjectOfType<UIManager>();
        WorldBulder = FindObjectOfType<WorldBulder>();
    }

    void FixedUpdate()
    {
        Vector3 minForce = Vector3.up * Power * rotationMultipler;
        Vector3 maxForce = Vector3.up * Power;

        currentPowerLeftEngine = Vector3.zero;
        currentPowerRightEngine = Vector3.zero;

        //Проверка на высоту, и уменьшение коэффициента мощности в зависимости от высоты
        if (GetCurrentAltitude() >= AltitudeDecreasePowerEngine)
        {
            curretnRatioPower = GetCurrentRatioPower();
        }
        else
        {
            curretnRatioPower = 1f;
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneLoader.GetComponent<SceneLoader>().RestartScene();
        }

        if (Input.GetKey(KeyCode.A))
        {
            //Добавление мощности левого сопла - 80%, правого - 100%
            currentPowerLeftEngine = minForce;
            currentPowerRightEngine = maxForce;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Добавление мощности левого сопла - 100%, правого - 80%
            currentPowerLeftEngine = maxForce;
            currentPowerRightEngine = minForce;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            //Применение ускорения вверх
            currentPowerLeftEngine = maxForce;
            currentPowerRightEngine = maxForce;
        } 

        LeftLegRB.AddRelativeForce(currentPowerLeftEngine * curretnRatioPower);
        RightLegRB.AddRelativeForce(currentPowerRightEngine * curretnRatioPower);
    }

    /// <summary>
    /// Возвращает текущее значени высоты (значение по вектору y)
    /// </summary>
    /// <returns></returns>
    public float GetCurrentAltitude()
    {
        return RoundingUpValueForHUD(transform.position.y);
    }

    /// <summary>
    /// Метод возврата округленного значения мощности левого двигателя
    /// </summary>
    /// <returns></returns>
    public float GetLeftEnginePower()
    {
        return RoundingUpValueForHUD(currentPowerLeftEngine.y * curretnRatioPower);
    }

    /// <summary>
    /// Метод возврата округленного значения мощности правого двигателя
    /// </summary>
    /// <returns></returns>
    public float GetRightEnginePower()
    {
        return RoundingUpValueForHUD(currentPowerRightEngine.y * curretnRatioPower);
    }

    /// <summary>
    /// Возвращает текущий коэффициент в зависимости от высоты
    /// </summary>
    /// <returns></returns>
    private float GetCurrentRatioPower()
    {
        //Определение диапазона, в котором требуется изменение коэффициента мощности
        float rangeChangedRatioPower = MaxAltitude - AltitudeDecreasePowerEngine;
        //Определение разности между текущей высотой и маскимальной высотой
        float currentDifferenceInDecreaseRatio = MaxAltitude - GetCurrentAltitude();
        
        //Если разность между маскимальной высотой и текущей равно 0, возврат 0, иначе вычисление коэффициента мощности
        if(currentDifferenceInDecreaseRatio <= 0)
        {
            return 0;
        }
        else
        {
            //Вычисление коэффициента мощности
            return  currentDifferenceInDecreaseRatio / rangeChangedRatioPower;
        }
    }

    /// <summary>
    /// Округление значения мощности для показва на экране
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float RoundingUpValueForHUD(float value)
    {
        double result = Math.Round(value, 1); 
        return (float)result;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Перезагрузка сцены
            UIManager.GetComponent<SceneLoader>().RestartScene();
            //SceneLoader.RestartScene();
        }

        if (collision.gameObject.tag == "Finish")
        {
            //Активация метода для загрузки нужного меню
            UIManager.SetActiveMenuAfterPassedLvl();
            //Установка скорости игры в 0
            //WorldBulder.SetSpeedGame(0f);
            WorldBulder.GetComponent<WorldBulder>().SetSpeedGame(0f);
        }
    }
}
