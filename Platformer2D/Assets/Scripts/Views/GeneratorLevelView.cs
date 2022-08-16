using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class GeneratorLevelView : MonoBehaviour
    {
        public Tilemap _tilemap;
        public Tile _tile;
        public int _mapHeight;
        public int _mapWidth;

        public bool _borders;

        [Range(0, 100)] public int _fillPercent;
        [Range(0, 100)] public int _factorSmooth; 
    }
}