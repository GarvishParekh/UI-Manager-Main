using UnityEngine;
using UnityEngine.UI;
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

    [Header("<b>Transition image values")]
    [SerializeField] private Image transitionImage;

    private float imageFillAmt = 0;

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
        ActionHandler.OpenCanvasWithTransitionAction += OpenCanvasWithTransition;

        ActionHandler.OpenPopup += OpenPopup;
        ActionHandler.ClosePopup += ClosePopup;
    }

    private void OnDisable()
    {
        ActionHandler.OpenCanvasAction -= OpenCanvas;
        ActionHandler.CloseCavnvasAction -= CloseCanvas;
        ActionHandler.OpenCanvasWithTransitionAction -= OpenCanvasWithTransition;

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

    
    private void OpenCanvasWithTransition(CanvasNames m_desireCanvas)
    {
        if (m_desireCanvas == openedCanvas) return;
        StartCoroutine(nameof(TransitionImageAnimation), m_desireCanvas);
    }

    #region Transition-animartion
    IEnumerator TransitionImageAnimation(CanvasNames m_desireCanvas)
    {
        imageFillAmt = 0;
        while (imageFillAmt < 1)
        {
            imageFillAmt += Time.deltaTime * 4;
            transitionImage.fillAmount = imageFillAmt;
            yield return null;
        }
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
        while (imageFillAmt > 0)
        {
            imageFillAmt -= Time.deltaTime * 4;
            transitionImage.fillAmount = imageFillAmt;
            yield return null;
        }
    }
    #endregion

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
}
