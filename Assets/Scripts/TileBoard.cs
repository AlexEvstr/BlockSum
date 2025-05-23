using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{

    private Vector2 touchStart;
    private Vector2 touchEnd;
    private float swipeThreshold = 50f;
    public GameManager gameManager;

    public Tile tilePrefab;

    public TileState[] tileStates;

    private TileGrid grid;

    private List<Tile> tiles;

    private bool waiting;

    private PlayerController playerInput;
    private Vector2 controllerMovement;

    private EmotionDisplay _emotionDisplay;
    [SerializeField] private GameAudioManager _gameAudioManager;

    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>();
        playerInput = new PlayerController();
    }

    private void Start()
    {
        _emotionDisplay = GetComponent<EmotionDisplay>();  
    }

    private void Update()
    {
        if (waiting)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEnd = touch.position;
                HandleSwipe();
            }
        }
    }

    private void HandleSwipe()
    {
        Vector2 swipeDelta = touchEnd - touchStart;

        if (swipeDelta.magnitude < swipeThreshold)
            return;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            if (swipeDelta.x > 0)
            {
                MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
            }
            else
            {
                MoveTiles(Vector2Int.left, 1, 1, 0, 1);
            }
        }
        else
        {
            if (swipeDelta.y > 0)
            {
                MoveTiles(Vector2Int.up, 0, 1, 1, 1);
            }
            else
            {
                MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
            }
        }
    }

    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    { 
        bool changed = false;

        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);

                if (cell.occupied)
                {
                    changed |= MoveTile(cell.tile, direction);
                }
            }
        }

        if (changed)
        {
            _gameAudioManager.PlayMoveSound();
            StartCoroutine(WaitForChanges());
        }
    }
    
    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;

        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);
 
        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                if (CanMerge(tile, adjacent.tile))
                {
                    Merge(tile, adjacent.tile);
                    return true;
                }

                break;
            }

            newCell = adjacent;

            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    private bool CanMerge(Tile a, Tile b)
    {
        return a.number == b.number && !b.isLocked;
    }

    private void Merge(Tile a, Tile b)
    {
        tiles.Remove(a);

        a.Merge(b.cell);

        int index = Math.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length - 1);

        int number = b.number * 2;

        b.SetState(tileStates[index], number);
        gameManager.IncreaseScore(number);
        _gameAudioManager.PlayMergeSound();
        if (number >= 32)
        {
            _emotionDisplay.ShowNextEmotion();
            _gameAudioManager.PlayEmotionSound();
        }
    }

    public void CreateTile()
    {
        Tile newTile = Instantiate(tilePrefab, grid.transform);

        newTile.SetState(tileStates[0], 2);

        newTile.Spawn(grid.GetRandomEmptyCell());

        tiles.Add(newTile);
    }

    private int IndexOf(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (state == tileStates[i])
            {
                return i;
            }
        }

        return -1;
    }

    public void ClearBoard()
    {
        foreach (var cell in grid.cells)
        {
            cell.tile = null;
        }

        foreach (var tile in tiles)
        {
            Destroy(tile.gameObject);
        }

        tiles.Clear();
    }

    private IEnumerator WaitForChanges()
    {
        waiting = true;
        yield return new WaitForSeconds(0.1f);

        waiting = false;

        foreach (var tile in tiles)
        {
            tile.isLocked = false;
        }

        if (tiles.Count != grid.size)
        {
            CreateTile();
        }

        if (CheckForGameOver())
        {
            gameManager.GameOver();
        }
    }

    private bool CheckForGameOver()
    {
        if (tiles.Count != grid.size)
        {
            return false;
        }

        foreach (var tile in tiles)
        {
            TileCell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            TileCell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            TileCell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            TileCell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);

            if (up != null && CanMerge(tile, up.tile))
            {
                return false;
            }
            if (down != null && CanMerge(tile, down.tile))
            {
                return false;
            }

            if (left != null && CanMerge(tile, left.tile))
            {
                return false;
            }

            if (right != null && CanMerge(tile, right.tile))
            {
                return false;
            }
        }
        return true;
    }
}