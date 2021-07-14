using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBulder : MonoBehaviour
{
    [SerializeField]
    private float speedGame = 0f;       //�������� ����. ����� ������� ���� ����� 0;

    //��������� �������� ������� ����� ������� ���� � 0;
    private void Awake()
    {
        Time.timeScale = speedGame;
    }

    //��������� �������� ���� � ����������� �� ����������� ���������
    public void SetSpeedGame(float speed)
    {
        Time.timeScale = speed;
    }
}
