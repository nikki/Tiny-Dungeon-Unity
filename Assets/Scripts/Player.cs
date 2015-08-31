using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player instance { get; private set; }

    public void CastSpell(SpellType type, int strength) {
        switch (type) {
            case SpellType.Earth:

                break;
            case SpellType.Water:

                break;
            case SpellType.Air:

                break;
            case SpellType.Fire:

                break;
            default:
                break;
        }

        Enemy.instance.TakeDamage(type, strength);
    }

    public void TakeDamage() {

    }

    void Awake() {
        // singleton
        instance = this;
    }

}