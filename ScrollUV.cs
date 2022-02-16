using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    MeshRenderer mRender;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mRender = GetComponent<MeshRenderer>();
        mat = mRender.material;
    }

    // Update is called once per frame
    void Update()
    {   
        Vector2 offset = mat.mainTextureOffset;
        offset.x += Time.deltaTime / 25f;
        mat.mainTextureOffset = offset;
        mat.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }
}
