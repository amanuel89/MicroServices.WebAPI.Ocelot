using System;
using System.IO;
using System.Net;
using System;
using System.IO;
using SixLabors.ImageSharp;
using System.Reflection;
using SixLabors.ImageSharp.Formats.Webp;

public class ImageUploadResponse
{
    public bool Success { get; set; }
    public string Path { get; set; }
    public string ErrorMessage { get; set; }
}

public class ImageUploader
{
    private readonly string _destinationFolder;

    public ImageUploader( )
    {
        _destinationFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    }

    public ImageUploadResponse UploadToWwwRoot(byte[] file, string fileName, IMAGECATEGORY category)
    {
        try
        {
            // Load the image from byte array
            using (var image = SixLabors.ImageSharp.Image.Load(file))
            {
                // Check if the loaded data represents a valid image
                if (image.Width == 0 || image.Height == 0)
                {
                    return new ImageUploadResponse { Success = false, ErrorMessage = "Invalid image format" };
                }

                // Check file size
                long fileSizeInBytes = file.Length;
                const long maxFileSize = 3 * 1024 * 1024; // 3 MB
                if (fileSizeInBytes > maxFileSize)
                {
                    return new ImageUploadResponse { Success = false, ErrorMessage = "Image size exceeds 3 MB limit" };
                }

                // Convert the image to WebP format
                using (MemoryStream webpStream = new MemoryStream())
                {
                    image.SaveAsWebp(webpStream);

                    // Get the byte array of the WebP image
                    byte[] webpData = webpStream.ToArray();

                    // Create category folder if it doesn't exist
                    string categoryFolder = category.ToString().ToLower();
                    string wwwRootPath = Path.Combine(_destinationFolder, categoryFolder);
                    if (!Directory.Exists(wwwRootPath))
                    {
                        Directory.CreateDirectory(wwwRootPath);
                    }

                    // Upload the WebP image
                    string webpFileName = Path.ChangeExtension(fileName, "webp");
                    string filePath = Path.Combine(wwwRootPath, webpFileName);
                    File.WriteAllBytes(filePath, webpData);

                    // Return success response with the image path
                    return new ImageUploadResponse { Success = true, Path = $"{categoryFolder}/{webpFileName}" };
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions and return error response
            return new ImageUploadResponse { Success = false, ErrorMessage = ex.Message };
        }
    }  
}
