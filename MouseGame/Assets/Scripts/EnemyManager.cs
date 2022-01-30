using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Chasing,
        Kill
    }
    public LayerMask layerToAvoid;
    public AudioClip[] audioClips;

    EnemyState state = EnemyState.Idle;
    bool playerInRange = false;

    Rigidbody rbody;
    NavMeshAgent agent;
    AudioSource audioSource;
    Coroutine coroutine, audioCoroutine;

    GameObject player;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        state = EnemyState.Idle;
    }

    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                Idling();
                break;
            case EnemyState.Chasing:
                Chasing();
                break;
            case EnemyState.Kill:
                agent.isStopped = true;
                StartCoroutine(FadeAudio());
                break;
        }

        if (audioCoroutine == null)
        {
            audioCoroutine = StartCoroutine(SelectAudioClips());
        }
    }

    void Idling()
    {
        if (playerInRange)
        {
            Debug.Log("player in range");
            if (SeePlayer())
            {
                StartChase();
                return;
            }
        }

        if (rbody.velocity == Vector3.zero && coroutine == null)
        {
            coroutine = StartCoroutine(WaitBeforeMoving());
        }
    }

    void Chasing()
    {
        if (!SeePlayer())
        {
            state = EnemyState.Idle;
            Debug.Log("return to idle");
            //agent.destination = transform.position;
            return;
        }

        agent.destination = player.transform.position;

    }

    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(Random.Range(0, 6));

        int randomX = Random.Range(-11, 11);
        int randomZ = Random.Range(-11, 11);
        Vector3 dest = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (PositionRaycast(dest))
            agent.destination = dest;
        coroutine = null;
    }

    void StartChase()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        state = EnemyState.Chasing;
    }

    public void InKillZone()
    {
        state = EnemyState.Kill;
        player.GetComponent<PlayerManager>().Caught();
    }

    bool PositionRaycast(Vector3 pos)
    {
        float overlapTestSize = agent.radius;
        Collider[] hitColliders = new Collider[10];
        int numberOfCollidersFound = Physics.OverlapSphereNonAlloc(pos, overlapTestSize, hitColliders, layerToAvoid);

        if (numberOfCollidersFound == 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator SelectAudioClips()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));

        if (!audioSource.isPlaying && Random.Range(0, 100) > 90)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }

        audioCoroutine = null;
        yield return null;
    }

    IEnumerator FadeAudio()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, audioSource.volume - 0.005f, 5f);
            yield return null;
        }
        audioSource.volume = 0;
    }

    public void StartRaycast(bool inRange)
    {
        playerInRange = inRange;
    }

    bool SeePlayer()
    {
        Vector3 heading = transform.position - player.transform.position;
        Vector3 direction = (heading / (heading.magnitude)) * -1;
        RaycastHit hit;
        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Physics.Raycast(startPoint, direction, out hit, 15, ~layerToAvoid);
        return hit.collider != null && hit.transform.tag == "Player";
    }
}
