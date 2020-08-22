using TMPro;
using UnityEngine;
 using UnityEngine.UI;
 
 [ExecuteInEditMode]
 public class CustomRenderQueue : MonoBehaviour {
 
     public UnityEngine.Rendering.CompareFunction comparison = UnityEngine.Rendering.CompareFunction.Always;
 
     public bool apply = true;
    private void Start()
    {
        Debug.Log("Updated material val");
        RawImage image = GetComponent<RawImage>();
        Material existingGlobalMat = image.materialForRendering;
        Material updatedMaterial = new Material(existingGlobalMat);
        updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
        image.material = updatedMaterial;
        
        foreach (TextMeshProUGUI t in GetComponentsInChildren<TextMeshProUGUI>())
        {
            Debug.Log("hehe");
            Material updMat = new Material(t.materialForRendering);
            updMat.SetInt("unity_GUIZTestMode", (int)comparison);
            t.fontMaterial = updMat;
        }
        
        foreach (Image i in GetComponentsInChildren<Image>())
        {
            Material updMat = new Material(i.materialForRendering);
            updMat.SetInt("unity_GUIZTestMode", (int)comparison);
            i.material = updMat;
        }

        
    }
    private void Update()
     {
     }
 }