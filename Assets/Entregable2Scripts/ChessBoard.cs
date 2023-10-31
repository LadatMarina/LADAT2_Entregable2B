using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public int value;
    public Sprite blackTileSprite;
    public Sprite whiteTileSprite;
    public Sprite appleSprite;

    public List<Vector3> blackTilePositionsList;
    public List<Vector3> whiteTilePositionsList;
    public List<Vector3> possiblePositionsToInstance;

    bool chessboardIsCreated;
    bool isOnTheWhiteTileVariable;

    private int blackValue;
    private int whiteValue;

    Vector3 randomPositionFruit;

    int points;

    public int randomIndex;
    public GameObject fullPanel;

    public GameObject appleParent;
    public GameObject tilesParent;

    private void Start()
    {
        blackTilePositionsList = new List<Vector3>();
        whiteTilePositionsList = new List<Vector3>();
        possiblePositionsToInstance = new List<Vector3>();

        fullPanel.SetActive(false);
        CreateChessBoard();
        AddingListFunciton();

        StartCoroutine(InstanceApples());
    }

    private void CreateChessBoard()
    {

        if (value != 0 && chessboardIsCreated == false) //si hi ha un valor de tampany per es chessboard...
        {
            //set camera in the center of the chessBoard
            if(value <= 8)
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

                    //set the GO to an empty parent to organize
                    whiteTile.transform.SetParent(tilesParent.transform);

                }
                chessboardIsCreated = true;
            }

        }
    }

    private void AddingListFunciton()
    {
        possiblePositionsToInstance.AddRange(blackTilePositionsList);
        possiblePositionsToInstance.AddRange(whiteTilePositionsList);
    }

    private IEnumerator InstanceApples()
    {
        for (int i = possiblePositionsToInstance.Count; i > 0; i--)
        {
            RandomPositionOfTheList();

            //create the apple
            GameObject appleFruitGO = new GameObject("apple Fruit");
            SpriteRenderer appleSpriteRenderer = appleFruitGO.AddComponent<SpriteRenderer>();
            appleSpriteRenderer.sprite = appleSprite;

            //Sorting order --> in top of the tiles
            appleSpriteRenderer.sortingOrder = 2;

            //set the apple on the random tile
            appleFruitGO.transform.position = randomPositionFruit;
            
            //set the GO to an empty parent to organize
            appleFruitGO.transform.SetParent(appleParent.transform);

            //if the apple is on the white tile +1 point , if is on the black +5
            if (IsOnTheWhiteTile() == true)
            {
                points++;
            }
            else
            {
                points += 5;
            }

            Debug.Log(points);

            //remove the position where the apple has been created
            possiblePositionsToInstance.RemoveAt(randomIndex);

            yield return new WaitForSeconds(1);
        }

        fullPanel.SetActive(true);
        //show the panel to indicate all the tiles are full
    }


    public void RandomPositionOfTheList()
    {
        //choose the random index
        randomIndex = Random.Range(0, possiblePositionsToInstance.Count);

        //set the apple position to the random pos
        randomPositionFruit = possiblePositionsToInstance[randomIndex];
    }

    public bool IsOnTheWhiteTile()
    {
        //if the apple position is into the white list return true, else, return false
        if (whiteTilePositionsList.Contains(randomPositionFruit))
        {
            return true;
        }
        return false;
    }

}
