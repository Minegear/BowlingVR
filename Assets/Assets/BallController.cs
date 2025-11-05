using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Player & Inputs")]
    public Transform player;           // Objet Player
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode throwKey = KeyCode.Space;
    public KeyCode resetKey = KeyCode.R;

    [Header("Ball Settings")]
    public float pickupDistance = 2f;  // Distance max pour ramasser
    public float throwForce = 15f;     // Force du lancer

    [Header("Highlight Settings")]
    public Color highlightColor = Color.yellow;
    public float emissionIntensity = 2f; // Force lumineuse
    public float highlightFadeSpeed = 5f; // Vitesse de transition

    private bool isHeld = false;
    private Rigidbody rb;
    private Renderer rend;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Color baseEmissionColor;
    private float currentEmissionStrength = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();

        // Sauvegarder position et rotation initiales
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Sauvegarder la couleur d’émission de base
        if (rend.material.HasProperty("_EmissionColor"))
            baseEmissionColor = rend.material.GetColor("_EmissionColor");
        else
            baseEmissionColor = Color.black;

        // Important : activer l’émission sur le matériau
        rend.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        // --- Gérer la surbrillance ---
        bool shouldHighlight = !isHeld && dist < pickupDistance;
        float targetStrength = shouldHighlight ? emissionIntensity : 0f;
        currentEmissionStrength = Mathf.Lerp(currentEmissionStrength, targetStrength, Time.deltaTime * highlightFadeSpeed);

        Color emissive = highlightColor * currentEmissionStrength;
        rend.material.SetColor("_EmissionColor", emissive);

        // --- Ramasser la boule ---
        if (!isHeld && dist < pickupDistance && Input.GetKeyDown(pickupKey))
        {
            PickUp();
        }

        // --- Lancer la boule ---
        if (isHeld && Input.GetKeyDown(throwKey))
        {
            Throw();
        }

        // --- Reset boule ---
        if (Input.GetKeyDown(resetKey))
        {
            ResetBall();
        }
    }

    void PickUp()
    {
        isHeld = true;
        rb.isKinematic = true;
        transform.SetParent(player);
        transform.localPosition = new Vector3(0.4f, 0.3f, 1f); // Position dans la main
        transform.localRotation = Quaternion.identity;
    }

    void Throw()
    {
        isHeld = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(player.forward * throwForce, ForceMode.Impulse);
    }

    void ResetBall()
    {
        isHeld = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}
