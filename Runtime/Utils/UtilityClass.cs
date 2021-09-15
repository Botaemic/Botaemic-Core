using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Botaemic.Core;


namespace Botaemic.Utils
{
    public static class UtilityClass
    {

        public const int defaultSortingOrder = 1000;
        public static TextMesh CreateText(string text, Vector3 localPosition = default(Vector3), int fontSize =30, Color? color=null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder= defaultSortingOrder, Transform parent = null)
        {
            if (color == null) { color = Color.white; }
            GameObject gameObject = new GameObject("Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = (Color)color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }



    }
}
