using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBulder : MonoBehaviour
{
    [SerializeField]
    private float speedGame = 0f;       //скорость игры. Перед началом игры равна 0;

    //Установка масштаба времени перед началом игры в 0;
    private void Awake()
    {
        Time.timeScale = speedGame;
    }

    //Изменение скорости игры в зависимости от переданного параметра
    public void SetSpeedGame(float speed)
    {
        Time.timeScale = speed;
    }
}
