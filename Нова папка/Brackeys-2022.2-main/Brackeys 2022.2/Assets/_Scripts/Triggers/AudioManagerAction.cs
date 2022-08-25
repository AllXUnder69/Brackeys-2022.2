using UnityEngine;

public enum AM_Action
{
    Play, Stop
}

[System.Serializable]
public class AudioManagerAction
{
    [HideInInspector]
    public string itemName;

    public AM_Action action;

    public string soundName;
}