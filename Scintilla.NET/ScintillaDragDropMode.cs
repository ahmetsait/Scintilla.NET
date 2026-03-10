namespace ScintillaNET;

/// <summary>
/// Drag &amp; drop mode for Scintilla.
/// </summary>
public enum ScintillaDragDropMode
{
    /// <summary>
    /// Override Scintilla's drag &amp; drop so that WinForms' drag &amp; drop functionality works like a .NET Control.
    /// </summary>
    WinForms,

    /// <summary>
    /// Scintilla's native drag &amp; drop. This allows moving selected text with mouse.
    /// </summary>
    Scintilla,
}
