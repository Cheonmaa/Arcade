using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [Header("ESP32 Input Reader")]
    public SerialReader esp32InputReader;

    [Header("Player Settings")]
    [Tooltip("Maximum health of the player")]
    public int maxHealth = 100;
    [Tooltip("Current health of the player")]
    public int currentHealth;
    [Tooltip("Attack power of the player")]
    public int damage = 10;
    public float attackRadius = 0.5f;
    [Tooltip("Flag to indicate if the hitbox has been touched")]
    public bool hitboxTouched = false;


    [Header("Components")]
    [Tooltip("Health bar component to display player's health")]
    public HealthBar healthBar;
    [Tooltip("Animator component for player animations")]
    public Animator anim;
    [Tooltip("Layer mask for enemies that can be attacked")]
    public LayerMask enemies;
    public GameObject attackHitbox;

    public bool debug;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        debug = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    /*void Update() // PC version
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
    }*/

    void Update() //esp32 version
    {
        if (debug)
        {
            Debug.Log("x1: " + esp32InputReader.x1 + "  y1:" + esp32InputReader.y1);
            Debug.Log("x2: " + esp32InputReader.x2 + "  y2:" + esp32InputReader.y2);
        }

        if (Input.GetKey(KeyCode.L)) // end round
        {
            GameManager.RoundNumber++;
            SceneManager.LoadScene("SELECTCHAR");
        }
        
        if (CompareTag("Player1"))
        {
            if (esp32InputReader.buttonState1P1)
            {

                if (esp32InputReader.y1 == 1)
                {
                    anim.SetBool("IsKicking", true);
                    if (debug)
                        Debug.Log("Kick");
                }
                else if (esp32InputReader.x1 == 1)
                {
                    anim.SetBool("IsJabing", true);
                    if (debug)
                        Debug.Log("Jab");
                }
                else
                {
                    anim.SetBool("IsPunching", true);
                    if (debug)
                        Debug.Log("Punch");
                }
            }
        }
        else if (CompareTag("Player2"))
        {
            if (esp32InputReader.buttonState1P2)
            {
                if (esp32InputReader.y2 == 1)
                {
                    anim.SetBool("IsKicking", true);
                    if (debug)
                        Debug.Log("Kick");
                }
                else if (esp32InputReader.x2 == -1)
                {
                    anim.SetBool("IsJabing", true);
                    if (debug)
                        Debug.Log("Jab");
                }
                else
                {
                    anim.SetBool("IsPunching", true);
                    if (debug)
                        Debug.Log("Punch");
                }
            }
        }
        else
        {
            Debug.Log("Player has no tag or tag not detected");
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
                hitboxTouched = true;
            }
        }
        hitboxTouched = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitbox.transform.position, attackRadius);
    }
}
