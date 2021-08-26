using System;
using UnityEngine;
using UnityEngine.UI;

public class UFOController : MonoBehaviour
{
    public Rigidbody LeftLegRB, RightLegRB;    
    public GameObject UIManager;
    public GameObject WorldBulder;
    public UFOAudioControl audioControl;
    public float rotationMultipler = 0.8f;          //���������� ���������� ���� ��������� ��� ��������       
    public float MaxAltitude;                       //������������ ������, �� ������� ����������� ���������
    public float AltitudeDecreasePowerEngine = 5f;  //������, � ������� ���������� ���������� ������������ �������� ���������

    Rigidbody mainRb;

    [Header("Flag is Alive"), SerializeField]
    private bool isAlive = true;

    public string maneAudioSourceEngineEffect = "Engine";
    public string maneEAudioSourcexploisionEffect = "Exploision";

    [SerializeField]
    private float power = 12f;                      //���� �����������
    public float Power { get => power; set => power = value; }

    private float curretnRatioPower = 1f;           //������� ����������� ��������
    private Vector3 currentPowerLeftEngine;         //������� �������� ������ ���������
    private Vector3 currentPowerRightEngine;        //������� �������� ������� ���������
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
            //�������� �� ������, � ���������� ������������ �������� � ����������� �� ������
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
                //���������� �������� ������ ����� - 80%, ������� - 100%
                currentPowerLeftEngine = minForce;
                currentPowerRightEngine = maxForce;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //���������� �������� ������ ����� - 100%, ������� - 80%
                currentPowerLeftEngine = maxForce;
                currentPowerRightEngine = minForce;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                //���������� ��������� �����
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
    /// ���������� ������� ������� ������ (�������� �� ������� y)
    /// </summary>
    /// <returns></returns>
    public float GetCurrentAltitude()
    {
        return RoundingUpValueForHUD(transform.position.y);
    }

    /// <summary>
    /// ����� �������� ������������ �������� �������� ������ ���������
    /// </summary>
    /// <returns></returns>
    public float GetLeftEnginePower()
    {
        return RoundingUpValueForHUD(currentPowerLeftEngine.y * curretnRatioPower);
    }

    /// <summary>
    /// ����� �������� ������������ �������� �������� ������� ���������
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
    /// ���������� ������� ����������� � ����������� �� ������
    /// </summary>
    /// <returns></returns>
    private float GetCurrentRatioPower()
    {
        //����������� ���������, � ������� ��������� ��������� ������������ ��������
        float rangeChangedRatioPower = MaxAltitude - AltitudeDecreasePowerEngine;
        //����������� �������� ����� ������� ������� � ������������ �������
        float currentDifferenceInDecreaseRatio = MaxAltitude - GetCurrentAltitude();
        
        //���� �������� ����� ������������ ������� � ������� ����� 0, ������� 0, ����� ���������� ������������ ��������
        if(currentDifferenceInDecreaseRatio <= 0)
        {
            return 0;
        }
        else
        {
            //���������� ������������ ��������
            return  currentDifferenceInDecreaseRatio / rangeChangedRatioPower;
        }
    }

    /// <summary>
    /// ���������� �������� �������� ��� ������� �� ������
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float RoundingUpValueForHUD(float value)
    {
        double result = Math.Round(value, 1); 
        return (float)result;
    }

    /// <summary>
    /// ���������� ������� �������� �� ��� � 
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
            //������������ �����
            //UIManager.GetComponent<SceneLoader>().RestartScene();
            GetComponent<UFODestructionBody>().DestoyBody();
            GetComponent<UFOActivationExploisionEffect>().ActivationExploision();
            
        }

        if (collision.gameObject.tag == "Finish")
        {
            //��������� ������ ��� �������� ������� ����
            UIManager.GetComponent<UIManager>().SetActiveMenuAfterPassedLvl();
            //��������� �������� ���� � 0
            //WorldBulder.SetSpeedGame(0f);
            WorldBulder.GetComponent<WorldBulder>().SetSpeedGame(0f);
        }
    }
}
