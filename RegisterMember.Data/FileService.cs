using System;
using System.IO;
using System.Threading.Tasks;

namespace RegisterMember.Data
{
    public static class FileService
    {
        public static async Task<string> ReadFile(string filepath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string contents = await reader.ReadToEndAsync();
                    return contents;
                }
            }
            catch(FileNotFoundException fnfex)
            {
                throw new Exception(fnfex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async void WriteFile(string filepath, string contents)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(filepath))
                {
                    await writer.WriteLineAsync(contents);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
