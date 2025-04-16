using UnityEngine;

public class SpriteSortingOrder : MonoBehaviour
{
    public float offset = 0;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = -(int)((transform.position.y + offset) * 100);
    }
}
