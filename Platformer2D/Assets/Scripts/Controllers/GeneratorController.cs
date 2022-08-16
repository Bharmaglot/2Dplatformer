using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    
    public class GeneratorController
    {
        private Tilemap _tilemap;
        private Tile _tile;
        private int _mapHeight;
        private int _mapWidth;

        private bool _borders;

        private int _fillPercent;
        private int _factorSmooth;

        private int[,] _map;

        public GeneratorController(GeneratorLevelView view)
        {
            _tilemap = view._tilemap;
            _tile = view._tile;
            _mapHeight = view._mapHeight;
            _mapWidth = view._mapWidth;
            _borders = view._borders;
            _fillPercent = view._fillPercent;
            _factorSmooth = view._factorSmooth;

            _map = new int[_mapWidth, _mapHeight];


        }


        public void Start()
        {
            FillMap();

            for (int i = 0; i < _factorSmooth; i++)
            {
                SmoothMap();
            }

            DrawTiles();
        }

        public void FillMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (x == 0 || x == _mapWidth - 1 || y == 0 || y == _mapHeight - 1)
                    {
                        if (_borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = Random.Range(0, 100) < _fillPercent ? 1 : 0;
                    }
                }
            }
        }

        public void SmoothMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    int neighbour = GetNeighbour(x, y);

                    if (neighbour > 4)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighbour < 4)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }


        public int GetNeighbour(int x, int y)
        {
            int neighbour = 0;
            for (int gridX = x - 1; gridX <= x + 1; gridX++)
            {
                for (int gridY = y - 1; gridY <= y + 1; gridY++)
                {
                    if (gridX >= 0 && gridX < _mapWidth && gridY >= 0 && gridY < _mapHeight)
                    {
                        if (gridX != x || gridY != y)
                        {
                            neighbour += _map[gridX, gridY];
                        }
                    }
                    else
                    {
                        neighbour++;
                    }
                }
            }
            return neighbour;
        }

        private void DrawTiles()
        {
            if (_map == null) return;
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if(_map[x,y]==1)
                    {
                        Vector3Int tilePosition = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight + y, 0);

                        _tilemap.SetTile(tilePosition, _tile);
                    }
                }
            }
        }
    }
}