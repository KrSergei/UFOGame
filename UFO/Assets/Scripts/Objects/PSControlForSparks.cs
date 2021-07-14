using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSControlForSparks : MonoBehaviour
{
    public GameObject gameObjectLight;     //Объект, к которому присоединен компонент ParticleSystem

    private ParticleSystem sparkPS;      //PS для искр
    
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
