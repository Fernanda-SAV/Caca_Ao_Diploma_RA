using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Config")]
    public int totalDisciplinas = 10;

    [Header("Estado")]
    public int disciplinas = 0;
    public HashSet<string> professores = new HashSet<string>();

    [Header("UI")]
    public UIHud hud;

    const string KEY_DISC = "DISCIPLINAS";
    const string KEY_PROFS = "PROFESSORES"; // string separada por |

    void Awake()
    {
        if (I == null) I = this;
        else { Destroy(gameObject); return; }

        Load();
        RefreshUI();
    }

    public void EncontrouTesouro()
    {
        disciplinas = Mathf.Min(totalDisciplinas, disciplinas + 1);
        hud?.ShowToast("✅ Tesouro encontrado! +1 disciplina");
        RefreshUI();
        Save();
        ChecarVitoria();
    }

    public void EncontrouVilao(bool reprovacao)
    {
        int perda = reprovacao ? 2 : 1;
        disciplinas = Mathf.Max(0, disciplinas - perda);

        hud?.ShowToast(reprovacao
            ? "❌ Reprovação encontrada! -2 disciplinas"
            : "⚠️ Nota baixa encontrada! -1 disciplina");

        RefreshUI();
        Save();
    }

    public void EncontrouProfessor(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) id = "Professor";

        bool novo = professores.Add(id);
        hud?.ShowToast(novo
            ? $"⭐ Card de Professor encontrado: {id}!"
            : $"⭐ Você já tinha o card: {id}");

        Save();
    }

    void RefreshUI()
    {
        float p = (totalDisciplinas <= 0) ? 0f : (float)disciplinas / totalDisciplinas;
        hud?.SetProgress(p);
        hud?.SetDisciplinas(disciplinas, totalDisciplinas);
    }


    void ChecarVitoria()
    {
        if (disciplinas >= totalDisciplinas)
        {
            hud?.ShowToast("🎓 Parabéns! Diploma conquistado!");
            // Depois você pode trocar de tela aqui
        }
    }

    void Save()
    {
        PlayerPrefs.SetInt(KEY_DISC, disciplinas);

        // salva professores como string: "Professor_1|Professor_2"
        string s = string.Join("|", professores);
        PlayerPrefs.SetString(KEY_PROFS, s);

        PlayerPrefs.Save();
    }

    void Load()
    {
        disciplinas = PlayerPrefs.GetInt(KEY_DISC, 0);

        professores.Clear();
        string s = PlayerPrefs.GetString(KEY_PROFS, "");
        if (!string.IsNullOrEmpty(s))
        {
            var parts = s.Split('|');
            foreach (var p in parts)
                if (!string.IsNullOrWhiteSpace(p))
                    professores.Add(p);
        }
    }
}
