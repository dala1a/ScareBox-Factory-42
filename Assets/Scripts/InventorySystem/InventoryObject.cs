using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

/** 
* A custom scriptable object that acts as the player inventory in the backend. 
* @author: Yunseo Jeon
* @since: 2025-05-25
*/
public class InventoryObject : ScriptableObject
{
    public Inventory Container;
    public ItemDatabaseObject database;
    public string savePath;

    /** 
    * Add an item to the inventory.
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */ 
    public void addItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.id)
            {
                Container.Items[i].addAmount(_amount);
                break;
            }
        }
        setEmptySlot(_item, _amount);
    }

    public void dropItem(ItemObject _item)
    {

    }



    /** 
    * Save the inventory in a file.
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */ 
    [ContextMenu("Save")]
    public void Save()
    {
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    /** 
    * Load items form a binary formatted save file
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */ 
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].updateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
        }
    }

    /** 
    * Update the slots when an object is put in the inventory. 
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    * @param _item: The new item. 
    * @param _amount: The amount of that new item. 
    * @return InventorySlot: The updated slot. 
    */ 
    public InventorySlot setEmptySlot(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].updateSlot(_item.id, _item, _amount);
                return Container.Items[i];
            }
        }
        return null;
    }

    /** 
    * Move item from one inventory slot to another. 
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    * @param item1: The item being moved
    * @param item2: The other item that is being moved.
    */
    public void moveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.updateSlot(item1.ID, item1.item, item1.amount);
        item1.updateSlot(temp.ID, temp.item, temp.amount);
    }
}

[System.Serializable]
/** 
* A class to store an array of inventory items
* @author: Yunseo Jeon
* @since: 2025-05-25
*/
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[7];
}

/** 
* Class that contains the attributes for each inventory slot. 
* @author: Yunseo Jeon
* @since: 2025-05-25
*/
[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;
    public int ID = -1;
    public int slotNumber;
    public bool isEquipped;

    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
        isEquipped = false;
    }

    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    /** 
    * Adding onto an item that already exists in the inventory. 
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    * @param value: The amount being added.
    */
    public void addAmount(int value)
    {
        amount += value;
    }

    /** 
    * Updates a slot. 
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    * @param _id: The id of the new item
    * @param _item: The new item
    * @param _amount: The amount of the new item
    *
    */
    public void updateSlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    /** 
    * Equips an item
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    public void equipItem()
    {
        isEquipped = true;
    }


    /** 
    * unequips an item
    * @author: Yunseo Jeon
    * @since: 2025-05-25
    */
    public void unequipItem()
    {
        isEquipped = false;
    }
}