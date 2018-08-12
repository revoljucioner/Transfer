using UnityEngine;

public class CloudSpriteScript : MonoBehaviour
{
    public Sprite[] sprites;

    public void ChangeSprite()
    {
        var sprite = sprites[Random.Range(0, sprites.Length)];
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void ChangeSprite(float size)
    {
        ChangeSprite();
        gameObject.transform.localScale = new Vector3(size, size, size);
    }
}