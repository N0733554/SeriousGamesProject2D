using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    [HideInInspector]
    public bool Highlighted = false;
    [HideInInspector]
    public bool Selected = false;
    [HideInInspector]
    public bool Selectable = true;

    private Color baseColor;
    public Color highlightColor;

    private SpriteRenderer sRenderer;

    protected virtual void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        baseColor = sRenderer.color;
    }

    protected virtual void Update()
    {
        if(Highlighted && Selectable && Input.GetMouseButtonDown(0))
        {
            SelectItem();
        }
    }

    void OnMouseEnter()
    {
        if(Selectable)
            HighlightItem();
    }

    void OnMouseExit()
    {        
        DeHighlightItem();
    }

    protected virtual void HighlightItem()
    {
        Highlighted = true;
        sRenderer.color = highlightColor;
        //Debug.Log("Highlighting " + this.name);
    }

    protected virtual void DeHighlightItem()
    {
        Highlighted = false;
        sRenderer.color = baseColor;
        //Debug.Log("DeHighlighting " + this.name);
    }

    protected virtual void SelectItem()
    {
        Selected = true;
        Selectable = false;
        Debug.Log(this.name + " Selected");
    }

    protected virtual void DeselectItem()
    {
        Selected = false;
        Selectable = true;
        Debug.Log(this.name + " Deselected");
    }
}
