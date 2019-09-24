using System;
using System.Text;

namespace OOP_RPG.Models.Interfaces
{
    public interface IConsole
    {
        StringBuilder Title { get; }
        int CursorLeft { get; set; }
        int CursorSize { get; set; }
        int CursorTop { get; set; }
        bool IsCursorVisible { get; set; }
        bool KeyAvailable { get; }
        bool CapsLock { get; }
        bool NumLock { get; }
        ConsoleColor BackgroundColor { get; set; }
        ConsoleColor TextColor { get; set; }
        void ResetColor();
        int Read();
        ConsoleKeyInfo ReadKey();
        ConsoleKeyInfo ReadKey(bool intercept);
        string ReadLine();
        void Clear();
        void SetCursorPosition(int left, int top);
        void Write(bool value);
        void Write(char value);
        void Write(char[] buffer);
        void Write(decimal value);
        void Write(double value);
        void Write(float value);
        void Write(int value);
        void Write(long value);
        void Write(object value);
        void Write(string value);
        void Write(string format, params object[] args);
        void Write(uint value);
        void Write(ulong value);
        void WriteLine();
        void WriteLine(bool value);
        void WriteLine(char value);
        void WriteLine(char[] buffer);
        void WriteLine(decimal value);
        void WriteLine(double value);
        void WriteLine(float value);
        void WriteLine(int value);
        void WriteLine(long value);
        void WriteLine(object value);
        void WriteLine(string value);
        void WriteLine(string format, params object[] args);
        void WriteLine(uint value);
        void WriteLine(ulong value);
    }
}