using UnityEngine;
using UnityEngine.Rendering.Universal; // potrzebne dla ShadowCaster2D

public class ShadowCasterManager : MonoBehaviour
{
    [Tooltip("Warstwa obiektów do dodania ShadowCastera")]
    public string targetLayer = "interactive";

    void Start()
    {
        AddShadowCastersToLayer();
    }

    void AddShadowCastersToLayer()
    {
        int layerMask = LayerMask.NameToLayer(targetLayer);
        if (layerMask == -1)
        {
            Debug.LogError($"Warstwa {targetLayer} nie istnieje!");
            return;
        }

        // Znajdź wszystkie obiekty w scenie
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (var obj in allObjects)
        {
            if (obj.layer == layerMask)
            {
                // Dodaj ShadowCaster2D jeśli nie istnieje
                if (obj.GetComponent<ShadowCaster2D>() == null)
                {
                    obj.AddComponent<ShadowCaster2D>();
                    count++;
                }
            }
        }

        Debug.Log($"Dodano ShadowCaster2D do {count} obiektów na warstwie '{targetLayer}'");
    }
}