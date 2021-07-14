using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    public GameObject gameObjectLight;     //������, � �������� ����������� �������� �����

    private Light blinkingLight;    //�������� �����

    public float intesiveLight;     //������� �������� ������������� �����

    void Start()
    {
        //��������� ���������� light
        blinkingLight = gameObjectLight.GetComponentInChildren<Light>();
        //��������� �������� �������� ������������� ��������� �����
        intesiveLight = blinkingLight.intensity;
        
        #region ������� ����� ��������� ������� ������
        //��������� �������� ������� 1
        Time.timeScale = 1f;
        //������ �������� �� ��������� ��������� �����
        StartCoroutine(DoBlink());
        #endregion
    }

    public void StartBlinkingLight()
    {
        StartCoroutine(DoBlink());
    }

    IEnumerator DoBlink()
    {
        //����� ����� ������������ ��������� �����
        yield return new WaitForSeconds(Random.value);
        //����������� ��������� �����
        //blinkingLight.enabled = false;
        gameObjectLight.SetActive(false);

        //����� ����� ���������� ��������� �����
        yield return new WaitForSeconds(Random.value);
        //��������� ��������� �����
        gameObjectLight.SetActive(true);
        //blinkingLight.enabled = true;
        //��������� ������������� ����� � ����������� �� ���������� ��������
        blinkingLight.intensity = intesiveLight * Random.value;
        //���������� ��������
        StartCoroutine(DoBlink());
    }
}
