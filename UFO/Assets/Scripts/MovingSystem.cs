using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSystem : MonoBehaviour
{
    public Transform[] movementSpots; //Массив точек передвижения
    public Transform movementObj; //Трансформ движущегося объекта
    public float speed; //Скорость пеердвижения
    public bool isCicle = false; //Флаг, что объект движется в обратную сторону по точкам


    private Transform targetSpot; //Целевая точка, к которой движется объект
    private int currentSpot; //Текущая точка
    private float minDistanceForChangeTargetSpot = .2f; //Минимальная дистанция, на которой происходит смена точки передвижения
    private bool forward; //Направление движения


    void Start()
    {
        forward = true;
        currentSpot = 0; //Установка текущей точки передвижения 
        targetSpot = movementSpots[currentSpot]; //Установка целейвой точки передвижения
    }

   
    void Update()
    {
        //Проверка на расстояние между объектом и целевой точкой, если оно меньше, чем минимальное расстояние, то переключение точки передвижения
        if(Vector3.Distance(movementObj.position , targetSpot.position) <= minDistanceForChangeTargetSpot)
        {
            if (forward)
                currentSpot++;
            else
                currentSpot--;
            
            //Проверка за выход предела массива точек и состояние флага цикличности
            if(currentSpot >= movementSpots.Length && isCicle) currentSpot = 0;
            else if(currentSpot >= movementSpots.Length && !isCicle)
            {
                forward = false;
                currentSpot = movementSpots.Length - 2;
            } else if (currentSpot < 0)
            {
                forward = true;
                currentSpot = 1;
            }

            targetSpot = movementSpots[currentSpot];
        }


        movementObj.position = Vector3.MoveTowards(movementObj.position, targetSpot.position, speed * Time.deltaTime);
    }
}
