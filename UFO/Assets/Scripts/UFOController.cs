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
    public float rotationMultipler = 0.8f;          //���������� ���������� ���� ��������� ��� ��������       
    public float MaxAltitude;                       //������������ ������, �� ������� ����������� ���������
    public float AltitudeDecreasePowerEngine = 5f;  //������, � ������� ���������� ���������� ������������ �������� ���������

    [SerializeField]
    private float power = 12f;                      //���� �����������
    private float curretnRatioPower = 1f;           //������� ����������� ��������
    private Vector3 currentPowerLeftEngine;         //������� �������� ������ ���������
    private Vector3 currentPowerRightEngine;        //������� �������� ������� ���������
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

        //�������� �� ������, � ���������� ������������ �������� � ����������� �� ������
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

        LeftLegRB.AddRelativeForce(currentPowerLeftEngine * curretnRatioPower);
        RightLegRB.AddRelativeForce(currentPowerRightEngine * curretnRatioPower);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //������������ �����
            UIManager.GetComponent<SceneLoader>().RestartScene();
            //SceneLoader.RestartScene();
        }

        if (collision.gameObject.tag == "Finish")
        {
            //��������� ������ ��� �������� ������� ����
            UIManager.SetActiveMenuAfterPassedLvl();
            //��������� �������� ���� � 0
            //WorldBulder.SetSpeedGame(0f);
            WorldBulder.GetComponent<WorldBulder>().SetSpeedGame(0f);
        }
    }
}
