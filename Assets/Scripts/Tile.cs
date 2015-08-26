using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tile : MonoBehaviour {

    public static Object prefab = Resources.Load("Prefabs/Tile");
    public static GameObject parent;

    public int x;
    public int y;

    public enum SpellType {
        Earth,
        Water,
        Air,
        Fire
    }
    public SpellType type;
    public bool selected;

    RectTransform rect;

    public AudioClip click;
    AudioSource audio;

    public static GameObject Create(int x, int y, GameObject parent) {
        // create a new tile
        GameObject tile = Instantiate(prefab) as GameObject;
        tile.transform.parent = parent.transform;

        // local scope reference to script component on tile
        Tile _tile = tile.GetComponent<Tile>();

        // set position
        _tile.SetPosition(x, y);

        // set spell type
        _tile.SetType();

        // set sprite
        _tile.SetSprite();

        // return tile game object
        return tile;
    }

    public void SetPosition(int x, int y)
    {
        // set x/y
        this.x = x;
        this.y = y;

        // set onscreen position
        rect.anchoredPosition = new Vector2((float)x * 128, (float)-y * 128);
        rect.localScale = Vector3.one;
    }

    public void SetType() {
        type = (SpellType)Random.Range(0, System.Enum.GetValues(typeof(SpellType)).Length);
    }

    public void SetSprite() {
        rect.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/t_" + type.ToString().ToLower());
    }

    public void SetSelected(bool select) {
        selected = select;
        rect.GetComponent<Image>().color = selected ? new Color32(255, 255, 255, 255) : new Color32(150, 150, 150, 200);
    }

    public void Awake() {
        // set reference to recttransform
        rect = GetComponent<RectTransform>();

        // set ref to audio component
        audio = GetComponent<AudioSource>();
    }
}