using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHover : MonoBehaviour
{
    public GameObject infoBox;
    public Text itemName;
    public Text itemDescription;
    public int boxNum;

    RectTransform infoPos;
    RectTransform itemPos;
    // Start is called before the first frame update
    void OnMouseEnter()
    {
        Debug.Log("Mouse has entered!");
        if (boxNum < Inventory_mineral.Instance.items.Count)
        {
            infoBox.SetActive(true);
            Debug.Log("ON");
            infoPos = infoBox.GetComponent<RectTransform>();
            itemPos = GetComponent<RectTransform>();
            ChangeMineralText(Inventory_mineral.Instance.items[boxNum].GetMineralType());
            infoPos.anchoredPosition = new Vector2(itemPos.anchoredPosition.x, itemPos.anchoredPosition.y);
            
        }
    }
    void OnMouseExit()
    {
        Debug.Log("Off");
        infoBox.SetActive(false);
    }

    private void ChangeMineralText(Mineral_type type)
    {
        if (type == Mineral_type.copper)
        {
            itemName.text = "Copper";
            itemDescription.text = "Basic earthly metal, Commonly used for ship upgrades and combining.";
        }
        if (type == Mineral_type.iron)
        {
            itemName.text = "Iron";
            itemDescription.text = "Basic earthly metal, used for upgrading breaking mode.";
        }
        if (type == Mineral_type.tin)
        {
            itemName.text = "Tin";
            itemDescription.text = "Basic earthly metal,generally used for tool upgrades ";
        }
        if (type == Mineral_type.coal)
        {
            itemName.text = "Coal";
            itemDescription.text = "Basic fuel for the smelter.";
        }
        if (type == Mineral_type.granite)
        {
            itemName.text = "Granite";
            itemDescription.text = "Granite-like substance that native to the planet. Durable after hardening can be made more durable by mixing with more hard metals";
        }
        if (type == Mineral_type.bronze)
        {
            itemName.text = "Bronze";
            itemDescription.text = "Advanced metal, needs to be mixed with copper and tin in the smelter. More durable than iron and copper, can be used for upgrading tools and ship";
        }
        if (type == Mineral_type.cobalt)
        {
            itemName.text = "Cobalt";
            itemDescription.text = "Advanced metal, strangely found on this alien planet. Can be used for the ranged attack mode upgrades";
        }
        if (type == Mineral_type.steel)
        {
            itemName.text = "Steel";
            itemDescription.text = "Advanced metal, needs to be processed through smelter with Iron. Durable and useful in many upgrades";
        }
        if (type == Mineral_type.tungsten)
        {
            itemName.text = "Tungsten";
            itemDescription.text = "Advanced rare metal, very chemical reactive. Can be used on a variety of upgrades";
        }
        if (type == Mineral_type.silver)
        {
            itemName.text = "Silver";
            itemDescription.text = "Advanced rare metal, energy superconductor. Used for tool upgrades";
        }
        if (type == Mineral_type.concrete)
        {
            itemName.text = "Concrete";
            itemDescription.text = "Advanced building material, can be mixed from the smelter with granite and steel.";
        }
        if (type == Mineral_type.adamantite)
        {
            itemName.text = "Adamantite";
            itemDescription.text = "Exotic metal, native to this planet. Extremely durable, equipment made of Adamantite gleams with a harsh silver light.";
        }
        if (type == Mineral_type.orichalum)
        {
            itemName.text = "Orichalum";
            itemDescription.text = "An exotic celestial metal from off world. Radiates a mystical purple aura, extremely rare on this planet.";
        }
        if (type == Mineral_type.chromatic2)
        {
            itemName.text = "Chromatic Metal";
            itemDescription.text = "Like a whirlpool of every metal blended together. This strange substance defies the very basis of understanding.";
        }
        if (type == Mineral_type.mithril)
        {
            itemName.text = "Mithril";
            itemDescription.text = "A mysterious mineral native to the planet, reflects shining golden light.";
        }
        if (type == Mineral_type.tartarite)
        {
            itemName.text = "Tartarite";
            itemDescription.text = "Twisted exotic metal, not native to the planet. Nothing knows where this metal comes from. It only appeared since the advent of the escaped specimen.";
        }
        if (type == Mineral_type.chromatic1)
        {
            itemName.text = "Chromatic Metal";
            itemDescription.text = "Like a whirlpool of every metal blended together. This strange substance defies the very basis of understanding.";
        }


        //itemDescription =
    }
}