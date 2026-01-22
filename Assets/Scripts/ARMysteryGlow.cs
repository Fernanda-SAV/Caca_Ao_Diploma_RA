using UnityEngine;

public class ARMysteryGlow : MonoBehaviour
{
    [HideInInspector] public GameObject prefabRevelado;
    [HideInInspector] public float yawOffset = 0f;

    [Tooltip("Se não for clicado, some sozinho (segundos).")]
    public float autoDespawnSeconds = 12f;

    float t0;

    void Start()
    {
        t0 = Time.time;
    }

    void Update()
    {
        if (autoDespawnSeconds > 0f && Time.time - t0 >= autoDespawnSeconds)
        {
            Destroy(gameObject);
        }
    }

    // Chamado pelo ARTapGameController quando o jogador toca no brilho
    public void Reveal()
    {
        if (prefabRevelado == null)
        {
            Destroy(gameObject);
            return;
        }

        GameObject item = Instantiate(prefabRevelado, transform.position, Quaternion.identity);

        // Faz item olhar para a câmera (horizontal) + offset
        var cam = Camera.main;
        if (cam != null)
        {
            Vector3 dir = (item.transform.position - cam.transform.position);
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
            {
                item.transform.rotation = Quaternion.LookRotation(dir);
                if (Mathf.Abs(yawOffset) > 0.01f)
                    item.transform.rotation *= Quaternion.Euler(0f, yawOffset, 0f);
            }
        }

      
        Destroy(gameObject);
    }
}
