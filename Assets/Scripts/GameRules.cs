using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules
{
    //2d array to save win condition of game, each row and col is 
    //arranged in rock paper scissor lizard spock order
    //Rock - rock means it's a draw index 0,0
    //Rock - paper mean rock will lose index 0,1
    //Rock - scissor mean rock will win index 0,2 and so on
    public readonly int[,] gameRules = new int[5, 5]
    {
        {0,-1,1,1,-1 }, //Rock
        { 1,0,-1,-1,1 }, //paper
        { -1,1,0,1,-1 }, // scissors
        { -1,1,-1,0,1 }, //lizard
        { 1,-1,1,-1,0 } //spock      

    };
}

public enum HandGestures
{
    none = -1,
    rock = 0,
    paper = 1,
    scissor = 2,
    lizard = 3,
    splock = 4,
    end = 5
}
