using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        RubyController rubyController = other.GetComponent<RubyController>();
        if (rubyController != null) {
            rubyController.ChangeHealth(GameConfig.STRAWBERRGY_VALUE);
            Destroy(gameObject);
        }
    }
}
