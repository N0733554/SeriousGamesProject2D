using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireNode : Node
{
    public bool Connected = false;
    WireNode Partner = null;
    WireModule module;

    protected override void Start()
    {
        base.Start();

        module = GetComponentInParent<WireModule>();
    }

    protected override void HighlightItem()
    {
        base.HighlightItem();

        module.selectedNode = this;
    }

    protected override void DeHighlightItem()
    {
        base.DeHighlightItem();

        module.selectedNode = null;
    }

    public void Connect()
    {
        Connected = true;
        Selectable = false;
    }

    public void setPartner(WireNode p)
    {
        Partner = p;
    }

    public bool isPartner(WireNode p)
    {
        return Partner == p;
    }

    public bool hasPartner()
    {
        return Partner;
    }
}
