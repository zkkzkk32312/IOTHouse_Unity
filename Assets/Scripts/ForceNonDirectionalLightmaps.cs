using UnityEngine;

public class ForceNonDirectionalLightmaps : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ForceLightmapMode()
    {
        //Debug.Log("[LightmapFix] Forcing lightmaps to Non-Directional mode...");

        // Step 1: Set lightmaps mode to Non-Directional
        LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;
        //Debug.Log("[LightmapFix] LightmapsMode set to: " + LightmapSettings.lightmapsMode);

        // Step 2: Disable directional keywords
        Shader.DisableKeyword("DIRLIGHTMAP_COMBINED");
        Shader.DisableKeyword("DIRLIGHTMAP_SEPARATE");
        //Debug.Log("[LightmapFix] Disabled directional lightmap shader keywords.");

        // Step 3: Strip directional textures from lightmap array
        var oldLightmaps = LightmapSettings.lightmaps;
        var newLightmaps = new LightmapData[oldLightmaps.Length];

        for (int i = 0; i < oldLightmaps.Length; i++)
        {
            newLightmaps[i] = new LightmapData
            {
                lightmapColor = oldLightmaps[i].lightmapColor
            };

            //Debug.Log($"[LightmapFix] Assigned lightmapColor only for index {i}.");
        }

        LightmapSettings.lightmaps = newLightmaps;
        //Debug.Log($"[LightmapFix] Replaced lightmap array with {newLightmaps.Length} color-only entries.");
    }
}
