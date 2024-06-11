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
                return "My hunger can't stop...";
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
                return "I WON'T STOP!!!";
            case 10:
                return "I NEED MORE!!!";
            default:
                return "I'm starving...";
        }
    }
}
