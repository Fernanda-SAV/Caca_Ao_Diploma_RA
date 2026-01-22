using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ProfessorCardData
{
    public string idProfessor;      // deve bater com ARItem.idProfessor salvo no GameManager
    public Sprite sprite;           // imagem do card (UI Sprite)
    public string displayName;      // nome bonito pra mostrar
}

public class AlbumUI : MonoBehaviour
{
    [Header("Tela do Álbum")]
    public GameObject albumPanel;

    [Header("Grid e Template")]
    public Transform gridParent;            // AlbumGrid
    public GameObject cardSlotTemplate;     // CardSlot_Template

    [Header("Dados dos Professores (10 itens)")]
    public List<ProfessorCardData> professores = new List<ProfessorCardData>();

    [Header("Contador")]
    public TMP_Text albumCounterText;
    public int totalProfessores = 10;


    // cache dos slots criados
    private readonly List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        // cria os slots na primeira vez
        BuildGrid();
        Refresh();
        if (albumPanel != null) albumPanel.SetActive(false);
    }

    public void Open()
    {
        // Como o AlbumUI está no próprio AlbumPanel,
        // usar "gameObject" garante que abre sempre.
        gameObject.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }



    void BuildGrid()
    {
        if (gridParent == null || cardSlotTemplate == null) return;

        // Remove template da visualização (ele vira prefab "molde")
        cardSlotTemplate.SetActive(false);

        // cria um slot para cada professor definido na lista
        for (int i = 0; i < professores.Count; i++)
        {
            GameObject slot = Instantiate(cardSlotTemplate, gridParent);
            slot.name = $"CardSlot_{i + 1}";
            slot.SetActive(true);
            slots.Add(slot);
        }
    }

    public void Refresh()
    {
        if (GameManager.I == null) return;

        // garante que slots e lista batem
        int n = Mathf.Min(slots.Count, professores.Count);

        for (int i = 0; i < n; i++)
        {
            var data = professores[i];
            var slot = slots[i];

            // pega refs
            var img = slot.transform.Find("CardImage")?.GetComponent<Image>();
            var nameText = slot.transform.Find("CardName")?.GetComponent<TMP_Text>();
            var locked = slot.transform.Find("LockedOverlay")?.gameObject;

            bool coletado = GameManager.I.ProfessorJaColetado(data.idProfessor);

            if (nameText != null)
                nameText.text = data.displayName;

            if (img != null)
                img.sprite = data.sprite;

            // se não coletou, mostra overlay bloqueado
            if (locked != null)
                locked.SetActive(!coletado);

            //deixar a imagem mais escura quando bloqueado
            if (img != null)
                img.color = coletado ? Color.white : new Color(1f, 1f, 1f, 0.35f);

            //atualiza contador x/10
            if (albumCounterText != null && GameManager.I != null)
            {
                int coletados = GameManager.I.TotalProfessoresColetados();
                albumCounterText.text = $"Professores coletados: {coletados} / {totalProfessores}";
            }

        }
    }
}
