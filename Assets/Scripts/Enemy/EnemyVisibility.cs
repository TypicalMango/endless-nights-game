using UnityEngine;

public class EnemyVisibility : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        Transform graphicsTransform = transform.Find("graphics");
        if (graphicsTransform != null)
        {
            spriteRenderer = graphicsTransform.GetComponent<SpriteRenderer>();
            animator = graphicsTransform.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("Graphics child not found for " + gameObject.name);
        }
    }

    public void SetVisibility(bool isVisible)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = isVisible;
        }
        if (animator != null)
        {
            animator.enabled = isVisible; // Optionally disable animations when not visible
        }
    }
}
