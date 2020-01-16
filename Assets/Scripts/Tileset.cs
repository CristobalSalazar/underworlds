using UnityEngine;

[CreateAssetMenu(menuName = "Tileset")]
public class Tileset : ScriptableObject
{
    public Sprite center;
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
}
