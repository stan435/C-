using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerExercise
{
    public abstract class Logger
    {
        public abstract void Log(string message);

        public virtual void Close()
        {

        }
    }

    internal class NullLogger : Logger
    {
        public override void Log(string message)
        {

        }
    }

    internal class StreamLogger : Logger
    {
        private readonly StreamWriter writer;

        public StreamLogger(StreamWriter writer)
        {
            this.writer = writer;
        }
        public override void Log(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }
    }
    internal class FileLogger : StreamLogger
    {
        private readonly FileStream fileStream;

        public static Logger Create(string path)
        {
            var fileStream = File.OpenWrite(path);

            return new FileLogger(fileStream);
        }

        private FileLogger(FileStream fileStream)
            : base(new StreamWriter(fileStream))
        {
            this.fileStream = fileStream;
        }

        public override void Close()
        {
            this.fileStream.Close();
        }
    }

    public class LogBroadcaster : Logger
    {
        private List<Logger> loggers;

        public LogBroadcaster(List<Logger> loggers)
        {
            this.loggers = loggers;
        }
        public override void Log(string message)
        {
            foreach (var log in loggers)
            {
                log.Log(message);
            }
        }

        public override void Close()
        {
            foreach (var log in loggers)
            {
                log.Close();
            }
        }
    }




}
