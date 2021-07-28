using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFODestructionBody : MonoBehaviour
{
    public GameObject[] separableComponenOfBody;  //����������, ���������� �� ��������� ���� ������

    [Header("Size collider destroyed"), SerializeField]
    private Vector3 sizeColliderWhenDestroed;   //������ ���������� ��� ������������ �

    public void DestoyBody()
    {
        StartCoroutine(SeparateBody());
    }


    IEnumerator SeparateBody()
    {
        //���������� �������� ����������
        GetComponent<BoxCollider>().size = sizeColliderWhenDestroed;
        //��������� ������� �������� �������
        Vector3 currentVelocity = gameObject.GetComponent<Rigidbody>().velocity;

        //�������� ���������� �������
        for (int i = 0; i < separableComponenOfBody.Length; i++)
        {
            //���������� FixedJoint
            if (separableComponenOfBody[i].GetComponent<FixedJoint>() != null)
                separableComponenOfBody[i].GetComponent<FixedJoint>().breakForce = 0f;
            //��������� �����������
            if (separableComponenOfBody[i].GetComponent<MeshCollider>() != null)
                separableComponenOfBody[i].GetComponent<MeshCollider>().isTrigger = false;

            //������� ��������
            separableComponenOfBody[i].GetComponent<Rigidbody>().velocity = currentVelocity;

            yield return null;
        }
    }
}
