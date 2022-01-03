using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb2d;

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 20) {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 director, float force) {
        rb2d.AddForce(director * force);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        Enemy enemy = other.collider.GetComponent<Enemy>(); 
        if (enemy != null) {
            enemy.Fix();
        }
        Destroy(gameObject);
    }
}
