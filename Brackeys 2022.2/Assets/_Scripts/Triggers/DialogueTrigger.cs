using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Vector2 bounds = Vector2.one * 10;

    [SerializeField] private int ID;

    [SerializeField] private bool hasTriggered = false;


    void Start()
    {
        GetComponent<BoxCollider2D>().size = bounds;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            DialogueSystem.Instance.UpdateDialogue(ID);
    }
}