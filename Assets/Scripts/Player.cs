using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public UIManager uiManager;  // lo conectarás desde el Inspector

    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Salto")]
    public float jumpForce = 7f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Sistema de vidas")]
    public int vidas = 3;
    private Vector3 posicionInicial;

    private Rigidbody rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posicionInicial = transform.position;
        if (uiManager != null)
        {
            GetComponent<Renderer>().material.color = Color.black;
            uiManager.ActualizarVidas(vidas);
        }

    }

    void Update()
    {
        // Movimiento
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0f, moveZ) * moveSpeed;

        Vector3 velocity = rb.velocity;
        velocity.x = move.x;
        velocity.z = move.z;
        rb.velocity = velocity;

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        // Ajuste de salto rápido
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Verificar si cayó
        if (transform.position.y < -55f)
        {
            PerderVida();
        }
    }

    void PerderVida()
    {
        vidas--;
        if (uiManager != null)
        {
            uiManager.ActualizarVidas(vidas);
        }

        if (vidas > 0)
        {
            // Reaparece en posición inicial
            transform.position = posicionInicial;
            rb.velocity = Vector3.zero;
        }
        else
        {
            // Reinicia escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
