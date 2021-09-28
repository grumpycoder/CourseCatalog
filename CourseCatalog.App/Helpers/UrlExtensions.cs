using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace CourseCatalog.App.Helpers
{
    public static class UrlHelperExtensions
    {
        private static string WebRootBasePath = HttpContext.Current.Server.MapPath("~");
        private static readonly ConcurrentDictionary<string, string> CachedFileHashes =
            new ConcurrentDictionary<string, string>();

        public static string HashedContentVersioned(this UrlHelper urlHelper, string contentPath)
        {
            string url = urlHelper.Content(contentPath);

            // Check if we already cached the file hash in the cache. If not, add it using the inner method.
            string fileHash = CachedFileHashes.GetOrAdd(url, key =>
            {
                var fileInfo = new FileInfo(WebRootBasePath + key);

                // If file exists, generate a hash of it, otherwise return null.
                return fileInfo.Exists
                        ? ComputeFileHash(fileInfo.OpenRead())
                        : null;
            });

            return $"{url}?v={fileHash}";
        }

        private static string ComputeFileHash(Stream fileStream)
        {
            using (SHA256 hasher = new SHA256Managed())
            using (fileStream)
            {
                byte[] hashBytes = hasher.ComputeHash(fileStream);

                var sb = new StringBuilder(hashBytes.Length * 2);

                foreach (byte hashByte in hashBytes)
                {
                    sb.AppendFormat("{0:x2}", hashByte);
                }

                return sb.ToString();
            }
        }

    }

    public static class UrlExtensions
    {
        private const string FileDateTicksCacheKeyFormat = "FileDateTicks_{0}";

        private static long GetFileDateTicks(this UrlHelper urlHelper, string filename)
        {
            var context = urlHelper.RequestContext.HttpContext;
            string cacheKey = string.Format(FileDateTicksCacheKeyFormat, filename);

            // Check if we already cached the ticks in the cache.
            if (context.Cache[cacheKey] != null)
            {
                return (long)context.Cache[cacheKey];
            }

            var physicalPath = context.Server.MapPath(filename);
            var fileInfo = new FileInfo(physicalPath);
            var dependency = new CacheDependency(physicalPath);

            // If file exists, read number of ticks from last write date. Or fall back to 0.
            long ticks = fileInfo.Exists ? fileInfo.LastWriteTime.Ticks : 0;

            // Add the number of ticks to cache for 12 hours.
            // The cache dependency will remove the entry if file is changed or deleted.
            context.Cache.Add(cacheKey, ticks, dependency,
                DateTime.Now.AddHours(12), TimeSpan.Zero,
                CacheItemPriority.Normal, null);

            return ticks;
        }

        public static string ContentVersioned(this UrlHelper urlHelper, string contentPath)
        {
            string url = urlHelper.Content(contentPath);

            long fileTicks = GetFileDateTicks(urlHelper, url);

            return $"{url}?v={fileTicks}";
        }
    }

}
