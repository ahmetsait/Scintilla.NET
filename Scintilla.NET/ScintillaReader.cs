﻿using System;
using System.IO;

namespace ScintillaNET;

/// <summary>
/// Implements a TextReader that reads from a Scintilla control.
/// </summary>
public class ScintillaReader : TextReader
{
    /// <summary>
    /// Arbitrarily chosen default buffer size
    /// </summary>
    private const int DefaultBufferSize = 256;

    /// <summary>
    /// Returns the number of buffered characters left to be read.
    /// </summary>
    private int BufferRemaining => this._data != null ? this._data.Length - this._dataIndex : 0;
    /// <summary>
    /// Returns the number of unbuffered characters left to be read.
    /// </summary>
    private int UnbufferedRemaining => this._lastData - this._nextData;
    /// <summary>
    /// Returns the total number of characters left to be read.
    /// </summary>
    private int TotalRemaining => BufferRemaining + UnbufferedRemaining;

    private readonly Scintilla _scintilla;
    private readonly int _bufferSize;
    private string _data;
    private int _dataIndex;
    private int _nextData;
    private readonly int _lastData;

    /// <summary>
    /// Initializes a new instance of the ScintillaReader class that reads all text from the specified Scintilla control.
    /// </summary>
    /// <param name="scintilla">The Scintilla control from which to read.</param>
    public ScintillaReader(Scintilla scintilla)
        : this(scintilla, 0, scintilla.TextLength)
    {
    }
    /// <summary>
    /// Initializes a new instance of the ScintillaReader class that reads all text from the specified Scintilla control.
    /// </summary>
    /// <param name="scintilla">The Scintilla control from which to read.</param>
    /// <param name="bufferSize">The number of characters to buffer at a time.</param>
    public ScintillaReader(Scintilla scintilla, int bufferSize)
        : this(scintilla, 0, scintilla.TextLength, bufferSize)
    {
    }
    /// <summary>
    /// Initializes a new instance of the ScintillaReader class that reads a subsection from the specified Scintilla control.
    /// </summary>
    /// <param name="scintilla">The Scintilla control from which to read.</param>
    /// <param name="start">The index of the first character to read.</param>
    /// <param name="end">The index just past the last character to read.</param>
    public ScintillaReader(Scintilla scintilla, int start, int end)
        : this(scintilla, start, end, DefaultBufferSize)
    {
    }
    /// <summary>
    /// Initializes a new instance of the ScintillaReader class that reads a subsection from the specified Scintilla control.
    /// </summary>
    /// <param name="scintilla">The Scintilla control from which to read.</param>
    /// <param name="start">The index of the first character to read.</param>
    /// <param name="end">The index just past the last character to read.</param>
    /// <param name="bufferSize">The number of characters to buffer at a time.</param>
    public ScintillaReader(Scintilla scintilla, int start, int end, int bufferSize)
    {
        this._scintilla = scintilla;
        this._bufferSize = bufferSize > 0 ? bufferSize : DefaultBufferSize;
        this._nextData = start;
        this._lastData = end;
        // ensure start state is valid
        BufferNextRegion();
    }

    /// <summary>
    /// Returns the next character to be read from the reader without actually removing it from the stream. Returns -1 if no characters are available.
    /// </summary>
    /// <returns>The next character from the input stream, or -1 if no more characters are available.</returns>
    public override int Peek()
    {
        // _data is set to null upon EOF
        return this._data != null ? this._data[this._dataIndex] : -1;
    }
    /// <summary>
    /// Removes a character from the stream and returns it. Returns -1 if no characters are available.
    /// </summary>
    /// <returns>The next character from the input stream, or -1 if no more characters are available.</returns>
    public override int Read()
    {
        if (this._data != null)
        {
            // EOF not reached
            char n = this._data[this._dataIndex++];
            if (this._dataIndex >= this._data.Length)
            {
                // end of buffer reached; load next section
                BufferNextRegion();
            }

            return n;
        }
        else
        {
            return -1;
        }
    }
    /// <summary>
    ///  Reads a maximum of count characters from the current stream and writes the data to buffer, beginning at index.
    /// </summary>
    /// <param name="buffer">The buffer to receive the characters.</param>
    /// <param name="index">The position in buffer at which to begin writing.</param>
    /// <param name="count">The maximum number of characters to read.</param>
    /// <returns>The actual number of characters that have been read. The number will be less than or equal to count.</returns>
    /// <exception cref="System.ArgumentNullException">buffer is null.</exception>
    /// <exception cref="System.ArgumentException">The buffer length minus index is less than count.</exception>
    /// <exception cref="System.ArgumentException">index or count is negative.</exception>
    public override int Read(char[] buffer, int index, int count)
    {
        return ReadBlock(buffer, index, count);
    }

    /// <summary>
    ///  Reads a maximum of count characters from the current stream and writes the data to buffer, beginning at index.
    /// </summary>
    /// <param name="buffer">The buffer to receive the characters.</param>
    /// <param name="index">The position in buffer at which to begin writing.</param>
    /// <param name="count">The maximum number of characters to read.</param>
    /// <returns>The actual number of characters that have been read. The number will be less than or equal to count.</returns>
    /// <exception cref="System.ArgumentNullException">buffer is null.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">The buffer length minus index is less than count.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">index or count is negative.</exception>
    public override int ReadBlock(char[] buffer, int index, int count)
    {
        if (this._data != null)
        {
            int bufferRemaining = BufferRemaining;
            if (count < bufferRemaining)
            {
                // buffer larger than read size
                this._data.CopyTo(this._dataIndex, buffer, index, count);
                return count;
            }
            else
            {
                // buffer smaller or equal to read size
                this._data.CopyTo(this._dataIndex, buffer, index, bufferRemaining);
                if (count > bufferRemaining)
                {
                    // buffer is smaller; read rest
                    string rest = this._scintilla.GetTextRange(
                        this._nextData,
                        Math.Min(count - bufferRemaining, UnbufferedRemaining));
                    rest.CopyTo(0, buffer, index + bufferRemaining, rest.Length);
                    count = bufferRemaining + rest.Length;
                    this._nextData += rest.Length;
                }
                // read at least up to buffer's end; refill buffer
                BufferNextRegion();
                return count;
            }
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Fills the buffer with the next section of text.
    /// </summary>
    private void BufferNextRegion()
    {
        if (this._nextData < this._lastData)
        {
            int size = Math.Min(this._lastData - this._nextData, this._bufferSize);
            this._data = this._scintilla.GetTextRange(this._nextData, size);
            this._nextData += this._data.Length;
            this._dataIndex = 0;
        }
        else
        {
            this._data = null;
        }
    }
}
