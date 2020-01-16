using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static GameObject[,] Map { get; private set; }
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Vector2 levelSize;
    [SerializeField] private Tileset tileset;
    [SerializeField] private Sprite stairsDown;

    private void Start()
    {
        CreateLevel();
        FillTiles();
        SetPlayerPosition();
    }

    private void SetPlayerPosition()
    {
        for (int i = 0; i < Map.GetLength(0); i ++)
        {

            for (int j = 0; j < Map.GetLength(1); j ++)
            {
                if (Map[i, j] != null)
                {
                    playerMovement.SetPosition(Map[i, j].transform.position);
                    return;
                }
            }
        }
    }

    private void SetTileSprite(ref SpriteRenderer spriteRenderer, ref Tileset tileset, Vector2 position)
    {
        // get neighbouring tiles
        GameObject upperTile = GetTile(position + Vector2.up);
        GameObject lowerTile = GetTile(position + Vector2.down);
        GameObject leftTile = GetTile(position + Vector2.left);
        GameObject rightTile = GetTile(position + Vector2.right);

        // diagonal tiles
        GameObject upperRightTile = GetTile(position + Vector2.up + Vector2.right);
        GameObject upperLeftTile = GetTile(position + Vector2.up + Vector2.left);
        GameObject lowerRightTile = GetTile(position + Vector2.down + Vector2.right);
        GameObject lowerLeftTile = GetTile(position + Vector2.down + Vector2.left);

        // border rules
        bool hasUpperBorder = upperTile == null;
        bool hasLowerBorder = lowerTile == null;
        bool hasLeftBorder = leftTile == null;
        bool hasRightBorder = rightTile == null;
        bool hasNoBorders = !hasUpperBorder && !hasLowerBorder && !hasLeftBorder && !hasRightBorder;
        // inset rules
        bool hasUpperLeftInset = upperLeftTile == null && (!hasUpperBorder && !hasLeftBorder);
        bool hasUpperRightInset = upperRightTile == null && (!hasUpperBorder && !hasRightBorder);
        bool hasLowerLeftInset = lowerLeftTile == null && (!hasLowerBorder && !hasLeftBorder);
        bool hasLowerRightInset = lowerRightTile == null && (!hasLowerBorder && !hasRightBorder);

        // 4 Inset
        if (hasNoBorders && hasUpperLeftInset && hasUpperRightInset && hasLowerLeftInset && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.insetAll;
        }
        // 3 Insets
        else if (hasNoBorders && hasUpperLeftInset && hasUpperRightInset && hasLowerLeftInset)
        {
            spriteRenderer.sprite = tileset.insetTopBottomLeft;
        }
        else if (hasNoBorders && hasUpperLeftInset && hasUpperRightInset && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.insetTopBottomRight;
        }
        else if (hasNoBorders && hasLowerLeftInset && hasLowerRightInset && hasUpperLeftInset)
        {
            spriteRenderer.sprite = tileset.insetBottomTopLeft;
        }
        else if (hasNoBorders && hasLowerLeftInset && hasLowerRightInset && hasUpperRightInset)
        {
            spriteRenderer.sprite = tileset.insetBottomTopRight;
        }
        // 2 insets
        else if (hasNoBorders && hasUpperLeftInset && hasUpperRightInset)
        {
            spriteRenderer.sprite = tileset.insetTop;
        }
        else if (hasNoBorders && hasLowerLeftInset && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.insetBottom;
        }
        else if (hasNoBorders && hasUpperLeftInset && hasLowerLeftInset)
        {
            spriteRenderer.sprite = tileset.insetLeft;
        }
        else if (hasNoBorders && hasUpperRightInset && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.insetRight;
        }
        else if (hasNoBorders && hasUpperLeftInset && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.insetTopLeftBottomRight;
        }
        else if (hasNoBorders && hasUpperRightInset && hasLowerLeftInset)
        {
            spriteRenderer.sprite = tileset.insetTopRightBottomLeft;
        }
        // 1 inset
        else if (hasNoBorders && hasUpperRightInset)
        {
            spriteRenderer.sprite = tileset.insetTopRight;
        }
        else if (hasNoBorders && hasUpperLeftInset)
        {
            spriteRenderer.sprite = tileset.insetTopLeft;
        }
        else if (hasNoBorders && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.insetBottomRight;
        }
        else if (hasNoBorders && hasLowerLeftInset)
        {
            spriteRenderer.sprite = tileset.insetBottomLeft;
        }
        // 2 Borders
        else if (hasUpperBorder && hasLeftBorder && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.topLeftBorderInset;
        }
        else if (hasUpperBorder && hasRightBorder && hasLowerLeftInset)
        {
            spriteRenderer.sprite = tileset.topRigthBorderInset;
        }
        else if (hasLowerBorder && hasLeftBorder && hasUpperRightInset)
        {
            spriteRenderer.sprite = tileset.bottomLeftBorderInset;
        }
        else if (hasLowerBorder && hasRightBorder && hasUpperLeftInset)
        {
            spriteRenderer.sprite = tileset.bottomRightBorderInset;
        }
        // 1 border 2 insets
        else if (hasLowerBorder && hasUpperLeftInset && hasUpperRightInset)
        {
            spriteRenderer.sprite = tileset.bottomBorderInsetTop;
        }
        else if (hasUpperBorder && hasLowerLeftInset && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.topBorderInsetBottom;
        }
        else if (hasLeftBorder && hasUpperRightInset && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.leftBorderInsetRight;
        }
        else if (hasRightBorder && hasUpperLeftInset && hasLowerLeftInset)
        {
            spriteRenderer.sprite = tileset.rightBorderInsetLeft;
        }
        // 1 border 1 inset
        else if (hasUpperBorder && hasLowerLeftInset)
        {
            spriteRenderer.sprite = tileset.topBorderInsetBottomLeft;
        }
        else if (hasUpperBorder && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.topBorderInsetBottomRight;
        }
        else if (hasLowerBorder && hasUpperLeftInset)
        {
            spriteRenderer.sprite = tileset.bottomBorderInsetTopLeft;
        }
        else if (hasLowerBorder && hasUpperRightInset)
        {
            spriteRenderer.sprite = tileset.bottomBorderInsetTopRight;
        }
        else if (hasLeftBorder && hasUpperRightInset)
        {
            spriteRenderer.sprite = tileset.leftBorderInsetTopRight;
        }
        else if (hasLeftBorder && hasLowerRightInset)
        {
            spriteRenderer.sprite = tileset.leftBorderInsetBottomRight;
        }
        else if (hasRightBorder && hasUpperLeftInset)
        {
            spriteRenderer.sprite = tileset.rightBorderInsetTopLeft;
        }
        else if (hasRightBorder && hasLowerLeftInset)
        {
            spriteRenderer.sprite = tileset.rightBorderInsetBottomLeft;
        }
        // ------ THREE BORDER ------
        // three border top
        else if (hasLeftBorder && hasUpperBorder && hasRightBorder) {
            spriteRenderer.sprite = tileset.threeBorderTop;
        }
        // three border bottom
        else if (hasLeftBorder && hasLowerBorder && hasRightBorder) {
            spriteRenderer.sprite = tileset.threeBorderBottom;
        }
        // three border left
        else if (hasLeftBorder && hasUpperBorder && hasLowerBorder) {
            spriteRenderer.sprite = tileset.threeBorderLeft;
        }
        // three border right;
        else if (hasRightBorder && hasUpperBorder && hasLowerBorder) {
            spriteRenderer.sprite = tileset.threeBorderRight;
        }

        // ------ TWO BORDER -----
        // upper left corner
        else if (hasLeftBorder && hasUpperBorder) {
            spriteRenderer.sprite = tileset.topLeft;
        }
        // lower left corner
        else if (hasLeftBorder && hasLowerBorder) {
            spriteRenderer.sprite = tileset.bottomLeft;
        }
        // upper right corner
        else if (hasRightBorder && hasUpperBorder) {
            spriteRenderer.sprite = tileset.topRight;
        }
        // lower right corner
        else if (hasRightBorder && hasLowerBorder) {
            spriteRenderer.sprite = tileset.bottomRight;
        }
        // two border vertical
        else if (hasLowerBorder && hasUpperBorder) {
            spriteRenderer.sprite = tileset.twoBorderVertical;
        }
        // two border horizontal
        else if (hasLeftBorder && hasRightBorder) {
            spriteRenderer.sprite = tileset.twoBorderHorizontal;
        }

        // ------ ONE BORDER ------
        // left border
        else if (hasLeftBorder)  {
            spriteRenderer.sprite = tileset.left;
        }
        // right border
        else if (hasRightBorder)  {
            spriteRenderer.sprite = tileset.right;
        }
        // top border
        else if (hasUpperBorder)  {
            spriteRenderer.sprite = tileset.top;
        }
        // lower border
        else if (hasLowerBorder)  {
            spriteRenderer.sprite = tileset.bottom;
        }
        else  {
            spriteRenderer.sprite = tileset.center;
        }
    }

    private GameObject GetTile(Vector2 position)
    {
        try {
            return Map[(int)position.x, (int)position.y];
        } catch {
            return null;
        }
    }

    private void CreateLevel()
    {
        Map = new GameObject[(int)levelSize.x, (int)levelSize.y];
        for (int x = 0; x < levelSize.x; x ++)
        {
            for (int y = 0; y < levelSize.y; y++)
            {
                // chance to generate holes
                if (Random.value < 0.1) {
                    Map [x, y] = null;
                    continue;
                }
                Vector3 position = new Vector3(x, y, 0);
                string title = $"{x.ToString()}, {y.ToString()}";
                GameObject tile = new GameObject(title);
                tile.transform.position = position;
                tile.transform.SetParent(transform);
                SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
                Map[x, y] = tile;
            }
        }
    }

    private void FillTiles()
    {
        foreach (GameObject obj in Map)
        {
            if (obj == null) continue;
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr == null) continue;
            SetTileSprite(ref sr, ref tileset, obj.transform.position);
        }
    }
}
