using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("Maximum health of the player")]
    public int maxHealth = 100;
    [Tooltip("Current health of the player")]
    public int currentHealth;
    [Tooltip("Attack power of the player")]
    public int damage = 10;
    public float attackRadius = 0.5f;
    

    [Header("Components")]
    [Tooltip("Health bar component to display player's health")]
    public HealthBar healthBar;
    [Tooltip("Animator component for player animations")]
    public Animator anim;
    [Tooltip("Layer mask for enemies that can be attacked")]
    public LayerMask enemies;
    public GameObject attackHitbox;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("IsKicking", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                anim.SetBool("IsJabing", true);
            }
            else
            {
                anim.SetBool("IsPunching", true);
            }
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    public void endAttack()
    {
        anim.SetBool("IsPunching", false);
        anim.SetBool("IsKicking", false);
        anim.SetBool("IsJabing", false);
    }

    public void Attacking()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackHitbox.transform.position, attackRadius, enemies);
        foreach (Collider2D enemies in hitEnemies)
        {
            if (enemies.gameObject == this.gameObject) continue;
            Player enemy = enemies.GetComponent<Player>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitbox.transform.position, attackRadius);
    }
}
