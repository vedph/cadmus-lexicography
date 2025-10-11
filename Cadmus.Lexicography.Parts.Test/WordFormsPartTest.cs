using Cadmus.Core;
using Cadmus.Seed.Lexicography.Parts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadmus.Lexicography.Parts.Test;

public sealed class WordFormsPartTest
{
    private static WordFormsPart GetPart()
    {
        WordFormsPartSeeder seeder = new();
        IItem item = new Item
        {
            FacetId = "default",
            CreatorId = "zeus",
            UserId = "zeus",
            Description = "Test item",
            Title = "Test Item",
            SortKey = ""
        };
        return (WordFormsPart)seeder.GetPart(item, null, null)!;
    }

    private static WordFormsPart GetEmptyPart()
    {
        return new WordFormsPart
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
        WordFormsPart part = GetPart();

        string json = TestHelper.SerializePart(part);
        WordFormsPart part2 =
            TestHelper.DeserializePart<WordFormsPart>(json)!;

        Assert.Equal(part.Id, part2.Id);
        Assert.Equal(part.TypeId, part2.TypeId);
        Assert.Equal(part.ItemId, part2.ItemId);
        Assert.Equal(part.RoleId, part2.RoleId);
        Assert.Equal(part.CreatorId, part2.CreatorId);
        Assert.Equal(part.UserId, part2.UserId);

        Assert.Equal(part.Forms.Count, part2.Forms.Count);
    }

    [Fact]
    public void GetDataPins_NoEntries_Ok()
    {
        WordFormsPart part = GetPart();
        part.Forms.Clear();

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
        WordFormsPart part = GetEmptyPart();

        // create 3 forms with predictable values for testing pins
        var expectedValues = new List<string>();
        var expectedDecValues = new List<string>();
        var expectedPos = new HashSet<string>();

        for (int n = 1; n <= 3; n++)
        {
            WordForm wf = new()
            {
                Value = $"form{n}"
            };
            expectedValues.Add(wf.Value);

            // alternate types and pos to exercise different code paths
            wf.Type = n % 2 == 0 ? "inflected" : "written";

            if (n == 1)
            {
                wf.Pos = "NOUN";
                expectedPos.Add("NOUN");
            }
            else if (n == 2)
            {
                wf.Pos = "VERB";
                expectedPos.Add("VERB");
            }
            else
            {
                // leave Pos null for third item to test absence
                wf.Pos = null;
            }

            // expected decorated value: type[pos]:value or type:value if no pos
            string dec = wf.Type
                + (string.IsNullOrEmpty(wf.Pos) ? "" : "[" + wf.Pos + "]")
                + ":" + wf.Value;
            expectedDecValues.Add(dec);

            part.Forms.Add(wf);
        }

        List<DataPin> pins = [.. part.GetDataPins(null)];

        // tot-count must be present and equal to 3
        DataPin? tot = pins.Find(p => p.Name == "tot-count");
        Assert.NotNull(tot);
        TestHelper.AssertPinIds(part, tot!);
        Assert.Equal("3", tot!.Value);

        // check that each expected value appears in the pins named "value"
        foreach (string v in expectedValues)
        {
            Assert.True(pins.Any(p => p.Name == "value" &&
                (p.Value == v || p.Value!.Contains(v))),
                $"Missing 'value' pin for '{v}'");
        }

        // check decorated values presence in pins named "dec-value"
        foreach (string dv in expectedDecValues)
        {
            Assert.True(pins.Any(p => p.Name == "dec-value" &&
                (p.Value == dv || p.Value!.Contains(dv))),
                $"Missing 'dec-value' pin for '{dv}'");
        }

        // check POS tags presence in pins named "pos"
        foreach (string pos in expectedPos)
        {
            Assert.True(pins.Any(p => p.Name == "pos" &&
                (p.Value == pos || p.Value!.Contains(pos))),
                $"Missing 'pos' pin for '{pos}'");
        }
    }
}
