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
                return "I'm starving...";
            case 3:
                return "I need more...";
            case 4:
                return "I can't stop...";
            case 5:
                return "On to the next Floor...";
            case 6:
                return "I need more...";
            case 7:
                return "On to the next Floor...";
            case 8:
                return "My soul is starving...";
            case 9:
                return "The hunger keeps growing...";
            case 10:
                return "I need to keep pushing forward...";
            case 11:
                return "On to the next Floor...";
            case 12:
                return "I need to reach higher...";
            case 13:
                return "Higher...";
            case 14:
                return "Higher";
            case 15:
                return "Don't stop";
            case 16:
                return "More";
            case 17:
                return "Give me more";
            case 18:
                return "Devour everything";
            case 19:
                return "Everything";
            case 20:
                return "Nothing Left";
            case 21:
                return "Is that it?";
            default:
                return "I'm starving...";
        }
    }
}
