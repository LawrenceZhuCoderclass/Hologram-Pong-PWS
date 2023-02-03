using System.Collections;
using UnityEngine;

public class GlitchControl : MonoBehaviour
{
    //How often should the glitch effect happen (higher value means more frequently)
    public float glitchChance = 0.01f;
    public bool highIntensity = false;

    Material hologramMaterial;
    WaitForSeconds glitchLoopWait = new WaitForSeconds(0.1f);

    void Awake()
    {
        hologramMaterial = GetComponent<Renderer>().material;
    }

    IEnumerator Start()
    {
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);

            if (glitchTest <= glitchChance)
            {
                //Do Glitch
                float originalGlowIntensity = hologramMaterial.GetFloat("_GlowIntensity");
                if (!highIntensity)
                {
                    hologramMaterial.SetFloat("_GlitchIntensity", Random.Range(0.07f, 0.1f));
                }
                else
                {
                    hologramMaterial.SetFloat("_GlitchIntensity", Random.Range(0.1f, 0.5f));
                }
                hologramMaterial.SetFloat("_GlowIntensity", originalGlowIntensity * Random.Range(0.14f, 0.44f));
                yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
                hologramMaterial.SetFloat("_GlitchIntensity", 0f);
                hologramMaterial.SetFloat("_GlowIntensity", originalGlowIntensity);
            }

            yield return glitchLoopWait;
        }
    }
}