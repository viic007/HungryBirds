using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerControls controls;

    [Header("Configuracion del Ataque")]
    public Transform attackPoint;
    public float attackRange = 0.5f; 
    public LayerMask enemyLayers; 

    public int attackDamage = 1;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Attack.performed += ctx => Attack();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            OrugaAI scriptOruga = enemy.GetComponent<OrugaAI>();
            if (scriptOruga != null)
            {
                scriptOruga.RecibirDano(attackDamage);
                continue;
            }
            Debug.Log("¡Le dimos a " + enemy.name + "!");

            UrracaAI scriptUrraca = enemy.GetComponent<UrracaAI>();
            if (scriptUrraca != null)
            {
                scriptUrraca.RecibirDano();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
