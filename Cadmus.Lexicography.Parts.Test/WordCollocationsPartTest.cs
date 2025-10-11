using Cadmus.Core;
using Cadmus.Seed.Lexicography.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Lexicography.Parts.Test;

public sealed class WordCollocationsPartTest
{
    private static WordCollocationsPart GetPart()
    {
        WordCollocationsPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (WordCollocationsPart)seeder.GetPart(item, null, null)!;
    }

    private static WordCollocationsPart GetEmptyPart()
    {
        return new WordCollocationsPart
        {
            ItemId = Guid.NewGuid().ToString(),
            RoleId = "some-role",
            CreatorId = "zeus",
            UserId = "another",
        };
    }

    [Fact]
    public void Part_Is_Serializable()
    {
        WordCollocationsPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        WordCollocationsPart part2 =
            TestHelper.DeserializePart<WordCollocationsPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Collocations.Count, part2.Collocations.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        WordCollocationsPart part = GetPart();
        part.Collocations.Clear();

        List<DataPin> pins = [.. part.GetDataPins(null)];

        Assert.Single(pins);
        DataPin pin = pins[0];
        Assert.Equal("tot-count", pin.Name);
        TestHelper.AssertPinIds(part, pin);
        Assert.Equal("0", pin.Value);
    }

    [Fact]
    public void GetDataPins_Entries_Ok()
    {
        WordCollocationsPart part = GetEmptyPart();

        for (int n = 1; n <= 3; n++)
        {
            // TODO add entry to part setting its pin-related
            // properties in a predictable way, so we can test them
        }

        List<DataPin> pins = [.. part.GetDataPins(null)];

        Assert.Equal(5, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        // TODO: assert counts and values e.g.:
        // pin = pins.Find(p => p.Name == "pos-bottom-count");
        // Assert.NotNull(pin);
        // TestHelper.AssertPinIds(part, pin!);
        // Assert.Equal("2", pin.Value);
    }
}
