using UnityEngine;
using UnityEngine.AI;

public class GhostBehavior : MonoBehaviour
{
    public Transform player; // Player objesi (Pac-Man)
    public Transform[] patrolPoints; // Patrol yapaca�� noktalar
    private NavMeshAgent agent;
    private bool isChasing = false;
    private bool isVulnerable = false; // Power Pellet etkisi alt�nda m�?
    public float chaseRadius = 5f;
    private float chaseDuration = 10f;
    private float chaseTimer = 0f;

    private PlayerController playerController; // PlayerController referans�
    private Vector3 startPosition; // Hayaletin ba�lang�� pozisyonu
    private float respawnDelay = 3f; // Yeniden do�ma s�resi
    private bool isRespawning = false; // Hayalet yeniden do�ma s�recinde mi?

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerController = player.GetComponent<PlayerController>(); // Referans� al
        startPosition = transform.position; // Hayaletin ba�lang�� pozisyonunu kaydet
        GotoNextPoint();
    }

    void Update()
    {
        if (isRespawning) return; // Yeniden do�ma s�recindeyken ba�ka bir i�lem yapma

        if (playerController != null && playerController.IsPowerActive())
        {
            if (!isVulnerable)
            {
                isVulnerable = true;
                agent.speed = 2f; // Savunmas�zken h�z�n� azalt
                Debug.Log("Ghost is vulnerable!");
            }

            // Pac-Man�den ka�
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Vector3 fleePosition = transform.position + fleeDirection * 5f;
            agent.destination = fleePosition;
        }
        else
        {
            if (isVulnerable)
            {
                isVulnerable = false;
                agent.speed = 3.5f; // Normal h�z�na d�n
                Debug.Log("Ghost is no longer vulnerable.");
            }

            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (!isChasing && distanceToPlayer <= chaseRadius)
            {
                StartChase();
            }

            if (isChasing)
            {
                agent.destination = player.position;
                chaseTimer += Time.deltaTime;
                if (chaseTimer >= chaseDuration)
                {
                    StopChase();
                }
            }
            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }
    }

    void StartChase()
    {
        isChasing = true;
        chaseTimer = 0f;
        Debug.Log("Chase started!");
    }

    void StopChase()
    {
        isChasing = false;
        GotoNextPoint();
        Debug.Log("Chase ended, returning to patrol.");
    }

    void GotoNextPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
    }

    public void KillGhost()
    {
        if (isVulnerable)
        {
            Debug.Log("Ghost killed!");
            SoundManager.instance.PlaySFX(SoundManager.instance.ghostKillSound); // Hayalet �ld�rme sesi
            isRespawning = true;
            agent.enabled = false; // Hayaletin hareketini durdur
            transform.position = startPosition; // Hayaleti ba�lang�� noktas�na ta��
            Invoke("Respawn", respawnDelay); // Yeniden do�may� geciktir
        }
    }

    void Respawn()
    {
        isRespawning = false;
        isVulnerable = false; // Savunmas�zl��� s�f�rla
        agent.enabled = true; // Hareketi tekrar aktif et
        GotoNextPoint(); // Patrol moduna d�n
        Debug.Log("Ghost respawned!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
    public void ResetPosition()
    {
        // Hayaleti ba�lang�� pozisyonuna ���nla
        transform.position = startPosition;
    }
    }
