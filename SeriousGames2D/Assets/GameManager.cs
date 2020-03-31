using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;

    public void EndGame(bool victory)
    {
        if(gameEnded == false)
        {
            gameEnded = true;
        }
    }
}
