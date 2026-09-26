using UnityEngine;

public interface INPCFactory
{
    GameObject CreateNPC(string npcType, Vector3 position);
}

public abstract class NPC_Factory : MonoBehaviour, INPCFactory
{
    public abstract GameObject CreateNPC(string npcType, Vector3 position);
    public abstract void SetNPCProperties(GameObject npc, string npcType);
    public abstract void SetNPCBehavior(GameObject npc, string npcType, bool isInRush, int lifetime, bool isDecisive, float appearance);
    public abstract void SetNPCState();
}
