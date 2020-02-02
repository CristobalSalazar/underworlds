using UnityEngine;

public class MyRenderTexture : MonoBehaviour
{
    RenderTexture renderTexture;
    // Start is called before the first frame update
    void Awake()
    {
        renderTexture = Resources.Load<RenderTexture>("RendText");
        renderTexture.height = Screen.height;
        renderTexture.width = Screen.width;
    }
}
