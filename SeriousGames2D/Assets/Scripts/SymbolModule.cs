using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolModule : Module
{
    SymbolNode[] nodeGroup;


    protected override void Start()
    {
        base.Start();

        nodeGroup = GetComponentsInChildren<SymbolNode>();

    }

    protected override void Update()
    {
        base.Update();
    }
}
