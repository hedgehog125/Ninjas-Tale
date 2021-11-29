using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour {
    [Header("Objects")]
    [SerializeField] private GameObject soundObject;
    [SerializeField] private GameObject soundObjects;

    [Header("")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int invincibilityTime;

    private int health;
    private int invincibilityTick;

    private void OnTriggerEnter2D(Collider2D collision) {
        ProcessCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        ProcessCollision(collision.gameObject);
    }

    private void ProcessCollision(GameObject source) {
        if (invincibilityTick == 0) {
            enemyDamager damager = source.GetComponent<enemyDamager>();
            if (damager != null) {
                health -= damager.amount;

                if (damager.sound != enemyDamager.Sounds.None) {
                    GameObject instance = Instantiate(soundObject);
                    instance.transform.parent = soundObjects.transform;
                    instance.transform.position = transform.position;
                    instance.transform.rotation = transform.rotation;

                    instance.GetComponent<soundObjectPlayer>().Play(damager.sound);
                }

                if (health <= 0) {
                    Destroy(gameObject);
                    return;
                }
                invincibilityTick = 1;
            }
        }
    }

    private void Start() {
        health = maxHealth;
    }

    private void FixedUpdate() {
        if (invincibilityTick != 0) {
            invincibilityTick++;
            if (invincibilityTick > invincibilityTime) {
                invincibilityTick = 0;
            }
        }
    }
}
