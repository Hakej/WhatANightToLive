﻿using Classes.Abstracts;
using Controllers;
using Handlers;
using UnityEngine;

namespace GameObjects
{
    public class Room : MonoBehaviour
    {
        public Room[] AdjacentRooms;
        public int Weight;
    }
}
