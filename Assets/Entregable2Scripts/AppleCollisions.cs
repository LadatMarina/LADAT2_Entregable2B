using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private ChessBoard
        chessBoardScript;

    void Start()
    {
        chessBoardScript = FindObjectOfType<ChessBoard>();
    }

    private void OnMouseDown()
    {
        chessBoardScript.possiblePositionsToInstance.Add(transform.position);
        Destroy(this);
    }
}
