using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTP
{
    public class Task
    {
        public string eviId;
        public int progress;
        public UploadState status;

        public Task()
        {
        }

        public void Start()
        {
            
        }

        public void Run()
        {
        }

        public void Pause()
        {
        }

        public void Cancel()
        {
        }
    }

    public enum UploadState
    {
        Uploading = 0,
        Paused = 1,
        Finished = 2
    }
}
