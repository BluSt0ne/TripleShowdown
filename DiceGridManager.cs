using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGridManager : MonoBehaviour
{
    public int width = 8;              // 그리드의 가로 크기
    public int height = 4;             // 그리드의 세로 크기
    public float cellSpacingX = 1.5f;  // 셀 간 가로 간격
    public float cellSpacingY = 1.5f;  // 셀 간 세로 간격

    public GameObject gridCellPrefab;  // 그리드 셀 프리팹
    public List<GameObject> gridObjects = new List<GameObject>();  // 그리드에 올려진 객체 리스트
    public GameObject[,] gridCells;    // 그리드 셀 배열

    public Vector2 gridOffset = new Vector2(0, -50);  // 그리드 이동을 위한 오프셋

    private void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];

        // 그리드의 중앙을 기준으로 오프셋을 설정
        Vector2 centerOffset = new Vector2((width - 1) * cellSpacingX / 2, (height - 1) * cellSpacingY / 2);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 셀 위치를 간격에 맞춰 설정 (gridOffset을 추가하여 위치 이동)
                Vector2 gridPosition = new Vector2(x * cellSpacingX, y * cellSpacingY);
                Vector2 spawnPosition = gridPosition - centerOffset + gridOffset; // gridOffset을 추가하여 이동시킴

                GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
                gridCell.transform.SetParent(transform);

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

    // 그리드를 이동시키는 메서드
    public void MoveGrid(Vector2 newOffset)
    {
        gridOffset = newOffset;
        CreateGrid();  // 이동 후 새로 그리드를 생성
    }
}
