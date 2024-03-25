using System.Globalization;

namespace Courses.WebApp.ViewModels.Index;

public class AppStoreModel
{
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public ushort StarCount { get; set; }
    public double Rating { get; set; }
    public double ReviewCount { get; set; }
    public string ReviewCountText => ReviewCount switch
    {
        < 1000 => ReviewCount.ToString(CultureInfo.InvariantCulture),
        _ => $"{Math.Floor(ReviewCount / 1000)}K+",
    };
    public string Image { get; set; } = null!;
}