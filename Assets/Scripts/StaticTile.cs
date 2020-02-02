using UnityEngine;

public abstract class StaticTile : MonoBehaviour
{
    public static Interactable[,] tiles;
    public SpriteRenderer spriteRenderer;

    public void InitStaticTileEvents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public static void InitTiles(Vector2 size)
    {
        tiles = new Interactable[(int)size.x, (int)size.y];
    }

    public static void AddTile(Vector2 position, Interactable sender)
    {
        tiles[(int)position.x, (int)position.y] = sender;
    }

    public static Interactable GetStaticTile(Vector2 position) {
        try {
            return tiles[(int)position.x, (int)position.y];
        } catch {
            return null;
        }
    }
}
