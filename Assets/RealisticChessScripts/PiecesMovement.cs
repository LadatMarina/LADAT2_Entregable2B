using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PiecesMovement : MonoBehaviour
{
    #region CHESSBOARD CREATION VARIABLES
    private int value = 8;
    public Sprite blackTileSprite;
    public Sprite whiteTileSprite;
    public Sprite queenSprite;

    public List<Vector3> blackTilePositionsList;
    public List<Vector3> whiteTilePositionsList;
    public List<Vector3> possiblePositionsToInstance;

    bool chessboardIsCreated;

    private int blackValue;
    private int whiteValue;

    public GameObject tilesParent;

    #endregion

    private Vector2Int CurrentCasella;
    private Vector2Int DestinationCasella;

    private void Start()
    {
        blackTilePositionsList = new List<Vector3>();
        whiteTilePositionsList = new List<Vector3>();
        possiblePositionsToInstance = new List<Vector3>();

        CreateChessBoard();

        //CREAR SA REINA AMB ES SEU SPRITE I TAL...

            //inicializam sa reina al seu lloc per defecte.
        CurrentCasella = new Vector2Int(5,1); 
    }
    private void Update()
    {
        //LÒGICA PER EMPLEAR
        /*Vector2Int vector = SubstractionVector(DestinationCasella, CurrentCasella);
        if ((vector.x == vector.y) || ((vector.x != 0) && (vector.y == 0)) || ((vector.x == 0) && (vector.y !=0)))
        {
            //se pot moure
        }
        else
        {
            //no se pot moure
        }*/
    }
    private void CreateChessBoard()
    {

        if (value != 0 && chessboardIsCreated == false) //si hi ha un valor de tampany per es chessboard...
        {
            //set camera in the center of the chessBoard
            if (value <= 8)
            {
                Camera.main.transform.position = new Vector3(value / 2f, value / 2f, -10);
            }
            else
            {
                Camera.main.transform.position = new Vector3(value / 2f, value / 2f, -30);
            }

            for (int y = value; y > 0; y--)
            {
                //if it's pair start with black tile, else, with 
                if (y % 2 == 0)
                {
                    blackValue = value;
                    whiteValue = value - 1;
                }
                else
                {
                    blackValue = value - 1;
                    whiteValue = value;
                }

                for (int x = blackValue; x > 0; x -= 2)
                {
                    //BLACK TILE
                    GameObject blackTile = new GameObject("black Tile");
                    SpriteRenderer blackSpriteRenderer = blackTile.AddComponent<SpriteRenderer>();
                    blackSpriteRenderer.sprite = blackTileSprite;

                    //set tile position
                    blackTile.transform.position = new Vector3(x, y, 0);

                    //save tile position into the list
                    blackTilePositionsList.Add(blackTile.transform.position);

                    //afegir-li sa component button per fer s'onClick --> m'agradaria fer lo de s'interactable, si tenc temps, ho cercaré, sinó, no fa falta


                    //set the GO to an empty parent to organize
                    blackTile.transform.SetParent(tilesParent.transform);
                }
                for (int x = whiteValue; x > 0; x -= 2)
                {
                    //WHITE TILE
                    GameObject whiteTile = new GameObject("white Tile");
                    SpriteRenderer whiteSpriteRenderer = whiteTile.AddComponent<SpriteRenderer>();
                    whiteSpriteRenderer.sprite = whiteTileSprite;

                    //set tile position
                    whiteTile.transform.position = new Vector3(x, y, 0);

                    //save tile position into the list
                    whiteTilePositionsList.Add(whiteTile.transform.position);

                    //afegir-li sa component button per fer s'onClick --> m'agradaria fer lo de s'interactable, si tenc temps, ho cercaré, sinó, no fa falta


                    //set the GO to an empty parent to organize
                    whiteTile.transform.SetParent(tilesParent.transform);

                }
                chessboardIsCreated = true;
            }

        }
    }

    

    private Vector2Int SubstractionVector(Vector2Int destination, Vector2Int currentPosition)
    {
        return new Vector2Int(destination.x - currentPosition.x, destination.y - currentPosition.y);
    }

    

}
