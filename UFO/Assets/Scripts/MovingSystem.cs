using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSystem : MonoBehaviour
{
    public Transform[] movementSpots; //������ ����� ������������
    public Transform movementObj; //��������� ����������� �������
    public float speed; //�������� ������������
    public bool isCicle = false; //����, ��� ������ �������� � �������� ������� �� ������


    private Transform targetSpot; //������� �����, � ������� �������� ������
    private int currentSpot; //������� �����
    private float minDistanceForChangeTargetSpot = .2f; //����������� ���������, �� ������� ���������� ����� ����� ������������
    private bool forward; //����������� ��������


    void Start()
    {
        forward = true;
        currentSpot = 0; //��������� ������� ����� ������������ 
        targetSpot = movementSpots[currentSpot]; //��������� �������� ����� ������������
    }

   
    void Update()
    {
        //�������� �� ���������� ����� �������� � ������� ������, ���� ��� ������, ��� ����������� ����������, �� ������������ ����� ������������
        if(Vector3.Distance(movementObj.position , targetSpot.position) <= minDistanceForChangeTargetSpot)
        {
            if (forward)
                currentSpot++;
            else
                currentSpot--;
            
            //�������� �� ����� ������� ������� ����� � ��������� ����� �����������
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
