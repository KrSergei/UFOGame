using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFODestructionBody : MonoBehaviour
{
    public GameObject[] separableComponenOfBody;  //Компоненты, отделяемые от основного тела игрока

    [Header("Size collider destroyed"), SerializeField]
    private Vector3 sizeColliderWhenDestroed;   //Размер коллайдера при столкновении с

    public void DestoyBody()
    {
        StartCoroutine(SeparateBody());
    }


    IEnumerator SeparateBody()
    {
        //Уменьшение размеров коллайдера
        GetComponent<BoxCollider>().size = sizeColliderWhenDestroed;
        //Получение текущей скорости объекта
        Vector3 currentVelocity = gameObject.GetComponent<Rigidbody>().velocity;

        //Имитация разрушения объекта
        for (int i = 0; i < separableComponenOfBody.Length; i++)
        {
            //Отключение FixedJoint
            if (separableComponenOfBody[i].GetComponent<FixedJoint>() != null)
                separableComponenOfBody[i].GetComponent<FixedJoint>().breakForce = 0f;
            //Включение коллайдеров
            if (separableComponenOfBody[i].GetComponent<MeshCollider>() != null)
                separableComponenOfBody[i].GetComponent<MeshCollider>().isTrigger = false;

            //Задание скорости
            separableComponenOfBody[i].GetComponent<Rigidbody>().velocity = currentVelocity;

            yield return null;
        }
    }
}
