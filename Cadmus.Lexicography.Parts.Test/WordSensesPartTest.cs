using Cadmus.Core;
using Cadmus.Seed.Lexicography.Parts;
using Cadmus.Refs.Bricks;
using System;
using System.Collections.Generic;

namespace Cadmus.Lexicography.Parts.Test;

public sealed class WordSensesPartTest
{
    private static WordSensesPart GetPart()
    {
        WordSensesPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (WordSensesPart)seeder.GetPart(item, null, null)!;
    }

    private static WordSensesPart GetEmptyPart()
    {
        return new WordSensesPart
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
        WordSensesPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        WordSensesPart part2 =
            TestHelper.DeserializePart<WordSensesPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Senses.Count, part2.Senses.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        WordSensesPart part = GetPart();
        part.Senses.Clear();

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
        WordSensesPart part = GetEmptyPart();

        part.Senses = [];

        // we'll create 3 senses:
        // - two with eid values (eid1, eid2) and one without
        // - related ids across senses such that there are exactly 2 unique related GIDs
        HashSet<string> expectedEids = [];
        HashSet<string> expectedRelated = [];

        for (int n = 1; n <= 3; n++)
        {
            WordSense sense = new();

            // set Eids for the first two senses only
            if (n <= 2)
            {
                sense.Eid = $"eid{n}";
                expectedEids.Add(sense.Eid);
            }

            // set related ids:
            sense.RelatedIds = [];
            if (n == 1)
            {
                // first sense has related id rel-a
                sense.RelatedIds.Add(new AssertedCompositeId
                {
                    Target = new PinTarget { Gid = "rel-a" }
                });
                expectedRelated.Add("rel-a");
            }
            else if (n == 2)
            {
                // second sense has related id rel-b and an empty/invalid one (skipped)
                sense.RelatedIds.Add(new AssertedCompositeId
                {
                    Target = new PinTarget { Gid = "rel-b" }
                });
                sense.RelatedIds.Add(new AssertedCompositeId
                {
                    Target = new PinTarget { Gid = "" } // should be ignored by part
                });
                expectedRelated.Add("rel-b");
            }
            else
            {
                // third sense has related id rel-a again (duplicate)
                sense.RelatedIds.Add(new AssertedCompositeId
                {
                    Target = new PinTarget { Gid = "rel-a" }
                });
            }

            part.Senses.Add(sense);
        }

        List<DataPin> pins = [.. part.GetDataPins(null)];

        // 1 tot-count + unique sense-id pins (2) + unique related-id pins (2) => 5
        Assert.Equal(5, pins.Count);

        DataPin? pin = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);
        Assert.Equal("3", pin!.Value);

        // assert each expected eid appears as a "sense-id" pin
        foreach (string eid in expectedEids)
        {
            DataPin? p = pins.Find(x => x.Name == "sense-id" && x.Value == eid);
            Assert.NotNull(p);
            TestHelper.AssertPinIds(part, p!);
        }

        // assert each expected related gid appears as a "related-id" pin
        foreach (string gid in expectedRelated)
        {
            DataPin? p = pins.Find(x => x.Name == "related-id" && x.Value == gid);
            Assert.NotNull(p);
            TestHelper.AssertPinIds(part, p!);
        }
    }
}
