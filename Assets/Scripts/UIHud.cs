using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHud : MonoBehaviour
{
    [Header("Barra de Progresso")]
    public Slider progressSlider;

    [Header("Texto de Progresso")]
    public TMP_Text progressText;

    [Header("Avisos (Toast)")]
    public GameObject toastPanel;
    public TMP_Text toastText;
    public float toastDuration = 1.6f;

    Coroutine toastRoutine;

    public void SetProgress(float value01)
    {
        if (progressSlider != null)
            progressSlider.value = Mathf.Clamp01(value01);
    }

    public void SetDisciplinas(int atual, int total)
    {
        if (progressText != null)
            progressText.text = $"Disciplinas: {atual}/{total}";
    }

    public void ShowToast(string msg)
    {
        if (toastPanel == null || toastText == null) return;

        toastText.text = msg;
        toastPanel.SetActive(true);

        if (toastRoutine != null) StopCoroutine(toastRoutine);
        toastRoutine = StartCoroutine(HideToastAfter());
    }

    IEnumerator HideToastAfter()
    {
        yield return new WaitForSeconds(toastDuration);
        toastPanel.SetActive(false);
    }
}
