using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject StartingRoom;

    [Header("Enemy AI")]
    [Range(0, 20)]
    public int Difficulty;
    public float MoveCooldown;

    private float _currentCooldown = 0f;
    private GameObject _currentRoom;
    
    private void Start()
    {
        if (Difficulty == 0)
        {
            Destroy(gameObject);
        }
        
        var roomTrans = StartingRoom.transform;
        
        transform.position = roomTrans.position;
        transform.localScale = roomTrans.localScale;
        transform.rotation = roomTrans.rotation;

        _currentRoom = StartingRoom;
    }

    private void Update()
    {
        _currentCooldown += Time.deltaTime;

        if (_currentCooldown < MoveCooldown)
        {
            return;
        }

        _currentCooldown = 0f;
        
        var randomNumber = Random.Range(1, 20);

        if (Difficulty <= randomNumber)
        {
            Debug.Log($"{gameObject.name}: I've failed my movement.");
            return;
        }

        Move();
    }

    private void Move()
    {

        var curRoom = _currentRoom.GetComponent<Room>();
        var adjRooms = curRoom.AdjacentRooms;
        var newRoom = adjRooms[Random.Range(0, adjRooms.Length)];

        if (newRoom.CompareTag("PlayerRoom"))
        {
            Attack();
            return;
        }
        
        _currentRoom = newRoom;
        
        var roomTrans = newRoom.transform;
        transform.position = roomTrans.position;
        transform.localScale = roomTrans.localScale;
        transform.rotation = roomTrans.rotation;
        
        Debug.Log($"{gameObject.name}: I've successfully moved to {newRoom.name}.");
    }

    private void Attack()
    {
        // TODO: Add attacking logic
        EventHandler.Instance.Lose();
    }
}
