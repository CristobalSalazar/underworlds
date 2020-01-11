using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 levelSize;
    [SerializeField] private Tileset tileset;
    private GameObject[,] map;

    private void Start()
    {
        CreateLevel();
    }

    public bool CanMove(Vector3 targetPosition)
    {
        try {
            GameObject exists = map[(int)targetPosition.x, (int)targetPosition.y];
            return exists != null;
        } catch {
            return false;
        }
    }

    private void CreateLevel()
    {
        map = new GameObject[(int)levelSize.x, (int)levelSize.y];
        for (int x = 0; x < levelSize.x; x ++)
        {
            for (int y = 0; y < levelSize.y; y++)
            {
                Vector3 position = new Vector3(x, y, 0);
                string title = $"{x.ToString()}, {y.ToString()}";
                GameObject tile = new GameObject(title);
                tile.transform.position = position;
                tile.transform.SetParent(transform);
                SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();

                if (x == 0 && y == 0) {
                    sr.sprite = tileset.bottomLeft;
                } else if (x == 0 && y == levelSize.y - 1) {
                    sr.sprite = tileset.topLeft;
                }  else if (x == levelSize.x - 1 && y == 0) {
                    sr.sprite = tileset.bottomRight;
                } else if (x == levelSize.x - 1 && y == levelSize.y -1) {
                    sr.sprite = tileset.topRight;
                } else if (x == 0) {
                    sr.sprite = tileset.left;
                } else if (x == levelSize.x - 1) {
                    sr.sprite = tileset.right;
                } else if (y == 0) {
                    sr.sprite = tileset.bottom;
                } else if (y == levelSize.y -1 ) {
                    sr.sprite = tileset.top;
                } else {
                    sr.sprite = tileset.center;
                }

                map[x, y] = tile;
            }
        }
    }
}
