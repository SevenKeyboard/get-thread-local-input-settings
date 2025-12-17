#Requires AutoHotkey v1.1.17+
;==============================================================
; getThreadLocalInputSettings — Query per-thread keyboard input settings via SystemParametersInfo
;
; GitHub: https://github.com/SevenKeyboard/get-thread-local-input-settings
; Author: SevenKeyboard Ltd. (2025)
; License: The Unlicense
;
; Documentation / References:
;   a regisrty key for "Let me set a different input method for each app window"
;     https://learn.microsoft.com/en-us/answers/questions/220632/a-regisrty-key-for-let-me-set-a-different-input-me
;==============================================================

/*
regRead userPreferencesMask, % "HKEY_CURRENT_USER\Control Panel\Desktop", % "UserPreferencesMask"
*/

class VersionManager_getThreadLocalInputSettings
{
    static _ := VersionManager_getThreadLocalInputSettings._init()
    _init()    {
        global
        GETTHREADLOCALINPUTSETTINGS_VERSION := "1.0.0"
    }
}
getThreadLocalInputSettings()    {
    static SPI_GETTHREADLOCALINPUTSETTINGS:=0x104E
    return format("{3}", varSetCapacity(pThreadLocalInputSettings, A_PtrSize, 0)
        ,dllCall("User32.dll\SystemParametersInfo", "UInt",SPI_GETTHREADLOCALINPUTSETTINGS, "UInt",0, "Ptr",&pThreadLocalInputSettings, "UInt",0, "Int")
        ,(errorLevel?false:numGet(&pThreadLocalInputSettings, 0, "Ptr")))
}