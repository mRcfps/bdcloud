using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BDCloud
{
    [Serializable]

    /// <summary>  
    /// </summary>
    ///  将上传队列进行缓存（在系统重启后恢复上传队列使用）
    /// <returns></returns> 
    public class uploadQueueCache
    {
        private static Dictionary<int, int> eviId_progress;
        private static string path = "save_progress.bin";
        public static void add_update_Item(int eviId, int progress)
        {
            if (eviId_progress == null) initialize();
            if (eviId_progress.ContainsKey(eviId))
            {
                eviId_progress[eviId] = Math.Max(eviId_progress[eviId],progress);
            }
            else
            {
                eviId_progress.Add(eviId, progress);
            }
            save();
        }
        public static void deleteItem(int eviId)
        {
            if (eviId_progress == null) initialize();
            if (eviId_progress.ContainsKey(eviId))
            {
                eviId_progress.Remove(eviId);
            }
            save();
        }
        public static int getProgress(int eviId)
        {
            if (eviId_progress == null) initialize();
            if (eviId_progress.ContainsKey(eviId))
            {
                return eviId_progress[eviId];
            }
            return 0;
        }
        private static void save()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, eviId_progress);
            stream.Close();
        }
        private static void initialize()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                eviId_progress = (Dictionary<int, int>)formatter.Deserialize(stream);
                stream.Close();
            }
            catch
            {
                eviId_progress = new Dictionary<int, int>();
            }
        }
    }
}
