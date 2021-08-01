using UnityEngine;

public class UFOActivationExploisionEffect : MonoBehaviour
{
    public GameObject exploisionEffect;

    public GameObject[] smokeEfect;

    public void ActivationExploision()
    {
        exploisionEffect.SetActive(true);
        ActivationSmokeEffect();
    }

    public void ActivationSmokeEffect()
    {
        for (int i = 0; i < smokeEfect.Length; i++)
        {
            smokeEfect[i].SetActive(true);
        }
    }
}
