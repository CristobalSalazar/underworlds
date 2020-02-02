using UnityEngine;

public class Tile
{
    public Tileset tileset;
    public GameObject gameObject;

    public bool isSteppable = true;
    public bool hasTopBorder;
    public bool hasBottomBorder;
    public bool hasLeftBorder;
    public bool hasRightBorder;
    public bool hasUpperLeftInset;
    public bool hasUpperRightInset;
    public bool hasLowerLeftInset;
    public bool hasLowerRightInset;
    public bool hasNoBorders;
    public bool hasNoInsets;
    public bool hasAllBorders;


    public bool SetColliders()
    {
        if (hasTopBorder) {
            EdgeCollider2D collider = gameObject.AddComponent<EdgeCollider2D>();
            collider.points = new Vector2[]
            {
                Vector2.up/2 + Vector2.left/2,
                Vector2.up/2 + Vector2.right/2
            };
        }
        if (hasBottomBorder) {
            EdgeCollider2D collider = gameObject.AddComponent<EdgeCollider2D>();
            collider.points = new Vector2[]
            {
                Vector2.down/2 + Vector2.left/2,
                Vector2.down/2 + Vector2.right/2
            };
        }
        if (hasLeftBorder) {
            EdgeCollider2D collider = gameObject.AddComponent<EdgeCollider2D>();
            collider.points = new Vector2[]
            {
                Vector2.up/2 + Vector2.left/2,
                Vector2.down/2 + Vector2.left/2
            };
        }
        if (hasRightBorder) {
            EdgeCollider2D collider = gameObject.AddComponent<EdgeCollider2D>();
            collider.points = new Vector2[]
            {
                Vector2.up/2 + Vector2.right/2,
                Vector2.down/2 + Vector2.right/2
            };
        }

        if (hasTopBorder || hasLeftBorder || hasBottomBorder || hasRightBorder)
            return true;
        else
            return false;
    }

    public void SetBorderFlags(Tileset tileset, Vector2 position)
    {
        // get neighbouring tiles
        Tile upperTile = LevelGenerator.GetTile(position + Vector2.up);
        Tile lowerTile = LevelGenerator.GetTile(position + Vector2.down);
        Tile leftTile = LevelGenerator.GetTile(position + Vector2.left);
        Tile rightTile = LevelGenerator.GetTile(position + Vector2.right);

        // diagonal tiles
        Tile upperRightTile = LevelGenerator.GetTile(position + Vector2.up + Vector2.right);
        Tile upperLeftTile = LevelGenerator.GetTile(position + Vector2.up + Vector2.left);
        Tile lowerRightTile = LevelGenerator.GetTile(position + Vector2.down + Vector2.right);
        Tile lowerLeftTile = LevelGenerator.GetTile(position + Vector2.down + Vector2.left);

        // border rules
        hasTopBorder = upperTile == null;
        hasBottomBorder = lowerTile == null;
        hasLeftBorder = leftTile == null;
        hasRightBorder = rightTile == null;
        hasNoBorders = !hasTopBorder && !hasBottomBorder && !hasLeftBorder && !hasRightBorder;
        hasAllBorders = hasTopBorder && hasBottomBorder && hasLeftBorder && hasRightBorder;

        // inset rules
        hasUpperLeftInset = upperLeftTile == null && (!hasTopBorder && !hasLeftBorder);
        hasUpperRightInset = upperRightTile == null && (!hasTopBorder && !hasRightBorder);
        hasLowerLeftInset = lowerLeftTile == null && (!hasBottomBorder && !hasLeftBorder);
        hasLowerRightInset = lowerRightTile == null && (!hasBottomBorder && !hasRightBorder);
        hasNoInsets = !hasUpperLeftInset && !hasUpperRightInset && !hasLowerLeftInset && !hasLowerRightInset;
    }

}
