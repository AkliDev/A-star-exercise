using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public interface IPlayerState
{
    void OnEnter();
    void Update();
    void OnExit();
}





