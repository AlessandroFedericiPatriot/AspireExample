using AspireExample.Application;
using AspireExample.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AspireExample.UnitTests.Domain
{
    public class GeneralDomainTests
    {       
        [Fact]
        public void FileDigest_creation()
        {
            var digest = new FileDigest(
                uploadId: FileUploadId.From(1),
                subject: "subject",
                summary: null,
                details: null);
        }

        [Fact]
        public void FileUpload_creation()
        {
            var upload = new FileUpload(
                fileName: "file.txt",
                location: new Uri("http://localhost"),
                contentType: "text/plain",
                size: 123);
        }
    }
}
