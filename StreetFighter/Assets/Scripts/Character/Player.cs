using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    [Header("Player Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public float attackRadius = 0.5f;
    public bool hitboxTouched = false;
    public int totalDamageDealt = 0;

    [Header("Combat System")]
    public Player opponentCombat;
    private PlayerMovement playerMovement;
    public string opponentTag = "Player2";
    private bool canAttack = true;
    private bool canBlock = true;
    private float blockCooldown = 5f;
    public bool isBlocking = false;


    [Header("Components")]
    public HealthBar healthBar;
    public Animator anim;
    private SpriteRenderer spriteRenderer;
    public bool debug;
    public LayerMask enemies;
    public GameObject attackHitbox;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        debug = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        if (this.tag == "Player2")
            opponentTag = "Player1";
        else
            opponentTag = "Player2";

        GameObject opponent = GameObject.FindGameObjectWithTag(opponentTag);
        if (opponent != null)
        {
            opponentCombat = opponent.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    /*void Update() // PC version
    {
        if (Input.GetMouseButtonDown(0) && canAttack && !isBlocking)
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

        if (CompareTag("Player1")) //WASD
        {
            if (Input.GetMouseButtonDown(0) && canAttack && !isBlocking)
            {

                if (Input.GetKey(KeyCode.S))
                {
                    anim.SetBool("IsKicking", true);
                }
                else if (Input.GetKey(KeyCode.D)
                {
                    anim.SetBool("IsJabing", true);
                }
                else
                {
                    anim.SetBool("IsPunching", true);
                }
            }
        }
        else if (CompareTag("Player2")) // IJKL
        {
            if (Input.GetMouseButtonDown(1) && canAttack && !isBlocking)
            {
                if (Input.GetKey(KeyCode.K))
                {
                    anim.SetBool("IsKicking", true);
                }
                else if (Input.GetKey(KeyCode.L))
                {
                    anim.SetBool("IsJabing", true);
                }
                else
                {
                    anim.SetBool("IsPunching", true);
                }
            }
        }

        if (CompareTag("Player1") && Input.GetKeyDown(KeyCode.Space) && canBlock)
        {
            StartCoroutine(Block());
        }
        else if (CompareTag("Player2") && Input.GetKeyDown(KeyCode.Return) && canBlock)
        {
            StartCoroutine(Block());
        }
        else
        {
            Debug.Log("Player has no tag or tag not detected");
        }
    }*/
        
        void Update() //esp32 version
        {
        if (CompareTag("Player1"))
        {
            if (Esp32InputReader.Instance.buttonState1P1 && canAttack && !isBlocking)
            {

                if (Esp32InputReader.Instance.y1 == 1)
                {
                    anim.SetBool("IsKicking", true);
                }
                else if (Esp32InputReader.Instance.x1 == 1)
                {
                    anim.SetBool("IsJabing", true);
                }
                else
                {
                    anim.SetBool("IsPunching", true);
                }
            }
        }
        else if (CompareTag("Player2"))
        {
            if (Esp32InputReader.Instance.buttonState1P2 && canAttack && !isBlocking)
            {
                if (Esp32InputReader.Instance.y2 == 1)
                {
                    anim.SetBool("IsKicking", true);
                }
                else if (Esp32InputReader.Instance.x2 == -1)
                {
                    anim.SetBool("IsJabing", true);
                }
                else
                {
                    anim.SetBool("IsPunching", true);
                }
            }
        }

        if (CompareTag("Player1")) {
            if (Esp32InputReader.Instance.buttonState2P1 && canBlock)
            {
                StartCoroutine(Block());
            }
        }
        else if (CompareTag("Player2"))
        {
            if (Esp32InputReader.Instance.buttonState2P2 && canBlock)
            {
                StartCoroutine(Block());
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
        opponentCombat.totalDamageDealt += damage;
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
                if (enemy.isBlocking)
                {
                    StartCoroutine(HasBeenBlocked());
                    return;
                }
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

    IEnumerator Block()
    {
        if (!canBlock || anim.GetBool("IsPunching") || anim.GetBool("IsKicking") || anim.GetBool("IsJabing"))
            yield break;
        isBlocking = true;
        canAttack = false;
        playerMovement.runSpeed = 0f;
        spriteRenderer.color = new Color(0.5f, 0.5f, 1f, 1f);
        yield return new WaitForSeconds(0.8f);
        isBlocking = false;
        canAttack = true;
        playerMovement.runSpeed = 40f;
        spriteRenderer.color = Color.white;

        canBlock = false;
        yield return new WaitForSeconds(blockCooldown);
        canBlock = true;
    }

    IEnumerator HasBeenBlocked()
    {
        endAttack();
        canAttack = false;
        playerMovement.runSpeed = 0f;
        spriteRenderer.color = new Color(1f, 0.5f, 0.5f, 1f); 
        yield return new WaitForSeconds(1f);
        canAttack = true;
        playerMovement.runSpeed = 40f;
        spriteRenderer.color = Color.white;
    }
}
