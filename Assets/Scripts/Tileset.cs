using UnityEngine;

[CreateAssetMenu(menuName = "Tileset")]
public class Tileset : ScriptableObject
{
    public Sprite center;
    public Sprite borderAll;
    [Header("One Border")]
    public Sprite top;
    public Sprite bottom;
    public Sprite left;
    public Sprite right;
    [Header("Two Borders")]
    public Sprite topRight;
    public Sprite topLeft;
    public Sprite bottomRight;
    public Sprite bottomLeft;
    public Sprite twoBorderVertical;
    public Sprite twoBorderHorizontal;
    [Header("Three Borders")]
    public Sprite threeBorderTop;
    public Sprite threeBorderBottom;
    public Sprite threeBorderLeft;
    public Sprite threeBorderRight;
    [Header ("4 Insets")]
    public Sprite insetAll;
    [Header("3 Insets")]
    public Sprite insetTopBottomLeft;
    public Sprite insetTopBottomRight;
    public Sprite insetBottomTopLeft;
    public Sprite insetBottomTopRight;
    [Header("2 Insets")]
    public Sprite insetTop;
    public Sprite insetBottom;
    public Sprite insetLeft;
    public Sprite insetRight;
    public Sprite insetTopLeftBottomRight;
    public Sprite insetTopRightBottomLeft;
    [Header("2 Insets 1 Border")]
    public Sprite topBorderInsetBottom;
    public Sprite bottomBorderInsetTop;
    public Sprite leftBorderInsetRight;
    public Sprite rightBorderInsetLeft;
    [Header("1 Inset")]
    public Sprite insetTopRight;
    public Sprite insetTopLeft;
    public Sprite insetBottomLeft;
    public Sprite insetBottomRight;
    [Header("1 Inset 1 Border")]
    public Sprite topBorderInsetBottomLeft;
    public Sprite topBorderInsetBottomRight;
    public Sprite bottomBorderInsetTopLeft;
    public Sprite bottomBorderInsetTopRight;
    public Sprite leftBorderInsetTopRight;
    public Sprite leftBorderInsetBottomRight;
    public Sprite rightBorderInsetTopLeft;
    public Sprite rightBorderInsetBottomLeft;
    [Header("1 Inset 2 Borders")]
    public Sprite topLeftBorderInset;
    public Sprite topRigthBorderInset;
    public Sprite bottomLeftBorderInset;
    public Sprite bottomRightBorderInset;

    public Sprite GetSprite(Tile tile)
    {
      if (tile.hasNoBorders && tile.hasUpperLeftInset && tile.hasUpperRightInset && tile.hasLowerLeftInset && tile.hasLowerRightInset)
            return insetAll;

        // 3 Insets
        else if (tile.hasNoBorders && tile.hasUpperLeftInset && tile.hasUpperRightInset && tile.hasLowerLeftInset)
            return insetTopBottomLeft;

        else if (tile.hasNoBorders && tile.hasUpperLeftInset && tile.hasUpperRightInset && tile.hasLowerRightInset)
            return insetTopBottomRight;

        else if (tile.hasNoBorders && tile.hasLowerLeftInset && tile.hasLowerRightInset && tile.hasUpperLeftInset)
            return insetBottomTopLeft;

        else if (tile.hasNoBorders && tile.hasLowerLeftInset && tile.hasLowerRightInset && tile.hasUpperRightInset)
            return insetBottomTopRight;

        // 2 insets
        else if (tile.hasNoBorders && tile.hasUpperLeftInset && tile.hasUpperRightInset)
            return insetTop;

        else if (tile.hasNoBorders && tile.hasLowerLeftInset && tile.hasLowerRightInset)
            return insetBottom;

        else if (tile.hasNoBorders && tile.hasUpperLeftInset && tile.hasLowerLeftInset)
            return insetLeft;

        else if (tile.hasNoBorders && tile.hasUpperRightInset && tile.hasLowerRightInset)
            return insetRight;

        else if (tile.hasNoBorders && tile.hasUpperLeftInset && tile.hasLowerRightInset)
            return insetTopLeftBottomRight;

        else if (tile.hasNoBorders && tile.hasUpperRightInset && tile.hasLowerLeftInset)
            return insetTopRightBottomLeft;

        // 1 inset
        else if (tile.hasNoBorders && tile.hasUpperRightInset)
            return insetTopRight;

        else if (tile.hasNoBorders && tile.hasUpperLeftInset)
            return insetTopLeft;

        else if (tile.hasNoBorders && tile.hasLowerRightInset)
            return insetBottomRight;

        else if (tile.hasNoBorders && tile.hasLowerLeftInset)
            return insetBottomLeft;

        // 2 Borders
        else if (tile.hasTopBorder && tile.hasLeftBorder && tile.hasLowerRightInset)
            return topLeftBorderInset;

        else if (tile.hasTopBorder && tile.hasRightBorder && tile.hasLowerLeftInset)
            return topRigthBorderInset;

        else if (tile.hasBottomBorder && tile.hasLeftBorder && tile.hasUpperRightInset)
            return bottomLeftBorderInset;

        else if (tile.hasBottomBorder && tile.hasRightBorder && tile.hasUpperLeftInset)
            return bottomRightBorderInset;

        // 1 border 2 insets
        else if (tile.hasBottomBorder && tile.hasUpperLeftInset && tile.hasUpperRightInset)
            return bottomBorderInsetTop;

        else if (tile.hasTopBorder && tile.hasLowerLeftInset && tile.hasLowerRightInset)
            return topBorderInsetBottom;

        else if (tile.hasLeftBorder && tile.hasUpperRightInset && tile.hasLowerRightInset)
            return leftBorderInsetRight;

        else if (tile.hasRightBorder && tile.hasUpperLeftInset && tile.hasLowerLeftInset)
            return rightBorderInsetLeft;

        // 1 border 1 inset
        else if (tile.hasTopBorder && tile.hasLowerLeftInset)
            return topBorderInsetBottomLeft;

        else if (tile.hasTopBorder && tile.hasLowerRightInset)
            return topBorderInsetBottomRight;

        else if (tile.hasBottomBorder && tile.hasUpperLeftInset)
            return bottomBorderInsetTopLeft;

        else if (tile.hasBottomBorder && tile.hasUpperRightInset)
            return bottomBorderInsetTopRight;

        else if (tile.hasLeftBorder && tile.hasUpperRightInset)
            return leftBorderInsetTopRight;

        else if (tile.hasLeftBorder && tile.hasLowerRightInset)
            return leftBorderInsetBottomRight;

        else if (tile.hasRightBorder && tile.hasUpperLeftInset)
            return rightBorderInsetTopLeft;

        else if (tile.hasRightBorder && tile.hasLowerLeftInset)
            return rightBorderInsetBottomLeft;

        else if (tile.hasAllBorders)
            return borderAll;

        // 3 border
        else if (tile.hasLeftBorder && tile.hasTopBorder && tile.hasRightBorder)
            return threeBorderTop;

        else if (tile.hasLeftBorder && tile.hasBottomBorder && tile.hasRightBorder)
            return threeBorderBottom;

        else if (tile.hasLeftBorder && tile.hasTopBorder && tile.hasBottomBorder)
            return threeBorderLeft;

        else if (tile.hasRightBorder && tile.hasTopBorder && tile.hasBottomBorder)
            return threeBorderRight;

        // 2 border
        else if (tile.hasLeftBorder && tile.hasTopBorder)
            return topLeft;

        else if (tile.hasLeftBorder && tile.hasBottomBorder)
            return bottomLeft;

        else if (tile.hasRightBorder && tile.hasTopBorder)
            return topRight;

        else if (tile.hasRightBorder && tile.hasBottomBorder)
            return bottomRight;

        else if (tile.hasBottomBorder && tile.hasTopBorder)
            return twoBorderVertical;

        else if (tile.hasLeftBorder && tile.hasRightBorder)
            return twoBorderHorizontal;

        // 1 border
        else if (tile.hasLeftBorder)
            return left;

        else if (tile.hasRightBorder)
            return right;

        else if (tile.hasTopBorder)
            return top;

        else if (tile.hasBottomBorder)
            return bottom;
        else
            return center;
    }
}
