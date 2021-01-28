using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BabyCarrot.Tools
{
    public enum LogType {  Daily, Monthly }

    public class LogManager
    {
        private string _path;

        #region Constructors
        public LogManager(string path, LogType logType, string prefix, string postfix)
        {
            _path = path;
            _SetLogPath(logType, prefix, postfix);
        }

        public LogManager(string prefix, string postfix)
            : this(Path.Combine(Application.Root, "Log"), LogType.Daily, prefix, postfix)
        {

        }

        public LogManager()
            : this(Path.Combine(Application.Root, "Log"), LogType.Daily, null, null)
        {
         //   _path = Path.Combine(Application.Root, "Log"); // this(~~~) 통해서 LogManager(string path) 넘겨줌
         //  _SetLogPath();
        }
        #endregion

        #region Methods
        private void _SetLogPath(LogType logType, string prefix, string postfix)
        {
            string path = string.Empty;
            string name = string.Empty;

            switch (logType)
            {
                case LogType.Daily:
                    path = String.Format(@"{0}\{1}\", DateTime.Now.Year, DateTime.Now.ToString("MM"));
                    name = DateTime.Now.ToString("yyyyMMdd"); 
                    break;
                case LogType.Monthly:
                    path = String.Format(@"{0}\", DateTime.Now.Year);
                    name = DateTime.Now.ToString("yyyyMM");
                    break;
            }

            _path = Path.Combine(_path, path);

            // 경로 없으면 경로 생성
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            if (!String.IsNullOrEmpty(prefix))
                name = prefix + name;
            if (!String.IsNullOrEmpty(postfix))
                name = name + postfix;
            name += ".txt";

            // 로그 파일 이름 설정
            _path = Path.Combine(_path, name);
        }

        public void Write(string data)
        {
            try
            { 
                // 없으면 생성 or else 추가
                using (StreamWriter writer = new StreamWriter(_path, true))
                {
                    writer.Write(data);
                }
            } catch(Exception ex)
            {   }
        }

        public void WriteLine(string data)
        {
            try { 
                using (StreamWriter writer = new StreamWriter(_path, true))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss\t") + data);
                }
            } catch(Exception ex)
            {   }
        }
        #endregion
    }
}
