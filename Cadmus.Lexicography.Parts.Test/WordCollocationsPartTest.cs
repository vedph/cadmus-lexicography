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

        // create 3 collocations with predictable POS sequences
        part.Collocations = [];
        HashSet<string> expectedPos = [];
        string[][] posSeqs =
        [
            ["ADJ", "NOUN"],
            ["ADJ", "NOUN", "VERB"],
            ["NOUN", "VERB"]
        ];

        for (int i = 0; i < 3; i++)
        {
            WordCollocation coll = new()
            {
                Rank = (short)(i + 1),
                Tokens = []
            };
            foreach (string p in posSeqs[i])
                coll.Tokens.Add(new WordCollocationToken { Pos = p });

            string joined = string.Join("+", posSeqs[i]);
            expectedPos.Add(joined);

            part.Collocations.Add(coll);
        }

        List<DataPin> pins = [.. part.GetDataPins(null)];

        // 1 tot-count + one pin per unique pos-sequence
        Assert.Equal(1 + expectedPos.Count, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        // assert every expected pos-sequence exists as a pin
        foreach (string pos in expectedPos)
        {
            DataPin? p = pins.Find(x => x.Name == "pos" && x.Value == pos);
            Assert.NotNull(p);
            TestHelper.AssertPinIds(part, p!);
        }
    }
}
