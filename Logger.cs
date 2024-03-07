using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlphaESS
{
    class Logger
    {
        public static StreamWriter w = System.IO.File.AppendText("log" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Millisecond.ToString() + ".txt");
        public static void LogLine(object msg)
        {
            Console.WriteLine(msg.ToString());
            w.WriteLine(msg);
        }

        public static void LogLine()
        {
            Console.WriteLine();
            w.WriteLine();
        }

        public static void Log(object msg)
        {
            Console.Write(msg.ToString());
            w.Write(msg);
        }

        public static void LogLineFileOnly(object msg)
        {
            w.WriteLine(msg.ToString());
        }

        public static void LogLineFileOnly()
        {
            w.WriteLine();
        }

        public static void LogFileOnly(object msg)
        {
            w.Write(msg.ToString());
        }

        public static void Close()
        {
            w.Close();
        }
        public static void Flush()
        {
            w.Flush();
        }
    }
}
