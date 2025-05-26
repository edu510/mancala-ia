using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Highlighter : MonoBehaviour
{
    public TMP_Text pitText;
    public Image image;
    public Color highlightedColor;
    private Color _oldColor;

    private void Start()
    {
        _oldColor = image.color;
    }

    public void Highlight(float highlightTime, int index, int count)
    {
        StartCoroutine(HighlightCoroutine(highlightTime, index, count));
    }
    
    private IEnumerator HighlightCoroutine(float highlightTime, int index, int count)
    {
        yield return new WaitForSeconds(highlightTime * index);
        image.color = highlightedColor;
        yield return new WaitForSeconds(highlightTime);
        image.color = _oldColor;
    }
}