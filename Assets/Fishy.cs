using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishy : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    public float multipler,moveSpeed,destructFloat;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        int randNo = Random.Range(1, 10);
        if (randNo % 2 == 0)
        {
            multipler = -1;
        }
        else
        {
            multipler = 1;
        }
        moveSpeed = moveSpeed * multipler;
        if (moveSpeed > 0)
        {
            _spriteRenderer.flipX = true;
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y,
            transform.position.z);
        if (Mathf.Abs(transform.localPosition.x) > destructFloat)
        {
            Destroy(this.gameObject);
        }
    }
}
