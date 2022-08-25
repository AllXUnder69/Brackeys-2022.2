using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private Vector2 bounds = Vector2.one * 10;

    [SerializeField] private AudioManagerAction[] am_actions;

    void OnValidate()
    {
        GetComponent<BoxCollider2D>().size = bounds;
        GetComponent<BoxCollider2D>().isTrigger = true;

        foreach (AudioManagerAction action in am_actions)
        {
            action.itemName = (action.action == AM_Action.Play ? "Play: " : action.action == AM_Action.Stop ? "Stop: " : string.Empty) + action.soundName;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (AudioManagerAction action in am_actions)
            {
                switch (action.action)
                {
                    case AM_Action.Play:
                        AudioManager.Instance.Play(action.soundName);
                        break;
                    case AM_Action.Stop:
                        AudioManager.Instance.Stop(action.soundName);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}