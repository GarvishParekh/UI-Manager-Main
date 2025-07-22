using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasIdentity : MonoBehaviour
{
    UiManager uiManager;
    CanvasGroup canvasGroup;
    ICanvasAnimation customAnimation;
    [SerializeField] private CanvasNames myCanvasName;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        customAnimation = GetComponent<ICanvasAnimation>();
    }

    private void Start()
    {
        uiManager = UiManager.instance;
        uiManager.AddCanvas(this);
    }

    public CanvasNames GetCanvasName()
    {
        return myCanvasName;
    }

    public void EnableCanvas()
    {
        if (customAnimation != null)
        {
            customAnimation.EnableAnimation();
        }
        else
        {
            canvasGroup.alpha = 1;
        }
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisableCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
