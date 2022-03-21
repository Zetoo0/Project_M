
using UnityEngine;

public class MovePlatform : MonoBehaviour
{ 
    //Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] int startPosition;
    public Transform[] points;
    private int i;
    
    void Start()
    {
        transform.position = points[startPosition].position;
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, moveSpeed * Time.deltaTime);
    }

}
