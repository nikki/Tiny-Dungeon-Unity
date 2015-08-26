using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public static Object prefab = Resources.Load("Prefabs/Tile");
    public static GameObject parent;

    public int x;
    public int y;

    public AudioClip click;
    AudioSource audio;

    public static GameObject Create(int x, int y, GameObject parent) {
        // create a new tile
        GameObject tile = Instantiate(prefab) as GameObject;
        tile.transform.parent = parent.transform;


        // image
        // Sprite sprite = (Sprite)props["sprite"];

        // position and dimensions
        // if (sprite) {
        //     rect.GetComponent<Image>().sprite = sprite;

        // set position
        tile.GetComponent<Tile>().SetPosition(x, y);

        // return tile game object
        return tile;
    }

    public void SetPosition(int x, int y)
    {
        // set x/y
        this.x = x;
        this.y = y;

        // get recttransform
        RectTransform rect = GetComponent<RectTransform>();

        // set onscreen position
        rect.anchoredPosition = new Vector2((float)x * 128, (float)-y * 128);
        rect.localScale = Vector3.one;
    }

    public void Awake() {
        audio = GetComponent<AudioSource>();
    }
}