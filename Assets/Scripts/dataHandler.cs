using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class dataHandler : MonoBehaviour
{

    private GameObject furniture;

    // hold info about all buttons
    [SerializeField] private ButtonManager buttonPrefab;
    // contain all buttons
    [SerializeField] private GameObject _buttonContainer;
    // items in the item folder
    [SerializeField] private List<Item> items;

    //[SerializeField] private String label;

    private int currentID = 0;


    private static dataHandler instance;
    public static dataHandler Instance
    {

        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<dataHandler>();
            }
            return instance;
        }
    }

    // have to put 'async' when you gonna use await in the method
    private void Start()
    {
        items = new List<Item>();
        LoadItems();
        //await Get(label);
        CreateButtons();
    }

    void LoadItems()
    {
        var item_object = Resources.LoadAll("Items", typeof(Item));

        foreach (var item in item_object)
        {
            items.Add(item as Item);
        }
    }

    // create a button for every item in the Item folder (Assets\Resourses\Item)
    void CreateButtons()
    {
        foreach (Item i in items)
        {
            ButtonManager b = Instantiate(buttonPrefab, _buttonContainer.transform);
            b.ItemID = currentID;
            b.ButtonTexture = i.itemImage;
            currentID++;
        }
    }

    public void SetFurniture(int id)
    {
        furniture = items[id].itemPrefab;
    }
    public GameObject GetFurniture()
    {
        return furniture;
    }

    // gets the assets from somewhere thats not the resource folder
    //public async Task Get(String label)
    //{
    //    var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

    //    foreach (var location in locations)
    //    {
    //        var obj = await Addressables.LoadAssetAsync<Item>(location).Task;
    //        items.Add(obj);
    //    }
    //}
}
