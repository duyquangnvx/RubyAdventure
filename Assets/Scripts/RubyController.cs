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

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // Setup health
        currentHealth = GameConfig.RUBY_MAX_HP;
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
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, GameConfig.RUBY_MAX_HP);
        Debug.Log("[Ruby] ChangeHealth: " + currentHealth);
    }
}
