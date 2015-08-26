using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    public static Vector2 baseDimensions;
    public static float scale;

    static GameObject currentTile;
    static GameObject[] chain;

    void Start() {
        // set base dimensions and scale
        baseDimensions = GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution;
        scale = baseDimensions.x / Screen.width;

    }

    public void First() {
        /*GameObject tile = */Board.GetTileAtScreenPosition(Input.mousePosition);
        // Debug.Log(tile);

      // var tile = board.gTASP(o);

      // if (tile) {
      //   tile = board.gTAC(tile);

      //   if (tile) {
      //     tile.selected = true;
      //     this.chain.push(tile);
      //     this.currentTile = tile;
      //   }
      // }
    }

    public void Next() {
        // Debug.Log("move:" + Vector3.Scale(Input.mousePosition, new Vector3(Game.scale, Game.scale, 0)));

      // var tile = board.gTASP(o),
      //     previousTile, undo;

      // if (tile) {
      //   tile = board.gTAC(tile);

      //   // tile is not same tile and is of correct type
      //   if (tile && tile !== this.currentTile && tile.type === this.currentTile.type) {

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
      //   }
      // }
    }

    public void Last() {
        // Debug.Log("up:" + Vector3.Scale(Input.mousePosition, new Vector3(Game.scale, Game.scale, 0)));

      // if (this.chain.length > 1) {
      //   // remove tiles
      //   this.chain.forEach(function(tile) {
      //     var cell = { x : tile.x, y : tile.y };
      //     board.removeTileAt(cell);
      //   });

      //   // cast the spell
      //   dungeon.castSpell(this.chain.length);

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
      // }

      // // reset chain
      // this.chain = [];
    }
}