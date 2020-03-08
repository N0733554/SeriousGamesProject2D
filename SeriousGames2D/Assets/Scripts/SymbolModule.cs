using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolModule : Module
{
    SymbolNode[] nodeGroup;
    int[] buttonOrder;
    int currentPlace = 0;

    protected override void Start()
    {
        base.Start();

        nodeGroup = GetComponentsInChildren<SymbolNode>();
        int i = 0;
        foreach (SymbolNode n in nodeGroup)
        {
            n.setIndex(i);
            i++;
        }

        GenerateOrder();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void RecieveButtonPress(int press)
    {
        if(press == buttonOrder[currentPlace])
        {
            nodeGroup[press].Press();
            currentPlace++;
            checkComplete();
        }
        else
        {
            resetSequence();
        }
    }

    void resetSequence()
    {
        currentPlace = 0;

        //Deselect all buttons
        foreach(SymbolNode n in nodeGroup)
        {
            n.DeselectButton();
        }
    }

    void checkComplete()
    {
        if(currentPlace >= 4)
        {
            CompleteModule();
            resetSequence();
        }
    }

    void GenerateOrder()
    {
        buttonOrder = new int[] { 0, 1, 2, 3 };
        for (var i = buttonOrder.Length - 1; i > 0; i--)
        {
            var r = Random.Range(0, i);
            var tmp = buttonOrder[i];
            buttonOrder[i] = buttonOrder[r];
            buttonOrder[r] = tmp;
        }
        //foreach (int i in buttonOrder)
        //    Debug.Log(i);
    }
}
