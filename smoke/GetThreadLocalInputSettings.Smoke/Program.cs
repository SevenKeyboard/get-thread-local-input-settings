using System;
using System.Runtime.InteropServices;
using SevenKeyboard;

if (!GetThreadLocalInputSettings.TryGet(out bool enabled))
{
    Console.Error.WriteLine("Thread-local input settings query failed.");
    return 1;
}

Console.WriteLine($"Process: {RuntimeInformation.ProcessArchitecture}; enabled: {enabled}");

// An unsupported action exercises native failure without changing a setting.
// This checks the native return contract, not the wrapper's failure branch.
int result = Native.SystemParametersInfoW(uint.MaxValue, 0, out _, 0);
if (result != 0)
{
    Console.Error.WriteLine("Unsupported action unexpectedly succeeded.");
    return 1;
}

Console.WriteLine($"Unsupported action failed as expected (error {Marshal.GetLastPInvokeError()}).");
return 0;

internal static partial class Native
{
    [LibraryImport("user32.dll", EntryPoint = "SystemParametersInfoW", SetLastError = true)]
    internal static partial int SystemParametersInfoW(uint action, uint parameter, out int value, uint flags);
}
