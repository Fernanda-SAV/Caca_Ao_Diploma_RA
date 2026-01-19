using System.Collections;
using UnityEngine;

public class CollectEffect : MonoBehaviour
{
    [Header("Referencie o objeto Glow (filho)")]
    public GameObject glow;

    [Header("Duração do brilho")]
    public float duration = 0.5f;

    public void Play()
    {
        if (glow == null) return;
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        glow.SetActive(true);
        yield return new WaitForSeconds(duration);
        glow.SetActive(false);
    }
}
