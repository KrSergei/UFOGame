using System.Collections;
using UnityEngine;

public class UFODestructionBody : MonoBehaviour
{
    public GameObject[] separableComponenOfBody;  //����������, ���������� �� ��������� ���� ������

    [Header("Size collider destroyed"), SerializeField]
    private Vector3 sizeColliderWhenDestroed;   //������ ���������� ��� ������������ �

    [SerializeField]
    private float explosionForce; //���� ������
    [SerializeField]
    private float explosionRadius; //������ ������

    public void DestoyBody()
    {
        StartCoroutine(SeparateBody());
    }

    IEnumerator SeparateBody()
    {
        //���������� �������� ����������
        GetComponent<BoxCollider>().size = sizeColliderWhenDestroed;

        //�������� ���������� �������
        for (int i = 0; i < separableComponenOfBody.Length; i++)
        {
            //���������� FixedJoint
            if (separableComponenOfBody[i].GetComponent<FixedJoint>() != null)
                separableComponenOfBody[i].GetComponent<FixedJoint>().breakForce = 0f;
            //��������� �����������
            if (separableComponenOfBody[i].GetComponent<Collider>() != null)
                separableComponenOfBody[i].GetComponent<Collider>().isTrigger = false;
            yield return null;
        }

        //��������� ���������� Rigidbody � �������� ��� ���� ������
        for (int i = 0; i < separableComponenOfBody.Length; i++)
        {
            Rigidbody rb = separableComponenOfBody[i].GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            yield return null;
        }
    }
}
