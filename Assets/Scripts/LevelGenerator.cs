using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static Tile[,] Map { get; private set; }
    public static List<Tile> borderTiles = new List<Tile>();
    public static LevelGenerator Main { get; private set; }
    public Vector2 LevelSize { get; private set; }

    [Range(0, 0.5f)] [SerializeField] private float density;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Tileset tileset;
    [SerializeField] private GameObject stairsDown;
    [SerializeField] private GameObject spikes;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Sprite[] details;
    private Material diffuse;

    public static int CurrentLevel { get; private set;}

    void Awake()
    {
        Main = this;
    }

    private void FillDetails()
    {
        foreach (Tile t in Map)
        {
            if (t == null || Random.value > 0.1) continue;

            Sprite s = details[Random.Range(0, details.Length - 1)];
            GameObject detail = Instantiate(Resources.Load<GameObject>("Prefabs/Environment/Quad"), t.gameObject.transform.position, Quaternion.identity, transform);
            MeshRenderer renderer = detail.GetComponent<MeshRenderer>();
            renderer.material = Resources.Load<Material>("Materials/Diffuse");
        }
    }

    private void Start()
    {
        diffuse = Resources.Load<Material>("Materials/Diffuse");
        LevelSize = new Vector2(10, 10);
        CurrentLevel = 1;
        GameEvents.On("NextLevel", LoadLevel);
        LoadLevel();
    }

    private void OnDestroy()
    {
        GameEvents.Unsubscribe("NextLevel", LoadLevel);
    }

    private void LoadLevel()
    {
        CurrentLevel ++;
        LevelSize += Vector2.one;
        ClearLevel();
        CreateLevel(LevelSize);
        CreateGaps();
        FillTiles();
        StaticTile.InitTiles(LevelSize);
        CreateStairs();
        CreateHazards();
        //FillDetails();
        SetPlayerPosition();
        GameEvents.Emit("LevelDidLoad");
    }

    private void CreateHazards()
    {
        for (int i = 0; i < Map.GetLength(0); i ++) {
            for (int j = 0; j < Map.GetLength(1); j ++) {
                Tile t = GetTile(new Vector2(i, j));
                if (t != null && t.hasNoBorders && t.hasNoInsets) {
                    if (StaticTile.GetStaticTile(new Vector2(i, j)) == null) {
                        if (Random.value < 0.1)
                        {
                            Instantiate(spikes, new Vector3(i, j, 0), spikes.transform.rotation, transform);
                        }
                        if (Random.value < 0.05f)
                        {
                            Instantiate(enemy, new Vector3(i, j, 0), enemy.transform.rotation, transform);
                        }
                    }
                }
            }
        }

    }

    private void CreateStairs()
    {
        Vector2 position;
        while (true)
        {
            int x = Random.Range(0, Map.GetLength(0));
            int y = Random.Range((int)Mathf.Ceil(Map.GetLength(1)/2), Map.GetLength(1));
            if (Map[x, y] != null) {
                position = new Vector2(x, y);
                break;
            }
        }
        GameObject stairs = Instantiate(stairsDown, position, Quaternion.identity, transform);
    }

    private void ClearLevel() {
        borderTiles.Clear();
        foreach (Transform t in transform)
            Destroy(t.gameObject);
    }

    private void SetPlayerPosition()
    {
        for (int i = 0; i < Map.GetLength(0); i ++)
        {
            for (int j = 0; j < Map.GetLength(1); j ++)
            {
                if (Map[i, j] != null) {
                    playerMovement.SetPosition(new Vector2(i, j));
                    return;
                }
            }
        }
    }

    public static Tile GetTile(Vector2 position)
    {
        try {
            return Map[(int)position.x, (int)position.y];
        } catch {
            return null;
        }
    }

    private void CreateLevel(Vector2 levelSize)
    {
        Map = new Tile[(int)levelSize.x, (int)levelSize.y];
        for (int x = 0; x < levelSize.x; x ++)
        {
            for (int y = 0; y < levelSize.y; y++)
            {
                Vector3 position = new Vector3(x, y, 0);
                string title = $"{x.ToString()}, {y.ToString()}";
                Tile tile = new Tile();
                GameObject tileObject = Resources.Load<GameObject>("Prefabs/Environment/Quad");
                tileObject = Instantiate(tileObject, position, Quaternion.identity, transform);
                tileObject.name = title;
                tileObject.layer = LayerMask.NameToLayer("Ground");
                tile.gameObject = tileObject;
                tile.tileset = tileset;
                Map[x, y] = tile;
            }
        }
    }

    private void CreateGaps()
    {
        GameObject shadowCaster = Resources.Load<GameObject>("Prefabs/Environment/Shadowcaster");

        for (int i = 0; i < Map.GetLength(0); i ++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                if (Random.value < density) {
                    Instantiate(shadowCaster, new Vector3(i, j, 0), Quaternion.identity, transform);
                    Destroy(Map[i,j].gameObject);
                    Map[i, j] = null;
                }
            }
        }
    }

    private void FillTiles()
    {
        for (int i = 0; i < Map.GetLength(0); i ++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                Vector2 position = new Vector2(i, j);
                Tile tile = GetTile(position);
                if (tile == null) continue;

                tile.SetBorderFlags(tileset, position);
                Sprite sprite = tileset.GetSprite(tile);
                MeshRenderer renderer = tile.gameObject.GetComponent<MeshRenderer>();
                renderer.material.SetTexture("_MainTex", sprite.texture);

                if (tile.SetColliders())
                    borderTiles.Add(tile);
            }
        }
    }
}
