using UnityEngine;

public class OpenChestMethod : MonoBehaviour
{
   public void ChestOpen()
    {
        GameManager.instance.AddOneFragVaport();
    }
}
