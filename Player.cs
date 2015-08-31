using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player instance { get; private set; }

    public void CastSpell(SpellType type, int strength) {
        // instantiate spell effect prefab
        Spell.instance.Cast((int)type);

        // damage enemy
        Enemy.instance.TakeDamage(type, strength);
    }

    public void TakeDamage() {

    }

    void Awake() {
        // singleton
        instance = this;
    }
}