# get-thread-local-input-settings
Windows per-window input method setting query for .NET 10.

```csharp
if (SevenKeyboard.GetThreadLocalInputSettings.TryGet(out bool enabled))
    Console.WriteLine(enabled);
```

Use `enabled` only when the query succeeds; a failed query does not mean the setting is disabled.
