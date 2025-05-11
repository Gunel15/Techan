namespace Techan.Extension
{
    public static class FileExtension
    {
        public static bool IsValidType(this IFormFile file,string type)
        {
            return file.ContentType.StartsWith(type);
        }

        public static bool IsValidSize(this IFormFile file,int kb)
        {
            return file.Length <= kb * 1024;
        }

        public static async Task<string> UploadAsync(this IFormFile file,string path)
        {
            CheckDirectory(path);
            string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            await using (FileStream sw=new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(sw);
            }
            return fileName;
        }

        public static async Task UploadWithNameAsync(this IFormFile file, string path)
        {
            //CheckDirectory(Path.Combine(Directory.GetDirectories(path)));
           await using (FileStream sw = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(sw);
            }
          
        }

        static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path)) 
                Directory.CreateDirectory(path);
        }
    }
}
