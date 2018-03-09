using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDCloud.Ftp
{
    public class Task
    {
        public TaskStatus Status;
        public int EviID;

        public Task()
        {
        }
    }

    public enum TaskStatus
    {
        UPLOADING = 0,
        WAITING = 1,
        FINISHED = 2
    }
}
