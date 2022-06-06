using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHolderScript : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    public void CanvasDestroyer()
    {
        _canvas.enabled = false;
    }
}
