using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get;  private set; }
    void OnValidate()
    {
        Instance = this;
    }

    public Transform leftWall;
    public Transform rightWall;

    public Transform ground;
    public Transform ceiling;
}