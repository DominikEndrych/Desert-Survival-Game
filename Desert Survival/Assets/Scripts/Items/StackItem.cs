using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

[CreateAssetMenu(fileName = "New stack item", menuName = "Items/Stack Item")]
public class StackItem : Item
{
    public override int MaxStack 
    {
        get
        {
            return maxStack;
        }

        set
        {
            maxStack = value;
        }
    }

   public int maxStack = 1;
}
