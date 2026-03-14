using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar el nivel

public class Trampa : MonoBehaviour
{
    // Esta función se activa automáticamente cuando algo entra en el "Trigger"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si lo que entró tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Reinicia la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}