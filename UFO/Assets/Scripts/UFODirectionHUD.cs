using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFODirectionHUD : MonoBehaviour
{
    public float speedRotationArrow = 5f;                    //�������� �������� �������

    private UFOController uFOController;
    [SerializeField] private Transform arrowDirection;
    Quaternion quaternionArrow;                         //��������� ������� ������

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
        //����������� �������� �� ��� Z � ����������� �� ��������
        Quaternion rotationZ = Quaternion.AngleAxis(uFOController.GetVelosityX() * speedRotationArrow, -Vector3.forward);
        //������� ���� �������� �� ��� Z
        arrowDirection.rotation = quaternionArrow * rotationZ;

        if (uFOController.GetVelosityX() == 0f)
            arrowDirection.rotation = quaternionArrow;
    }

}
