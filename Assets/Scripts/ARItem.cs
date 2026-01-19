using UnityEngine;

public class ARItem : MonoBehaviour
{
    public enum Tipo { Tesouro, VilaoNotaBaixa, VilaoReprovacao, Professor }

    [Header("Defina o tipo deste prefab")]
    public Tipo tipo;

    [Header("Use para professor (ex: Professor_1, Professor_2)")]
    public string idProfessor;
}
