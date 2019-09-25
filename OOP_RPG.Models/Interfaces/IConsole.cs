using System;

namespace OOP_RPG.Models.Interfaces
{
    /// <summary>
    /// Represents a Console
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Gets or sets the title to display in the console title bar.
        /// </summary>
        string Title { set; }

        /// <summary>
        /// Gets or sets the column position of the cursor within the buffer area.
        /// </summary>
        int CursorLeft { get; set; }

        /// <summary>
        /// Gets or sets the height of the cursor within a character cell.
        /// </summary>
        int CursorSize { get; set; }

        /// <summary>
        /// Gets or sets the row position of the cursor within the buffer area.
        /// </summary>
        int CursorTop { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the cursor is visible.
        /// </summary>
        bool IsCursorVisible { get; set; }

        /// <summary>
        /// Gets a value indicating whether a key press is available in the input stream.
        /// </summary>
        bool KeyAvailable { get; }

        /// <summary>
        /// Gets a value indicating whether the CAPS LOCK keyboard toggle is turned on or
        /// turned off.
        /// </summary>
        bool CapsLock { get; }

        /// <summary>
        /// Gets a value indicating whether the NUM LOCK keyboard toggle is turned on or
        /// turned off.
        /// </summary>
        bool NumLock { get; }

        /// <summary>
        /// Gets or sets the foreground color of the console.
        /// </summary>
        ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color of the console.
        /// </summary>
        ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// Sets the foreground and background console colors to their defaults.
        /// </summary>
        void ResetColor();

        /// <summary>
        /// Reads the next character from the standard input stream.
        /// </summary>
        /// <returns>
        /// The next character from the input stream, or negative one (-1) if there are currently
        /// no more characters to be read.
        /// </returns>
        int Read();

        /// <summary>
        /// Obtains the next character or function key pressed by the user. The pressed key
        /// is optionally displayed in the console window.
        /// </summary>
        /// <returns>
        /// An object that describes the System.ConsoleKey constant and Unicode character,
        /// if any, that correspond to the pressed console key. The System.ConsoleKeyInfo
        /// object also describes, in a bitwise combination of System.ConsoleModifiers values,
        /// whether one or more Shift, Alt, or Ctrl modifier keys was pressed simultaneously
        /// with the console key.
        /// </returns>
        ConsoleKeyInfo ReadKey();

        /// <summary>
        /// Obtains the next character or function key pressed by the user. The pressed key
        /// is optionally displayed in the console window.
        /// </summary>
        /// <param name="intercept">
        /// Determines whether to display the pressed key in the console window. true to
        /// not display the pressed key; otherwise, false.
        /// </param>
        /// <returns>
        /// An object that describes the System.ConsoleKey constant and Unicode character,
        /// if any, that correspond to the pressed console key. The System.ConsoleKeyInfo
        /// object also describes, in a bitwise combination of System.ConsoleModifiers values,
        /// whether one or more Shift, Alt, or Ctrl modifier keys was pressed simultaneously
        /// with the console key.
        /// </returns>
        ConsoleKeyInfo ReadKey(bool intercept);

        /// <summary>
        /// Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <returns>
        /// The next line of characters from the input stream, or null if no more lines are
        /// available.
        /// </returns>
        string ReadLine();

        /// <summary>
        /// Clears the console buffer and corresponding console window of display information.
        /// </summary>
        void Clear();

        /// <summary>
        /// Sets the position of the cursor.
        /// </summary>
        /// <param name="left">
        /// The column position of the cursor. Columns are numbered from left to right starting
        /// at 0.
        /// </param>
        /// <param name="top">
        /// The row position of the cursor. Rows are numbered from top to bottom starting
        /// at 0.
        /// </param>
        void SetCursorPosition(int left, int top);

        /// <summary>
        /// Writes the text representation of the specified Boolean value to the standard
        /// output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(bool value);

        /// <summary>
        /// Writes the specified Unicode character value to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(char value);

        /// <summary>
        /// Writes the specified array of Unicode characters to the standard output stream.
        /// </summary>
        /// <param name="buffer">
        /// A Unicode character array.
        /// </param>
        void Write(char[] buffer);

        /// <summary>
        /// Writes the text representation of the specified System.Decimal value to the standard
        /// output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(decimal value);

        /// <summary>
        /// Writes the text representation of the specified double-precision floating-point
        /// value to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(double value);

        /// <summary>
        /// Writes the text representation of the specified single-precision floating-point
        /// value to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(float value);

        /// <summary>
        /// Writes the text representation of the specified 32-bit signed integer value to
        /// the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(int value);

        /// <summary>
        /// Writes the text representation of the specified 32-bit unsigned integer value
        /// to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(uint value);

        /// <summary>
        /// Writes the text representation of the specified 64-bit signed integer value to
        /// the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(long value);

        /// <summary>
        /// Writes the text representation of the specified 64-bit unsigned integer value 
        /// to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(ulong value);

        /// <summary>
        /// Writes the text representation of the specified object to the standard output
        /// stream.
        /// </summary>
        /// <param name="value">
        /// The value to write, or null.
        /// </param>
        void Write(object value);

        /// <summary>
        /// Writes the specified string value to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void Write(string value);

        /// <summary>
        /// Writes the text representation of the specified array of objects to the standard
        /// output stream using the specified format information.
        /// </summary>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An array of objects to write using format.
        /// </param>
        void Write(string format, params object[] args);

        /// <summary>
        /// Writes the current line terminator to the standard output stream.
        /// </summary>
        void WriteLine();

        /// <summary>
        /// Writes the text representation of the specified Boolean value, followed by the
        /// current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(bool value);

        /// <summary>
        /// Writes the specified Unicode character, followed by the current line terminator,
        /// value to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(char value);

        /// <summary>
        /// Writes the specified array of Unicode characters, followed by the current line
        /// terminator, to the standard output stream.
        /// </summary>
        /// <param name="buffer">
        /// A Unicode character array.
        /// </param>
        void WriteLine(char[] buffer);

        /// <summary>
        /// Writes the text representation of the specified System.Decimal value, followed
        /// by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(decimal value);

        /// <summary>
        /// Writes the text representation of the specified double-precision floating-point
        /// value, followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(double value);

        /// <summary>
        /// Writes the text representation of the specified single-precision floating-point
        /// value, followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(float value);

        /// <summary>
        /// Writes the text representation of the specified 32-bit signed integer value,
        /// followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(int value);

        /// <summary>
        /// Writes the text representation of the specified 32-bit unsigned integer value,
        /// followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(uint value);

        /// <summary>
        /// Writes the text representation of the specified 64-bit signed integer value,
        /// followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(long value);

        /// <summary>
        /// Writes the text representation of the specified 64-bit unsigned integer value,
        /// followed by the current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(ulong value);

        /// <summary>
        /// Writes the text representation of the specified object, followed by the current
        /// line terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(object value);

        /// <summary>
        /// Writes the specified string value, followed by the current line terminator, to
        /// the standard output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write.
        /// </param>
        void WriteLine(string value);

        /// <summary>
        /// Writes the text representation of the specified array of objects, followed by
        /// the current line terminator, to the standard output stream using the specified
        /// format information.
        /// </summary>
        /// <param name="format">
        /// A composite format string.
        /// </param>
        /// <param name="args">
        /// An array of objects to write using format.
        /// </param>
        void WriteLine(string format, params object[] args);
    }
}