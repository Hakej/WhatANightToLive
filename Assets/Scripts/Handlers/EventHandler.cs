﻿using System;
using Classes.Abstracts;
using GameObjects;
using UnityEngine;

namespace Handlers
{
    public class EventHandler : Singleton<EventHandler>
    {
        public event Action OnWin;
        public event Action OnLose;
        public event Action<bool> OnPlayerFocusChange;
        public event Action<bool, string> OnPowerToggle;
        public event Action<Enemy> OnEnemySpawn;
        public event Action<Enemy, Room, Room> OnEnemyChangingRoom;
        public event Action<GameObject> OnComputerUIActiveFeatureChange;
        public event Action<bool> OnEyesToggle;
        public event Action<AudioDecoy> OnDestroyedAudioDecoy;
        public event Action<AudioDecoy> OnFixedAudioDecoy;
        public event Action<AudioDecoy, AudioDecoy> OnChangedAudioDecoy;
        public event Action<Door> OnDoorHit;
        public event Action<Door, Door> OnChangedClosedDoor;
        public event Action OnSuccessfulAttack;


        public void Win()
        {
            OnWin?.Invoke();
        }

        public void Lose()
        {
            OnLose?.Invoke();
        }

        public void PlayerFocusChange(bool isFocused)
        {
            OnPlayerFocusChange?.Invoke(isFocused);
        }

        public void PowerToggle(bool isPowerOn, string gameObjectTag)
        {
            OnPowerToggle?.Invoke(isPowerOn, gameObjectTag);
        }

        public void EnemySpawn(Enemy enemy)
        {
            OnEnemySpawn?.Invoke(enemy);
        }

        public void EnemyChangingRoom(Enemy enemy, Room oldRoom, Room newRoom)
        {
            OnEnemyChangingRoom?.Invoke(enemy, oldRoom, newRoom);
        }

        public void ComputerUIActiveFeatureChange(GameObject feature)
        {
            OnComputerUIActiveFeatureChange?.Invoke(feature);
        }

        public void EyesToggle(bool areAyesClosed)
        {
            OnEyesToggle?.Invoke(areAyesClosed);
        }

        public void DestroyedAudioDecoy(AudioDecoy audioDecoy)
        {
            OnDestroyedAudioDecoy?.Invoke(audioDecoy);
        }

        public void FixedAudioDecoy(AudioDecoy audioDecoy)
        {
            OnFixedAudioDecoy?.Invoke(audioDecoy);
        }

        public void ChangedAudioDecoy(AudioDecoy oldDecoy, AudioDecoy newDecoy)
        {
            OnChangedAudioDecoy?.Invoke(oldDecoy, newDecoy);
        }

        public void ChangedClosedDoor(Door oldClosedDoor, Door newClosedDoor)
        {
            OnChangedClosedDoor?.Invoke(oldClosedDoor, newClosedDoor);
        }

        public void DoorHit(Door hitDoor)
        {
            OnDoorHit?.Invoke(hitDoor);
        }
    }
}
