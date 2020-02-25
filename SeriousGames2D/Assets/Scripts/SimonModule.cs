using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonModule : Module
{
    SimonNode[] nodeGroup;

    string State = "startState";
    public int sequenceLength = 5;
    int[] Sequence;

    int seqNumber = 1;
    int countNumber = 0;

    bool isWaiting = false;
    public float waitDelay;
    protected override void Start()
    {
        base.Start();

        nodeGroup = GetComponentsInChildren<SimonNode>();
        int i = 0;
        foreach (SimonNode n in nodeGroup)
        {
            n.setIndex(i);
            i++;
            n.GetComponent<BoxCollider2D>().enabled = false;
        }

        SelectState();
        CreateSequence();
    }

    protected override void Update()
    {
        base.Update();

        if(!isComplete)
            StartCoroutine(ExecuteAfterTime(waitDelay));
    }

    protected override void SelectItem()
    {
        base.SelectItem();

        foreach (SimonNode n in nodeGroup)
        {
            n.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    protected override void DeselectItem()
    {
        base.DeselectItem();

        foreach (SimonNode n in nodeGroup)
        {
            n.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        if (isWaiting)
            yield break;

        isWaiting = true;

        yield return new WaitForSeconds(time);

        SendSequence();

        isWaiting = false;
    }

    void SendSequence()
    {
        for (int i = 0; i < seqNumber; i++)
        {
            nodeGroup[Sequence[i]].Glow();
        }
    }

    public void RecieveButtonPress(SimonNode n)
    {        
        int press = ToCorrect(n.getIndex());

        if(press == Sequence[countNumber]) // If the button pressed is the correct one
        {
            Debug.Log("CORRECT!");
            // Increase number of whic the button should be pressed
            countNumber++;
            // Check if the Module has been completed and if it hasnt then move onto the next number in the sequence
            checkComplete();    
        }

        else
        {
            Debug.Log("INCORRECT!");
            resetSequence();
        }
    }

    void checkComplete()
    {
        if(seqNumber == sequenceLength && countNumber == seqNumber)
        {
            CompleteModule();
        }

        if(countNumber == seqNumber)
        {
            Debug.Log("Sequence Complete!");
            seqNumber++;
            countNumber = 0;
        }
    }

    private void resetSequence()
    {
        seqNumber = 1;
        countNumber = 0;
    }

    void SelectState()
    {
        int s = Random.Range(0, 3);
        //print("state number: " + s);
        switch(s)
        {
            case 0:
                {
                    State = "CW";
                    break;
                }
            case 1:
                {
                    State = "OP";
                    break;
                }
            case 2:
                {
                    State = "AC";
                     break;
                }
            default:
                {
                    State = "Default";
                    break;
                }
        }
        print("State is: " + State);
    }

    int ToCorrect(int n)
    {
        switch(State)
        {
            case "CW":
                {
                    n -= 1;
                    break;
                }
            case "OP":
                {
                    n += 2;
                    break;
                }
            case "AC":
                {
                    n += 1;
                    break;
                }
            default:
                {
                    Debug.Log("Error! Couldn't Find State: " + State);
                    break;
                }
        }
        if (n > 3)
            n -= 4;
        if (n < 0)
            n += 4;

        return n;
    }

    void CreateSequence()
    {
        Sequence = new int[sequenceLength];
        for(int i = 0; i < sequenceLength; i++)
        {
            int n = Random.Range(0, 4);
            Sequence[i] = n;
            //Debug.Log("Sequence number " + i + " is " + n);
        }
    }
}
