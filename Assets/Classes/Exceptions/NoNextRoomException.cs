using System;
using GameObjects;

namespace What_a_Night_to_Live.Assets.Classes.Exceptions
{
    public class NoNextRoomException : Exception
    {
        public NoNextRoomException()
        {

        }

        public NoNextRoomException(Room room)
                    : base(String.Format("No room connected to: {0}", room.name))
        {

        }
    }
}