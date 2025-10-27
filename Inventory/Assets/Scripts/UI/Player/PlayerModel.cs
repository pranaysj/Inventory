using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModel
{
    private int money;
    private int weight;
    private int maxWeight = 30;


    public int Money { get => money; set => money = value; }
    public int Weight { get => weight; set => weight = value; }
    public int MaxWeight => maxWeight;

    private int playerBagWeight = 0;

    internal int UpdateBagWeight(int weight)
    {
        playerBagWeight += weight;
        Weight = playerBagWeight;
        return playerBagWeight;
    }
}
