using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1.5f;
    public bool vertical;
    Rigidbody2D rb2d;

    int direction = 1;
    float movingTimer;
    public float movingTime = 1.5f;

    Animator animator;

    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {   
        rb2d = GetComponent<Rigidbody2D>();
        movingTimer = movingTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken) {
            return;
        }

        movingTimer -= Time.deltaTime;
        if (movingTimer < 0) {
            direction *= -1;
            movingTimer = movingTime;
        }

        if (vertical) {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
    }

    private void FixedUpdate() {
        if (!broken) {
            return;  
        }

        Vector2 position = rb2d.position;
        if (vertical) {
            position.y += (speed * Time.deltaTime * direction);
        }
        else {
            position.x += (speed * Time.deltaTime * direction);
        }
        rb2d.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        RubyController rubyController = other.GetComponent<RubyController>();
        if (rubyController != null) {
            rubyController.ChangeHealth(-GameConfig.ENEMY_ATK);
        }
    }

    public void Fix() {
        broken = false;
        rb2d.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
