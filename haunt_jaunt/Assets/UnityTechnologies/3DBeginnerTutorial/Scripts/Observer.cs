using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    private IEnumerator colorchange;

    Renderer enemy;
    bool m_IsPlayerInRange;

    private void Awake()
    {
        enemy = this.transform.parent.gameObject.GetComponent<Renderer>();
        colorchange = LerpFunc(Color.red, 8);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    StartCoroutine(colorchange);
                }
            }
            if(enemy.material.color == Color.red)
            {
                gameEnding.CaughtPlayer();
            }
        }
        else if(enemy.material.color != Color.white )
        {
            StopCoroutine(colorchange);
        }
    }
    IEnumerator LerpFunc(Color endv, float duration)
    {
        Debug.Log("Started");
        float time = 0;
        Color startv = enemy.material.color;
        while (time < duration)
        {
            enemy.material.color = Color.Lerp(startv, endv, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        enemy.material.color = endv;
        Debug.Log("Finished");
    }
}