using UnityEngine;

public class InputManager : MonoBehaviour
{
    // values
    private float timer = 0;
    private float coolDownTime = .15f;

    // checks
    private bool onCoolDown = false;
    private bool instructionPopOpened = true;

    private void Update() => CycleCanvas();

    private void CycleCanvas()
    {
        
        CooldownTimer();

        // timer conditions
        if (onCoolDown) return;
        if (Input.anyKeyDown)
        {
            timer = coolDownTime;
            onCoolDown = true;
        }
        

        // input conditions
        if (Input.GetKeyDown(KeyCode.Q))
            ActionHandler.OpenCanvasAction(CanvasNames.MAIN_MENU_CANVAS);
        else if (Input.GetKeyDown(KeyCode.W))
            ActionHandler.OpenCanvasAction(CanvasNames.MAP_SELECTION_CANVAS);
        else if (Input.GetKeyDown(KeyCode.E))
            ActionHandler.OpenCanvasAction(CanvasNames.SETTINGS_CANVAS);
        else if (Input.GetKeyDown(KeyCode.R))
            ActionHandler.OpenCanvasAction(CanvasNames.SHOP_CANVAS);
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (instructionPopOpened) ActionHandler.ClosePopup(PopUpNames.INSTRUCTION_POPUP);
            else ActionHandler.OpenPopup(PopUpNames.INSTRUCTION_POPUP);

            instructionPopOpened = !instructionPopOpened;
        }
    }

    private void CooldownTimer()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else onCoolDown = false;
    }
}
