using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    public bool isFlikering = false;

    public float timeDelay;

    private void Start()
    {
        Time.timeScale = 1f;
    }


    void Update()
    {
        if (isFlikering == false)
            StartCoroutine(FlikeringLight());
    }

    IEnumerator FlikeringLight()
    {
        isFlikering = true;

        gameObject.GetComponent<Light>().enabled = false;

        timeDelay = Random.value;

        yield return new WaitForSeconds(timeDelay);

        gameObject.GetComponent<Light>().enabled = true;

        timeDelay = Random.value;

        yield return new WaitForSeconds(timeDelay);

        isFlikering = false;
    }
}
