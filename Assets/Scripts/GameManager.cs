using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Config")]
    public int totalDisciplinas = 10;
    public int totalProfessores = 10;

    [Header("Estado")]
    public int disciplinas = 0;
    public HashSet<string> professores = new HashSet<string>();

    [Header("UI")]
    public UIHud hud;

    const string KEY_DISC = "DISCIPLINAS";
    const string KEY_PROFS = "PROFESSORES"; // string separada por |

    bool vitoriaJaMostrada = false;

    void Awake()
    {
        if (I == null) I = this;
        else { Destroy(gameObject); return; }

        Load();
        RefreshUI();

        // Se já estava completo ao abrir (ex.: voltou do menu), mostra fim
        if (DiplomaConquistado())
            MostrarFimDeJogo();
    }

    

    public bool DiplomaConquistado()
    {
        return disciplinas >= totalDisciplinas;
    }

    public bool TodosProfessoresColetados()
    {
        return professores.Count >= totalProfessores;
    }

    public bool ProfessorJaColetado(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return false;
        return professores.Contains(id);
    }

    public int TotalProfessoresColetados()
    {
        return professores.Count;
    }

  

    public void EncontrouTesouro()
    {
        if (DiplomaConquistado()) return; // já terminou

        disciplinas = Mathf.Min(totalDisciplinas, disciplinas + 1);
        hud?.ShowToast("Tesouro encontrado! +1 disciplina");

        RefreshUI();
        Save();
        ChecarVitoria();
    }

    public void EncontrouVilao(bool reprovacao)
    {
        if (DiplomaConquistado()) return; // já terminou

        int perda = reprovacao ? 2 : 1;
        disciplinas = Mathf.Max(0, disciplinas - perda);

        hud?.ShowToast(reprovacao
            ? "Reprovação encontrada! -2 disciplinas"
            : "Nota baixa encontrada! -1 disciplina");

        RefreshUI();
        Save();
    }

    public void EncontrouProfessor(string id)
    {
        if (TodosProfessoresColetados())
        {
            hud?.ShowToast("Você já coletou todos os professores!");
            return;
        }

        if (string.IsNullOrWhiteSpace(id)) id = "Professor";

        bool novo = professores.Add(id);
        hud?.ShowToast(novo
            ? $"Card de Professor encontrado: {id}!"
            : $"Você já tinha o card: {id}");

        Save();

        if (TodosProfessoresColetados())
            hud?.ShowToast("Você coletou TODOS os Professores!");
    }

    // ======= UI =======

    void RefreshUI()
    {
        float p = (totalDisciplinas <= 0) ? 0f : (float)disciplinas / totalDisciplinas;
        hud?.SetProgress(p);
        hud?.SetDisciplinas(disciplinas, totalDisciplinas);
    }

    void ChecarVitoria()
    {
        if (DiplomaConquistado())
            MostrarFimDeJogo();
    }

    void MostrarFimDeJogo()
    {
        if (vitoriaJaMostrada) return;
        vitoriaJaMostrada = true;

        hud?.ShowEndScreen("Parabéns! Diploma conquistado!\n\nVocê completou 10/10 disciplinas.");
    }

    

    public void RestartGame()
    {
        // limpa PlayerPrefs do jogo
        PlayerPrefs.DeleteKey(KEY_DISC);
        PlayerPrefs.DeleteKey(KEY_PROFS);
        PlayerPrefs.Save();

        // recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

  

    void Save()
    {
        PlayerPrefs.SetInt(KEY_DISC, disciplinas);

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
