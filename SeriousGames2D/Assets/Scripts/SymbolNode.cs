using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolNode : Node
{
    int index;
    SymbolModule module;

    protected override void Start()
    {
        base.Start();
        module = GetComponentInParent<SymbolModule>();
        Type = "toggle";
    }

    protected override void Update()
    {
        base.Update();        
    }

    protected override void SelectItem()
    {
        module.RecieveButtonPress(index);
    }

    public void DeselectButton()
    {
        isPressed = false;
        currentColor = baseColor;
    }

    public void Press()
    {
        isPressed = true;
        currentColor = pressedColor;
    }

    public void setIndex(int i)
    {
        index = i;
    }
}
