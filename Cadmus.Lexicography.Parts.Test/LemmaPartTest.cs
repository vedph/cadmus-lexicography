using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
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
        // TODO: set only the properties required for pins
        // in a predictable way so we can test them

        List<DataPin> pins = [.. part.GetDataPins(null)];
        Assert.Single(pins);

        // lid
        DataPin? pin = pins.Find(p => p.Name == "id" && p.Value == "steph");
        Assert.NotNull(pin);
        TestHelper.AssertPinIds(part, pin!);

        // TODO test other pins
    }
}
