using System.Text.Json;

namespace AspireExample.UnitTests.Domain
{
    public class GeneralDomainTests
    {
        [Fact]
        public void FileUpload_creation()
        {
            var upload = FileUpload.CreateNew(
                fileName: "file.txt",
                location: new Uri("http://localhost"),
                contentType: "text/plain",
                size: 123);
        }

        static FileUpload DummyUpload = FileUpload
            .CreateNew("dummy.txt", new Uri("http://localhost"), "text/plain", 123);

        [Fact]
        public void FileDigest_creation()
        {
            var digest = FileDigest.For(
                fileUpload: DummyUpload,
                subject: "subject",
                summary: null,
                details: null);
        }

        [Fact]
        public void StronglyTypedId_serializes_right()
        {
            var digest = FileDigest.For(
                fileUpload: DummyUpload,
                subject: "subject",
                summary: "just a summary",
                details: "some details");
            
            var json = JsonSerializer.Serialize(digest);
        }
    }
}
