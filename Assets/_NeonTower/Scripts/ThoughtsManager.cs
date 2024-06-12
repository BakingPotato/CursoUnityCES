using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ThoughtsManager
{
    public static string getThought(int floor)
    {
        switch (floor)
        {
            case 1:
                return "I'm starving...";
            case 2:
                return "I need more...";
            case 3:
                return "My hunger cannot stop...";
            case 4:
                return "I can't stop...";
            case 5:
                return "On to the next Floor...";
            case 6:
                return "I need more...";
            case 7:
                return "My soul is starving...";
            case 8:
                return "It's time to feast...";
            case 9:
                return "The hole keeps growing bigger...";
            case 10:
                return "I need to keep pushing forward...";
            case 11:
                return "On to the next Floor...";
            case 12:
                return "I need to reach higher...";
            case 13:
                return "Higher...";
            case 14:
                return "HIGHER!!!";
            case 15:
                return "I WON'T STOP!!!";
            case 16:
                return "I NEED MORE!!!";
            case 17:
                return "GIVE ME MORE!!!";
            case 18:
                return "I WILL DEVOUR EVERYTHING!!!";
            case 19:
                return "TIL THERE'S NOTHING LEFT!!!";
            default:
                return "I'm starving...";
        }
    }
}
