using System.Collections;
using UnityEngine;

public class UFODestructionBody : MonoBehaviour
{
    public GameObject[] separableComponenOfBody;  //Компоненты, отделяемые от основного тела игрока

    [Header("Size collider destroyed"), SerializeField]
    private Vector3 sizeColliderWhenDestroed;   //Размер коллайдера при столкновении с

    [SerializeField]
    private float explosionForce; //Сила взрыва
    [SerializeField]
    private float explosionRadius; //Радиус взрыва

    public void DestoyBody()
    {
        StartCoroutine(SeparateBody());
    }

    IEnumerator SeparateBody()
    {
        //Уменьшение размеров коллайдера
        GetComponent<BoxCollider>().size = sizeColliderWhenDestroed;

        //Имитация разрушения объекта
        for (int i = 0; i < separableComponenOfBody.Length; i++)
        {
            //Отключение FixedJoint
            if (separableComponenOfBody[i].GetComponent<FixedJoint>() != null)
                separableComponenOfBody[i].GetComponent<FixedJoint>().breakForce = 0f;
            //Включение коллайдеров
            if (separableComponenOfBody[i].GetComponent<Collider>() != null)
                separableComponenOfBody[i].GetComponent<Collider>().isTrigger = false;
            yield return null;
        }

        //Получение компонента Rigidbody и придание ему силы взрыва
        for (int i = 0; i < separableComponenOfBody.Length; i++)
        {
            Rigidbody rb = separableComponenOfBody[i].GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            yield return null;
        }
    }
}
