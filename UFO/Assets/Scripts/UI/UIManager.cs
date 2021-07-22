using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /* Сокращение в начале имени методов
     * BA - button activity - реакция на нажатие кнопки
     */

    /// <summary>
    /// Массив UI
    /// canvas[0] - Start menu
    /// canvas[1] - Next level menu
    /// canvas[2] - Congratulation menu
    /// canvas[3] - Playing menu
    /// </summary>
    public Canvas[] canvas;

    public GameObject WorldBulder;          //WorldBulder

    private int currentIndexScene;          //Индекс текущей сцены

    private void Awake()
    {
        //Активация игрового меню
        canvas[0].gameObject.SetActive(true);
        //Деактивация всех остальных меню
        for (int i = 1; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //Получение индекса текущей сцены
        currentIndexScene = SceneManager.GetActiveScene().buildIndex;
    }


    /// <summary>
    /// реакция на нажатие кнопки старт
    /// </summary>
    public void BAStart()
    {
        //Установка timeScale = 1
        WorldBulder.GetComponent<WorldBulder>().SetSpeedGame(1f);
        //Отключение меню старт
        ChangeCurrentUIActivity(canvas[0].gameObject);
        ChangeCurrentUIActivity(canvas[3].gameObject);
    }

    #region Next level menu

    /// <summary>
    /// Определение, какое меню нужно активировать после прохождения уровня.
    /// Если был пройден не последний уровень, то активация Next level menu,
    /// иначе активация Congratulation menu
    /// </summary>
    public void SetActiveMenuAfterPassedLvl()
    {
        //проверка, какое меню нужно активировать после прохождения уровня
        GameObject activationMenu = (CheckPossibleActivationMenu(currentIndexScene))
            ? activationMenu = canvas[1].gameObject             
            : activationMenu = canvas[2].gameObject;
        //Вызво метода для активации нужного меню
        ChangeCurrentUIActivity(activationMenu);
    }

    /// <summary>
    /// Загрузка следующей сцены
    /// </summary>
    public void BANextLevel()
    {
        SceneManager.LoadScene(currentIndexScene + 1);
    }
    #endregion


    /// <summary>
    /// Проверка возможности активации  Next level menu
    /// </summary>
    /// <param name="indexCurrentScene">индекс текущей сцены</param>
    /// <returns></returns>
    private bool CheckPossibleActivationMenu(int indexCurrentScene)
    {
        //Проверка на то, что индекс текущей сцены + 1 меньше количества сцен, если да, то возврат true, иначе false
        bool result = (indexCurrentScene + 1 < SceneManager.sceneCountInBuildSettings) ? true : false;
        return result;
    }

    /// <summary>
    /// Изменения текущей активности переданного gameObject на противоположное от текущего значения state
    /// </summary>
    /// <param name="gameObject">принимаемый объект</param>
    /// <param name="state">состояние на которое необходимо изменить активность объекта</param>
    private void ChangeCurrentUIActivity(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
