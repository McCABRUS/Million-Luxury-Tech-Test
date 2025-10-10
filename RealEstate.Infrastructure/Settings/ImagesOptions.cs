namespace RealEstate.Infrastructure.Settings
{
    public class ImagesOptions
    {
        public string ImagesRootPath { get; set; } = string.Empty;
        public string ImagesRequestPath { get; set; } = "./assets/img/propertyImgs";
        public string? PublicBaseUrl { get; set; }
        public string? CacheControlHeader { get; set; } = "public,max-age=86400";
        public bool ServeUnknownFileTypes { get; set; } = false;

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(ImagesRootPath))
                throw new System.InvalidOperationException("ImagesOptions.ImagesRootPath cannot be empty. Set the physical path to the images.");

            if (string.IsNullOrWhiteSpace(ImagesRequestPath) || !ImagesRequestPath.StartsWith("/"))
                throw new System.InvalidOperationException("ImagesOptions.ImagesRequestPath must start with '/' and cannot be empty.");
        }
    }
}