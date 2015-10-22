using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sharp.Knife.Format;
using Xunit;

namespace Sharp.Knife.UnitTests.Format
{
    public class CollectionDelimiterFormatterTest
    {
        private IEnumerable<short> _ints;
        private IEnumerable<bool> _bools;
        private readonly IEnumerable<string> _strings;
        private readonly IEnumerable<DateTime> _dateTimes;
        private readonly IEnumerable<Person> _people;

        public CollectionDelimiterFormatterTest()
        {
            _ints = new List<short> { 1, 2, 3, 4, 5 };
            _bools = new List<bool> { true, false, false, false, true };
            _strings = new List<string> { "hello", "world", "sharp" };
            _dateTimes = new List<DateTime> { new DateTime(2014, 12, 25), new DateTime(2015, 1, 1), new DateTime(1900, 1, 1) };
            _people = new Collection<Person>
                {
                    new Person{FirstName = "Bryan", LastName = "Byrd"},
                    new Person{FirstName = "Jenevieve", LastName = "Byrd"},
                    new Person{FirstName = "Michael", LastName = "Byrd"}
                };
        }

        [Fact]
        public void EmptyLists()
        {
            _ints = null;
            _bools = new List<Boolean>();
            Assert.Equal(string.Empty, string.Format(new IntCollectionDelimiterFormatter(", "), "{0}", _ints));
            Assert.Equal(string.Empty, string.Format(new IntCollectionDelimiterFormatter(", "), "{0}", _bools));
        }

        [Fact]
        public void CommaDelimited()
        {
            Assert.Equal("1, 2, 3, 4, 5", string.Format(new IntCollectionDelimiterFormatter(", "), "{0}", _ints));
            Assert.Equal("true,false,false,false,true", string.Format(new BoolCollectionDelimiterFormatter(","), "{0}", _bools).ToLower());
            Assert.Equal("hello,world,sharp", string.Format(new StringCollectionDelimiterFormatter(","), "{0}", _strings));
            Assert.Equal("12/25/2014,01/01/2015,01/01/1900", string.Format(new DateTimeCollectionDelimiterFormatter(","), "{0}", _dateTimes));
            Assert.Equal("Bryan Byrd,Jenevieve Byrd,Michael Byrd", string.Format(new PersonCollectionDelimiterFormatter(","), "{0}", _people));
        }

        [Fact]
        public void SemiColonDelimited()
        {
            Assert.Equal("1;2;3;4;5", string.Format(new IntCollectionDelimiterFormatter(";"), "{0}", _ints));
            Assert.Equal("true;false;false;false;true", string.Format(new BoolCollectionDelimiterFormatter(";"), "{0}", _bools).ToLower());
            Assert.Equal("hello;world;sharp", string.Format(new StringCollectionDelimiterFormatter(";"), "{0}", _strings));
            Assert.Equal("12/25/2014;01/01/2015;01/01/1900", string.Format(new DateTimeCollectionDelimiterFormatter(";"), "{0}", _dateTimes));
            Assert.Equal("Bryan Byrd;Jenevieve Byrd;Michael Byrd", string.Format(new PersonCollectionDelimiterFormatter(";"), "{0}", _people));
        }

        [Fact]
        public void DelimitedDelimited()
        {
            Assert.Equal("1delimited2delimited3delimited4delimited5", string.Format(new IntCollectionDelimiterFormatter("delimited"), "{0}", _ints));
            Assert.Equal("truedelimitedfalsedelimitedfalsedelimitedfalsedelimitedtrue", string.Format(new BoolCollectionDelimiterFormatter("delimited"), "{0}", _bools).ToLower());
            Assert.Equal("hellodelimitedworlddelimitedsharp", string.Format(new StringCollectionDelimiterFormatter("delimited"), "{0}", _strings));
            Assert.Equal("12/25/2014delimited01/01/2015delimited01/01/1900", string.Format(new DateTimeCollectionDelimiterFormatter("delimited"), "{0}", _dateTimes));
            Assert.Equal("Bryan ByrddelimitedJenevieve ByrddelimitedMichael Byrd", string.Format(new PersonCollectionDelimiterFormatter("delimited"), "{0}", _people));
        }

        [Fact]
        public void SpaceDelimited()
        {
            Assert.Equal("1 2 3 4 5", string.Format(new IntCollectionDelimiterFormatter(" "), "{0}", _ints));
            Assert.Equal("true false false false true", string.Format(new BoolCollectionDelimiterFormatter(" "), "{0}", _bools).ToLower());
            Assert.Equal("hello world sharp", string.Format(new StringCollectionDelimiterFormatter(" "), "{0}", _strings));
            Assert.Equal("12/25/2014 01/01/2015 01/01/1900", string.Format(new DateTimeCollectionDelimiterFormatter(" "), "{0}", _dateTimes));
            Assert.Equal("Bryan Byrd Jenevieve Byrd Michael Byrd", string.Format(new PersonCollectionDelimiterFormatter(" "), "{0}", _people));
        }
    }

    public class IntCollectionDelimiterFormatter : CollectionDelimiterFormatter
    {
        public IntCollectionDelimiterFormatter(string delimiter) : base(delimiter) { }

        public override string FormatObject(object aObject)
        {
            var primitive = aObject as short? ?? 0;
            return primitive.ToString();
        }
    }

    public class BoolCollectionDelimiterFormatter : CollectionDelimiterFormatter
    {
        public BoolCollectionDelimiterFormatter(string delimiter) : base(delimiter) { }

        public override string FormatObject(object aObject)
        {
            var primitive = aObject is bool && (bool)aObject;
            return primitive.ToString();
        }
    }

    public class StringCollectionDelimiterFormatter : CollectionDelimiterFormatter
    {
        public StringCollectionDelimiterFormatter(string delimiter) : base(delimiter) { }

        public override string FormatObject(object aObject)
        {
            var s = aObject as string;
            var primitive = s ?? string.Empty;
            return primitive;
        }
    }

    public class DateTimeCollectionDelimiterFormatter : CollectionDelimiterFormatter
    {
        public DateTimeCollectionDelimiterFormatter(string delimiter) : base(delimiter) { }

        public override string FormatObject(object aObject)
        {
            var dateTime = aObject as DateTime? ?? new DateTime();
            return dateTime.ToString("MM/dd/yyyy");
        }
    }

    public class PersonCollectionDelimiterFormatter : CollectionDelimiterFormatter
    {
        public PersonCollectionDelimiterFormatter(string delimiter) : base(delimiter) { }

        public override string FormatObject(object aObject)
        {
            var person = aObject as Person;
            return person != null ? person.FirstName + " " + person.LastName : string.Empty;
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}