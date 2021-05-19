using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UFOController : MonoBehaviour
{
    public Rigidbody LeftLegRB,
                     RightLegRB;
    public SceneLoader SceneLoader;
    public UIManager UIManager;
    public WorldBulder WorldBulder;

    public float rotationMultipler = 0.8f;  //Показатель уменьшения силы двигателя при повороте

    public float Power = 12f;         //Сила воздействия

    private int currentIndexScene;     //Индекс текущей сцены

    void Start()
    {
        SceneLoader = FindObjectOfType<SceneLoader>();
        UIManager = FindObjectOfType<UIManager>();
        WorldBulder = FindObjectOfType<WorldBulder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneLoader.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //Добавление мощности левого сопла - 80%, правого - 100%
            LeftLegRB.AddRelativeForce(Vector3.up * Power * rotationMultipler);
            RightLegRB.AddRelativeForce(Vector3.up * Power);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Добавление мощности левого сопла - 100%, правого - 80%
            LeftLegRB.AddRelativeForce(Vector3.up * Power);
            RightLegRB.AddRelativeForce(Vector3.up * Power * rotationMultipler);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            //Применение ускорения вверх для левого двигателя
            LeftLegRB.AddRelativeForce(Vector3.up * Power);
            //Применение ускорения вверх для правого двигателя
            RightLegRB.AddRelativeForce(Vector3.up * Power);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //Применение ускорения вверх для левого двигателя
            LeftLegRB.AddRelativeForce(Vector3.forward * Power);
            //Применение ускорения вверх для правого двигателя
            RightLegRB.AddRelativeForce(Vector3.forward * Power);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //Применение ускорения вперед
            LeftLegRB.AddRelativeForce(- Vector3.forward * Power);
            //Применение ускорения назад
            RightLegRB.AddRelativeForce( - Vector3.forward * Power);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Перезагрузка сцены
            //UIManager.GetComponent<SceneLoader>().RestartScene();
            SceneLoader.RestartScene();
        }

        if (collision.gameObject.tag == "Finish")
        {
            //Активация метода для загрузки нужного меню
            UIManager.SetActiveMenuAfterPassedLvl();
            //UIManager.GetComponent<UIManager>().SetActiveMenuAfterPassedLvl();
            //Установка скорости игры в 0
            WorldBulder.SetSpeedGame(0f);
            //WorldBulder.GetComponent<WorldBulder>().SetSpeedGame(0f);
        }
    }
}
