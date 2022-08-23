using UnityEngine;

//[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    public Camera Camera => GetComponent<Camera>();

    [SerializeField] private Transform player;
    [SerializeField] private float speed;

    [Space]
    //x
    [SerializeField] private Vector2 leftPoint;
    public Vector3 LeftPoint => transform.position + (Vector3)leftPoint;

    [SerializeField] private Vector2 rightPoint;
    public Vector3 RightPoint => transform.position + (Vector3)rightPoint;

    //y
    [SerializeField] private Vector2 bottomPoint;
    public Vector3 BottomPoint => transform.position + (Vector3)bottomPoint;

    [SerializeField] private Vector2 upperPoint;
    public Vector3 UpperPoint => transform.position + (Vector3)upperPoint;

    void Update()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10f);

        bool isCollidingLeft = Physics2D.Raycast(LeftPoint, Vector3.forward);
        bool isCollidingRight = Physics2D.Raycast(RightPoint, Vector3.forward);
        bool isCollidingBottom = Physics2D.Raycast(BottomPoint, Vector3.forward);
        bool isCollidingUp = Physics2D.Raycast(UpperPoint, Vector3.forward);

        bool isColliding = isCollidingLeft && isCollidingRight && isCollidingBottom && isCollidingLeft;

        /*if (isCollidingLeft && targetPos.x <= transform.position.x)
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, targetPos.y),
                (speed + player.GetComponent<Rigidbody2D>().velocity.magnitude) / 10f * Time.deltaTime);
        if (isCollidingRight && targetPos.x >= transform.position.x)
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, targetPos.y),
                (speed + player.GetComponent<Rigidbody2D>().velocity.magnitude) / 10f * Time.deltaTime);
        if (isCollidingBottom && targetPos.y <= transform.position.y)
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(targetPos.x, transform.position.x),
                (speed + player.GetComponent<Rigidbody2D>().velocity.magnitude) / 10f * Time.deltaTime);*/

        /*if (!isColliding)
            transform.position = Vector3.MoveTowards(transform.position,
                targetPos,
                (speed + player.GetComponent<Rigidbody2D>().velocity.magnitude) / 10f * Time.deltaTime);

        if (isCollidingLeft)
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(targetPos.x, transform.position.x, 10f));*/
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(LeftPoint, 0.2f);
        Gizmos.DrawWireSphere(RightPoint, 0.2f);

        Gizmos.DrawWireSphere(UpperPoint, 0.2f);
        Gizmos.DrawWireSphere(BottomPoint, 0.2f);
    }

    /*float xSize => 1.7f * GetComponent<Camera>().orthographicSize;
    float ySize => GetComponent<Camera>().orthographicSize;



    //x
    Transform LeftWall => LevelManager.Instance.leftWall;
    Transform RightWall => LevelManager.Instance.rightWall;
    //y
    Transform Ground => LevelManager.Instance.ground;
    Transform Ceiling => LevelManager.Instance.ceiling;

    void Update()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10f);

        targetPos.x = Mathf.Clamp(targetPos.x, LeftWall.position.x + xSize, RightWall.position.x - xSize);
        targetPos.y = Mathf.Clamp(targetPos.y, Ground.position.y + ySize, Ceiling.position.y - ySize);

        transform.position = Vector3.MoveTowards(transform.position,
                targetPos,
                (speed + player.GetComponent<Rigidbody2D>().velocity.magnitude) / 10f * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Vector3 point1 = new Vector3(LeftWall.position.x + xSize, Ceiling.position.y - ySize);
        Vector3 point2 = new Vector3(RightWall.position.x - xSize, Ceiling.position.y - ySize);
        Vector3 point3 = new Vector3(RightWall.position.x - xSize, Ground.position.y + ySize);
        Vector3 point4 = new Vector3(LeftWall.position.x + xSize, Ground.position.y + ySize);

        Gizmos.DrawLine(point1, point2);
        Gizmos.DrawLine(point2, point3);
        Gizmos.DrawLine(point3, point4);
        Gizmos.DrawLine(point4, point1);
    }*/
}