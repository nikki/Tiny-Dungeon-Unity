using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public enum SpellType {
    Earth,
    Water,
    Air,
    Fire
}

public class Tile : MonoBehaviour {

    public static Object prefab = Resources.Load("Prefabs/Tile");
    public static GameObject parent;

    public int x;
    public int y;
    public float size;

    public SpellType type;
    public bool selected;

    RectTransform rect;

    public AudioClip click;
    AudioSource audio;

    public static GameObject Create(GameObject parent, int x, int y, int? tweenY = null) {
        // create a new tile
        GameObject tile = Instantiate(prefab) as GameObject;
        tile.transform.parent = parent.transform;

        // local scope reference to script component on tile
        Tile _tile = tile.GetComponent<Tile>();

        // set size
        _tile.SetSize();

        // set base position
        _tile.SetPosition(x, y);

        // set spell type
        _tile.SetType();

        // set sprite
        _tile.SetSprite();

        // tween y position
        _tile.TweenTo(x, y, tweenY);

        // return tile game object
        return tile;
    }

    public void SetPosition(int x, int y)
    {
        // set x/y
        this.x = x;
        this.y = y;

        // set onscreen position
        rect.anchoredPosition3D = new Vector3((float)x * size, (float)-y * size, 0f);
        rect.localScale = Vector3.one;
    }

    public void SetSize() {
        // set size ref
        size = Board.tileSize;

        // set tile size
        rect.sizeDelta = new Vector2(size, size);
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

    public void TweenTo(int x, int y, int? tweenY) {
        // set new x/y
        this.x = x;
        this.y = y;

        // calculate y position for newly created tiles ('dropped' from height)
        if (tweenY is int && tweenY != 0) {
            float newY = (float)(-y * size) - (float)(tweenY * size);
            rect.anchoredPosition3D = new Vector3((float)x * size, newY, 0f);
        }

        // tween to new pos
        rect.DOAnchorPos3D(new Vector3((float)x * size, (float)-y * size, 0f), 0.4f, true).SetEase(Ease.InOutExpo);
        rect.localScale = Vector3.one;
    }

    public void Awake() {
        // set reference to recttransform
        rect = GetComponent<RectTransform>();

        // set ref to audio component
        audio = GetComponent<AudioSource>();
    }
}