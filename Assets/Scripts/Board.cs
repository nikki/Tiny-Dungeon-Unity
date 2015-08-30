using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {

    public static GameObject board;
    public static int offsetY;

    public static int size = 7;
    public static GameObject[,] grid;

    public static float tileSize = 91;
    public static int numTiles = 0;

    public static void CreateGrid() {
        grid = new GameObject[size, size];

        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                CreateTileAt(x, y);
            }
        }
    }

    public static void CreateTileAt(int x, int y, int? tweenY = null) {
        grid[x, y] = Tile.Create(board, x, y, tweenY);
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

        if (PositionInBounds(scaledPos)) { // round floats down
            return GetTileAtCell(new Vector2((int)(scaledPos.x / tileSize), (int)(scaledPos.y / tileSize)));
        }
        return null;
    }

    public static void MoveTileTo(GameObject tile, Vector2 cell) {
        Tile _tile = tile.GetComponent<Tile>();
        int cx = (int)cell.x; int cy = (int)cell.y;

        grid[_tile.x, _tile.y] = null;
        grid[cx, cy] = tile;
        _tile.TweenTo(cx, cy, null);
    }

    public static void RemoveTileAt(Vector2 cell) {
        Destroy(grid[(int)cell.x, (int)cell.y]);
        grid[(int)cell.x, (int)cell.y] = null;
        numTiles -= 1;
    }

    public static bool CellInBounds(Vector2 cell) {
      return (int)cell.x >= 0 && (int)cell.x < size &&
             (int)cell.y >= 0 && (int)cell.y < size;
    }

    public static bool PositionInBounds(Vector2 pos) {
      return (int)pos.x >= 0 && (int)pos.x < size * tileSize &&
             (int)pos.y >= 0 && (int)pos.y < size * tileSize;
    }

    public static bool ValidMove(Vector2 prev, Vector2 next) {
        int px = (int)prev.x; int py = (int)prev.y;
        int nx = (int)next.x; int ny = (int)next.y;

        return ((nx == px - 1 || nx == px + 1) && ny == py) ||
               ((ny == py - 1 || ny == py + 1) && nx == px);
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
        int[] tweenY = new int[size];

        for (int x = 0; x < size; x++) {
            tweenY[x] = 0;

            for (int y = size - 1; y >= 0; y--) { // reverse dir
                Vector2 cell = new Vector2((float)x, (float)y);
                GameObject tile = GetTileAtCell(cell);

                if (tile) {
                    Vector2 pos = GetFarthestPos(cell);
                    MoveTileTo(tile, pos);
                } else {
                    tweenY[x] += 1; // save tween pos
                }
            }
        }

        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                Vector2 cell = new Vector2((float)x, (float)y);
                GameObject tile = GetTileAtCell(cell);

                if (!tile) {
                    CreateTileAt(x, y, -tweenY[x]);
                }
            }
        }
    }

    void Awake () {
        // set reference to gameobject
        board = gameObject;

        // set offset
        offsetY = (int)GetComponent<RectTransform>().anchoredPosition.y;

        // create grid
        CreateGrid();
    }
}