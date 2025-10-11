using Cadmus.Core;
using Cadmus.Lexicography.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Reflection;

namespace Cadmus.Seed.Lexicography.Parts.Test;

public sealed class WordCollocationsPartSeederTest
{
    private static readonly PartSeederFactory _factory =
        TestHelper.GetFactory();
    private static readonly SeedOptions _seedOptions =
        _factory.GetSeedOptions();
    private static readonly IItem _item =
        _factory.GetItemSeeder().GetItem(1, "facet");

    [Fact]
    public void TypeHasTagAttribute()
    {
        Type t = typeof(WordCollocationsPartSeeder);
        TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
        Assert.NotNull(attr);
        Assert.Equal("seed.it.vedph.lexicography.word-collocations", attr!.Tag);
    }

    [Fact]
    public void Seed_Ok()
    {
        WordCollocationsPartSeeder seeder = new();
        seeder.SetSeedOptions(_seedOptions);

        IPart? part = seeder.GetPart(_item, null, _factory);

        Assert.NotNull(part);

        WordCollocationsPart? p = part as WordCollocationsPart;
        Assert.NotNull(p);

        TestHelper.AssertPartMetadata(p!);

        Assert.NotEmpty(p!.Collocations);
    }
}
