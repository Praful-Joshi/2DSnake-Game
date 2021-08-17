using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public int score = 0;
    public int initialSize = 4;
    public bool isShield = false, isScoreBoost = false;
    private bool canDie = true;
    public bool isOutOfScreen = false;

    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;

    private Vector2 direction = Vector2.right;
    public UIController uIController;

    void Start()
    {
        ResetState();
        InvokeRepeating("Move", 0.08f, 0.08f);
    }

    void Update()
    {
        Input();

        if (isShield)
        {
            canDie = false;
        }
    }

    void Move()
    {
        //Make the snake body move forward
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        //move snake
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;
        this.transform.position = new Vector2(x, y);

        //Screen Wrap
        if (_segments[_segments.Count - 1].position.y > 24.2f)
        {
            this.transform.position = new Vector2(this.transform.position.x, -24.2f);
        }
        else if (_segments[_segments.Count - 1].position.y < -24.2f)
        {
            this.transform.position = new Vector2(this.transform.position.x, 24.2f);
        }

        if (_segments[_segments.Count - 1].position.x < -40.1)
        {
            this.transform.position = new Vector2(40.1f, this.transform.position.y);
        }
        else if (_segments[_segments.Count - 1].position.x > 40.1)
        {
            this.transform.position = new Vector2(-40.1f, this.transform.position.y);
        }

        if (_segments[_segments.Count - 1].position.y > 24.2f || _segments[_segments.Count - 1].position.y < -24.2f || _segments[_segments.Count - 1].position.x > 40.1f || _segments[_segments.Count - 1].position.x < -40.1f)
        {
            isOutOfScreen = true;
        }
        else
        {
            isOutOfScreen = false;
        }
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
            if (isScoreBoost)
            {
                score += 10;
            }
            else
            {
                score += 5;
            }
        }

        else if (other.gameObject.CompareTag("Obstacle") && canDie && score >= 7 && !isOutOfScreen && !isShield)
        {
            Time.timeScale = 0f;
            uIController.GameOverPanel();
        }

        else if (other.gameObject.CompareTag("Shield"))
        {
            isShield = true;
            Invoke("shieldOver", 10.0f);
        }

        else if (other.gameObject.CompareTag("ScoreBooster"))
        {
            isScoreBoost = true;
            Invoke("ScoreBoostOver", 10.0f);
        }


    }

    void shieldOver()
    {
        isShield = false;
        canDie = true;
    }

    void ScoreBoostOver()
    {
        isScoreBoost = false;
    }

    internal void ResetState()
    {
        score = 0;

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
    }
}