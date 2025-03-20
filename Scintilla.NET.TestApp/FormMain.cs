using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ScintillaNET.TestApp;

public partial class FormMain : Form
{
    string baseTitle;
    string? currentFileName = null;

    public string? CurrentFileName
    {
        get => this.currentFileName;
        set
        {
            BaseTitle = Path.GetFileName(this.currentFileName = value);
        }
    }

    public string BaseTitle
    {
        get => this.baseTitle;
        set
        {
            this.Text = (this.baseTitle = value) + (scintilla.Modified ? " *" : "");
        }
    }

    private const int selectionHighlightIndicatorIndex = 8;

    public FormMain()
    {
        InitializeComponent();

        scintilla.StyleNeeded += scintilla_StyleNeeded;

        baseTitle = this.Text;

        scintilla.AssignCmdKey(Keys.Control | Keys.Shift | Keys.Z, Command.Redo);

        scintilla.LexerName = "";

        SetScintillaStyles(scintilla);
        AdjustLineNumberMargin(scintilla);
        AdjustMarkerMargin(scintilla);
        AdjustFoldMargin(scintilla);
        InitSelectionHighlight(scintillaDebug, selectionHighlightIndicatorIndex);

        Version scintillaNetVersion = scintilla.GetType().Assembly.GetName().Version;
        string version = scintillaNetVersion.Revision == 0 ? scintillaNetVersion.ToString(3) : scintillaNetVersion.ToString();
        string scintillaVersion = scintilla.ScintillaVersion;
        string lexillaVersion = scintilla.LexillaVersion;

        toolStripStatusLabel_Version.Text = $"ScintillaNET v{version} (Scintilla v{scintillaVersion}, Lexilla v{lexillaVersion})";

        foreach (var group in Lexilla.GetLexerNames().ToArray().OrderBy(x => x).GroupBy(x => char.ToUpperInvariant(x[0])))
        {
            char first = group.Key;

            if (group.Count() > 1)
            {
                var item = (ToolStripMenuItem)lexersToolStripMenuItem.DropDownItems.Add(first.ToString());

                foreach (string lexer in group)
                    item.DropDownItems.Add(lexer, null, Lexer_Click);
            }
            else
                lexersToolStripMenuItem.DropDownItems.Add(group.Single(), null, Lexer_Click);
        }
    }

    private void Lexer_Click(object sender, EventArgs e)
    {
        ToolStripItem item = (ToolStripItem)sender;
        scintilla.LexerName = item.Text;
        SetScintillaStyles(scintilla);
        scintilla.Colorize(0, scintilla.TextLength);
        AdjustFoldMargin(scintilla);
    }

    private void FormMain_Shown(object sender, EventArgs e)
    {
        {
            scintilla.Text = "𠄀𮯠";
            scintilla.DeleteRange(1, 2);
            Debug.Assert(scintilla.GetTextRange(0, 2) == "𠏠");
        }

        {
            scintilla.Text = "𠄀𮯠";
            scintilla.DeleteRange(1, 1);
            Debug.Assert(scintilla.GetTextRange(0, 3) == "�𮯠");
        }

        {
            scintilla.Text = "𠄀𮯠";
            scintilla.DeleteRange(0, 1);
            Debug.Assert(scintilla.GetTextRange(0, 3) == "�𮯠");
            scintilla.DeleteRange(0, 1);
            Debug.Assert(scintilla.GetTextRange(0, 2) == "𮯠");
        }

        {
            scintilla.Text = "𮯠";
            scintilla.InsertText(1, "𠄀");
            Debug.Assert(scintilla.GetTextRange(0, 4) == "�𠄀�");
        }

        string original = "\n𠀀一丁";
        scintilla.Text = original;

        scintilla.Select();
    }

    private static string ToHex(string data)
    {
        return ToHex(data.ToCharArray().Select(c => (ushort)c));
    }

    private static string ToHex(IEnumerable<byte> data)
    {
        return string.Join(" ", data.Select(b => b.ToString("x2")));
    }

    private static string ToHex(IEnumerable<ushort> data)
    {
        return string.Join(" ", data.Select(b => b.ToString("x4")));
    }

    private static string ToHex(IEnumerable<uint> data)
    {
        return string.Join(" ", data.Select(b => b.ToString("x8")));
    }

    private static void SetScintillaStyles(Scintilla scintilla)
    {
        scintilla.StyleClearAll();

        // Configure the CPP (C#) lexer styles
        scintilla.Styles[1].ForeColor = Color.FromArgb(0x00, 0x80, 0x00);  // COMMENT
        scintilla.Styles[2].ForeColor = Color.FromArgb(0x00, 0x80, 0x00);  // COMMENT LINE
        scintilla.Styles[3].ForeColor = Color.FromArgb(0x00, 0x80, 0x80);  // COMMENT DOC
        scintilla.Styles[4].ForeColor = Color.FromArgb(0xFF, 0x80, 0x00);  // NUMBER
        scintilla.Styles[5].ForeColor = Color.FromArgb(0x00, 0x00, 0xFF);  // INSTRUCTION WORD
        scintilla.Styles[6].ForeColor = Color.FromArgb(0x80, 0x80, 0x80);  // STRING
        scintilla.Styles[7].ForeColor = Color.FromArgb(0x80, 0x80, 0x80);  // CHARACTER
        scintilla.Styles[9].ForeColor = Color.FromArgb(0x80, 0x40, 0x00);  // PREPROCESSOR
        scintilla.Styles[10].ForeColor = Color.FromArgb(0x00, 0x00, 0x80); // OPERATOR
        scintilla.Styles[11].ForeColor = Color.FromArgb(0x00, 0x00, 0x00); // DEFAULT
        scintilla.Styles[13].ForeColor = Color.FromArgb(0x00, 0x00, 0x00); // VERBATIM
        scintilla.Styles[14].ForeColor = Color.FromArgb(0x00, 0x00, 0x00); // REGEX
        scintilla.Styles[15].ForeColor = Color.FromArgb(0x00, 0x80, 0x80); // COMMENT LINE DOC
        scintilla.Styles[16].ForeColor = Color.FromArgb(0x80, 0x00, 0xFF); // TYPE WORD
        scintilla.Styles[17].ForeColor = Color.FromArgb(0x00, 0x80, 0x80); // COMMENT DOC KEYWORD
        scintilla.Styles[18].ForeColor = Color.FromArgb(0x00, 0x80, 0x80); // COMMENT DOC KEYWORD ERROR
        scintilla.Styles[23].ForeColor = Color.FromArgb(0x00, 0x80, 0x00); // PREPROCESSOR COMMENT
        scintilla.Styles[24].ForeColor = Color.FromArgb(0x00, 0x80, 0x80); // PREPROCESSOR COMMENT DOC
        scintilla.Styles[5].Bold = true;
        scintilla.Styles[10].Bold = true;
        scintilla.Styles[14].Bold = true;
        scintilla.Styles[17].Bold = true;

        scintilla.SetKeywords(0,
            "abstract add alias as ascending async await base break case catch checked continue default delegate descending do dynamic else event explicit extern false finally fixed for foreach from get global goto group if implicit in interface internal into is join let lock nameof namespace new null object operator orderby out override params partial private protected public readonly ref remove return sealed select set sizeof stackalloc switch this throw true try typeof unchecked unsafe using value virtual when where while yield");
        scintilla.SetKeywords(1,
            "bool byte char class const decimal double enum float int long nint nuint sbyte short static string struct uint ulong ushort var void");
    }

    private static byte CountDigits(int x)
    {
        if (x == 0)
            return 1;

        byte result = 0;
        while (x > 0)
        {
            result++;
            x /= 10;
        }

        return result;
    }

    private static readonly Dictionary<Scintilla, int> maxLineNumberCharLengthMap = [];

    private static void AdjustLineNumberMargin(Scintilla scintilla)
    {
        int maxLineNumberCharLength = CountDigits(scintilla.Lines.Count);
        if (maxLineNumberCharLength == (maxLineNumberCharLengthMap.TryGetValue(scintilla, out int charLen) ? charLen : 0))
            return;

        const int padding = 2;
        scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('0', maxLineNumberCharLength + 1)) + padding;
        maxLineNumberCharLengthMap[scintilla] = maxLineNumberCharLength;
    }

    private static void AdjustMarkerMargin(Scintilla scintilla)
    {
        scintilla.Margins[1].Width = 16;
        scintilla.Margins[1].Sensitive = false;
        //scintilla.Markers[Marker.HistoryRevertedToModified].SetForeColor(Color.Orange);
        //scintilla.Markers[Marker.HistoryRevertedToModified].SetBackColor(scintilla.Margins[1].BackColor);
        //scintilla.Markers[Marker.HistoryRevertedToOrigin].SetForeColor(Color.Orange);
        //scintilla.Markers[Marker.HistoryRevertedToOrigin].SetBackColor(scintilla.Margins[1].BackColor);
    }

    private static void AdjustFoldMargin(Scintilla scintilla)
    {
        // Instruct the lexer to calculate folding
        scintilla.SetProperty("fold", "1");

        // Configure a margin to display folding symbols
        scintilla.Margins[2].Type = MarginType.Symbol;
        scintilla.Margins[2].Mask = Marker.MaskFolders;
        scintilla.Margins[2].Sensitive = true;
        scintilla.Margins[2].Width = 20;

        // Set colors for all folding markers
        for (int i = 25; i <= 31; i++)
        {
            scintilla.Markers[i].SetForeColor(SystemColors.ControlLightLight);
            scintilla.Markers[i].SetBackColor(SystemColors.ControlDark);
        }

        // Configure folding markers with respective symbols
        scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
        scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
        scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
        scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
        scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
        scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
        scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

        // Enable automatic folding
        scintilla.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            CurrentFileName = openFileDialog.FileName;
            scintilla.Text = File.ReadAllText(CurrentFileName, Encoding.UTF8);
            scintilla.ClearChangeHistory();
            scintilla.SetSavePoint();
        }
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (CurrentFileName is null && saveFileDialog.ShowDialog(this) == DialogResult.OK)
            CurrentFileName = saveFileDialog.FileName;

        if (CurrentFileName is not null)
        {
            File.WriteAllText(CurrentFileName, scintilla.Text, Encoding.UTF8);
            scintilla.SetSavePoint();
        }
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        //if (scintilla.Modified)
        //{
        //    if (MessageBox.Show("You have unsaved changes, are you sure to exit?", "Scintilla", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
        //        e.Cancel = true;
        //}
    }

    private void describeKeywordSetsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        scintilla.ReplaceSelection(scintilla.DescribeKeywordSets());
    }

    private static unsafe byte[] RawText(Scintilla scintilla)
    {
        int length = scintilla.DirectMessage(NativeMethods.SCI_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero).ToInt32();
        IntPtr ptr = scintilla.DirectMessage(NativeMethods.SCI_GETRANGEPOINTER, new IntPtr(0), new IntPtr(length));
        if (ptr == IntPtr.Zero)
            return [];

        byte[] result = new byte[length];
        Marshal.Copy(ptr, result, 0, length);
        return result;
    }

    private static int ScintillaByteLength(Scintilla scintilla)
    {
        return scintilla.DirectMessage(NativeMethods.SCI_GETTEXTLENGTH).ToInt32();
    }

    private void scintilla_TextChanged(object sender, EventArgs e)
    {
        AdjustLineNumberMargin(scintilla);
        RefreshDebugText();
    }

    private void RefreshDebugText()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("Raw Length: {0}\n", ScintillaByteLength(scintilla));
        sb.AppendFormat("Text Length: {0}\n", scintilla.TextLength);
        sb.AppendLine();

        sb.AppendLine("Raw UTF-8:");
        sb.AppendLine(ToHex(RawText(scintilla)));
        sb.AppendLine();

        sb.AppendLine("Raw UTF-16:");
        sb.AppendLine(ToHex(scintilla.Text));
        sb.AppendLine();

        sb.AppendFormat("Styled Needed: {0}-{1}\n", lastStyleNeededRange.start, lastStyleNeededRange.end);
        sb.AppendFormat("Last SCI_GETENDSTYLED: {0}\n", lastEndStyled);
        sb.AppendLine();

        {
            sb.AppendLine("GetCharAt:");
            int len = scintilla.TextLength;
            for (int i = 0; i < len + 2; i++)
            {
                char c = scintilla.GetCharAt(i);
                sb.AppendFormat("{0}:({1}){2:x4} ", i, c, (uint)c);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        {
            sb.AppendLine("GetCodePointAt:");
            int len = scintilla.TextLength;
            for (int i = 0; i < len + 2; i++)
            {
                int c = scintilla.GetCodePointAt(i);
                sb.AppendFormat("{0}:({1}){2:x8} ", i, char.ConvertFromUtf32(c), c);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        sb.AppendLine("GetTextRange:");
        sb.AppendLine(ToHex(scintilla.GetTextRange(0, scintilla.TextLength)));
        sb.AppendLine(scintilla.GetTextRange(scintilla.SelectionStart, scintilla.SelectionEnd - scintilla.SelectionStart));
        sb.AppendLine();

        {
            sb.AppendLine("Lines.LineFromCharPosition:");
            int len = scintilla.TextLength;
            for (int i = 0; i <= len; i++)
            {
                int bytePos = scintilla.Lines.LineFromCharPosition(i);
                sb.AppendFormat("{0}:{1} ", i, bytePos);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        {
            sb.AppendLine("Lines.CharPositionFromLine:");
            int len = scintilla.Lines.Count;
            for (int i = 0; i <= len; i++)
            {
                int bytePos = scintilla.Lines.CharPositionFromLine(i);
                sb.AppendFormat("{0}:{1} ", i, bytePos);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        {
            sb.AppendLine("Lines.CharToBytePosition:");
            int len = scintilla.TextLength;
            for (int i = 0; i <= len; i++)
            {
                var pos = scintilla.Lines.CharToBytePosition(i);
                sb.AppendFormat("{0}:{1}{2} ", i, pos.BytePosition, pos.LowSurrogate ? "!" : "");
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        {
            sb.AppendLine("Lines.ByteToCharPosition:");
            int len = ScintillaByteLength(scintilla);
            for (int i = 0; i <= len; i++)
            {
                int bytePos = scintilla.Lines.ByteToCharPosition(i);
                sb.AppendFormat("{0}:{1} ", i, bytePos);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        {
            sb.AppendLine("Line Data Dump:");
            sb.Append(scintilla.Lines.Dump());
        }
        sb.AppendLine();

        {
            const int SCI_POSITIONBEFORE = 2417;
            const int SCI_POSITIONAFTER = 2418;
            sb.AppendLine("Position After:");
            int len = ScintillaByteLength(scintilla);
            for (int i = 0; i < len + 2; i++)
            {
                int nextPosition = scintilla.DirectMessage(SCI_POSITIONAFTER, new IntPtr(i)).ToInt32();
                sb.AppendFormat("{0}:{1} ", i, nextPosition);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        {
            int len = ScintillaByteLength(scintilla);
            int count = scintilla.DirectMessage(NativeMethods.SCI_COUNTCHARACTERS, new IntPtr(0), new IntPtr(len)).ToInt32();
            sb.AppendFormat("SCI_COUNTCHARACTERS: {0}\n", count.ToString());
        }

        {
            int selStart = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONSTART).ToInt32();
            int selEnd = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONEND).ToInt32();
            int count = scintilla.DirectMessage(NativeMethods.SCI_COUNTCHARACTERS, new IntPtr(selStart), new IntPtr(selEnd)).ToInt32();
            sb.AppendFormat("SCI_COUNTCHARACTERS (selection): {0}\n", count.ToString());
        }

        {
            int len = ScintillaByteLength(scintilla);
            int count = scintilla.DirectMessage(NativeMethods.SCI_COUNTCODEUNITS, new IntPtr(0), new IntPtr(len)).ToInt32();
            sb.AppendFormat("SCI_COUNTCODEUNITS: {0}\n", count.ToString());
        }

        {
            int selStart = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONSTART).ToInt32();
            int selEnd = scintilla.DirectMessage(NativeMethods.SCI_GETSELECTIONEND).ToInt32();
            int count = scintilla.DirectMessage(NativeMethods.SCI_COUNTCODEUNITS, new IntPtr(selStart), new IntPtr(selEnd)).ToInt32();
            sb.AppendFormat("SCI_COUNTCODEUNITS (selection): {0}\n", count.ToString());
        }
        sb.AppendLine();

        {
            sb.AppendLine("SCI_COUNTCODEUNITS:");
            int len = ScintillaByteLength(scintilla);
            for (int i = 0; i < len; i += 4)
            {
                int count = scintilla.DirectMessage(NativeMethods.SCI_COUNTCODEUNITS, new IntPtr(i), new IntPtr(i + 4)).ToInt32();
                sb.AppendFormat("{0}-{1}:{2} ", i, i + 4, count);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        {
            sb.AppendLine("Relative Positions:");
            int len = ScintillaByteLength(scintilla);
            for (int i = 0; i < len + 2; i++)
            {
                int nextPosition = scintilla.DirectMessage(NativeMethods.SCI_POSITIONRELATIVE, new IntPtr(i), new IntPtr(1)).ToInt32();
                sb.AppendFormat("{0}:{1} ", i, nextPosition);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        {
            sb.AppendLine("Relative Code Unit Positions:");
            int len = ScintillaByteLength(scintilla);
            for (int i = 0; i < len + 2; i++)
            {
                int nextPosition = scintilla.DirectMessage(NativeMethods.SCI_POSITIONRELATIVECODEUNITS, new IntPtr(i), new IntPtr(1)).ToInt32();
                sb.AppendFormat("{0}:{1} ", i, nextPosition);
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        int firstVisibleLine = scintillaDebug.FirstVisibleLine;
        scintillaDebug.Text = sb.ToString();
        scintillaDebug.FirstVisibleLine = firstVisibleLine;
    }

    private static int InitSelectionHighlight(Scintilla scintilla, int indicatorIndex)
    {
        // Update indicator appearance
        scintilla.Indicators[indicatorIndex].Style = IndicatorStyle.RoundBox;
        scintilla.Indicators[indicatorIndex].Under = true;
        scintilla.Indicators[indicatorIndex].ForeColor = Color.Lime;
        scintilla.Indicators[indicatorIndex].OutlineAlpha = 0;
        scintilla.Indicators[indicatorIndex].Alpha = 64;

        return indicatorIndex;
    }

    private static void UpdateSelectionHighlight(Scintilla scintilla, int indicatorIndex)
    {
        int targetStart = scintilla.Lines[scintilla.FirstVisibleLine].Position;
        int targetEnd = scintilla.Lines[scintilla.FirstVisibleLine + scintilla.LinesOnScreen + 1].EndPosition;

        // Remove all uses of our indicator
        scintilla.IndicatorCurrent = indicatorIndex;
        scintilla.IndicatorClearRange(targetStart, targetEnd);

        string selectedText = scintilla.SelectedText;
        if (selectedText.Length == 0)
            return;

        // Search the document
        scintilla.TargetStart = targetStart;
        scintilla.TargetEnd = targetEnd;
        scintilla.SearchFlags = SearchFlags.None;
        while (scintilla.SearchInTarget(selectedText) != -1)
        {
            // Mark the search results with the current indicator
            scintilla.IndicatorFillRange(scintilla.TargetStart, scintilla.TargetEnd - scintilla.TargetStart);

            // Search the remainder of the document
            scintilla.TargetStart = scintilla.TargetEnd;
            scintilla.TargetEnd = scintilla.TextLength;
        }
    }

    private void scintilla_SavePointLeft(object sender, EventArgs e)
    {
        Text = BaseTitle + " *";
    }

    private void scintilla_SavePointReached(object sender, EventArgs e)
    {
        Text = BaseTitle;
    }

    private void toolStripMenuItem_Find_Click(object sender, EventArgs e)
    {
        Search(toolStripTextBox_Find.Text);
    }

    private void Search(string text, bool reverse = false)
    {
        if (string.IsNullOrEmpty(text))
            return;

        int start = reverse ? scintilla.AnchorPosition : scintilla.CurrentPosition;
        int end = reverse ? 0 : scintilla.TextLength;
        int pos = scintilla.FindText(SearchFlags.None, text, start, end);
        if (pos == -1)
        {
            start = reverse ? scintilla.TextLength : 0;
            end = reverse ? scintilla.AnchorPosition - text.Length : scintilla.CurrentPosition + text.Length;
            pos = scintilla.FindText(SearchFlags.None, text, start, end);
            if (pos == -1)
            {
                toolStripStatusLabel.Text = $"\"{text}\" not found in document.";
                return;
            }
            else
                toolStripStatusLabel.Text = $"Search wrapped.";
        }
        else
            toolStripStatusLabel.Text = "";

        int caret = pos + text.Length, anchor = pos;
        scintilla.SetSelection(caret, anchor);
        scintilla.ScrollRange(anchor, caret);
    }

    private void toolStripTextBox_Find_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && (e.Modifiers & ~Keys.Shift) == 0)
        {
            Search(toolStripTextBox_Find.Text, e.Shift);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void scintillaHexDebug_UpdateUI(object sender, UpdateUIEventArgs e)
    {
        if ((e.Change & (UpdateChange.Selection | UpdateChange.HScroll | UpdateChange.VScroll)) != 0)
            UpdateSelectionHighlight(scintillaDebug, selectionHighlightIndicatorIndex);
    }

    private void scintilla_UpdateUI(object sender, UpdateUIEventArgs e)
    {
        if (e.Change.HasFlag(UpdateChange.Selection))
            RefreshDebugText();
    }

    private (int start, int end) lastStyleNeededRange;

    private int lastEndStyled;

    private void scintilla_StyleNeeded(object sender, StyleNeededEventArgs e)
    {
        lastEndStyled = scintilla.DirectMessage(NativeMethods.SCI_GETENDSTYLED).ToInt32();

        int start = scintilla.GetEndStyled();
        int end = e.Position;

        lastStyleNeededRange = (start, end);

        scintilla.StartStyling(start);
        scintilla.SetStyling(end - start, 5);

        RefreshDebugText();
    }
}
