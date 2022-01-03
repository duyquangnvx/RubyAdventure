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

    // Start is called before the first frame update
    void Start()
    {   
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movingTimer -= Time.deltaTime;
        if (movingTimer < 0) {
            direction *= -1;
            movingTimer = movingTime;
        }
    }

    private void FixedUpdate() {
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
}
