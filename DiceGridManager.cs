using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGridManager : MonoBehaviour
{
    public int width = 8;              // �׸����� ���� ũ��
    public int height = 4;             // �׸����� ���� ũ��
    public float cellSpacingX = 1.5f;  // �� �� ���� ����
    public float cellSpacingY = 1.5f;  // �� �� ���� ����

    public GameObject gridCellPrefab;  // �׸��� �� ������
    public List<GameObject> gridObjects = new List<GameObject>();  // �׸��忡 �÷��� ��ü ����Ʈ
    public GameObject[,] gridCells;    // �׸��� �� �迭

    public Vector2 gridOffset = new Vector2(0, -50);  // �׸��� �̵��� ���� ������

    private void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];

        // �׸����� �߾��� �������� �������� ����
        Vector2 centerOffset = new Vector2((width - 1) * cellSpacingX / 2, (height - 1) * cellSpacingY / 2);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // �� ��ġ�� ���ݿ� ���� ���� (gridOffset�� �߰��Ͽ� ��ġ �̵�)
                Vector2 gridPosition = new Vector2(x * cellSpacingX, y * cellSpacingY);
                Vector2 spawnPosition = gridPosition - centerOffset + gridOffset; // gridOffset�� �߰��Ͽ� �̵���Ŵ

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

    // �׸��带 �̵���Ű�� �޼���
    public void MoveGrid(Vector2 newOffset)
    {
        gridOffset = newOffset;
        CreateGrid();  // �̵� �� ���� �׸��带 ����
    }
}
