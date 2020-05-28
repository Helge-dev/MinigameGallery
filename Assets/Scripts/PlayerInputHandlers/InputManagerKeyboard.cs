using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Controls;

public static class InputManagerKeyboard
{
    static readonly KeyControl[] upKeys = { Keyboard.current.wKey, Keyboard.current.tKey, Keyboard.current.iKey, Keyboard.current.leftBracketKey, Keyboard.current.upArrowKey };
    static readonly KeyControl[] downKeys = { Keyboard.current.sKey, Keyboard.current.gKey, Keyboard.current.kKey, Keyboard.current.quoteKey, Keyboard.current.downArrowKey };
    static readonly KeyControl[] leftKeys = { Keyboard.current.aKey, Keyboard.current.fKey, Keyboard.current.jKey, Keyboard.current.semicolonKey, Keyboard.current.leftArrowKey };
    static readonly KeyControl[] rightKeys = { Keyboard.current.dKey, Keyboard.current.hKey, Keyboard.current.lKey, Keyboard.current.backslashKey, Keyboard.current.rightArrowKey };

    static readonly KeyControl[] northKey = { Keyboard.current.slashKey, Keyboard.current.minusKey, Keyboard.current.digit3Key, Keyboard.current.digit7Key, Keyboard.current.vKey };
    static readonly KeyControl[] southKey = { Keyboard.current.periodKey, Keyboard.current.digit0Key, Keyboard.current.digit2Key, Keyboard.current.digit6Key, Keyboard.current.cKey };
    static readonly KeyControl[] westKey = { Keyboard.current.commaKey, Keyboard.current.digit9Key, Keyboard.current.digit1Key, Keyboard.current.digit5Key, Keyboard.current.xKey };
    static readonly KeyControl[] eastKey = { Keyboard.current.mKey, Keyboard.current.digit8Key, Keyboard.current.backquoteKey, Keyboard.current.digit4Key, Keyboard.current.zKey };

    static readonly KeyControl[] startKey = { Keyboard.current.qKey, Keyboard.current.rKey, Keyboard.current.uKey, Keyboard.current.pKey, Keyboard.current.rightShiftKey };

    static public bool KeyboardMoveUp(int userID) => upKeys[userID].isPressed;
    static public bool KeyboardMoveDown(int userID) => downKeys[userID].isPressed;
    static public bool KeyboardMoveLeft(int userID) => leftKeys[userID].isPressed;
    static public bool KeyboardMoveRight(int userID) => rightKeys[userID].isPressed;

    static public bool KeyboardNorth(int userID) => northKey[userID].isPressed;
    static public bool KeyboardSouth(int userID) => southKey[userID].isPressed;
    static public bool KeyboardWest(int userID) => westKey[userID].isPressed;
    static public bool KeyboardEast(int userID) => eastKey[userID].isPressed;

    static public bool KeyboardStart(int userID) => startKey[userID].isPressed;

    static public string KeyboardMoveUpButton(int userID) => GetSwedishTranslationOfKey(upKeys[userID]).ToUpper();
    static public string KeyboardMoveDownButton(int userID) => GetSwedishTranslationOfKey(downKeys[userID]).ToUpper();
    static public string KeyboardMoveLeftButton(int userID) => GetSwedishTranslationOfKey(leftKeys[userID]).ToUpper();
    static public string KeyboardMoveRightButton(int userID) => GetSwedishTranslationOfKey(rightKeys[userID]).ToUpper();
                  
    static public string KeyboardNorthButton(int userID) => GetSwedishTranslationOfKey(northKey[userID]).ToUpper();
    static public string KeyboardSouthButton(int userID) => GetSwedishTranslationOfKey(southKey[userID]).ToUpper();
    static public string KeyboardWestButton(int userID) => GetSwedishTranslationOfKey(westKey[userID]).ToUpper();
    static public string KeyboardEastButton(int userID) => GetSwedishTranslationOfKey(eastKey[userID]).ToUpper();
                  
    static public string KeyboardStartButton(int userID) => GetSwedishTranslationOfKey(startKey[userID]).ToUpper();

    static string GetSwedishTranslationOfKey(KeyControl c)
    {
        if (c.name == "leftBracket")
            return "å";
        else if (c.name == "quote")
            return "ä";
        else if (c.name == "semicolon")
            return "ö";
        else if (c.name == "backslash")
            return "*";
        else if (c.name == "slash")
            return "minus";
        else if (c.name == "period")
            return "period";
        else if (c.name == "minus")
            return "plus";
        else if (c.name == "backquote")
            return "§";
        return c.name;
    }
}
