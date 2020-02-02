using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Test : MonoBehaviour
{
    private LightSource lightSource;
    private Sprite sprite;
    public List<Vector2> points;
    public List<ushort> tris;
    private Vector2 size;
    // Start is called before the first frame update
    void Start()
    {
        points = new List<Vector2>();
        tris = new List<ushort>();
        lightSource = GetComponent<LightSource>();
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        size = new Vector2((Camera.main.orthographicSize + 1) * 4, Camera.main.orthographicSize * 8);
        Texture2D texture = new Texture2D((int)size.x, (int)size.y, DefaultFormat.LDR, TextureCreationFlags.None);

        sprite = Sprite.Create(texture, new Rect(Vector2.zero, size), Vector2.one * 0.5f, 1);
        sr.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        points.Clear();
        tris.Clear();
        points.Add(size/2);
        foreach (LightCollision lightCollision in lightSource.lightCollisions)
        {
            Vector2 point = (Vector2)transform.worldToLocalMatrix.MultiplyPoint3x4((Vector2)transform.position - lightCollision.hitPoint) + size/2;
            points.Add(point);
        }
        points.Add(Vector2.zero);

        for (ushort i = 1; i < points.Count - 1; i ++)
        {
            tris.Add(0);
            tris.Add(i);
            tris.Add((ushort)(i + 1));
        }

        if (points != null && tris != null)
        {
            sprite.OverrideGeometry(points.ToArray(), tris.ToArray());
        }
    }
}
