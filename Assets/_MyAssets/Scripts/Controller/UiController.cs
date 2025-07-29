using UnityEngine;

public class UiController : MonoBehaviour
{
    public void B_MainMenu(bool useShutter)
    {
        if (useShutter) ActionHandler.OpenCanvasWithTransitionAction(CanvasNames.MAIN_MENU_CANVAS);
        else ActionHandler.OpenCanvasAction(CanvasNames.MAIN_MENU_CANVAS);
    }

    public void B_Shop(bool useShutter)
    {
        if (useShutter) ActionHandler.OpenCanvasWithTransitionAction(CanvasNames.SHOP_CANVAS);
        else ActionHandler.OpenCanvasAction(CanvasNames.SHOP_CANVAS);
    }

    public void B_Setting(bool useShutter)
    {
        if (useShutter) ActionHandler.OpenCanvasWithTransitionAction(CanvasNames.SETTINGS_CANVAS);
        else ActionHandler.OpenCanvasAction(CanvasNames.SETTINGS_CANVAS);
    }

    public void B_MapSelection(bool useShutter)
    {
        if (useShutter) ActionHandler.OpenCanvasWithTransitionAction(CanvasNames.MAP_SELECTION_CANVAS);
        else ActionHandler.OpenCanvasAction(CanvasNames.MAP_SELECTION_CANVAS);
    }
}
