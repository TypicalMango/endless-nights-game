using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private float viewRadius; // The radius of the field of view
    [Range(0, 360)]
    [SerializeField]
    private float viewAngle; // The angle of the field of view
    [SerializeField]
    private LayerMask obstacleMask; // Layer mask for obstacles
    [SerializeField]
    private LayerMask targetMask; // Layer mask for targets (enemies)

    private LineRenderer lineRenderer; // LineRenderer to visualize the field of view

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is missing from the FieldOfView GameObject. Please add one.");
            return; // Exit the method if the LineRenderer is not found
        }
        lineRenderer.positionCount = 51; // Set the number of points for the line renderer (50 + 1 for the center)
        DrawFieldOfView();
    }

    private void Update()
    {
        DrawFieldOfView();
        DetectEnemies();
    }

    private void DrawFieldOfView()
    {
        if (lineRenderer == null) return; // Exit if the LineRenderer is missing

        float angleStep = viewAngle / 50; // Calculate angle step based on segments
        float currentAngle = -viewAngle / 2;

        // Draw the cone mesh
        for (int i = 0; i < 50; i++)
        {
            float rad = currentAngle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0);
            Vector3 point = transform.position + direction * viewRadius;
            lineRenderer.SetPosition(i, point);
            currentAngle += angleStep; // Increment the angle
        }
        
        // Set the last position to the player's position (the center of the cone)
        lineRenderer.SetPosition(50, transform.position); // Close the cone
    }

    private void DetectEnemies()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        foreach (Collider2D target in targetsInViewRadius)
        {
            EnemyVisibility enemy = target.GetComponentInChildren<EnemyVisibility>();
            if (enemy != null)
            {
                Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.up, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.transform.position);
                    if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        enemy.SetVisibility(true); // Show the enemy if it's within the view
                    }
                    else
                    {
                        enemy.SetVisibility(false); // Hide if obstructed
                    }
                }
                else
                {
                    enemy.SetVisibility(false); // Hide if outside the angle
                }
            }
        }
    }
}
