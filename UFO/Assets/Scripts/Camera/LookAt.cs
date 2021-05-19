using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    #region 1 �������
    [Header("Paarametrs")]
    [SerializeField]
    private Transform playerTransform;   //��������� ������
    private Vector3 ofset;

    private void Awake()
    {
        //��������� ��������
        ofset = transform.position - playerTransform.position;
    }

    void Update()
    {
        //����������� ������ ����� �� �������
        transform.position = playerTransform.position + ofset;
    }
    #endregion

    #region 2 �������

    //public float dumping = 1.5f;                 //���������� ������ ��� ���������
    //public Vector2 offset = new Vector2(5f, 5f); //�������� ������ ������������ ���������
    //public bool isLeft;                          //��������� �� �������� �����
    //public Transform playerTransform;            //��������� ���������, �� ������� ������� ������
    //private int lastX;                           //��������� �������� ���������� X, ��� ����������� ������� ������

    //private void Start()
    //{
    //    //���������� �������� ������(������������ ������ ������ � ������)
    //    offset = new Vector2(Mathf.Abs(offset.x), offset.y); 
    //}

    //void Update()
    //{
    //    if (playerTransform)
    //    {
    //        int currentX = Mathf.RoundToInt(playerTransform.position.x);
    //        //����������� ����������� ������, ���� ������� �������, ������ ��� ���������, ����� ��������� ������.����� �����
    //        if (currentX > lastX)
    //            isLeft = false;
    //        else if (currentX < lastX)
    //            isLeft = true;

    //        //����������� ��������� ������� ������
    //        lastX = Mathf.RoundToInt(playerTransform.position.x);
    //    }

    //    //����������� �������, ���� ������ ��������� ������
    //    Vector3 target;
    //    if (isLeft)
    //    {
    //        target = new Vector3(playerTransform.position.x - offset.x, playerTransform.position.y + offset.y, transform.position.z);
    //    }
    //    else
    //    {
    //        target = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y, transform.position.z);
    //    }

    //    //������� ������� �������� ������
    //    transform.position = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime); 
    //}

    ///// <summary>
    ///// ����� �������� � ����� ������� ��������� ��������
    ///// </summary>
    ///// <param name="playerisLeft"></param>
    //public void GetPosition(bool playerisLeft)
    //{
    //    //��������� ������� ������ �� ��� x
    //    lastX = Mathf.RoundToInt(playerTransform.position.x);

    //    //����������� ����������� ������.���� �����, �� ����������� ������ �����, ����� ������ ����������� ������
    //    if (playerisLeft)
    //    {
    //        transform.position = new Vector3(playerTransform.position.x - offset.x, playerTransform.position.y + offset.y, transform.position.z);
    //    }
    //    else
    //    {
    //        transform.position = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y, transform.position.z);
    //    }
    //}
    #endregion
}
