using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float range = 0.5f;
    [SerializeField] float power = 5f;
    [SerializeField] float timeBetweenAttacks = 1f;

    float timeSinceLastAttack = 0;

    PlayerHealth player;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (Vector2.Distance(transform.position, player.transform.position) <= range)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (timeSinceLastAttack >= timeBetweenAttacks)
        {
            FaceTarget();
            animator.SetTrigger("attack");
            timeSinceLastAttack = 0;
        }
    }

    private void FaceTarget()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        bool facingRight = transform.localScale.x >= 0;
        if (facingRight && direction.x < 0 || !facingRight && direction.x >= 0)
        {
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    // Event in animation
    private void HitEvent()
    {
        player.TakeDamage(power);
    }
}
