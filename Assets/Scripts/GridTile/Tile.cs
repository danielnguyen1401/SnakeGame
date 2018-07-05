using UnityEngine;

// abstract: it means can not implement by its self
public abstract class Tile : MonoBehaviour
{
    protected Vector2Int gridPos;
    public Vector2Int GridPos { get { return gridPos; } }

    public void MoveToGrid(int x, int y)
    {
        float xf = x;
        float yf = y;

        transform.position = new Vector3(
            GridManager.Instance.StartPoint.x + (xf / GridManager.Instance.GridSize),
            GridManager.Instance.StartPoint.y + (yf / GridManager.Instance.GridSize),
            transform.position.z
        );
        gridPos = new Vector2Int(x, y);
    }

    public void MoveToTile(Tile tile)
    {
        MoveToGrid(tile.gridPos.x, tile.gridPos.y);
    }
}