using UnityEngine.Device;
using Application = UnityEngine.Application;

namespace Constants
{
    public static class SaveNames
    {
        public static readonly string FilePath = Application.persistentDataPath + "/PlayerProgressionData.json";
    }
}