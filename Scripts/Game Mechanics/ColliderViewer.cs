using UnityEngine;
using System.Collections;

public class ColliderViewer : MonoBehaviour
{
    public  BoxCollider2D BColl;


    void Start()
    {
        BColl = GetComponent<BoxCollider2D>();
    }

    void OnDrawGizmos()
    {

        Vector2 Xleft = BColl.bounds.min;
        Vector2 Yright = BColl.bounds.max;

        Vector2 Xright = new Vector2(Yright.x,Xleft.y);
        Vector2 Yleft = new Vector2(Xleft.x, Yright.y);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(Xleft,Yleft);
        Gizmos.DrawLine(Yleft,Yright);
        Gizmos.DrawLine(Yright,Xright);
        Gizmos.DrawLine(Xright,Xleft);
    }

}
