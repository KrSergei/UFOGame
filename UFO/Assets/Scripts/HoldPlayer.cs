using UnityEngine;

public class HoldPlayer : MonoBehaviour
{
    public GameObject Player;   //Объкт Player

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == Player)
        {
            Player.transform.parent = transform;  //Сделать дочерним объект, который попадает в коллайдер
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == Player)
        {
            Player.transform.parent = null;      //Исключить объект из дочернего, который покидает коллайдер
        }
    }
}
