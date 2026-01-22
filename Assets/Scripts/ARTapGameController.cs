using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapGameController : MonoBehaviour
{
    [Header("AR")]
    public ARRaycastManager raycastManager;

    [Header("Prefabs - Itens")]
    public GameObject Tesouro_Aprovacao;
    public GameObject VILAO_NotaBaixa;
    public GameObject VILAO_Reprovacao;

    [Header("Prefabs - Professores (Cards Premium)")]
    public List<GameObject> Professores = new List<GameObject>();

    [Header("Brilho (Mystery)")]
    public GameObject glowPrefab;
    public float glowAlturaExtra = 0.05f;

    [Tooltip("Se o jogador não clicar, o brilho some sozinho.")]
    public float glowTempoDeVida = 10f;

    [Header("Spawn Automático")]
    public bool spawnAutomatico = true;
    public float intervaloSpawn = 2.8f;
    public int maxGlowsAoMesmoTempo = 3;

    [Header("Anti-empilhamento")]
    public float raioOcupacao = 0.25f;

    public LayerMask mascaraOcupacao; // <- TEM que ser LayerMask
    public int tentativasSpawn = 6;



    [Tooltip("Distância padrão à frente da câmera (fallback se não achar plano).")]
    public float distanciaFrente = 1.4f;

    [Tooltip("Altura padrão (fallback se não achar plano).")]
    public float alturaFrente = -0.1f;

    [Header("Chances (%)")]
    [Range(0, 100)] public int chanceTesouro = 60;
    [Range(0, 100)] public int chanceVilao = 30;
    [Range(0, 100)] public int chancePremium = 10;

    [Tooltip("Giro extra (graus) aplicado após olhar para a câmera. Use 180 se nascer de costas.")]
    public float yawOffset = 0f;

    [Header("Teste")]
    public bool testeSomenteProfessores = false;

    static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    int glowsVivos = 0;
    Coroutine loop;

    void Awake()
    {
        if (raycastManager == null)
            raycastManager = GetComponent<ARRaycastManager>();
    }

    void OnEnable()
    {
        if (spawnAutomatico && loop == null)
            loop = StartCoroutine(SpawnLoop());
    }

    void OnDisable()
    {
        if (loop != null)
        {
            StopCoroutine(loop);
            loop = null;
        }
    }

    void Update()
    {
        // Toque: primeiro tenta clicar em objeto (glow OU item)
        if (Input.touchCount == 0) return;
        Touch t = Input.GetTouch(0);
        if (t.phase != TouchPhase.Began) return;

        TryHitObjectOrGlow(t.position);
    }

    IEnumerator SpawnLoop()
    {
        // Pequeno delay inicial
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            yield return new WaitForSeconds(intervaloSpawn);

            if (!spawnAutomatico) continue;

            // Se terminou o jogo, não spawna mais nada
            if (GameManager.I != null && GameManager.I.DiplomaConquistado())
                continue;

            // Limite de glows para não lotar a tela
            if (glowsVivos >= maxGlowsAoMesmoTempo)
                continue;

            SpawnMysteryGlow();
        }
    }

    void SpawnMysteryGlow()
    {
        if (glowPrefab == null) return;

        GameObject prefabRevelado = EscolherPrefab();
        if (prefabRevelado == null) return;

        for (int i = 0; i < tentativasSpawn; i++)
        {
            Vector3 pos;

            // Tenta pegar um ponto no plano à frente (centro da tela)
            if (TryGetPoseFrenteNoPlano(out Pose pose))
            {
                pos = pose.position + Vector3.up * glowAlturaExtra;

                // pequeno deslocamento aleatório (para não aparecer sempre no mesmo lugar)
                pos += Random.insideUnitSphere * 0.15f;
                pos.y = pose.position.y + glowAlturaExtra;
            }
            else
            {
                // fallback: frente da câmera com variação
                pos = CameraFallbackPosition();
                pos += (Random.insideUnitSphere * 0.2f);
            }

            // Checa se a posição está livre
            if (!PosicaoLivre(pos)) continue;

            // Instancia o brilho
            GameObject glow = Instantiate(glowPrefab, pos, Quaternion.identity);
            glowsVivos++;

            // Ajusta layer do glow automaticamente (garantia)
            glow.layer = LayerMask.NameToLayer("ARInteractable");

            // Ao destruir, diminuir contagem
            glow.AddComponent<OnDestroyCallback>().onDestroyed += () =>
            {
                glowsVivos = Mathf.Max(0, glowsVivos - 1);
            };

            // Configura o reveal
            var mg = glow.GetComponent<ARMysteryGlow>();
            if (mg == null) mg = glow.AddComponent<ARMysteryGlow>();

            mg.prefabRevelado = prefabRevelado;
            mg.yawOffset = yawOffset;
            mg.autoDespawnSeconds = glowTempoDeVida;   // ✅ brilho some sozinho

            // Faz o brilho olhar pra câmera
            FaceCameraFlat(glow);

            // sucesso -> sai da função
            return;
        }

        // Se falhar todas as tentativas: não spawna nada desta vez
    }


    bool TryGetPoseFrenteNoPlano(out Pose pose)
    {
        pose = default;

        if (raycastManager == null) return false;

        // Raycast do CENTRO da tela para encontrar chão à frente
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.55f);

        TrackableType tipos = TrackableType.PlaneWithinPolygon | TrackableType.PlaneEstimated;
        bool ok = raycastManager.Raycast(center, hits, tipos);
        if (!ok || hits.Count == 0) return false;

        pose = hits[0].pose;
        return true;
    }

    Vector3 CameraFallbackPosition()
    {
        Camera cam = Camera.main;
        if (cam == null) return Vector3.zero;

        // Um pouco à frente e um pouco abaixo do centro da câmera (como se estivesse “no chão”)
        Vector3 pos = cam.transform.position + cam.transform.forward * distanciaFrente;
        pos.y += alturaFrente;
        return pos;
    }

    void FaceCameraFlat(GameObject go)
    {
        Camera cam = Camera.main;
        if (cam == null || go == null) return;

        Vector3 dir = (go.transform.position - cam.transform.position);
        dir.y = 0f;

        if (dir.sqrMagnitude > 0.001f)
        {
            go.transform.rotation = Quaternion.LookRotation(dir);
            if (Mathf.Abs(yawOffset) > 0.01f)
                go.transform.rotation *= Quaternion.Euler(0f, yawOffset, 0f);
        }
    }

    bool PosicaoLivre(Vector3 pos)
    {
        Collider[] cols = Physics.OverlapSphere(pos, raioOcupacao, mascaraOcupacao);
        return cols == null || cols.Length == 0;
    }

    bool TryHitObjectOrGlow(Vector2 screenPos)
    {
        Camera cam = Camera.main;
        if (cam == null) return false;

        Ray ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 50f))
        {
            // 1) Se tocou no BRILHO
            var glow = hit.collider.GetComponentInParent<ARMysteryGlow>();
            if (glow != null)
            {
                glow.Reveal();
                return true;
            }

            // 2) Se tocou em um ITEM real (tesouro/vilão/professor)
            ARItem item = hit.collider.GetComponentInParent<ARItem>();
            if (item == null) return false;

            switch (item.tipo)
            {
                case ARItem.Tipo.Tesouro:
                    {
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

    GameObject EscolherPrefab()
    {
        // Se já terminou o jogo (10/10), não spawna mais nada
        if (GameManager.I != null && GameManager.I.DiplomaConquistado())
            return null;

        // Modo teste: só professores
        if (testeSomenteProfessores)
            return PickProfessorPreferindoNaoColetado();

        int total = chanceTesouro + chanceVilao + chancePremium;
        if (total <= 0) return null;

        int roll = Random.Range(1, total + 1);

        // Se disciplinas completas, não gera tesouro/vilão (na prática o jogo terminou e já retorna null acima)
        if (roll <= chanceTesouro) return Tesouro_Aprovacao;

        if (roll <= chanceTesouro + chanceVilao)
            return (Random.value < 0.5f) ? VILAO_NotaBaixa : VILAO_Reprovacao;

        return PickProfessorPreferindoNaoColetado();
    }

    GameObject PickProfessorPreferindoNaoColetado()
    {
        if (Professores == null || Professores.Count == 0) return null;

        // Se já coletou todos, não spawna mais professor
        if (GameManager.I != null && GameManager.I.TodosProfessoresColetados())
            return null;

        List<GameObject> naoColetados = new List<GameObject>();

        foreach (var p in Professores)
        {
            if (p == null) continue;

            var item = p.GetComponent<ARItem>();
            if (item == null || item.tipo != ARItem.Tipo.Professor)
            {
                naoColetados.Add(p);
                continue;
            }

            if (GameManager.I == null || !GameManager.I.ProfessorJaColetado(item.idProfessor))
                naoColetados.Add(p);
        }

        if (naoColetados.Count > 0)
            return naoColetados[Random.Range(0, naoColetados.Count)];

        return Professores[Random.Range(0, Professores.Count)];
    }
}

// Pequeno helper para detectar destruição e ajustar contador
public class OnDestroyCallback : MonoBehaviour
{
    public System.Action onDestroyed;
    void OnDestroy() => onDestroyed?.Invoke();
}
