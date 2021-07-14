using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSControlForSparks : MonoBehaviour
{
    public GameObject gameObjectLight;     //������, � �������� ����������� ��������� ParticleSystem

    private ParticleSystem sparkPS;      //PS ��� ����
    
    void Start()
    {
        sparkPS = gameObjectLight.GetComponentInChildren<ParticleSystem>();

        StartSparking();
    }


    public void StartSparking()
    {
        StartCoroutine(DoSparking());
    }

    IEnumerator DoSparking()
    {
        sparkPS.Play();
        yield return new WaitForSeconds(2f);
        StartCoroutine(DoSparking());
    }
}
