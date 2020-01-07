using System;
using Xunit;
using ExpectedObjects;
using Newbeststories.Models;

namespace Newbeststories.Tests
{
    public class StoryTest
    {
        private readonly long id;
        private readonly string title;
        private readonly string uri;
        private readonly string postedBy;
        private readonly DateTime time;
        private readonly int score;
        private readonly int commentCount;

        public StoryTest()
        {
            id = (long) 1;
            title = "Test title";
            uri = "http://someurl.html";
            postedBy = "Some One";
            time = new DateTime();
            score = 1;
            commentCount = 1;
        }

        [Fact]
        public void Should_Create_A_Story()
        {
            // Arrange
            var expectedStory = new
            {
                id = (long) 1,
                title = "Test title",
                uri = "http://someurl.html",
                postedBy = "Some One",
                time = new DateTime(),
                score = 1,
                commentCount = 1,
            };

            // Act
            Story story = new Story(
                expectedStory.id,
                expectedStory.title,
                expectedStory.uri,
                expectedStory.postedBy,
                expectedStory.time,
                expectedStory.score,
                expectedStory.commentCount
            );

            // Assert
            expectedStory.ToExpectedObject().ShouldMatch(story);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_ThrowException_When_Id_Is_Invalid(long invalid)
        {
            Assert.Throws<ArgumentException>(() =>
                new Story(invalid, title, uri, postedBy, time, score, commentCount)
            ).WithMessage("Invalid id");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_ThrowException_When_Title_Is_Invalid(string invalid)
        {
            Assert.Throws<ArgumentException>(() =>
                new Story(id, invalid, uri, postedBy, time, score, commentCount)
            ).WithMessage("Invalid title - ID: 1");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_ThrowException_When_PostedBy_Is_Invalid(string invalid)
        {
            Assert.Throws<ArgumentException>(() =>
                new Story(id, title, uri, invalid, time, score, commentCount)
            ).WithMessage("Invalid posted by - ID: 1");
        }
    }
}
