using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tiles;

    private List<GameObject> vertTiles = new List<GameObject>();
    private List<GameObject> tempTiles = new List<GameObject>();

    [SerializeField]
    private int horizontalRows;
    [SerializeField]
    private int verticalRows;

    /// <summary>
    /// Generates a Matrix of tiles based on the given values
    /// </summary>
    public void GenerateFloor()
    {
        ClearObjects();

        GameObject tile = CreateRandomTile(Vector3.zero, vertTiles);

        CreateVertRow(vertTiles[0]);

        for (int i = 0; i < verticalRows; i++)
        {
            CreateHorLines(vertTiles[i]);
            tempTiles.Clear();
        }
    }

    /// <summary>
    /// Destroy all Tiles
    /// </summary>
    public void ClearObjects()
    {
        for (int i = this.transform.childCount; i > 0; i--)
            DestroyImmediate(this.transform.GetChild(0).gameObject);

        vertTiles.Clear();
    }

    /// <summary>
    /// Create tiles on the Y axis until the wanted amount of vertical tiles is reached
    /// </summary>
    /// <param name="_previousTile">Represents the previously created tile</param>
    private void CreateVertRow(GameObject _previousTile)
    {
        Vector3 offset = CalculateOffsetY(_previousTile); //get offset

        GameObject tile = CreateRandomTile(offset, vertTiles); //create tile

        if (vertTiles.Count < verticalRows) //create tiles untl the wanted amount is reached
            CreateVertRow(tile);
    }

    /// <summary>
    /// Create tiles on the x axis until the wanted amount of honrizontal tiles is reached
    /// </summary>
    /// <param name="_vertRow">Represents the Y position of the Horizontal Line</param>
    private void CreateHorLines(GameObject _vertRow)
    {
        Vector3 offset = CalculateOffsetX(_vertRow); //get offset

        GameObject tile = CreateRandomTile(offset, tempTiles); //create tile

        if (tempTiles.Count < horizontalRows - 1) //create tiles until the wanted amount is reached
            CreateHorLines(tile);
    }

    /// <summary>
    /// Calculate the X Offset to the previous GameObject based on its size
    /// </summary>
    /// <param name="_prevObj">The Object next to it</param>
    /// <returns></returns>
    private Vector3 CalculateOffsetX(GameObject _prevObj)
    {
        Vector3 prevPos = _prevObj.transform.position; //get position of the previous object
        BoxCollider prevCol = _prevObj.GetComponent<BoxCollider>(); //get collider of the previous object

        return new Vector3(prevPos.x + prevCol.size.x, prevPos.y, prevPos.z); //return the position next to the previous object based on the size and position
    }

    /// <summary>
    /// Calculate the Y Offset to the previous GameObject based on its size
    /// </summary>
    /// <param name="_prevObj">The Object under it</param>
    /// <returns></returns>
    private Vector3 CalculateOffsetY(GameObject _prevObj)
    {
        Vector3 prevPos = _prevObj.transform.position; //get position of the previous object
        BoxCollider prevCol = _prevObj.GetComponent<BoxCollider>(); //get collider of the previous object

        return new Vector3(prevPos.x, prevPos.y, prevPos.z + prevCol.size.x); //return the position above the previous object based on the size and position
    }

    /// <summary>
    /// Create a random tile from the Tiles array
    /// </summary>
    /// <param name="_offset">Offset to the previous Tile</param>
    /// <param name="_addToList">The List the tile should be added to</param>
    /// <returns></returns>
    private GameObject CreateRandomTile(Vector3 _offset, List<GameObject> _addToList)
    {
        int rnd = Random.Range(0, tiles.Length);

        GameObject tile = Instantiate(tiles[rnd], _offset, tiles[rnd].transform.rotation); //Create a random Tile
        _addToList.Add(tile); //add that tile to a list
        tile.transform.SetParent(transform); //set that tile as a child of the gameobject

        return tile; //return tile
    }
}
