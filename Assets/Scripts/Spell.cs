using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SpellType {
    Fire,
    Water,
    Nature,
    Arcane,
    Holy,
    Shadow
}

public class Spell : MonoBehaviour {

    public static Spell instance { get; private set; }

    [System.Serializable]
    public class Effects
    {
        public SpellType type;
        public GameObject normal;
        public GameObject super;
        public GameObject hit;
    }

    public Effects[] effects = new Effects[System.Enum.GetValues(typeof(SpellType)).Length];
    public List<SpellType> active = new List<SpellType>();

    public void Cast(int type) {

        // a square was formed
        if (Game.isSuper) {
            GameObject effect = CFX_SpawnSystem.GetNextObject(effects[type].super);
        } else { // normal spell
            GameObject effect = CFX_SpawnSystem.GetNextObject(effects[type].normal);
        }
    }

    public void Hit(int type) {
        GameObject effect = CFX_SpawnSystem.GetNextObject(effects[type].hit);
        // effect.transform.position = Enemy.instance.transform.position;
    }

    void Awake() {
        // singleton
        instance = this;

        // add currently active spell types
        active.Add(SpellType.Fire);
        active.Add(SpellType.Water);
        active.Add(SpellType.Nature);
    }
}