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
    //bool blackTilesAreFull = false;
    //bool whiteTilesAreFull = false;
    //bool canCreateApples = true;

    private int blackValue;
    private int whiteValue;

    Vector3 randomPositionFruit;

    int points;

    public int randomIndex;
    public GameObject fullPanel;

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
            Camera.main.transform.position = new Vector3(value / 2, value / 2, -10);

            for (int y = value; y > 0; y--)
            {
                if (y % 2 == 0) //si és parell
                {
                    blackValue = value;
                    whiteValue = value - 1;
                }
                else
                {
                    blackValue = value - 1;
                    whiteValue = value;
                }

                for (int x = blackValue; x > 0; x -= 2) //mentre i sigui major que 0, anam baixant 2
                {
                    //creació de sa tile black
                    GameObject blackTile = new GameObject("black Tile");
                    SpriteRenderer blackSpriteRenderer = blackTile.AddComponent<SpriteRenderer>();
                    blackSpriteRenderer.sprite = blackTileSprite;

                    //set tile position
                    blackTile.transform.position = new Vector3(x, y, 0);

                    //save tile position into the list
                    blackTilePositionsList.Add(blackTile.transform.position);

                    //Debug.Log(blackTile.transform.position);
                }
                for (int x = whiteValue; x > 0; x -= 2)
                {
                    //creació de sa tile white
                    GameObject whiteTile = new GameObject("white Tile");
                    SpriteRenderer whiteSpriteRenderer = whiteTile.AddComponent<SpriteRenderer>();
                    whiteSpriteRenderer.sprite = whiteTileSprite;

                    //set tile position
                    whiteTile.transform.position = new Vector3(x, y, 0);

                    //save tile position into the list
                    whiteTilePositionsList.Add(whiteTile.transform.position);

                    //Debug.Log(whiteTile.transform.position)
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
            //Renderer appleRenderer = appleFruitGO.AddComponent<Renderer>();
            appleSpriteRenderer.sortingOrder = 10;
            appleFruitGO.transform.position = randomPositionFruit;

            if (isOnTheWhiteTileVariable == true)
            {
                points++;
            }
            else
            {
                points += 5;
            }

            Debug.Log(points);

            possiblePositionsToInstance.RemoveAt(randomIndex);

            yield return new WaitForSeconds(1);
        }

        fullPanel.SetActive(true);
        //activam es panel de que tot està plè
    }


    public void RandomPositionOfTheList()
    {
        //choose the random index
        randomIndex = Random.Range(0, possiblePositionsToInstance.Count);

        //set the apple position to the random pos
        randomPositionFruit = possiblePositionsToInstance[randomIndex];

        isOnTheWhiteTileVariable = IsOnTheWhiteTile();
    }

    public bool IsOnTheWhiteTile()
    {
        //si sa posició de sa poma està a sa llista white return true, else, return false
        if (whiteTilePositionsList.Contains(randomPositionFruit))
        {
            return true;
        }
        return false;
    }

}
