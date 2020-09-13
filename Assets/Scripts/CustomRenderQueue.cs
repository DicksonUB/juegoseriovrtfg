using TMPro;
using UnityEngine;
 using UnityEngine.UI;
 
 [ExecuteInEditMode]
 public class CustomRenderQueue : MonoBehaviour {
 
     public UnityEngine.Rendering.CompareFunction comparison = UnityEngine.Rendering.CompareFunction.Always;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public TextMeshProUGUI text5;
     public bool apply = true;
    private void Start()
    {
        Debug.Log("Updated material val");
        RawImage image = GetComponent<RawImage>();
        Material existingGlobalMat = image.materialForRendering;
        Material updatedMaterial = new Material(existingGlobalMat);
        updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
        image.material = updatedMaterial;
        
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Text"))
        {
            TextMeshProUGUI text = g.GetComponent<TextMeshProUGUI>();
            Debug.Log(g.name);
            Material updMat = new Material(text.materialForRendering);
            updMat.SetInt("unity_GUIZTestMode", (int)comparison);
            text.fontMaterial = updMat;
        }
        Material updMat1 = new Material(text1.materialForRendering);
        updMat1.SetInt("unity_GUIZTestMode", (int)comparison);
        text1.fontMaterial = updMat1;
        Material updMat2 = new Material(text2.materialForRendering);
        updMat2.SetInt("unity_GUIZTestMode", (int)comparison);
        text2.fontMaterial = updMat2;
        Material updMat3 = new Material(text3.materialForRendering);
        updMat3.SetInt("unity_GUIZTestMode", (int)comparison);
        text3.fontMaterial = updMat3;
        Material updMat4= new Material(text4.materialForRendering);
        updMat4.SetInt("unity_GUIZTestMode", (int)comparison);
        text4.fontMaterial = updMat4;
        Material updMat5 = new Material(text5.materialForRendering);
        updMat5.SetInt("unity_GUIZTestMode", (int)comparison);
        text5.fontMaterial = updMat5;

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