using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xunit;
using NSubstitute;
using FluentAssertions;
using Newbeststories.Services;
using Newbeststories.Models;

namespace Newbeststories.Tests
{
    public class StoryServiceTest
    {
        [Fact]
        public async void Should_Get_The_List_Of_Ids()
        {
            // Arrange
            var listOfIds = new List<long> { 1, 2 };
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var url = "http://good.uri";
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage() {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(listOfIds), Encoding.UTF8, "application/json") 
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);
            var service = new StoryService(httpClientFactoryMock);

            // Act
            var result = await service.getIds(url);

            // Assert
            result.Should()
            .BeOfType<List<long>>().And
            .HaveCount(2).And
            .Contain(1).And
            .Contain(2);
        }

        [Fact]
        public async void Should_Get_A_Story()
        {
            // Arrange
            Story story = new Story (5, "Title Goes Here", "Post URI", "Posted By Someone", new DateTime(), 100, 10);
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var url = "http://good.uri";
            string str = JsonConvert.SerializeObject(story);
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage() {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(str, Encoding.UTF8, "application/json") 
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);
            var service = new StoryService(httpClientFactoryMock);

            // Act
            var result = await service.getStory(url);

            // Assert
            result.Should()
                .BeOfType<Story>().And
                .Match<Story>((s) => 
                    s.id == (long)5 &&
                    s.title == "Title Goes Here"
            );
        }

        [Fact]
        public void Should_Get_A_Formated_Date()
        {
            // Arrange
            long dateLong = 1578017228;
            DateTime date = new DateTime(2020, 1, 3, 2, 7, 8);

            // Act
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var service = new StoryService(httpClientFactoryMock);
            var result = service.getFormatedDate(dateLong);

            // Assert
            result.Should().Be(date);
        }
    }
}
