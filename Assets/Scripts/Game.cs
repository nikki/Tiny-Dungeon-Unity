using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public static Vector2 baseDimensions;
    public static float scale;

    static GameObject currentTile;
    static List<GameObject> chain = new List<GameObject>();

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
        GameObject previousTile;
        int undo;

        // tile is not same tile and is of correct type
        if (tile && tile != currentTile) {
            Tile _tile = tile.GetComponent<Tile>();
            Tile _currentTile = currentTile.GetComponent<Tile>();

            // refs to script components
            if (_tile.type == _currentTile.type) {

                // is this move a valid move?
                if (Board.ValidMove(new Vector2(_currentTile.x, _currentTile.y), new Vector2(_tile.x, _tile.y))) {

                    // this isn't a previous tile?
                    // previousTile = chain.fil



                    _tile.SetSelected(true);
                    chain.Add(tile);
                    currentTile = tile;
                }
            }


        }



        //     // is this move a valid move?
        //     if (board.validMove(this.currentTile, tile)) {

        //       // this isn't a previous tile?
        //       previousTile = this.chain.filter(function(prev) {
        //         return tile === prev;
        //       });

        //       // undo chain
        //       if (previousTile.length) {
        //         undo = this.chain.indexOf(previousTile[0]);
        //         previousTile = this.chain.splice(undo, this.chain.length - undo);
        //         previousTile.forEach(function(tile) {
        //           tile.selected = false;
        //         });
        //       }

        //       tile.selected = true;
        //       this.chain.push(tile);
        //       this.currentTile = tile;
        //     }


    }

    public void Last() {

        // 2+ tiles in chain
        if (chain.Count > 1) {

            // remove tiles
            foreach(GameObject tile in chain) {
                Tile _tile = tile.GetComponent<Tile>();
                Board.RemoveTileAt(new Vector2(_tile.x, _tile.y));
            }

            //   // cast the spell
            //   dungeon.castSpell(this.chain.length);
            //

            //   // currently fighting e?
            //   if (TM.wait) {
            //     // hit e
            //     dungeon.hE({ x : board.x, y : board.y }, this.currentTile, this.chain.length);
            //   } else {
            //     // matches += time
            //     dungeon.gainTime(this.chain.length / 2); // 0.5 secs for each tile matched
            //   }

            //   // add more tiles to board
            //   board.rMT();
            // } else {
            //   // deselect first tile
            //   if (this.chain[0]) this.chain[0].selected = false;

        }

        // reset chain
        chain.Clear();
    }

    void Start() {
        // set base dimensions and scale
        baseDimensions = GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution;
        scale = baseDimensions.x / Screen.width;
    }
}