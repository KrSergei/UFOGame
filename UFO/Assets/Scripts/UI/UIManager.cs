using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /* ���������� � ������ ����� �������
     * BA - button activity - ������� �� ������� ������
     */

    /// <summary>
    /// ������ UI
    /// canvas[0] - Start menu
    /// canvas[1] - Next level menu
    /// canvas[2] - Congratulation menu
    /// canvas[3] - Playing menu
    /// </summary>
    public Canvas[] canvas;

    public GameObject WorldBulder;          //WorldBulder

    private int currentIndexScene;          //������ ������� �����

    private void Awake()
    {
        //��������� �������� ����
        canvas[0].gameObject.SetActive(true);
        //����������� ���� ��������� ����
        for (int i = 1; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //��������� ������� ������� �����
        currentIndexScene = SceneManager.GetActiveScene().buildIndex;
    }


    /// <summary>
    /// ������� �� ������� ������ �����
    /// </summary>
    public void BAStart()
    {
        //��������� timeScale = 1
        WorldBulder.GetComponent<WorldBulder>().SetSpeedGame(1f);
        //���������� ���� �����
        ChangeCurrentUIActivity(canvas[0].gameObject);
        ChangeCurrentUIActivity(canvas[3].gameObject);
    }

    #region Next level menu

    /// <summary>
    /// �����������, ����� ���� ����� ������������ ����� ����������� ������.
    /// ���� ��� ������� �� ��������� �������, �� ��������� Next level menu,
    /// ����� ��������� Congratulation menu
    /// </summary>
    public void SetActiveMenuAfterPassedLvl()
    {
        //��������, ����� ���� ����� ������������ ����� ����������� ������
        GameObject activationMenu = (CheckPossibleActivationMenu(currentIndexScene))
            ? activationMenu = canvas[1].gameObject             
            : activationMenu = canvas[2].gameObject;
        //����� ������ ��� ��������� ������� ����
        ChangeCurrentUIActivity(activationMenu);
    }

    /// <summary>
    /// �������� ��������� �����
    /// </summary>
    public void BANextLevel()
    {
        SceneManager.LoadScene(currentIndexScene + 1);
    }
    #endregion


    /// <summary>
    /// �������� ����������� ���������  Next level menu
    /// </summary>
    /// <param name="indexCurrentScene">������ ������� �����</param>
    /// <returns></returns>
    private bool CheckPossibleActivationMenu(int indexCurrentScene)
    {
        //�������� �� ��, ��� ������ ������� ����� + 1 ������ ���������� ����, ���� ��, �� ������� true, ����� false
        bool result = (indexCurrentScene + 1 < SceneManager.sceneCountInBuildSettings) ? true : false;
        return result;
    }

    /// <summary>
    /// ��������� ������� ���������� ����������� gameObject �� ��������������� �� �������� �������� state
    /// </summary>
    /// <param name="gameObject">����������� ������</param>
    /// <param name="state">��������� �� ������� ���������� �������� ���������� �������</param>
    private void ChangeCurrentUIActivity(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
