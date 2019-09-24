using OOP_RPG.Models.Interfaces;
using System;

namespace OOP_RPG.Models
{
    public class Terminal : IConsole
    {
        public ConsoleColor TextColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        private string _title;
        public string Title
        {
            get => Environment.OSVersion.Platform switch
            {
                PlatformID.Win32NT => _title,
                PlatformID.Unix => throw new InvalidOperationException($"{nameof(PlatformID)}.{nameof(PlatformID.Unix)} doesn't support reading the console title"),
                _ => throw new Exception($"Unexpected {nameof(PlatformID)} type ({Environment.OSVersion.Platform})")
            };
            set => _title = value;
        }

        public bool IsCursorVisible { get; set; }
        public bool CapsLock { get; }
        public bool NumLock { get; }
        public bool KeyAvailable { get; }
        public int CursorTop { get; set; }
        public int CursorLeft { get; set; }
        public int CursorSize { get; set; }

        public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);

        public void Clear() => Console.Clear();
        public void ResetColor() => Console.ResetColor();

        public int Read() => Console.Read();
        public string ReadLine() => Console.ReadLine();
        public ConsoleKeyInfo ReadKey() => Console.ReadKey();
        public ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);


        public void Write(bool value) => Console.Write(value);
        public void Write(decimal value) => Console.Write(value);
        public void Write(double value) => Console.Write(value);
        public void Write(float value) => Console.Write(value);
        public void Write(int value) => Console.Write(value);
        public void Write(uint value) => Console.Write(value);
        public void Write(long value) => Console.Write(value);
        public void Write(ulong value) => Console.Write(value);
        public void Write(object value) => Console.Write(value);
        public void Write(char value) => Console.Write(value);
        public void Write(char[] buffer) => Console.Write(buffer);
        public void Write(string value) => Console.Write(value);
        public void Write(string format, params object[] args) => Console.Write(format, args);


        public void WriteLine() => Console.WriteLine();
        public void WriteLine(bool value) => Console.WriteLine(value);
        public void WriteLine(decimal value) => Console.WriteLine(value);
        public void WriteLine(double value) => Console.WriteLine(value);
        public void WriteLine(float value) => Console.WriteLine(value);
        public void WriteLine(int value) => Console.WriteLine(value);
        public void WriteLine(uint value) => Console.WriteLine(value);
        public void WriteLine(long value) => Console.WriteLine(value);
        public void WriteLine(ulong value) => Console.WriteLine(value);
        public void WriteLine(object value) => Console.WriteLine(value);
        public void WriteLine(char value) => Console.WriteLine(value);
        public void WriteLine(char[] buffer) => Console.WriteLine(buffer);
        public void WriteLine(string value) => Console.WriteLine(value);
        public void WriteLine(string format, params object[] args) => Console.WriteLine(format, args);
    }
}
