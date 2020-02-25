using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolNode : Node
{
    string symbol;
    public Color pressedColor;

    protected override void Start()
    {
        base.Start();
        Type = "toggle";
    }

    protected override void Update()
    {
        base.Update();

        if (isPressed)
            GetComponent<SpriteRenderer>().color = pressedColor;
    }
    
    public void AssignSymbol(string s)
    {
        symbol = s;
    }

    public string getSymbol()
    {
        return symbol;
    }
}
