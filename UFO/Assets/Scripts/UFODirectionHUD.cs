using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFODirectionHUD : MonoBehaviour
{
    public float speedRotationArrow = 5f;                    //Скорость поворота стрелки

    private UFOController uFOController;
    [SerializeField] private Transform arrowDirection;
    Quaternion quaternionArrow;                         //Стартовый поворот стелки

    private void Start()
    {
        uFOController = GetComponent<UFOController>();
        quaternionArrow = arrowDirection.rotation;
    }

    private void Update()
    {
        RotationArrowDependingOnVelosity();
    }

    private void RotationArrowDependingOnVelosity()
    {
        //Определение поворота по оси Z в зависимости от скорости
        Quaternion rotationZ = Quaternion.AngleAxis(uFOController.GetVelosityX() * speedRotationArrow, -Vector3.forward);
        //Задание угла поворота по оси Z
        arrowDirection.rotation = quaternionArrow * rotationZ;

        if (uFOController.GetVelosityX() == 0f)
            arrowDirection.rotation = quaternionArrow;
    }

}
