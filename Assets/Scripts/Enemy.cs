using UnityEngine;

public class Enemy: MonoBehaviour
{
    private float timer = 0f;
    private bool isNearPlant = false;

    void Update()
    { 
        if (isNearPlant)
        {
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                TakeDamage();
                timer = 0f;
            }
        }
    }

    void TakeDamage()
    {
        Debug.Log("¡Warning! The enemy " + gameObject.name + "is damaging the crops. ");
    }

    private void OnTriggerEnter2D(Collider2D other)
    //Comprueba si el objeto con el que colisiona es de tipo Plant
    { 
        if (other.CompareTag("Plant"))
        {
            isNearPlant = true;
            timer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    { 
        if (other.CompareTag("Plant"))
        {
            isNearPlant = false;
            timer = 0f;
        }
    }
}

