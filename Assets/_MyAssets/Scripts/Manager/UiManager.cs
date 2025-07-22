using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private CanvasNames defaultCanvas;
    [Header("<b>Canvas collections")]
    [SerializeField] private List<CanvasIdentity> allCanvas = new List<CanvasIdentity>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(nameof(OpenDefaultCanvas));
    }

    private IEnumerator OpenDefaultCanvas()
    {
        yield return new WaitForSeconds(0.3f);
        OpenCanvas(defaultCanvas);
    }

    #region CANVAS-HANDLERS
    public void OpenCanvas(CanvasNames m_desireCanvas)
    {
        foreach (CanvasIdentity canvas in allCanvas)
        {
            if (canvas.GetCanvasName() == m_desireCanvas)
            {
                canvas.EnableCanvas();
            }
            else
            {
                canvas.DisableCanvas();
            }
        }
    }

    public void CloseCanvas(CanvasNames m_desireCanvas)
    {
        foreach (CanvasIdentity canvas in allCanvas)
        {
            if (canvas.GetCanvasName() == m_desireCanvas)
            {
                canvas.DisableCanvas();
            }
        }
    }

    public void CloseAllCanvas()
    {
        foreach (CanvasIdentity canvas in allCanvas)
        {
            canvas.EnableCanvas();
        }
    }

    public void AddCanvas(CanvasIdentity canvasToAdd)
    {
        allCanvas.Add(canvasToAdd);
    }
    #endregion
}
