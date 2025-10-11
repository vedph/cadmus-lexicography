using Cadmus.Core;
using Cadmus.Lexicography.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Reflection;

namespace Cadmus.Seed.Lexicography.Parts.Test;

public sealed class LemmaPartSeederTest
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
        Type t = typeof(LemmaPartSeeder);
        TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
        Assert.NotNull(attr);
        Assert.Equal("seed.it.vedph.lexicography.lemma", attr!.Tag);
    }

    [Fact]
    public void Seed_Ok()
    {
        LemmaPartSeeder seeder = new();
        seeder.SetSeedOptions(_seedOptions);

        IPart? part = seeder.GetPart(_item, null, _factory);

        Assert.NotNull(part);

        LemmaPart? p = part as LemmaPart;
        Assert.NotNull(p);

        TestHelper.AssertPartMetadata(p!);

        Assert.NotEmpty(p!.Lid);
        Assert.NotEmpty(p.Forms);
    }
}
