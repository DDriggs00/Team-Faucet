using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameDB : MonoBehaviour
{

    public Sprite[] sprites;
    public static List<Item> itemList = new List<Item>();

    // Use this for initialization
    void Awake()
    {
        ///////////////////////Weapons
        Item i0 = new Item();
        i0.name = "Sword Lv 1";
        i0.type = Item.Type.equip;
        i0.sprite = sprites[0];
        itemList.Add(i0);

        Item i1 = new Item();
        i1.name = "Sword Lv 2";
        i1.type = Item.Type.equip;
        i1.sprite = sprites[1];
        itemList.Add(i1);

        Item i2 = new Item();
        i2.name = "Sword Lv 3";
        i2.type = Item.Type.equip;
        i2.sprite = sprites[2];
        itemList.Add(i2);

        Item i3 = new Item();
        i3.name = "Sword Lv 4";
        i3.type = Item.Type.equip;
        i3.sprite = sprites[3];
        itemList.Add(i3);

        Item i4 = new Item();
        i4.name = "Sword Lv 5";
        i4.type = Item.Type.equip;
        i4.sprite = sprites[4];
        itemList.Add(i4);

        Item i5 = new Item();
        i5.name = "Sword Lv 6";
        i5.type = Item.Type.equip;
        i5.sprite = sprites[5];
        itemList.Add(i5);

        Item i6 = new Item();
        i6.name = "Sword Lv 7";
        i6.type = Item.Type.equip;
        i6.sprite = sprites[6];
        itemList.Add(i6);

        ///////////////////////Armor

        Item i7 = new Item();
        i7.name = "Armor Lv 1";
        i7.type = Item.Type.equip;
        i7.sprite = sprites[7];
        itemList.Add(i7);

        Item i8 = new Item();
        i8.name = "Armor Lv 2";
        i8.type = Item.Type.equip;
        i8.sprite = sprites[8];
        itemList.Add(i8);

        Item i9 = new Item();
        i9.name = "Armor Lv 3";
        i9.type = Item.Type.equip;
        i9.sprite = sprites[9];
        itemList.Add(i9);

        Item i10 = new Item();
        i10.name = "Armor Lv 4";
        i10.type = Item.Type.equip;
        i10.sprite = sprites[10];
        itemList.Add(i10);

        Item i11 = new Item();
        i11.name = "Armor Lv 5";
        i11.type = Item.Type.equip;
        i11.sprite = sprites[11];
        itemList.Add(i11);

        Item i12 = new Item();
        i12.name = "Armor Lv 6";
        i12.type = Item.Type.equip;
        i12.sprite = sprites[12];
        itemList.Add(i12);

        Item i13 = new Item();
        i13.name = "Armor Lv 7";
        i13.type = Item.Type.equip;
        i13.sprite = sprites[13];
        itemList.Add(i13);

        ///////////////////////Consumables

        Item i14 = new Item();
        i14.name = "Health Potion";
        i14.type = Item.Type.consumable;
        i14.sprite = sprites[14];
        itemList.Add(i14);

        ///////////////////////Miscellaneous

        Item i15 = new Item();
        i15.name = "Gold Lv 1";
        i15.type = Item.Type.misc;
        i15.sprite = sprites[15];
        itemList.Add(i15);

        Item i16 = new Item();
        i16.name = "Gold Lv 2";
        i16.type = Item.Type.misc;
        i16.sprite = sprites[16];
        itemList.Add(i16);

        Item i17 = new Item();
        i17.name = "Gold Lv 3";
        i17.type = Item.Type.misc;
        i17.sprite = sprites[17];
        itemList.Add(i17);

        Item i18 = new Item();
        i18.name = "Gold Lv 4";
        i18.type = Item.Type.misc;
        i18.sprite = sprites[18];
        itemList.Add(i18);

        Item i19 = new Item();
        i19.name = "Gold Lv 5";
        i19.type = Item.Type.misc;
        i19.sprite = sprites[19];
        itemList.Add(i19);

        Item i20 = new Item();
        i20.name = "Gold Lv 6";
        i20.type = Item.Type.misc;
        i20.sprite = sprites[20];
        itemList.Add(i20);

        Item i21 = new Item();
        i21.name = "Gold Lv 7";
        i21.type = Item.Type.misc;
        i21.sprite = sprites[21];
        itemList.Add(i21);

        Item i22 = new Item();
        i22.name = "Gold Lv 8";
        i22.type = Item.Type.misc;
        i22.sprite = sprites[22];
        itemList.Add(i22);

        Item i23 = new Item();
        i23.name = "Gold Lv 9";
        i23.type = Item.Type.misc;
        i23.sprite = sprites[23];
        itemList.Add(i23);

        Item i24 = new Item();
        i24.name = "Gold Lv 10";
        i24.type = Item.Type.misc;
        i24.sprite = sprites[24];
        itemList.Add(i24);

        Item i25 = new Item();
        i25.name = "Dodo Egg";
        i25.type = Item.Type.misc;
        i25.sprite = sprites[25];
        itemList.Add(i25);

        Item i26 = new Item();
        i26.name = "Book";
        i26.type = Item.Type.misc;
        i26.sprite = sprites[26];
        itemList.Add(i26);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
