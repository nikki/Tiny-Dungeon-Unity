using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public static Enemy instance { get; private set; }

    SpellType weakness = SpellType.Fire;

    bool isBoss = false;

    int health = 1000;
    bool dead = false;

    public void CastSpell() {

    }

    public void TakeDamage(SpellType type, int strength) {
        int multiplier = 1;

        // deduct health
        if (type == weakness) multiplier = 2;
        health -= strength * multiplier;

        if (health < 0) {
            // kill
            Die();
        } else {
            // instantiate spell effect prefab
            Spell.instance.Hit((int)type);

            // play hit animation
            GetComponent<Animation>().Play("hit" + multiplier);
        }
    }

    public void Die() {
        if (dead) return;

        // death animation
        GetComponent<Animation>().Play("die");

        // temp dead flag
        dead = true;

        // destroy gameobject
    }

    void Awake() {
        // singleton
        instance = this;

        // play idle animation on awake
        GetComponent<Animation>().Play("idle");
    }

    void Update() {
        // default to idle animation
        if (!dead && !GetComponent<Animation>().isPlaying) {
            GetComponent<Animation>().Play("idle");
        }
    }
}