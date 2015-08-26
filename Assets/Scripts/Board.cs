using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {

    public static int offsetY;

    public static int size = 5;
    public static GameObject[,] grid;
    public static int numTiles = 0;

    public static int tileSize = 128;

    void CreateGrid() {
        grid = new GameObject[size, size];

        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                CreateTileAt(x, y);
            }
        }
    }

    void CreateTileAt(int x, int y) {
        grid[x, y] = Tile.Create(x, y, gameObject);
        numTiles += 1;
    }

    public static GameObject GetTileAtCell(Vector2 cell) {
        if (CellInBounds(cell)) {
            return grid[(int)cell.x, (int)cell.y];
        }
        return null;
    }

    public static GameObject GetTileAtScreenPosition(Vector3 mousePos) {
        mousePos.y = -mousePos.y + Screen.height; // invert y
        Vector3 scaledPos = Vector3.Scale(mousePos, new Vector3(Game.scale, Game.scale, 0));
        scaledPos.y += offsetY; // add board y offset (already scaled)

        // MoveTileTo( GetTileAtCell(new Vector2((int)(scaledPos.x / tileSize), (int)(scaledPos.y / tileSize))), new Vector2(0, 0));

        if (PositionInBounds(scaledPos)) { // round floats down
            return GetTileAtCell(new Vector2((int)(scaledPos.x / tileSize), (int)(scaledPos.y / tileSize)));
        }

        return null;
    }

    public static void MoveTileTo(GameObject tile, Vector2 cell) {
        grid[(int)cell.x, (int)cell.y] = null;
        tile.GetComponent<Tile>().SetPosition((int)cell.x, (int)cell.y);
    }

    public static void RemoveTileAt(Vector2 cell) {
        Destroy(grid[(int)cell.x, (int)cell.y]);
        numTiles -= 1;
    }

    public static bool CellInBounds(Vector2 cell) {
      return cell.x >= 0 && cell.x < size &&
             cell.y >= 0 && cell.y < size;
    }

    public static bool PositionInBounds(Vector2 pos) {
      return pos.x >= 0 && pos.x < size * tileSize &&
             pos.y >= 0 && pos.y < size * tileSize;
    }

    public static bool ValidMove(Vector2 prev, Vector2 next) {
        return ((next.x == prev.x - 1 || next.x == prev.x + 1) && next.y == prev.y) ||
               ((next.y == prev.y - 1 || next.y == prev.y + 1) && next.x == prev.x);
    }

    public static Vector2 GetFarthestPos(Vector2 cell) {
        Vector2 prev;

        do {
            prev = cell;
            cell = new Vector2(prev.x, prev.y + 1); // downwards vector
        } while (CellInBounds(cell) && !GetTileAtCell(cell));

        return prev;
    }

    public static void ReplaceMatchedTiles() {
      // !!! TODO

      // var x, y, cell, tile, pos, sY = [];

      // for (x = 0; x < this.size; x++) {
      //   sY[x] = 0;

      //   for (y = this.size - 1; y >= 0; y--) { // reverse dir
      //     cell = { x : x, y : y };
      //     tile = this.gTAC(cell);

      //     if (tile) {
      //       pos = this.gFP(cell);
      //       this.moveTileTo(tile, pos);
      //     } else {
      //       sY[x] += 1; // save tween pos
      //     }
      //   }
      // }

      // for (x = 0; x < this.size; x++) {
      //   for (y = 0; y < this.size; y++) {
      //     cell = { x : x, y : y };
      //     tile = this.gTAC(cell);

      //     if (!tile) {
      //       this.cTA({ x : x, y : y, sY : -sY[x] });
      //     }
      //   }
      // }
    }

    void Start () {
        // set offset
        offsetY = (int)GetComponent<RectTransform>().anchoredPosition.y;

        // create grid
        CreateGrid();
    }
}