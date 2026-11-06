//==============================================================
// GetThreadLocalInputSettings — Query per-thread keyboard input settings via SystemParametersInfo
//
// GitHub: https://github.com/SevenKeyboard/get-thread-local-input-settings
// Author: SevenKeyboard Ltd. (2025-2026)
// License: The Unlicense
//
// Documentation / References:
//   a regisrty key for "Let me set a different input method for each app window"
//     https://learn.microsoft.com/en-us/answers/questions/220632/a-regisrty-key-for-let-me-set-a-different-input-me
//   SystemParametersInfoW (SPI_GETTHREADLOCALINPUTSETTINGS)
//     https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfow
//==============================================================

using System.Runtime.InteropServices;

namespace SevenKeyboard;

public static partial class GetThreadLocalInputSettings
{
    private const uint SpiGetThreadLocalInputSettings = 0x104E;

    /// <summary>Queries whether input settings have per-thread rather than session scope.</summary>
    /// <param name="enabled">The setting on success; false on failure.</param>
    /// <returns>True if the native query succeeded.</returns>
    public static bool TryGet(out bool enabled)
    {
        enabled = false;
        if (SystemParametersInfoW(SpiGetThreadLocalInputSettings, 0, out int value, 0) == 0)
            return false;

        enabled = value != 0;
        return true;
    }

    // Both the return value and *pvParam are 4-byte Win32 BOOLs on x86 and x64.
    [LibraryImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
    private static partial int SystemParametersInfoW(uint action, uint parameter, out int value, uint flags);
}
