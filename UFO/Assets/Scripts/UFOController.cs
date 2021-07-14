using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UFOController : MonoBehaviour
{
    public Rigidbody LeftLegRB,
                     RightLegRB;
    private SceneLoader SceneLoader;
    public UIManager UIManager;
    public WorldBulder WorldBulder;

    public float rotationMultipler = 0.8f;  //���������� ���������� ���� ��������� ��� ��������

    public float Power = 12f;         //���� �����������

    private int currentIndexScene;     //������ ������� �����

    void Start()
    {
        SceneLoader = FindObjectOfType<SceneLoader>();
        UIManager = FindObjectOfType<UIManager>();
        WorldBulder = FindObjectOfType<WorldBulder>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneLoader.GetComponent<SceneLoader>().RestartScene();
        }

        if (Input.GetKey(KeyCode.A))
        {
            //���������� �������� ������ ����� - 80%, ������� - 100%
            LeftLegRB.AddRelativeForce(Vector3.up * Power * rotationMultipler);
            RightLegRB.AddRelativeForce(Vector3.up * Power);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //���������� �������� ������ ����� - 100%, ������� - 80%
            LeftLegRB.AddRelativeForce(Vector3.up * Power);
            RightLegRB.AddRelativeForce(Vector3.up * Power * rotationMultipler);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            //���������� ��������� ����� ��� ������ ���������
            LeftLegRB.AddRelativeForce(Vector3.up * Power);
            //���������� ��������� ����� ��� ������� ���������
            RightLegRB.AddRelativeForce(Vector3.up * Power);
        }
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
