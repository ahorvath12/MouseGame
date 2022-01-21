using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectObject : MonoBehaviour
{
    public GameObject ObjectInRange { get; private set; }
    public LayerMask targetLayer;
    [Range(0.1f, 10)]
    public float radius;
    public UnityEvent OnTriggerEnterEvent, OnTriggerExitEvent;

    [Header("Gizmo parameters")]
    public Color gizmoColor = Color.green;
    public bool showGizmos = true;


    void FixedUpdate()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, radius, targetLayer);
        bool playerDetected = collider.Length > 0;
        if (playerDetected)
        {
            OnTriggerEnterEvent?.Invoke();
            ObjectInRange = collider[0].gameObject;
        }
        else
        {
            OnTriggerExitEvent?.Invoke();
            ObjectInRange = null;
        }
    }

    void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }

}
