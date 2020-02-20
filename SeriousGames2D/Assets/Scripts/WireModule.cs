using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireModule : Module
{
    WireNode[] nodeGroup;

    [HideInInspector]
    public WireNode selectedNode;

    bool isConnecting = false;
    WireNode currentConnecting;
    GameObject DrawnWire;

    public Material wireMat;
    public float wireWidth;

    protected override void Start()
    {
        base.Start();

        nodeGroup = GetComponentsInChildren<WireNode>();

        foreach (WireNode n in nodeGroup)
        {
            n.GetComponent<CircleCollider2D>().enabled = false;
        }

        generateConnections();
    }

    protected override void Update()
    {
        base.Update();

        bool canConnect = true;

        if (!isComplete)
        {

            if (connectionsComplete())
                CompleteModule();

            if (isConnecting)
            {
                UpdateDrawnWire();

                if (Input.GetMouseButtonUp(0))
                {
                    canConnect = false;
                    if (selectedNode && (selectedNode != currentConnecting))
                    {
                        if (isValidConnection(currentConnecting, selectedNode))
                        {
                            print("VALID CONNECTION");
                            ConnectNodes(currentConnecting, selectedNode);
                        }
                        else
                        {
                            print("INVALID CONNECTION");
                            currentConnecting.Deselect();
                            selectedNode.Deselect();
                            DrawnWire.SetActive(false);
                            Destroy(DrawnWire);                            
                        }
                    }
                    else
                    {
                        DrawnWire.SetActive(false);
                        Destroy(DrawnWire);
                    }
                    StopConnecting();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (selectedNode && canConnect)
                    StartConnecting(selectedNode);
            }
        }
    }

    protected override void SelectItem()
    {
        base.SelectItem();

        foreach(WireNode n in nodeGroup)
        {
            n.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    protected override void DeselectItem()
    {
        base.DeselectItem();

        foreach (WireNode n in nodeGroup)
        {
            n.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    void ConnectNodes(WireNode a, WireNode b)
    {
        a.Connect();
        b.Connect();

        Debug.Log("Connected " + a + " to " + b);
    }

    bool isValidConnection(WireNode a, WireNode b)
    {
        return a.isPartner(b);
    }

    void StartConnecting(WireNode n)
    {
        isConnecting = true;
        isEscapable = false;

        currentConnecting = n;

        DrawnWire = new GameObject();
        DrawnWire.AddComponent<LineRenderer>();
        LineRenderer lr = DrawnWire.GetComponent<LineRenderer>();
        lr.material = wireMat;
        lr.startWidth = wireWidth;
        lr.endWidth = wireWidth;
        lr.sortingOrder = 5;
        lr.SetPosition(0, currentConnecting.transform.position);
    }

    void StopConnecting()
    {
        isConnecting = false;
        isEscapable = true;

        currentConnecting = null;
    }

    void UpdateDrawnWire()
    {
        var end = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        end = Camera.main.ScreenToWorldPoint(end);

        LineRenderer lr = DrawnWire.GetComponent<LineRenderer>();

        lr.SetPosition(1, end);
        lr.sortingOrder = 3;
    }

    void generateConnections()
    {
        int numberConnections = Random.Range(1, 4);
        List<int> ConnectedNumbers = new List<int>();

        for(int i = 0; i < numberConnections; i++)
        {
            int x = Random.Range(0, 3);
            while (ConnectedNumbers.Contains(2 * x + 1))
                x = Random.Range(0, 3);
            int Left = 2 * x + 1;

            x = Random.Range(0, 3);
            while (ConnectedNumbers.Contains(2 * x + 2))
                x = Random.Range(0, 3);
            int Right = 2 * x + 2;

            nodeGroup[Left - 1].setPartner(nodeGroup[Right - 1]);
            nodeGroup[Right - 1].setPartner(nodeGroup[Left - 1]);

            ConnectedNumbers.Add(Left);
            ConnectedNumbers.Add(Right);

            //Debug.Log("Added Connection between " + Left + " and " + Right);
        }
    }

    bool connectionsComplete()
    {
        int required = 0;
        int completed = 0;
        for(int n = 0; n < nodeGroup.Length; n +=2 )
        {
            if(nodeGroup[n].hasPartner())
            {
                required++;
                if (nodeGroup[n].Connected)
                    completed++;
            }
        }
        if (required == completed)
            return true;
        else
            return false;
    }
    
}
