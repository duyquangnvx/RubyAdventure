using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float horizontal;
    private float vertical;

    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    // Timer for damage
    public float TIME_INVINCIBLE = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Vector2 lookDirector = new Vector2(1, 0);

    Animator animator;

    public GameObject projectilePrefab;

    AudioSource audioSource;
    public AudioClip fixSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        // Setup health
        currentHealth = GameConfig.RUBY_MAX_HP;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0) {
                isInvincible = false;
            }
        }

        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            lookDirector.Set(move.x, move.y);
            lookDirector.Normalize();
        }

        animator.SetFloat("Look X", lookDirector.x);
        animator.SetFloat("Look Y", lookDirector.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.Space)) { 
            Launch();
        }
    }

    void FixedUpdate()
    {
        movement();
    }
    
    void movement()
    {
        Vector2 position = rb2d.position;
        position.x += (2f * horizontal * Time.deltaTime);
        position.y += (2f * vertical * Time.deltaTime);
        rb2d.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
        if (amount < 0) {
            if (isInvincible) {
                return;
            }
            isInvincible = true;
            invincibleTimer = TIME_INVINCIBLE;
            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(fixSound);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, GameConfig.RUBY_MAX_HP);
        Debug.Log("[Ruby] ChangeHealth: " + currentHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)GameConfig.RUBY_MAX_HP);
    }

    void Launch() {
        GameObject projectileObject = Instantiate(projectilePrefab, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirector, 300f);

        animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }
}
