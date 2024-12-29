using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using AspireExample.Domain;

namespace AspireExample.Infrastructure;

public class LocalFileStorage : IFileStorage
{    
    public Task<Result<int>> Delete(string? filter = null)
    {
        throw new NotImplementedException();
    }

    public Task<Result<FileUploadId>> SaveAsync(string fileName, string contentType, Stream fileStream, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
