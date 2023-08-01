using UnityEngine;
using System.Collections;
using System;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private int segments;
    public float XRadius { get; private set; }
    public float YRadius { get; private set; }

    private LineRenderer lineRenderer;
    private CapsuleCollider capsuleCollider;
    public static Action<float> RangeHandle;
    private void Awake()
    {
        RangeHandle += RangeHandler;
    }
    private void OnDestroy()
    {
        RangeHandle -= RangeHandler;
    }

    void Start()
    {
        XRadius = PropertyManager.instance.Range;
        YRadius = PropertyManager.instance.Range;
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius = XRadius;
        capsuleCollider.center = Vector3.zero;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        CreatePoints();
    }


    private void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 1f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * XRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * YRadius;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            angle += (360f / segments);
        }
    }

    private void RangeHandler(float newRadius)
    {
        capsuleCollider.radius = XRadius;
        XRadius = newRadius;
        YRadius = newRadius;
        CreatePoints();
    }
}