using System.Collections;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float walkSpeed = 2f;  // Velocidad de movimiento
    public float minWaitTime = 1f; // Tiempo mínimo de espera
    public float maxWaitTime = 3f; // Tiempo máximo de espera

    private Animator animator;
    private Rigidbody rb;  // Usamos Rigidbody para 3D
    private Vector3 moveDirection;  // Dirección del movimiento
    private bool isWalking = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Aseguramos que la escala esté en una mayor escala (2, 2, 2)
        transform.localScale = new Vector3(3, 3, 3);

        if (rb == null)
        {
            Debug.LogWarning("No se encontró Rigidbody en el objeto. Asegúrate de que el componente esté agregado.");
        }

        // Congelamos la rotación en los ejes X y Z para evitar que el enemigo gire
        rb.freezeRotation = true;

        // Inicia el ciclo de caminar y detenerse
        StartCoroutine(WalkCycle());
    }

    IEnumerator WalkCycle()
    {
        while (true)
        {
            // Decide aleatoriamente una dirección (X, Y, Z)
            moveDirection = new Vector3(
                Random.Range(-1f, 1f),  // Movimiento aleatorio en el eje X
                0f,                     // Mantiene la altura
                Random.Range(-1f, 1f)   // Movimiento aleatorio en el eje Z
            ).normalized;  // Normalizamos para que no se mueva más rápido diagonalmente

            // Comienza el movimiento
            isWalking = true;
            animator.SetBool("isWalking", true);  // Activamos la animación de caminar

            // Calculamos la dirección en la que moverse
            rb.velocity = moveDirection * walkSpeed; // Mueve al enemigo en todas direcciones

            // Espera un tiempo aleatorio para caminar
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            // Detenemos el movimiento
            isWalking = false;
            animator.SetBool("isWalking", false);  // Desactivamos la animación de caminar
            rb.velocity = Vector3.zero;  // Detenemos la velocidad

            // Espera un tiempo aleatorio antes de que comience a moverse nuevamente
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }
}


// using System.Collections;
// using UnityEngine;

// public class Skeleton : MonoBehaviour
// {
//     public float walkSpeed = 2f; // Velocidad de movimiento
//     public float minWaitTime = 1f; // Tiempo mínimo de espera
//     public float maxWaitTime = 3f; // Tiempo máximo de espera

//     private Animator animator;
//     private Rigidbody rb;  // Usamos Rigidbody para 3D
//     private bool isWalking = false;
//     private int direction = 1; // 1 = Derecha, -1 = Izquierda

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         rb = GetComponent<Rigidbody>();

//         // Congelamos la rotación en los ejes X y Z para evitar que el enemigo gire
//         rb.freezeRotation = true;

//         // Aseguramos que la escala esté en (1, 1, 1)
//         transform.localScale = new Vector3(2, 2, 2);

//         if (rb == null)
//         {
//             Debug.LogWarning("No se encontró Rigidbody en el objeto. Asegúrate de que el componente esté agregado.");
//         }

//         // Inicia el ciclo de caminar y detenerse
//         StartCoroutine(WalkCycle());
//     }

//     IEnumerator WalkCycle()
//     {
//         while (true) // Bucle infinito
//         {
//             // Decide aleatoriamente una dirección (-1 o 1)
//             direction = Random.Range(0, 2) == 0 ? -1 : 1;

//             // Gira el personaje según la dirección
//             transform.localScale = new Vector3(direction, 1, 1);

//             // Activa el movimiento
//             isWalking = true;
//             animator.SetBool("isWalking", true);  // Establecemos isWalking en true
//             if (rb != null)  // Verificamos si el Rigidbody está presente
//             {
//                 rb.velocity = new Vector3(walkSpeed * direction, rb.velocity.y, rb.velocity.z); // Movemos en el eje X
//             }

//             // Camina por un tiempo aleatorio
//             yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

//             // Se detiene
//             isWalking = false;
//             animator.SetBool("isWalking", false); // Establecemos isWalking en false
//             if (rb != null)  // Verificamos nuevamente antes de detener el Rigidbody
//             {
//                 rb.velocity = Vector3.zero;  // Detenemos el movimiento
//             }

//             // Espera un tiempo aleatorio antes de volver a moverse
//             yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
//         }
//     }
// }
