using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    // Member Variables
    public const int maxHealth = 100;
    public bool isAlive;

    // When "currentHealth" value changes the value will be synced
    // all clients and the "OnChangeHealth" function will be called to update the 
    // healthbar canvas.
    [SyncVar(hook ="OnChangeHealth")]
    public int currentHealth = maxHealth;

    //  UI
    public RectTransform healthBar;

    private void Start()
    {
        isAlive = true;
    }
    public void TakeDamage(int amount)
    {
        // only the server will keep track of damage so
        // to keep all of the heaths synced we don't allow
        // the clients to deal damage.
        if (!isServer) return; 

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            currentHealth = maxHealth;
            RpcRespawn();
        }
    }

    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            isAlive = true;
            this.transform.position = Vector3.zero;
        }
    }
}
