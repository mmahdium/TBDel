using System.Text.Json.Serialization;
using TBDel.Models;


// All this just because the binary size was 40MB for this little CLI tool
namespace TBDel.Services
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(DatabaseContent))]
    [JsonSerializable(typeof(FileEntry))]
    [JsonSerializable(typeof(FolderEntry))]
    internal partial class JsonContext : JsonSerializerContext
    {
    }
}