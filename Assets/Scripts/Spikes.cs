using UnityEngine;

public class Spikes : StaticTile, Interactable
{
    public Texture spikeTexture;
    void OnEnable()
    {
        this.InitStaticTileEvents();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material.SetTexture("_MainTex", spikeTexture);
        StaticTile.AddTile(transform.position, this);
    }

    public void Interact(object sender)
    {
        PlayerHealth.main.Damage(Random.Range(5, 10));
    }
}
