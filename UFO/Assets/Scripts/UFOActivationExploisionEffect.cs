using UnityEngine;

public class UFOActivationExploisionEffect : MonoBehaviour
{
    public GameObject exploisionEffect;

    public void ActivationExploision()
    {
        exploisionEffect.SetActive(true);
    }
}
