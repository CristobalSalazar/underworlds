using System.Collections.Generic;
using UnityEngine;

public struct LightCollision
{
    public Vector2 lightNormal;
    public Vector2 hitPoint;
    public float angle;
    public LightCollision(Vector2 lightNormal, Vector2 hitPoint, float angle = 0)
    {
        this.lightNormal = lightNormal;
        this.hitPoint = hitPoint;
        this.angle = angle;
    }

}

public class LightSource : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public List<LightCollision> lightCollisions;

    void Start()
    {
        lightCollisions = new List<LightCollision>();
        GameEvents.On("PlayerEndStep", CalculateCollisions);
        CalculateCollisions();
    }

    void OnDestroy()
    {
        GameEvents.Unsubscribe("PlayerEndStep", CalculateCollisions);
    }

    void CalculateCollisions()
    {
        lightCollisions.Clear();
        foreach (Tile t in LevelGenerator.borderTiles)
        {
            if (t == null) continue;
            EdgeCollider2D[] cols = t.gameObject.GetComponents<EdgeCollider2D>();
            if (cols == null) continue;


            foreach (EdgeCollider2D edge in cols) {
                foreach (Vector2 vertex in edge.points) {

                    Vector3 vertexWorldPosition = t.gameObject.transform.position + new Vector3(vertex.x, vertex.y, 0);
                    RaycastHit2D  hit = Physics2D.Raycast(transform.position, vertexWorldPosition - transform.position, 100, layerMask);
                    Debug.DrawLine(this.transform.position, hit.point, Color.green, 0.1f);
                    Vector2 dist = hit.point - (Vector2)transform.position;
                    lightCollisions.Add(new LightCollision(dist.normalized, hit.point));
                }
            }
        }
    }
}
