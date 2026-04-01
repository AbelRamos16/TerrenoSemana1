using UnityEngine;

public class ClickAttack : MonoBehaviour
{
    public Camera playerCamera;
    public Animator animator;
    public float attackDistance = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Animación SIEMPRE se ejecuta
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // IMPORTANTE: distancia limitada + debug
        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            Debug.Log("Golpeaste: " + hit.transform.name);

            if (hit.transform.CompareTag("Zombie"))
            {
                Destroy(hit.transform.gameObject);
                Debug.Log("Zombie eliminado");
            }
        }
    }
}