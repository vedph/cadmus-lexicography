using Cadmus.Core;
using Cadmus.Lexicography.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Lexicography.Parts.Test;

public sealed class WordSensesPartSeederTest
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
        Type t = typeof(WordSensesPartSeeder);
        TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
        Assert.NotNull(attr);
        Assert.Equal("seed.it.vedph.lexicography.word-senses", attr!.Tag);
    }

    [Fact]
    public void Seed_Ok()
    {
        WordSensesPartSeeder seeder = new();
        seeder.SetSeedOptions(_seedOptions);

        IPart? part = seeder.GetPart(_item, null, _factory);

        Assert.NotNull(part);

        WordSensesPart? p = part as WordSensesPart;
        Assert.NotNull(p);

        TestHelper.AssertPartMetadata(p!);

        Assert.NotEmpty(p!.Senses);
    }
}
