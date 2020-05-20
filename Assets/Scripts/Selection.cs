using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private string[] selectableTags = { "Glass", "Plastic", "Paper", "Bin" };
    public Material highlightMaterial;
    public Material defaultMaterial;
    public Transform selection;

    private Transform _selection;
    private GameObject pointer;
    public bool TestingMode = false;
    void Update()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }
        Vector3 pointer = Vector3.zero;
        if(!TestingMode) pointer.Set(Screen.width/2, Screen.height/2 ,0);
            else pointer.Set(Input.mousePosition.x, Input.mousePosition.y,0f);
        var ray = Camera.main.ScreenPointToRay(pointer);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            selection = hit.transform;
            foreach (string tag in selectableTags)
                if (selection.CompareTag(tag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        defaultMaterial = selectionRenderer.material;
                        selectionRenderer.material = highlightMaterial;
                    }
                    _selection = selection;
                }
        }
    }
}
