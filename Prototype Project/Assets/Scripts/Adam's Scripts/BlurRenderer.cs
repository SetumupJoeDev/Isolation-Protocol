using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurRenderer : MonoBehaviour
{
    [SerializeField]
    private Camera      m_blurCamera;

    [SerializeField]
    private Material    m_blurMaterial;

    // Start is called before the first frame update
    void Start()
    {
        // Releases previous camera target texture
        if(m_blurCamera.targetTexture != null)
        {
            m_blurCamera.targetTexture.Release();
        }

        // Formats new target texture and assigns it to material
        m_blurCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        m_blurMaterial.SetTexture("_RenTex", m_blurCamera.targetTexture);
    }
}
