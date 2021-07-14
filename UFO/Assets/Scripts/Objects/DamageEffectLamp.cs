using System.Collections;
using UnityEngine;

public class DamageEffectLamp : MonoBehaviour
{
    public GameObject lamp; //�����, � ������� ���������� ��������� ParticleSystem

    public ParticleSystem damageEffect; //������� ������, ������������ ��� ������������ � ����������� ������
    public ParticleSystem sparks; //������� ������ ��� �������� ������� ��������
    public Light flashEffect; //�������� �����, ������������ ��� ������������ � ����������� ������  

    [SerializeField]
    private float pauseOffLight = 0.1f;    //����� ����� ����������� ��������� ����� 
    [SerializeField]
    private int intensityForFlash = 10;    //������������� ����� ��� �������
    [SerializeField]
    private int pauseBetweenSparking = 1;   //����� ����� ��������� ��������

    void Start()
    {
        lamp.GetComponent<GameObject>();
    }

    /// <summary>
    /// ����������� �� ������������ � �������� �����
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //������ �������� ��� �������� ������� ������������
            StartCoroutine(DoDamageEffect());
        }
    }

    /// <summary>
    /// ������ �������� ��� �������� ������� ������������ � ������ � �� �����������
    /// </summary>
    /// <returns></returns>
    IEnumerator DoDamageEffect()
    {
        //lamp.GetComponent<ParticleSystem>();
        //��������� ������������� ����� ��� �������
        flashEffect.intensity = intensityForFlash;
        //��������� ������� ������ ��� ������� ����
        damageEffect.Play();
        //����� ����� ���������� ������� �����
        yield return new WaitForSeconds(pauseOffLight);
        //���������� �������
        lamp.gameObject.SetActive(false);
        //������ �������� ��� ������� ��������
        StartCoroutine(DoStarks());
        yield return null;
    }

    /// <summary>
    /// ������ ������� ������ ��� �������� ������� ��������
    /// </summary>
    /// <returns></returns>
    IEnumerator DoStarks()
    {
        //������ ������� ������ ��� ������� ��������
        sparks.Play();
        //��������� �������� ����� ��� ���������� ������� ������� ������ ������� ��������
        yield return new WaitForSeconds(Random.Range(0, pauseBetweenSparking));
        //��������� ������ ������� ��������
        StartCoroutine(DoStarks());
    }
}
