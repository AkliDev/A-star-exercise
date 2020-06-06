using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.SwitchState(new MoveMode(GameManager.instance));
    }

    public void EditMode()
    {
        GameManager.instance.SwitchState(new EditMode(GameManager.instance));
    }
}
