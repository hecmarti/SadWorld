using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCWalk : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 5f;
    private float startXPosition;
    private float finalAngleValue;

    private bool fliping = false;
    private bool moving = true;
    private bool movingRight = true;
    public bool MovingRight => movingRight;

    [Header("Rotations")]
    [SerializeField]
    private float flipSpeed = 0.5f;
    [SerializeField]
    private Transform rotatingSprite;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        startXPosition = transform.position.x;

        animator.keepAnimatorStateOnDisable = true;

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if(fliping) moving = true;

        StopAllCoroutines();
        rotatingSprite.rotation = Quaternion.Euler(0, finalAngleValue, 0);
    }

    void Update()
    {
        if (!moving) return;

        float moveDirection = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * moveDirection * speed * Time.deltaTime);

        if (transform.position.x >= distance+startXPosition && movingRight)
        {
            Flip();
            startXPosition = transform.position.x;
            movingRight = false;
        }
        else if (transform.position.x <= startXPosition - distance && !movingRight)
        {
            Flip();
            startXPosition = transform.position.x;
            movingRight = true;
        }
    }

    private void Flip()
    {
        StopAllCoroutines();

        rotatingSprite.rotation = Quaternion.Euler(0, finalAngleValue, 0);

        StartCoroutine("FlipCharater");
    }

    private IEnumerator FlipCharater()
    {
        float newAngle = rotatingSprite.eulerAngles.y;
        finalAngleValue = 180 + newAngle;

        fliping = true;
        moving = false;

        while (finalAngleValue != newAngle)
        {
            newAngle += flipSpeed * Time.deltaTime;

            if (newAngle > finalAngleValue) { newAngle = finalAngleValue; }

            rotatingSprite.rotation = Quaternion.Euler(0, newAngle, 0);

            yield return null;
        }

        moving = true;
        fliping = false;
    }

    public void StopMoving()
    {
        StopAllCoroutines();
        rotatingSprite.rotation = Quaternion.Euler(0, finalAngleValue, 0);
        fliping = false;
        moving = false;
    }
}
