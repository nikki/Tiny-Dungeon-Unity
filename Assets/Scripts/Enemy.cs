using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public static Enemy instance { get; private set; }

    int health = 10;
    SpellType weakness = SpellType.Fire;

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
            // play hit animation
            Debug.Log(Random.Range(1, 3));
            GetComponent<Animation>().Play("hit" + multiplier);
        }

        // back to idle again
        // GetComponent<Animation>().Play("idle");
    }

    public void Die() {
        // death animation
        GetComponent<Animation>().Play("die");

        // destroy gameobject
    }

    void Awake() {
        // singleton
        instance = this;
    }

}