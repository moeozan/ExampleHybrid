using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PoolEnemy>())
        {
            Damage(other.GetComponent<PoolEnemy>());
            gameObject.SetActive(false);
        }
    }

    private void Damage(PoolEnemy enemy)
    {
        PropertyManager.instance.DamageCalculator(enemy.gameObject.transform.position);
        enemy.TakeDamage(PropertyManager.instance.Damage);
    }

    public void ArrowFiring(Transform target)
    {
        transform.LookAt(target.position);
        rb.velocity = (target.position - transform.position) * speed;
    }
}
