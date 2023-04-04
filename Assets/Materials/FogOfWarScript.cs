using UnityEngine;

public class FogOfWarScript : MonoBehaviour
{
    public Material fogMaterial;
    public Camera fogCamera;
    public LayerMask fogLayer;

    private RenderTexture fogTexture;

    void Start()
    {
        // Create a RenderTexture with the same dimensions as the main camera
        fogTexture = new RenderTexture(Screen.width, Screen.height, 24);

        // Assign the RenderTexture to the fog camera
        fogCamera.targetTexture = fogTexture;
    }

    void Update()
    {
        // Set the fog camera to the same position and rotation as the main camera
        fogCamera.transform.position = Camera.main.transform.position;
        fogCamera.transform.rotation = Camera.main.transform.rotation;

        // Render the fog texture from the fog camera's perspective
        fogCamera.Render();

        // Assign the fog texture to the fog material
        fogMaterial.SetTexture("_FogOfWarTex", fogTexture);

        // Set the fog material's position to the main camera's position
        fogMaterial.SetVector("_CamPos", Camera.main.transform.position);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Apply the fog material to the image
        Graphics.Blit(src, dest, fogMaterial);
    }

    void OnPostRender()
    {
        // Clear the fog texture
        RenderTexture.active = fogTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;
    }

    void OnDestroy()
    {
        // Release the fog texture
        if (fogTexture != null)
        {
            fogTexture.Release();
            fogTexture = null;
        }
    }
}