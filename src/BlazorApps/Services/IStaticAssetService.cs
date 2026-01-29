// ------------------------------------------------------------------------
// This file is licensed to you under the MIT License.
// ------------------------------------------------------------------------

namespace BlazorApps.Services;

public interface IStaticAssetService
{
    public Task<string?> GetAsync(string assetUrl, bool useCache = true);
}
