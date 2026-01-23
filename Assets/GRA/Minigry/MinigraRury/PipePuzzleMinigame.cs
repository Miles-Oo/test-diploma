using System;
using System.Collections.Generic;
using UnityEngine;

public class PipePuzzleUIMinigame : MonoBehaviour
{
    [Header("Grid")]
    public int width = 6;
    public int height = 6;

    [Header("UI")]
    [SerializeField] private RectTransform gridParent;   // GridContainer
    [SerializeField] private PipeTileUI tilePrefab;      // prefab: Image + PipeTileUI
    [SerializeField] private Vector2 cellSize = new Vector2(36, 36);
    [SerializeField] private Vector2 spacing = new Vector2(2, 2);

    [Header("Sprites")]
    [SerializeField] private Sprite straightSprite;
    [SerializeField] private Sprite elbowSprite;
    [SerializeField] private Sprite tJunctionSprite;
    [SerializeField] private Sprite crossSprite;

    [Header("Generation")]
    public bool lockStartEnd = true;
    public int seed = 0; // 0 = losowo
    [Range(0f, 1f)] public float scrambleChance = 1f; // 1 = zawsze miesza (poza start/end)
    public int ensureMinDegree = 2;                    // min stopień połączeń (usuwa "liście")

    private PipeTileUI[,] _tiles;
    private int[,] _desiredMasks;

    private Vector2Int _startPos;
    private Vector2Int _endPos;

    private System.Random _rng;

    public Action OnWin;

    public void StartRun()
    {
        GenerateNewBoard();
        enabled = true;
    }

    public void StopRun()
    {
        enabled = false;
        ClearOld();
    }

    public void GenerateNewBoard()
    {
        if (gridParent == null || tilePrefab == null)
        {
            Debug.LogError("PipePuzzleUIMinigame: gridParent albo tilePrefab jest NULL.");
            return;
        }

        ClearOld();

        int usedSeed = (seed == 0) ? UnityEngine.Random.Range(int.MinValue, int.MaxValue) : seed;
        _rng = new System.Random(usedSeed);

        _tiles = new PipeTileUI[width, height];
        _desiredMasks = new int[width, height];

        // 1) Generuj drzewo (gwarant spójności)
        BuildSpanningTree();

        // 2) Dodaj połączenia, żeby nie było stopnia 1 (czyli EndCap niepotrzebny)
        EnsureMinDegreeAll(ensureMinDegree);

        // 3) Wybierz start/end na krawędzi (daleko)
        PickStartEndOnPerimeterFar();

        // 4) Stwórz kafelki i poscrambluj
        CreateTilesFromMasks();

        // 5) Highlight + win check
        UpdateHighlights();
        CheckWin();
    }

    private void ClearOld()
    {
        if (gridParent == null) return;
        for (int i = gridParent.childCount - 1; i >= 0; i--)
            Destroy(gridParent.GetChild(i).gameObject);
    }

    // ---------- GENERATOR: DFS spanning tree ----------
    private void BuildSpanningTree()
    {
        bool[,] visited = new bool[width, height];
        var stack = new Stack<Vector2Int>();

        var start = new Vector2Int(_rng.Next(0, width), _rng.Next(0, height));
        visited[start.x, start.y] = true;
        stack.Push(start);

        while (stack.Count > 0)
        {
            var cur = stack.Peek();
            var neighbors = GetUnvisitedNeighbors(cur, visited);

            if (neighbors.Count == 0) { stack.Pop(); continue; }

            var next = neighbors[_rng.Next(neighbors.Count)];
            AddConnection(cur, next);

            visited[next.x, next.y] = true;
            stack.Push(next);
        }
    }

    private List<Vector2Int> GetUnvisitedNeighbors(Vector2Int c, bool[,] visited)
    {
        var list = new List<Vector2Int>(4);
        int x = c.x, y = c.y;

        if (y + 1 < height && !visited[x, y + 1]) list.Add(new Vector2Int(x, y + 1));
        if (x + 1 < width  && !visited[x + 1, y]) list.Add(new Vector2Int(x + 1, y));
        if (y - 1 >= 0     && !visited[x, y - 1]) list.Add(new Vector2Int(x, y - 1));
        if (x - 1 >= 0     && !visited[x - 1, y]) list.Add(new Vector2Int(x - 1, y));

        return list;
    }

    private void AddConnection(Vector2Int a, Vector2Int b)
    {
        int ax = a.x, ay = a.y;
        int bx = b.x, by = b.y;

        if (bx == ax && by == ay + 1) { _desiredMasks[ax, ay] |= PipeTileUI.UP;    _desiredMasks[bx, by] |= PipeTileUI.DOWN; }
        else if (bx == ax + 1 && by == ay) { _desiredMasks[ax, ay] |= PipeTileUI.RIGHT; _desiredMasks[bx, by] |= PipeTileUI.LEFT; }
        else if (bx == ax && by == ay - 1) { _desiredMasks[ax, ay] |= PipeTileUI.DOWN;  _desiredMasks[bx, by] |= PipeTileUI.UP; }
        else if (bx == ax - 1 && by == ay) { _desiredMasks[ax, ay] |= PipeTileUI.LEFT;  _desiredMasks[bx, by] |= PipeTileUI.RIGHT; }
    }

    private bool AreConnected(Vector2Int a, Vector2Int b)
    {
        int ax = a.x, ay = a.y;
        int bx = b.x, by = b.y;

        if (bx == ax && by == ay + 1) return (_desiredMasks[ax, ay] & PipeTileUI.UP) != 0;
        if (bx == ax + 1 && by == ay) return (_desiredMasks[ax, ay] & PipeTileUI.RIGHT) != 0;
        if (bx == ax && by == ay - 1) return (_desiredMasks[ax, ay] & PipeTileUI.DOWN) != 0;
        if (bx == ax - 1 && by == ay) return (_desiredMasks[ax, ay] & PipeTileUI.LEFT) != 0;

        return false;
    }

    private int BitCount(int mask)
    {
        int c = 0;
        if ((mask & PipeTileUI.UP) != 0) c++;
        if ((mask & PipeTileUI.RIGHT) != 0) c++;
        if ((mask & PipeTileUI.DOWN) != 0) c++;
        if ((mask & PipeTileUI.LEFT) != 0) c++;
        return c;
    }

    private int Degree(int x, int y) => BitCount(_desiredMasks[x, y]);

    // Usuwa liście: każdy kafelek ma minDegree połączeń (dodaje nowe krawędzie)
    private void EnsureMinDegreeAll(int minDegree, int maxPasses = 10)
    {
        for (int pass = 0; pass < maxPasses; pass++)
        {
            bool changed = false;

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                while (Degree(x, y) < minDegree)
                {
                    if (!TryAddExtraEdge(new Vector2Int(x, y)))
                        break;

                    changed = true;
                }
            }

            if (!changed) break;
        }
    }

    private bool TryAddExtraEdge(Vector2Int cell)
    {
        var candidates = new List<Vector2Int>(4);
        int x0 = cell.x, y0 = cell.y;

        if (y0 + 1 < height) candidates.Add(new Vector2Int(x0, y0 + 1));
        if (x0 + 1 < width)  candidates.Add(new Vector2Int(x0 + 1, y0));
        if (y0 - 1 >= 0)     candidates.Add(new Vector2Int(x0, y0 - 1));
        if (x0 - 1 >= 0)     candidates.Add(new Vector2Int(x0 - 1, y0));

        // shuffle
        for (int i = candidates.Count - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            (candidates[i], candidates[j]) = (candidates[j], candidates[i]);
        }

        foreach (var nb in candidates)
        {
            if (!AreConnected(cell, nb))
            {
                AddConnection(cell, nb);
                return true;
            }
        }

        return false;
    }

    // ---------- Start/End ----------
    private void PickStartEndOnPerimeterFar()
    {
        var per = new List<Vector2Int>();

        for (int x = 0; x < width; x++)
        {
            per.Add(new Vector2Int(x, 0));
            per.Add(new Vector2Int(x, height - 1));
        }
        for (int y = 1; y < height - 1; y++)
        {
            per.Add(new Vector2Int(0, y));
            per.Add(new Vector2Int(width - 1, y));
        }

        _startPos = per[_rng.Next(per.Count)];

        int bestDist = -1;
        Vector2Int best = _startPos;

        foreach (var p in per)
        {
            if (p == _startPos) continue;
            int d = Mathf.Abs(p.x - _startPos.x) + Mathf.Abs(p.y - _startPos.y);
            if (d > bestDist) { bestDist = d; best = p; }
        }

        _endPos = best;
    }

    // ---------- UI create ----------
    private void CreateTilesFromMasks()
    {
        float gridW = width * cellSize.x + (width - 1) * spacing.x;
        float gridH = height * cellSize.y + (height - 1) * spacing.y;

        float offsetX = -gridW / 2f + cellSize.x / 2f;
        float offsetY = -gridH / 2f + cellSize.y / 2f;

        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
        {
            var tile = Instantiate(tilePrefab, gridParent);
            tile.SetManager(this);

            var rt = tile.GetComponent<RectTransform>();
            rt.sizeDelta = cellSize;

            float px = offsetX + x * (cellSize.x + spacing.x);
            float py = offsetY + y * (cellSize.y + spacing.y);
            rt.anchoredPosition = new Vector2(px, py);

            bool isStart = (x == _startPos.x && y == _startPos.y);
            bool isEnd   = (x == _endPos.x && y == _endPos.y);

            int desiredMask = _desiredMasks[x, y];
            (PipeTileUI.PipeType type, int rotCorrect) = MaskToTypeAndRotation(desiredMask);

            bool locked = lockStartEnd && (isStart || isEnd);

            // Start/End: zawsze poprawna rotacja (żeby dało się przejść bez obracania)
            int rotFinal = rotCorrect;

            // reszta: scramble
            if (!locked && UnityEngine.Random.value <= scrambleChance)
                rotFinal = UnityEngine.Random.Range(0, 4);

            tile.Setup(type, rotFinal, locked, isStart, isEnd, SpriteFor(type));
            _tiles[x, y] = tile;
        }
    }

    private Sprite SpriteFor(PipeTileUI.PipeType t) => t switch
    {
        PipeTileUI.PipeType.Straight  => straightSprite,
        PipeTileUI.PipeType.Elbow     => elbowSprite,
        PipeTileUI.PipeType.TJunction => tJunctionSprite,
        PipeTileUI.PipeType.Cross     => crossSprite,
        _ => null
    };

    private (PipeTileUI.PipeType type, int rotSteps) MaskToTypeAndRotation(int mask)
    {
        int degree = BitCount(mask);

        if (degree >= 4) return (PipeTileUI.PipeType.Cross, 0);

        if (degree == 3)
        {
            int missing = (~mask) & (PipeTileUI.UP | PipeTileUI.RIGHT | PipeTileUI.DOWN | PipeTileUI.LEFT);
            // baza TJunction: brak LEFT
            if ((missing & PipeTileUI.LEFT) != 0) return (PipeTileUI.PipeType.TJunction, 0);
            if ((missing & PipeTileUI.UP) != 0) return (PipeTileUI.PipeType.TJunction, 1);
            if ((missing & PipeTileUI.RIGHT) != 0) return (PipeTileUI.PipeType.TJunction, 2);
            return (PipeTileUI.PipeType.TJunction, 3);
        }

        if (degree == 2)
        {
            bool vertical = (mask & PipeTileUI.UP) != 0 && (mask & PipeTileUI.DOWN) != 0;
            bool horizontal = (mask & PipeTileUI.LEFT) != 0 && (mask & PipeTileUI.RIGHT) != 0;

            if (vertical) return (PipeTileUI.PipeType.Straight, 0);
            if (horizontal) return (PipeTileUI.PipeType.Straight, 1);

            // Elbow baza: Up+Right
            if (mask == (PipeTileUI.UP | PipeTileUI.RIGHT)) return (PipeTileUI.PipeType.Elbow, 0);
            if (mask == (PipeTileUI.RIGHT | PipeTileUI.DOWN)) return (PipeTileUI.PipeType.Elbow, 1);
            if (mask == (PipeTileUI.DOWN | PipeTileUI.LEFT)) return (PipeTileUI.PipeType.Elbow, 2);
            return (PipeTileUI.PipeType.Elbow, 3);
        }

        // degree==1 nie powinno się zdarzyć po EnsureMinDegreeAll, ale na wszelki wypadek:
        // Dopychamy do Straight jako fallback (wizualnie i tak będzie "prawie")
        Debug.LogWarning("MaskToTypeAndRotation: degree==1 (nie powinno). Rozważ zwiększyć ensureMinDegree.");
        return (PipeTileUI.PipeType.Straight, 0);
    }

    // ---------- rotate/hightlight/win ----------
    public void OnTileRotated()
    {
        UpdateHighlights();
        CheckWin();
    }

    private void CheckWin()
    {
        if (HasPath(_startPos, _endPos))
        {
            Debug.Log("PIPE UI WIN!");
            OnWin?.Invoke();
        }
    }

    private bool HasPath(Vector2Int start, Vector2Int goal)
    {
        bool[,] visited = new bool[width, height];
        var q = new Queue<Vector2Int>();

        q.Enqueue(start);
        visited[start.x, start.y] = true;

        while (q.Count > 0)
        {
            var p = q.Dequeue();
            if (p == goal) return true;

            var t = _tiles[p.x, p.y];
            int mask = t.CurrentMask();

            TryEnqueue(p, new Vector2Int(p.x, p.y + 1), PipeTileUI.UP, PipeTileUI.DOWN, mask, visited, q);
            TryEnqueue(p, new Vector2Int(p.x + 1, p.y), PipeTileUI.RIGHT, PipeTileUI.LEFT, mask, visited, q);
            TryEnqueue(p, new Vector2Int(p.x, p.y - 1), PipeTileUI.DOWN, PipeTileUI.UP, mask, visited, q);
            TryEnqueue(p, new Vector2Int(p.x - 1, p.y), PipeTileUI.LEFT, PipeTileUI.RIGHT, mask, visited, q);
        }

        return false;
    }

    private void TryEnqueue(
        Vector2Int from, Vector2Int to,
        int needFrom, int needTo,
        int fromMask, bool[,] visited, Queue<Vector2Int> q)
    {
        if ((fromMask & needFrom) == 0) return;
        if (to.x < 0 || to.x >= width || to.y < 0 || to.y >= height) return;
        if (visited[to.x, to.y]) return;

        var nb = _tiles[to.x, to.y];
        if (nb == null) return;

        if ((nb.CurrentMask() & needTo) == 0) return;

        visited[to.x, to.y] = true;
        q.Enqueue(to);
    }

    private void UpdateHighlights()
    {
        bool[,] connected = new bool[width, height];
        var q = new Queue<Vector2Int>();

        q.Enqueue(_startPos);
        connected[_startPos.x, _startPos.y] = true;

        while (q.Count > 0)
        {
            var p = q.Dequeue();
            var t = _tiles[p.x, p.y];
            int mask = t.CurrentMask();

            TryEnqueueHighlight(p, new Vector2Int(p.x, p.y + 1), PipeTileUI.UP, PipeTileUI.DOWN, mask, connected, q);
            TryEnqueueHighlight(p, new Vector2Int(p.x + 1, p.y), PipeTileUI.RIGHT, PipeTileUI.LEFT, mask, connected, q);
            TryEnqueueHighlight(p, new Vector2Int(p.x, p.y - 1), PipeTileUI.DOWN, PipeTileUI.UP, mask, connected, q);
            TryEnqueueHighlight(p, new Vector2Int(p.x - 1, p.y), PipeTileUI.LEFT, PipeTileUI.RIGHT, mask, connected, q);
        }

        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
            _tiles[x, y]?.SetHighlighted(connected[x, y]);
    }

    private void TryEnqueueHighlight(
        Vector2Int from, Vector2Int to,
        int needFrom, int needTo,
        int fromMask,
        bool[,] connected,
        Queue<Vector2Int> q)
    {
        if ((fromMask & needFrom) == 0) return;
        if (to.x < 0 || to.x >= width || to.y < 0 || to.y >= height) return;
        if (connected[to.x, to.y]) return;

        var nb = _tiles[to.x, to.y];
        if (nb == null) return;

        if ((nb.CurrentMask() & needTo) == 0) return;

        connected[to.x, to.y] = true;
        q.Enqueue(to);
    }
}
