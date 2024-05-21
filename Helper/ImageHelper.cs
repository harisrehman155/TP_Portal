
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;

namespace TP_Portal.Helper;

public class ImageHelper
{
    private readonly IWebHostEnvironment _environment;
    public ImageHelper(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<(List<string> ImagePaths, int FailedCount)> UploadMultiImage(IFormFileCollection fileCollection, string orderNo)
    {
        int successCount = 0, failCount = 0;
        var imagesPaths = new List<string>();
        try
        {
            // Path to the Upload folder inside wwwroot
            string uploadFolder = Path.Combine(_environment.WebRootPath, "Upload");

            // Create the Upload folder if it doesn't exist
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Path to the order-specific folder inside Upload folder
            string orderFolderPath = Path.Combine(uploadFolder, orderNo);

            // Create the order-specific folder if it doesn't exist
            if (!Directory.Exists(orderFolderPath))
            {
                Directory.CreateDirectory(orderFolderPath);
            }
            else
            {
                Directory.Delete(orderFolderPath, true);
                Directory.CreateDirectory(orderFolderPath);
            }

            foreach (var file in fileCollection)
            {
                // Path to save the uploaded file inside the order-specific folder
                string imagePath = Path.Combine(orderFolderPath, file.FileName);

                // Save the uploaded file to the specified path
                using (FileStream stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    successCount++;
                    imagesPaths.Add(imagePath);
                }
            }
        }
        catch (IOException ex)
        {
            // Handle IO exceptions (e.g., disk full, permissions issue)
            failCount++;
        }
        catch (UnauthorizedAccessException ex)
        {
            // Handle permissions-related exceptions
            failCount++;
        }
        catch (Exception ex)
        {
            // Handle other unexpected exceptions
            failCount++;
        }

        return (imagesPaths, failCount);
    }

    public async Task<(List<string> ImagePaths, int FailedCount)> UploadOrderFiles(IFormFileCollection fileCollection, string orderNo)
    {
        int successCount = 0, failCount = 0;
        var imagesPaths = new List<string>();
        try
        {
            // Path to the Upload folder inside wwwroot
            string uploadFolder = Path.Combine(_environment.WebRootPath, "Download");

            // Create the Upload folder if it doesn't exist
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Path to the order-specific folder inside Upload folder
            string orderFolderPath = Path.Combine(uploadFolder, orderNo);

            // Create the order-specific folder if it doesn't exist
            if (!Directory.Exists(orderFolderPath))
            {
                Directory.CreateDirectory(orderFolderPath);
            }
            else
            {
                Directory.Delete(orderFolderPath, true);
                Directory.CreateDirectory(orderFolderPath);
            }

            foreach (var file in fileCollection)
            {
                // Path to save the uploaded file inside the order-specific folder
                string imagePath = Path.Combine(orderFolderPath, file.FileName);

                // Save the uploaded file to the specified path
                using (FileStream stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    successCount++;
                    imagesPaths.Add(imagePath);
                }
            }
        }
        catch (IOException ex)
        {
            // Handle IO exceptions (e.g., disk full, permissions issue)
            failCount++;
        }
        catch (UnauthorizedAccessException ex)
        {
            // Handle permissions-related exceptions
            failCount++;
        }
        catch (Exception ex)
        {
            // Handle other unexpected exceptions
            failCount++;
        }

        return (imagesPaths, failCount);
    }

    public async Task<FileStreamResult> DownloadOrderFiles(string orderNo)
    {
        try
        {
            // Path to the Upload folder inside wwwroot
            string uploadFolder = Path.Combine(_environment.WebRootPath, "Download");

            // Path to the order-specific folder inside Upload folder
            string orderFolderPath = Path.Combine(uploadFolder, orderNo);

            if (Directory.Exists(orderFolderPath))
            {
                // Create a unique temporary file to store the zip archive
                string zipFilePath = Path.Combine(Path.GetTempPath(), $"{orderNo}.zip");

                // Create a new zip archive for the order-specific folder
                ZipFile.CreateFromDirectory(orderFolderPath, zipFilePath, CompressionLevel.Fastest, false);

                // Read the zip file into a MemoryStream
                MemoryStream memoryStream = new MemoryStream();
                using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Open))
                {
                    await zipStream.CopyToAsync(memoryStream);
                }

                // Clean up the temporary zip file
                File.Delete(zipFilePath);

                // Reset the MemoryStream position to the beginning
                memoryStream.Position = 0;

                // Return the zip file as a downloadable file with a .zip extension
                return new FileStreamResult(memoryStream, "application/zip")
                {
                    FileDownloadName = $"{orderNo}.zip"
                };
            }
            else
            {
                return null;
            }
        }
        catch (IOException ex)
        {
            // Handle IO exceptions (e.g., disk full, permissions issue)
            throw;
        }
    }
}





// public async Task<File> DownloadOrderFiles(string orderNo)
// {
//     try
//     {
//         // Path to the Upload folder inside wwwroot
//         string uploadFolder = Path.Combine(_environment.WebRootPath, "Download");

//         // Path to the order-specific folder inside Upload folder
//         string orderFolderPath = Path.Combine(uploadFolder, orderNo);

//         if (Directory.Exists(orderFolderPath))
//         {
//             // Create a unique temporary file to store the zip archive
//             string zipFilePath = Path.Combine(Path.GetTempPath(), $"{orderNo}.zip");

//             // Create a new zip archive for the order-specific folder
//             ZipFile.CreateFromDirectory(orderFolderPath, zipFilePath, CompressionLevel.Fastest, false);

//             // Read the zip file into a MemoryStream
//             MemoryStream memoryStream = new MemoryStream();
//             using (FileStream zipStream = new FileStream(zipFilePath, FileMode.Open))
//             {
//                 await zipStream.CopyToAsync(memoryStream);
//             }

//             // Clean up the temporary zip file
//             System.IO.File.Delete(zipFilePath);

//             // Reset the MemoryStream position to the beginning
//             memoryStream.Position = 0;

//             // Return the zip file as a downloadable file with a .zip extension
//             return File(memoryStream, "application/zip", $"{orderNo}.zip");
//         }
//         else
//         {
//             return NotFound();
//         }
//     }
//     catch (IOException ex)
//     {
//         // Handle IO exceptions (e.g., disk full, permissions issue)
//     }
// }

