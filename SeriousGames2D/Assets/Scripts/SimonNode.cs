using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonNode : Node
{
    SimonModule module;
    int index;
    protected override void Start()
    {
        base.Start();
        module = GetComponentInParent<SimonModule>();
    }

    protected override void Update()
    {
        base.Update();             
    }

    protected override void SelectItem()
    {
        //Glow();
        module.RecieveButtonPress(this);
    }

    public void Glow()
    {
        Debug.Log(this.name + " Glowing");
    }

    public void setIndex(int i)
    {
        index = i;
    }
    public int getIndex()
    {
        return index;
    }

}
