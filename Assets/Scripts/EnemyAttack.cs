using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float range = 1f;
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
            animator.SetTrigger("attack");
            player.TakeDamage(power);
            timeSinceLastAttack = 0;
        }
    }
}
