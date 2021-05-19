using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    #region 1 вариант
    [Header("Paarametrs")]
    [SerializeField]
    private Transform playerTransform;   //трансформ игрока
    private Vector3 ofset;

    private void Awake()
    {
        //Получение значения
        ofset = transform.position - playerTransform.position;
    }

    void Update()
    {
        //Перемещение камеры вслед за игроком
        transform.position = playerTransform.position + ofset;
    }
    #endregion

    #region 2 вариант

    //public float dumping = 1.5f;                 //Сглаживани камеры при остановке
    //public Vector2 offset = new Vector2(5f, 5f); //Смещение камеры относительно персонажа
    //public bool isLeft;                          //Смещается ли персонаж влево
    //public Transform playerTransform;            //Трансформ персонажа, за которым следует камера
    //private int lastX;                           //Последнее значение координаты X, для определения позиции камеры

    //private void Start()
    //{
    //    //вычисление смещения камеры(расположение камеры справо и сверху)
    //    offset = new Vector2(Mathf.Abs(offset.x), offset.y); 
    //}

    //void Update()
    //{
    //    if (playerTransform)
    //    {
    //        int currentX = Mathf.RoundToInt(playerTransform.position.x);
    //        //Определение направление игрока, если текущая позиция, больше чем последняя, игрок движеться вправо.иначе влево
    //        if (currentX > lastX)
    //            isLeft = false;
    //        else if (currentX < lastX)
    //            isLeft = true;

    //        //Определение последней позиции игрока
    //        lastX = Mathf.RoundToInt(playerTransform.position.x);
    //    }

    //    //Определение позиции, куда должна двигаться камера
    //    Vector3 target;
    //    if (isLeft)
    //    {
    //        target = new Vector3(playerTransform.position.x - offset.x, playerTransform.position.y + offset.y, transform.position.z);
    //    }
    //    else
    //    {
    //        target = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y, transform.position.z);
    //    }

    //    //задание вектора движения камеры
    //    transform.position = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime); 
    //}

    ///// <summary>
    ///// метод проверки в какую сторону движеться персонаж
    ///// </summary>
    ///// <param name="playerisLeft"></param>
    //public void GetPosition(bool playerisLeft)
    //{
    //    //получение позиции игрока по оси x
    //    lastX = Mathf.RoundToInt(playerTransform.position.x);

    //    //определение направления игрока.если влево, то пеермещение камеры влево, иначе камера расположено справо
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
