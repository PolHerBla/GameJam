using UnityEngine;
using UnityEngine.InputSystem;

public class ManejoLinterna : MonoBehaviour
{
    public GameObject luzLinterna; // Arrastra aquí la luz de la linterna
    private Transform jugador;
    private bool estaCerca = false;
    private bool recogida = false;


    void Start()
{
    luzLinterna.SetActive(false); // Apaga la luz al iniciar el juego
}

void Update()
{
    // 1. Detectamos la tecla X (Teclado)
    bool tecladoX = Keyboard.current != null && Keyboard.current.xKey.isPressed;

    // 2. Detectamos el RT (Mando)
    // Usamos un pequeño margen (0.1) para que se active en cuanto se hunda un poco
    bool mandoRT = Gamepad.current != null && Gamepad.current.rightTrigger.ReadValue() > 0.1f;

    // Si cualquiera de los dos está pulsado
    bool intentandoUsar = tecladoX || mandoRT;

    if (!recogida)
    {
        if (estaCerca && intentandoUsar)
        {
            AgarrarLinterna();
        }
    }
    else
    {
        if (!intentandoUsar)
        {
            SoltarLinterna();
        }
    }
}

private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Jugador detectado cerca de la linterna!"); // <--- AÑADE ESTO
            estaCerca = true;
            jugador = other.transform;
        }
    }

void AgarrarLinterna()
{
    recogida = true;
    luzLinterna.SetActive(true);
    
    // Si tienes un Rigidbody en la linterna, lo desactivamos
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    if (rb != null) {
        rb.simulated = false; // Esto hace que la linterna deje de existir para la física
    }

    transform.SetParent(jugador);
    transform.localPosition = new Vector3(0.5f, 0, 0); 
}

void SoltarLinterna()
{
    recogida = false;
    luzLinterna.SetActive(false);
    
    // Al soltarla, volvemos a activar su física
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    if (rb != null) {
        rb.simulated = true;
    }

    transform.SetParent(null);
}

    // Detectar si el jugador entra en el área

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            estaCerca = false;
        }
    }
}