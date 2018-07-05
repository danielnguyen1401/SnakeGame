﻿public class SnakeTile : Tile
{
    public void MoveLeft()
    {
        MoveToGrid(gridPos.x - 1, gridPos.y);
    }

    public void MoveRight()
    {
        MoveToGrid(gridPos.x + 1, gridPos.y);
    }

    public void MoveUp()
    {
        MoveToGrid(gridPos.x, gridPos.y + 1);
    }

    public void MoveDown()
    {
        MoveToGrid(gridPos.x, gridPos.y - 1);
    }
}