using System;
using Cadmus.Core;
using System.Collections.Generic;
using Cadmus.Seed.Lexicography.Parts;

namespace Cadmus.Lexicography.Parts.Test;

public sealed class LemmaPartTest
{
    private static LemmaPart GetPart()
    {
        LemmaPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (LemmaPart)seeder.GetPart(item, null, null)!;
    }

    private static LemmaPart GetEmptyPart()
    {
        return new LemmaPart
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
        LemmaPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        LemmaPart part2 = TestHelper.DeserializePart<LemmaPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);
    }

    [Fact]
    public void GetDataPins_Ok()
    {
        LemmaPart part = GetEmptyPart();
        part.Lid = "test";

        // create 3 predictable word forms
        part.Forms = [];
        for (int i = 1; i <= 3; i++)
        {
            part.Forms.Add(new WordForm
            {
                Type = $"t{i}",
                Value = $"v{i}",
                // set Pos for first two forms only to get pos pins
                Pos = i == 1 ? "n" : (i == 2 ? "v" : null)
            });
        }

        List<DataPin> pins = [.. part.GetDataPins(null)];

        // expected pins:
        // 1 lid + 3 value + 3 dec-value + 2 pos = 9
        Assert.Equal(9, pins.Count);

        // LID
        DataPin? lidPin = pins.Find(p => p.Name == "lid" && p.Value == "test");
        Assert.NotNull(lidPin);
        TestHelper.AssertPinIds(part, lidPin!);

        // value pins
        for (int i = 1; i <= 3; i++)
        {
            string expected = $"v{i}";
            DataPin? p = pins.Find(pin => pin.Name == "value" && pin.Value == expected);
            Assert.NotNull(p);
            TestHelper.AssertPinIds(part, p!);
        }

        // dec-value pins: format "type[pos]:value" (pos part omitted if empty)
        var expectedDec = new List<string>
        {
            "t1[n]:v1",
            "t2[v]:v2",
            "t3:v3"
        };
        foreach (string dec in expectedDec)
        {
            DataPin? p = pins.Find(pin => pin.Name == "dec-value" && pin.Value == dec);
            Assert.NotNull(p);
            TestHelper.AssertPinIds(part, p!);
        }

        // pos pins (unique)
        foreach (string pos in new[] { "n", "v" })
        {
            DataPin? p = pins.Find(pin => pin.Name == "pos" && pin.Value == pos);
            Assert.NotNull(p);
            TestHelper.AssertPinIds(part, p!);
        }
    }
}
