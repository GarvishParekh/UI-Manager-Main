using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("<b>Default opened ui")]
    [SerializeField] private CanvasNames defaultCanvas;
    [SerializeField] private PopUpNames defaultPopup;

    [Header("<b>Canvas collections")]
    [SerializeField] private List<CanvasIdentity> allCanvas = new List<CanvasIdentity>();
    [SerializeField] private List<PopupIdentity> allPopup = new List<PopupIdentity>();

    private CanvasNames openedCanvas;

    private void Awake()
    {
        instance = this;
        openedCanvas = defaultCanvas + 1;
    }

    private void Start()
    {
        StartCoroutine(nameof(OpenDefaultUi));
    }

    #region ACTION-HANDLERS
    private void OnEnable()
    {
        ActionHandler.OpenCanvasAction += OpenCanvas;
        ActionHandler.CloseCavnvasAction += CloseCanvas;

        ActionHandler.OpenPopup += OpenPopup;
        ActionHandler.ClosePopup += ClosePopup;
    }

    private void OnDisable()
    {
        ActionHandler.OpenCanvasAction -= OpenCanvas;
        ActionHandler.CloseCavnvasAction -= CloseCanvas;

        ActionHandler.OpenPopup -= OpenPopup;
        ActionHandler.ClosePopup -= ClosePopup;
    }
    #endregion

    private IEnumerator OpenDefaultUi()
    {
        yield return new WaitForSeconds(0.3f);
        OpenCanvas(defaultCanvas);
        OpenPopup(defaultPopup);
    }

    #region CANVAS-HANDLERS
    private void OpenCanvas(CanvasNames m_desireCanvas)
    {
        if (m_desireCanvas == openedCanvas) return;

        foreach (CanvasIdentity canvas in allCanvas)
        {
            if (canvas.GetCanvasName() == m_desireCanvas)
            {
                canvas.EnableCanvas();
                openedCanvas = m_desireCanvas;
            }
            else
            {
                canvas.DisableCanvas();
            }
        }
    }

    private void CloseCanvas(CanvasNames m_desireCanvas)
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
            canvas.DisableCanvas();
        }
    }

    public void AddCanvas(CanvasIdentity m_canvasToAdd)
    {
        allCanvas.Add(m_canvasToAdd);
    }
    #endregion

    #region POPUP-HANDLER
    private void OpenPopup(PopUpNames m_desirePopup)
    {
        foreach (PopupIdentity popup in allPopup)
        {
            if (popup.GetPopupName() == m_desirePopup)
            {
                popup.EnablePopup();
            }
            else
            {
                popup.DisablePopup();
            }
        }
    }

    private void ClosePopup(PopUpNames m_desirePopup)
    {
        foreach (PopupIdentity popup in allPopup)
        {
            if (popup.GetPopupName() == m_desirePopup)
            {
                popup.DisablePopup();
            }
        }
    }

    public void CloseAllPopup()
    {
        foreach (PopupIdentity popup in allPopup)
        {
            popup.DisablePopup();
        }
    }
    public void AddPopup(PopupIdentity m_popupToAdd)
    {
        allPopup.Add(m_popupToAdd);
    }
    #endregion

    // ---------- canvas creator ---------- 
    [ContextMenu("UI-creator/Create canvas")]
    private void CreateCanvas()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Main-Canvas");
        if (prefab == null) return;

        GameObject spawnedPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        spawnedPrefab.name = "Main-Canvas";
    }

    [ContextMenu("UI-creator/Create popup")]
    private void CreatePopup()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Main-Popup");
        if (prefab == null) return;

        GameObject spawnedPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        spawnedPrefab.name = "Main-Popup";
    }
}
