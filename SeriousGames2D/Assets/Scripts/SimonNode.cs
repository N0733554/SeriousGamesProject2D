using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonNode : Node
{
    SimonModule module;
    
    protected override void Start()
    {
        base.Start();
        module = GetComponentInParent<SimonModule>();
    }

    protected override void Update()
    {
        base.Update();             
    }    
}
