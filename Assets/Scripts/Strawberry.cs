using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    public AudioClip audioClip;
    bool isActive;
    private void Awake() {
        isActive = true;
    }
    private void Start() {
        isActive = true;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (isActive) {
            RubyController rubyController = other.GetComponent<RubyController>();
            if (rubyController != null) {
                rubyController.ChangeHealth(GameConfig.STRAWBERRGY_VALUE);
                rubyController.PlaySound(audioClip);
                // Destroy(gameObject);
                 isActive = false;
                var render = GetComponent<Renderer>();
                if (render) {
                    render.enabled = false;
                    Invoke("Reset", 10f);
                }
            }
        }
    }

    void Reset() {
        var render = GetComponent<Renderer>();
        if (render) {
            render.enabled = true;
        }
        isActive = true;
    }
}
