using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public static Vector2 baseDimensions;
    public static float scale;

    static GameObject currentTile;
    static List<GameObject> chain = new List<GameObject>();
    static List<GameObject> previousTiles = new List<GameObject>();

    public void First() {
        GameObject tile = Board.GetTileAtScreenPosition(Input.mousePosition);

        if (tile) {
            tile.GetComponent<Tile>().SetSelected(true);
            chain.Add(tile);
            currentTile = tile;
        }
    }

    public void Next() {
        GameObject tile = Board.GetTileAtScreenPosition(Input.mousePosition);

        // tile is not same tile and is of correct type
        if (tile && tile != currentTile) {
            Tile _tile = tile.GetComponent<Tile>();
            Tile _currentTile = currentTile.GetComponent<Tile>();

            // refs to script components
            if (_tile.type == _currentTile.type) {

                // is this move a valid move?
                if (Board.ValidMove(new Vector2(_currentTile.x, _currentTile.y), new Vector2(_tile.x, _tile.y))) {

                    // is this a previous tile?
                    if (chain.Contains(tile)) {
                        // made a square!
                        if (chain.Count > 3 && tile == chain[0]) {
                            Debug.Log("ZOMG SQUARE!");
                        }

                        // undo chain if previous tile
                        int undo = chain.IndexOf(tile);

                        // "splice" but not
                        previousTiles = chain.GetRange(undo, chain.Count - undo);
                        chain.RemoveRange(undo, chain.Count - undo);

                        // deselect tile
                        foreach(GameObject previousTile in previousTiles) {
                            if (previousTile != tile) {
                                previousTile.GetComponent<Tile>().SetSelected(false);
                            }
                        }
                    }

                    _tile.SetSelected(true);
                    chain.Add(tile);
                    currentTile = tile;
                }
            }
        }
    }

    public void Last() {
        // 2+ tiles in chain
        if (chain.Count > 1) {

            // remove tiles
            foreach(GameObject tile in chain) {
                Tile _tile = tile.GetComponent<Tile>();
                Board.RemoveTileAt(new Vector2(_tile.x, _tile.y));
            }

            // cast the spell
            Player.instance.CastSpell(currentTile.GetComponent<Tile>().type, chain.Count);

            //   // currently fighting e?
            //   if (TM.wait) {
            //     // hit e
            //     dungeon.hE({ x : board.x, y : board.y }, this.currentTile, this.chain.length);
            //   } else {
            //     // matches += time
            //     dungeon.gainTime(this.chain.length / 2); // 0.5 secs for each tile matched
            //   }

            // add more tiles to board
            Board.ReplaceMatchedTiles();
        } else {
            // deselect first tile
            if (chain[0]) chain[0].GetComponent<Tile>().SetSelected(false);
        }

        // reset chain
        chain.Clear();
    }

    void Start() {
        // set base dimensions and scale
        baseDimensions = GameObject.Find("Screens").GetComponent<CanvasScaler>().referenceResolution;
        scale = baseDimensions.x / Screen.width;
    }
}