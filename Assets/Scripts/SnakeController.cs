using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    internal int score = 0;
    public int initialSize = 4;
    private bool isShield = false;

    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;

    private Vector2 direction = Vector2.right;
    public ShieldController shieldController;
    public UIController uIController;

    void Start()
    {
        ResetState();
        InvokeRepeating("Move", 0.08f, 0.08f);
    }

    void Update()
    {
        Input();
        this.GetComponent<BoxCollider2D>().enabled = !isShield; //Enable and Disable shield
        Debug.Log(score);

    }

    void Move()
    {
        Vector2 v = transform.position; //Gap will be created here

        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        //move snake
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;
        this.transform.position = new Vector2(x, y);
    }

    void Input()
    {
        // Only allow turning up or down while moving in the x-axis
        if (this.direction.x != 0f)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.W) || UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.direction = Vector2.up;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.S) || UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.direction = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (this.direction.y != 0f)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.D) || UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.direction = Vector2.right;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.A) || UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.direction = Vector2.left;
            }
        }
    }

    void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
        }
        else if (other.gameObject.CompareTag("Shield"))
        {
            isShield = true;
            Destroy(shieldController.gameObject);
            Invoke("ShieldOver", 3f);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            ResetState();
        }
    }

    internal void ResetState()
    {

        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(segmentPrefab));
        }

        this.transform.position = Vector3.zero;

        score = 0;
    }

    void ShieldOver()
    {
        isShield = false;
    }
}