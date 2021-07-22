using UnityEngine;

public class HoldPlayer : MonoBehaviour
{
    public GameObject Player;   //����� Player

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == Player)
        {
            Player.transform.parent = transform;  //������� �������� ������, ������� �������� � ���������
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == Player)
        {
            Player.transform.parent = null;      //��������� ������ �� ���������, ������� �������� ���������
        }
    }
}
