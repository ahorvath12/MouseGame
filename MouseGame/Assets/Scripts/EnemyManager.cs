using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Chasing
    }

    EnemyState state = EnemyState.Idle;

    Rigidbody rbody;
    NavMeshAgent agent;
    Coroutine coroutine;

    GameObject player;

    float prevX, prevZ;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        state = EnemyState.Idle;
    }

    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Idle:
                Idling();
                break;
            case EnemyState.Chasing:
                Chasing();
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
        agent.destination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
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
}
