using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public SpriteRenderer sprite;
    public BoxCollider2D boxCol;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sprite.enabled = false;
        boxCol.enabled = false;
    }
}
