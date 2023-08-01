using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PoolEnemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseSpeed;
    [SerializeField] private EnemyDamageText damageText;
    [SerializeField] private Image healthBar;
    private float m_Damage;
    private float m_Speed;

    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    public float CurrentHealth { get; private set; }

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private Collider m_collider;
    private float m_exp;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        agent.isStopped = false;
        m_Speed = baseSpeed + (LevelManager.instance.Level * .05f);
        m_Damage = GameUtilities.FloatHandler(m_Damage);
        m_Damage = baseDamage + (baseDamage * LevelManager.instance.MonsterDamageMultiplier);
        m_exp = PropertyManager.instance.ExperiencePerEnemy * LevelManager.instance.ExpBonus;
        m_exp = GameUtilities.FloatHandler(m_exp);
        m_collider.enabled = true;
        healthBar.fillAmount = 1;
        animator.ResetTrigger("Hit");
        animator.ResetTrigger("Attack");
        animator.SetBool("Death", false);
        CurrentHealth = maxHealth;
        transform.LookAt(Vector3.zero);
        agent.SetDestination(player.position);
        agent.speed = m_Speed;
    }

    private void Update()
    {
        MovementHandler();
    }

    private void MovementHandler()
    {
        if (Vector3.Distance(transform.position, player.position) < 2f) 
        {
            agent.speed = 0;

            if (CurrentHealth > 0)
                animator.SetTrigger("Attack");
        }
    }

    public void TakeDamage(float damage)
    {
        damage = GameUtilities.FloatHandler(damage);
        healthBar.DOFillAmount((CurrentHealth - damage) / maxHealth, .1f);
        damageText.ShowDamage(damage.ToString());
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            StartCoroutine(Death());
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    private IEnumerator Death()
    {
        m_collider.enabled = false;
        animator.ResetTrigger("Hit");
        animator.ResetTrigger("Attack");
        animator.SetBool("Death", true);
        PropertyManager.ExpHandle.Invoke(m_exp);
        LevelManager.instance.ExperienceChecker();
        agent.speed = 0;
        CoinPool.instance.InitializeCoin(transform.position);
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        EnemyPooling.instance.EnemyHandler(gameObject);
    }

    public void SetZeroMovement()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }

    public void SetOneMovement()
    {
        agent.isStopped = false;
        agent.speed = m_Speed;
    }

    public void DamageToPlayer()
    {
        PropertyManager.CurrentHealthHandle.Invoke(-m_Damage);
    }
}
