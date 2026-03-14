using UnityEngine;

using UnityEngine.InputSystem;



public class MovimientoJugador : MonoBehaviour

{

    private Rigidbody2D rb;

    private Vector2 direccionMover;



    [Header("Configuración de Movimiento")]

    [SerializeField] private float velocidad = 5f;

    [SerializeField] private float fuerzaSalto = 10f;



    [Header("Detección de Suelo")]

    [SerializeField] private Transform detectorSuelo;// Un objeto vacío a los pies del jugador

    [SerializeField] private float radioDeteccion = 0.2f; // Tamaño del círculo de detección

    [SerializeField] private LayerMask capaSuelo;
    [SerializeField] private LayerMask capaPlataforma;




    private bool estaEnSuelo;

    void Start()

    {

        rb = GetComponent<Rigidbody2D>();

    }



    public void Mover(InputAction.CallbackContext context)

    {

        direccionMover = context.ReadValue<Vector2>();

    }



    public void Saltar(InputAction.CallbackContext context)

    {

        // Solo saltamos si se presiona el botón Y estamos tocando el suelo

        if (context.started && estaEnSuelo)

        {

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);

        }

    }



    void FixedUpdate()

    {

        // Revisamos si el detectorSuelo está tocando la capaSuelo

        estaEnSuelo = Physics2D.OverlapCircle(detectorSuelo.position, radioDeteccion, capaSuelo | capaPlataforma);


        rb.linearVelocity = new Vector2(direccionMover.x * velocidad, rb.linearVelocity.y);

    }



    // Opcional: Para ver el círculo de detección en el editor de Unity

    private void OnDrawGizmos()

    {

        if (detectorSuelo != null)

        {

            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(detectorSuelo.position, radioDeteccion);

        }

    }

}

