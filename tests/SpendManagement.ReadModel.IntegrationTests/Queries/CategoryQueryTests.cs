﻿using Application.Queries.Category.GetCategories;
using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using SpendManagement.ReadModel.IntegrationTests.Fixtures;
using SpendManagement.ReadModel.IntegrationTests.Helpers;
using System.Net;

namespace SpendManagement.ReadModel.IntegrationTests.Queries
{
    [Collection(nameof(SharedFixtureCollection))]
    public class CategoryQueryTests(MongoDBFixture mongoDBFixture) : BaseTests
    {
        private readonly Fixture _fixture = new();
        private readonly MongoDBFixture _mongoDBFixture = mongoDBFixture;

        [Fact]
        private async Task OnGivenAValidGuidsAsCategoryFilter_ShouldBeReturnedAValidCategoryFromDataBase()
        {
            //Arrange
            var categoryId = _fixture.Create<Guid>();

            var category = _fixture.Build<Fixtures.Category>()
                .With(x => x.Id, categoryId)
                .Create();

            await _mongoDBFixture.InsertCategoryAsync(category);

            var categoryFilter = _fixture
                .Build<GetCategoriesRequest>()
                .With(x => x.CategoryIds, new List<Guid> { categoryId })
                .Create();

            //Act
            var (StatusCode, Content) = await GetAsync("/getCategories", categoryFilter, nameof(categoryFilter.CategoryIds));

            //Assert
            StatusCode.Should().Be(HttpStatusCode.OK);

            var categoriesResponse = JsonConvert.DeserializeObject<GetCategoriesResponse>(Content);

            categoriesResponse?.Results.Should().NotBeNull();
            categoriesResponse?.Results.Should().Contain(x => x.Id.Equals(categoryId));
        }

        [Fact]
        private async Task OnGivenAValidNamesAsCategoryFilter_ShouldBeReturnedAValidCategoryFromDataBase()
        {
            //Arrange
            var categoryName = _fixture.Create<string>();

            var category = _fixture.Build<Fixtures.Category>()
                .With(x => x.Name, categoryName)
                .Create();

            await _mongoDBFixture.InsertCategoryAsync(category);

            var categoryFilter = _fixture
                .Build<GetCategoriesRequest>()
                .With(x => x.CategoryNames, new List<string> { categoryName })
                .Without(x => x.CategoryIds)
                .Create();

            //Act
            var (StatusCode, Content) = await GetAsync("/getCategories", categoryFilter, nameof(categoryFilter.CategoryNames));

            //Assert
            StatusCode.Should().Be(HttpStatusCode.OK);

            var categoriesResponse = JsonConvert.DeserializeObject<GetCategoriesResponse>(Content);

            categoriesResponse?.Results.Should().NotBeNull();
            categoriesResponse?.Results.Should().Contain(x => x.Name!.Equals(categoryName));
        }
    }
}
