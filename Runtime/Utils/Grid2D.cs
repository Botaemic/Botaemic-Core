using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Botaemic.Core;


namespace Botaemic.Utils
{
    public class Grid2D
    {
        private int _width = 10;
        private int _height = 10;
        private float _cellSize = 1f;
        private Vector3 _originPosition = Vector3.zero;
        private int[,] gridArray;
        private bool _showDebug = true;

        public Grid2D(int width, int height, float cellSize, Vector3 originPosition)
        {
            this._width = width;
            this._height = height;
            this._cellSize = cellSize;
            this._originPosition = originPosition;

            gridArray = new int[width, height];

            //bool showDebug = true;
            if (_showDebug)
            {
               // TextMesh[,] debugTextArray = new TextMesh[width, height];
                for (int x = 0; x < gridArray.GetLength(0); x++)
                {
                    for (int y = 0; y < gridArray.GetLength(1); y++)
                    {
                    //    debugTextArray[x, y] = UtilityClass.CreateText(gridArray[x, y].ToString(), GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter, TextAlignment.Left);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    }
                }
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

                //OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) => {
                //    debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
                //};
            }
        }

        #region Public Accessors
        public int Width { get => _width; }
        public int Height { get => _height; }
        public float CellSize { get => _cellSize; }
        public bool ShowDebug { get => _showDebug; set => _showDebug = value; }
        #endregion

        #region Public Functions
        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _cellSize + _originPosition;
        }

        private void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
            y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
        }

        public void SetValue(int x, int y, int value)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                gridArray[x, y] = value;
                //if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
            }
        }

        public void SetValue(Vector3 worldPosition, int value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetValue(x, y, value);
        }

        public int GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                return gridArray[x, y];
            }
            else
            {
                return 0;
            }
        }

        public int GetValue(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetValue(x, y);
        }
        #endregion

        #region Logging
        private void Log(string text)
        {
            DebugUtility.Log(text);
        }

        private void LogWarning(string text)
        {
            DebugUtility.Log("WARNING! " + text);
        }
        #endregion
    }
}
