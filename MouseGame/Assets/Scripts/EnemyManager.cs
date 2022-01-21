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

    EnemyState state = EnemyState.Idle;

    Rigidbody rbody;
    NavMeshAgent agent;
    Coroutine coroutine;

    GameObject player;

    float prevX, prevZ;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
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
                break;
        }
        prevX = transform.position.x;
        prevZ = transform.position.z;
    }

    void Idling()
    {
        if (rbody.velocity == Vector3.zero && coroutine == null)
        {
            coroutine = StartCoroutine(WaitBeforeMoving());
        }
    }

    void Chasing()
    {
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

    public void StartChase(bool inRange)
    {
        if (!inRange)
        {
            state = EnemyState.Idle;
            return;
        }
        if (coroutine != null)
            StopCoroutine(coroutine);

        state = EnemyState.Chasing;
    }

    public void InKillZone()
    {
        state = EnemyState.Kill;
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
}
