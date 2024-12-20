using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 8;
    public int height = 4;

    public float cellSpacingX = 1.5f; // 가로 간격
    public float cellSpacingY = 1.5f; // 세로 간격

    public GameObject gridCellPrefab;
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[,] gridCells;

    private void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];
        Vector2 centerOffset = new Vector2((width - 1) * cellSpacingX / 2, (height - 1) * cellSpacingY / 2);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 셀 위치를 간격에 맞춰 설정
                Vector2 gridPosition = new Vector2(x * cellSpacingX, y * cellSpacingY);
                Vector2 spawnPosition = gridPosition - centerOffset; // 중앙 오프셋 적용

                GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
                gridCell.transform.SetParent(transform);

                // `RectTransform` 크기 조정 (UI 요소일 경우)
                RectTransform rectTransform = gridCell.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.sizeDelta = new Vector2(cellSpacingX, cellSpacingY); // 직사각형 크기로 설정
                }

                gridCell.GetComponent<GridCell>().gridIndex = new Vector2(x, y);
                gridCells[x, y] = gridCell;
            }
        }
    }


    public bool AddObjectToGrid(GameObject obj, Vector2 gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height)
        {
            GridCell cell = gridCells[(int)gridPosition.x, (int)gridPosition.y].GetComponent<GridCell>();

            if (cell.cellFull) return false;
            else
            {
                GameObject newObj = Instantiate(obj, GetComponent<Transform>().position, Quaternion.identity);
                newObj.transform.SetParent(transform);
                gridObjects.Add(newObj);
                cell.cellFull = true;
                return true;
            }
        }
        else return false;
    }
}
