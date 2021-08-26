using System;
using UnityEngine;
using UnityEngine.UI;

public class UFOController : MonoBehaviour
{
    public Rigidbody LeftLegRB, RightLegRB;    
    public GameObject UIManager;
    public GameObject WorldBulder;
    public UFOAudioControl audioControl;
    public float rotationMultipler = 0.8f;          //Показатель уменьшения силы двигателя при повороте       
    public float MaxAltitude;                       //Максимальная высота, на которой отключаются двигатели
    public float AltitudeDecreasePowerEngine = 5f;  //Высота, с которой начинается уменьшение коэффициента мощности двигателя

    Rigidbody mainRb;

    [Header("Flag is Alive"), SerializeField]
    private bool isAlive = true;

    public string maneAudioSourceEngineEffect = "Engine";
    public string maneEAudioSourcexploisionEffect = "Exploision";

    [SerializeField]
    private float power = 12f;                      //Сила воздействия
    public float Power { get => power; set => power = value; }

    private float curretnRatioPower = 1f;           //Текущий коэффициент мощности
    private Vector3 currentPowerLeftEngine;         //Текущая мощность левого двигателя
    private Vector3 currentPowerRightEngine;        //Текущая мощность правого двигателя
    private SceneLoader SceneLoader;

    void Start()
    {
        mainRb = GetComponent<Rigidbody>();
        SceneLoader = UIManager.GetComponent<SceneLoader>();        
        audioControl = GetComponent<UFOAudioControl>();
    }

    void FixedUpdate()
    {
        Vector3 minForce = Vector3.up * Power * rotationMultipler;
        Vector3 maxForce = Vector3.up * Power;

        currentPowerLeftEngine = Vector3.zero;
        currentPowerRightEngine = Vector3.zero;

        if (Input.GetKey(KeyCode.R))
        {
            SceneLoader.GetComponent<SceneLoader>().RestartScene();
        }

        if (isAlive)
        {
            //Проверка на высоту, и уменьшение коэффициента мощности в зависимости от высоты
            if (GetCurrentAltitude() >= AltitudeDecreasePowerEngine)
            {
                curretnRatioPower = GetCurrentRatioPower();
            }
            else
            {
                curretnRatioPower = 1f;
            }

            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<UFODestructionBody>().DestoyBody();
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
            else
                OffSoundEffect(maneAudioSourceEngineEffect);

            LeftLegRB.AddRelativeForce(currentPowerLeftEngine * curretnRatioPower);
            RightLegRB.AddRelativeForce(currentPowerRightEngine * curretnRatioPower);

            if (currentPowerLeftEngine.y + currentPowerRightEngine.y > 0)
                OnSoundEffect(maneAudioSourceEngineEffect);
        }
        else
        {
            OffSoundEffect(maneAudioSourceEngineEffect);
        }
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

    public void OnSoundEffect(string nameEffect)
    {
        audioControl.PlaySound(nameEffect);
    }

    public void OffSoundEffect(string nameEffect)
    {
        audioControl.PauseSound(nameEffect);
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

    /// <summary>
    /// Возвращает текущую скорость по оси Х 
    /// </summary>
    /// <returns></returns>
    public float GetVelosityX()
    {
        return mainRb.velocity.x;
    }
    public float GetVelosityY()
    {
        return mainRb.velocity.y;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (isAlive)
                OnSoundEffect(maneEAudioSourcexploisionEffect);
            isAlive = false;
            //Перезагрузка сцены
            //UIManager.GetComponent<SceneLoader>().RestartScene();
            GetComponent<UFODestructionBody>().DestoyBody();
            GetComponent<UFOActivationExploisionEffect>().ActivationExploision();
            
        }

        if (collision.gameObject.tag == "Finish")
        {
            //Активация метода для загрузки нужного меню
            UIManager.GetComponent<UIManager>().SetActiveMenuAfterPassedLvl();
            //Установка скорости игры в 0
            //WorldBulder.SetSpeedGame(0f);
            WorldBulder.GetComponent<WorldBulder>().SetSpeedGame(0f);
        }
    }
}
