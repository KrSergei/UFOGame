using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    /// <summary>
    /// ������������ ������� �����
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// �������� ����� �� ���������� �������
    /// </summary>
    /// <param name="buildIndex">������ ����������� �����</param>
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
