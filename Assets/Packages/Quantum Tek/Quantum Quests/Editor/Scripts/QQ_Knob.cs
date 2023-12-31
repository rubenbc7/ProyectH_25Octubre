﻿using System.Collections.Generic;

namespace QuantumTek.QuantumQuest.Editor
{
    [System.Serializable]
    public class QQ_Knob
    {
        public int ID;
        public string Name;
        public QQ_KnobType Type;
        public float Y;
        public bool AllowMultipleConnections;
        public QQ_KnobDictionary Connections = new QQ_KnobDictionary();

        public QQ_Knob(int id, string name, QQ_KnobType type, float y, bool allowMultipleConnections = true)
        {
            ID = id;
            Name = name;
            Type = type;
            Y = y;
            AllowMultipleConnections = allowMultipleConnections;
        }

        /// <summary>
        /// Returns a list of all knobs from a connection that are connected to this.
        /// </summary>
        /// <param name="connectionID">The connected node's id.</param>
        /// <returns></returns>
        public List<int> GetKnobs(int connectionID)
        {
            List<int> knobs = new List<int>();

            if (Connections.ContainsKey(connectionID))
                knobs.AddRange(Connections.Get(connectionID));

            return knobs;
        }

        /// <summary>
        /// Disconnects a node from this knob.
        /// </summary>
        /// <param name="connectionID">The connected node's id.</param>
        public void Disconnect(int connectionID)
        {
            if (Connections.ContainsKey(connectionID))
                Connections.Remove(connectionID);
        }

        /// <summary>
        /// Disconnects a node's knob from this knob.
        /// </summary>
        /// <param name="connectionID">The connected node's id.</param>
        /// <param name="connectionKnobID">The connected knob's id.</param>
        public void Disconnect(int connectionID, int connectionKnobID)
        {
            if (Connections.ContainsKey(connectionID) && Connections.Get(connectionID).Contains(connectionKnobID))
                Connections.Get(connectionID).Remove(connectionKnobID);
            if (Connections.Get(connectionID).Count == 0)
                Connections.Remove(connectionID);
        }
    }
}