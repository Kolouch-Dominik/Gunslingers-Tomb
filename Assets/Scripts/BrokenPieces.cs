using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; set; } = 1f;
    [field: SerializeField] public float Deceleration { get; set; } = 5f;
    [field: SerializeField] public SpriteRenderer Sprite { get; set; }


    private Vector3 moveDirection;
    private float LifeTime { get; set; } = 3f;
    private float fadeSpeed { get; set; } = 2.5f;


    void Start()
    {
        moveDirection.x = Random.Range(-Speed, Speed);
        moveDirection.y = Random.Range(-Speed, Speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Deceleration * Time.deltaTime);

        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Sprite.color = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, Mathf.MoveTowards(Sprite.color.a, 0f, Time.deltaTime * fadeSpeed));
            if (Sprite.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
