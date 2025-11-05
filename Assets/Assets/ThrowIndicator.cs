using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ThrowIndicator : MonoBehaviour
{
    [Header("References")]
    public Transform player;       // Player pour position des pieds
    public Transform playerCamera; // Caméra pour direction du regard

    [Header("Settings")]
    public float length = 5f;      // Longueur du trait

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;      // 2 points : début et fin
        lr.enabled = true;         // Toujours visible
        lr.useWorldSpace = true;   // Utiliser le monde
    }

    void Update()
    {
        if (player == null || playerCamera == null)
            return;

        // Point de départ : pieds du joueur
        Vector3 start = player.position + Vector3.up * 0.2f; // 10 cm au-dessus des pieds


        // Point d'arrivée : devant le joueur dans la direction du regard
        Vector3 end = start + playerCamera.forward * length;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
