using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapGameController : MonoBehaviour
{
    [Header("AR")]
    public ARRaycastManager raycastManager;

    [Header("Prefabs")]
    public GameObject Tesouro_Aprovacao;
    public GameObject VILAO_NotaBaixa;
    public GameObject VILAO_Reprovacao;
    public GameObject Professor_1;
    public GameObject Professor_2;

    [Header("Chances (%)")]
    [Range(0, 100)] public int chanceTesouro = 60;
    [Range(0, 100)] public int chanceVilao = 30;
    [Range(0, 100)] public int chancePremium = 10;

    [Header("Spawn")]
    public float alturaExtra = 0.05f;

    static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        if (raycastManager == null)
            raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0) return;
        Touch t = Input.GetTouch(0);
        if (t.phase != TouchPhase.Began) return;

        // 1) Primeiro: tentou tocar em um OBJETO?
        if (TryHitObject(t.position)) return;

        // 2) Se não tocou em objeto: tenta spawnar no PLANO
        TrySpawnOnPlane(t.position);
    }

    bool TryHitObject(Vector2 screenPos)
    {
        Camera cam = Camera.main;
        if (cam == null) return false;

        Ray ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 50f))
        {
            ARItem item = hit.collider.GetComponentInParent<ARItem>();
            if (item == null) return false;

            // Ações do jogo
            switch (item.tipo)
            {
                case ARItem.Tipo.Tesouro:
                    {
                        // toca o brilho antes de destruir
                        var fx = item.GetComponentInParent<CollectEffect>();
                        if (fx != null) fx.Play();

                        GameManager.I?.EncontrouTesouro();

                        Destroy(item.gameObject, 0.55f);
                        return true;
                    }


                case ARItem.Tipo.VilaoNotaBaixa:
                    GameManager.I?.EncontrouVilao(false);
                    break;

                case ARItem.Tipo.VilaoReprovacao:
                    GameManager.I?.EncontrouVilao(true);
                    break;

                case ARItem.Tipo.Professor:
                    GameManager.I?.EncontrouProfessor(item.idProfessor);
                    break;
            }

            Destroy(item.gameObject);
            return true;
        }

        return false;
    }

    void TrySpawnOnPlane(Vector2 screenPos)
    {
        if (raycastManager == null) return;

        TrackableType tipos = TrackableType.PlaneWithinPolygon | TrackableType.PlaneEstimated;
        bool ok = raycastManager.Raycast(screenPos, hits, tipos);
        if (!ok || hits.Count == 0) return;

        Pose p = hits[0].pose;
        Vector3 pos = p.position + Vector3.up * alturaExtra;

        GameObject prefab = EscolherPrefab();
        if (prefab == null) return;

        GameObject novo = Instantiate(prefab, pos, Quaternion.identity);

        // “carta” olha para a câmera
        Camera cam = Camera.main;
        if (cam != null)
        {
            Vector3 dir = (novo.transform.position - cam.transform.position);
            dir.y = 0;
            if (dir.sqrMagnitude > 0.001f)
                novo.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    GameObject EscolherPrefab()
    {
        int total = chanceTesouro + chanceVilao + chancePremium;
        if (total <= 0) return null;

        int roll = Random.Range(1, total + 1);

        if (roll <= chanceTesouro) return Tesouro_Aprovacao;

        if (roll <= chanceTesouro + chanceVilao)
            return (Random.value < 0.5f) ? VILAO_NotaBaixa : VILAO_Reprovacao;

        return (Random.value < 0.5f) ? Professor_1 : Professor_2;
    }
}
