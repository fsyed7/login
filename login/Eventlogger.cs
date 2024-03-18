using System;
using System;.IO;
using Newtonsoft.Json;

// Define a class to represent the data packet that will be logged
public class DataPacket
{
    public string CanvasId{ get; set; }
    public string UserId{ get; set; }
    public string ActionType{ get; set; } 
    public string Content{ get; set; } 
    public DateTime Timestamp{ get; set; }

        public string SerializeToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class EventLogger
{
    private string logDirectory;

    public EventLogger(string logDir)
    {
        logDirectory = logDir;
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
    }

    public void LogDataPacket(DataPacket packet)
    {
        string fileName = $"log_{packet.CanvasId}_{DateTime.Now:yyyyMMdd}.txt";
        string filePath = Path.Combine(logDirectory, fileName);

        string json = packet.SerializeToJson();
        File.AppendAllText(filePath, json + Environment.NewLine);
    }
}

// Example 
public class NoteSyncApp
{
    private EventLogger logger = new EventLogger("Path_To_Log_Folder");

    public void ProcessAction(string canvasId, string userId, string actionType, string content)
    {
        
        DataPacket packet = new DataPacket
        {
            CanvasId = canvasId,
            UserId = userId,
            ActionType = actionType,
            Content = content,
            Timestamp = DateTime.UtcNow
        };

        // Log the data packet
        logger.LogDataPacket(packet);
    }
}

