using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f); //násobení urèuje pøesnost poøadí vykreslování. 10 postaèí
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
