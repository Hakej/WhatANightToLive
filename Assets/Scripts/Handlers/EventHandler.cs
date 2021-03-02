using System;
using Classes.Abstracts;
using GameObjects;
using UnityEngine;

namespace Handlers
{
    public class EventHandler : Singleton<EventHandler>
    {
        public event Action OnWin;
        public event Action OnLose;
        public event Action<bool> OnPlayerFocusChangeStart;
        public event Action<bool> OnPlayerFocusChangeStop;
        public event Action<bool, string> OnPowerToggle;
        public event Action<bool> OnPlayerSanityCrossing50;
        public event Action<bool> OnPlayerSanityCrossing25;
        public event Action<bool> OnPlayerSanityCrossing1;
        public event Action<Enemy> OnEnemySpawn;
        public event Action<Room, Room> OnEnemyChangingRoom;

        public void Win()
        {
            OnWin?.Invoke();
        }

        public void Lose()
        {
            OnLose?.Invoke();
        }

        public void PlayerFocusChangeStart(bool isFocused)
        {
            OnPlayerFocusChangeStart?.Invoke(isFocused);
        }
    
        public void PlayerFocusChangeStop(bool isFocused)
        {
            OnPlayerFocusChangeStop?.Invoke(isFocused);
        }

        public void PowerToggle(bool isPowerOn, string gameObjectTag)
        {
            OnPowerToggle?.Invoke(isPowerOn, gameObjectTag);
        }

        public void PlayerSanityCrossing50(bool isBelow)
        {
            OnPlayerSanityCrossing50?.Invoke(isBelow);
        }
    
        public void PlayerSanityCrossing25(bool isBelow)
        {
            OnPlayerSanityCrossing25?.Invoke(isBelow);
        }
        
        public void PlayerSanityCrossing1(bool isBelow)
        {
            OnPlayerSanityCrossing1?.Invoke(isBelow);
        }
        
        public void EnemySpawn(Enemy enemy)
        {
            OnEnemySpawn?.Invoke(enemy);
        }
        
        public void EnemyChangingRoom(Room oldRoom, Room newRoom)
        {
            OnEnemyChangingRoom?.Invoke(oldRoom, newRoom);
        }
    }
}
