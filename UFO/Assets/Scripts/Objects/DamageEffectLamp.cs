using System.Collections;
using UnityEngine;

public class DamageEffectLamp : MonoBehaviour
{
    public GameObject lamp; //Лампа, с которой прикреплен компонент ParticleSystem

    public ParticleSystem damageEffect; //Система частиц, активируемая при столкновении с коллайдером игрока
    public ParticleSystem sparks; //Система частиц для создания эффекта искрения
    public Light flashEffect; //Источник света, активируемый при столкновении с коллайдером игрока  

    [SerializeField]
    private float pauseOffLight = 0.1f;    //Пауза перед выключением источника света 
    [SerializeField]
    private int intensityForFlash = 10;    //Интенсивность света при вспышке
    [SerializeField]
    private int pauseBetweenSparking = 1;   //Пауза между эффектами искрения

    void Start()
    {
        lamp.GetComponent<GameObject>();
    }

    /// <summary>
    /// Определение на столкновение с объектом игрок
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Запуск корутины для создания эффекта столкновения
            StartCoroutine(DoDamageEffect());
        }
    }

    /// <summary>
    /// ЗАпуск корутины для создания эффекта столкновения с лампой и ее уничтожения
    /// </summary>
    /// <returns></returns>
    IEnumerator DoDamageEffect()
    {
        //lamp.GetComponent<ParticleSystem>();
        //Установка интенсивности света при вспышке
        flashEffect.intensity = intensityForFlash;
        //Включение системы частиц для эффекта искр
        damageEffect.Play();
        //Паука перед выключение объекта лампа
        yield return new WaitForSeconds(pauseOffLight);
        //Отключение объекта
        lamp.gameObject.SetActive(false);
        //Запуск корутины для эффекта искрения
        StartCoroutine(DoStarks());
        yield return null;
    }

    /// <summary>
    /// Запуск системы частиц для создания эффекта искрения
    /// </summary>
    /// <returns></returns>
    IEnumerator DoStarks()
    {
        //Запуск системы частиц для эффекта искрения
        sparks.Play();
        //Случайное значение паузы для повторного запуска системы частиц эффекта искрения
        yield return new WaitForSeconds(Random.Range(0, pauseBetweenSparking));
        //Повторный запуск эффекта искрения
        StartCoroutine(DoStarks());
    }
}
