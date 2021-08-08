using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Tacx.Activities.Api.Controllers;
using Tacx.Activities.Core.Commands;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Queries;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Tacx.Activities.Api.Tests.Controllers
{
    public class Tests
    {
        [Test]
        public async Task Create_Returns_BadRequest_Invalid_FormFile()
        {
            var mediator = new Mock<IMediator>();
            var data = JsonSerializer.SerializeToUtf8Bytes(mediator);
            await using var stream = new MemoryStream(data);


            var sut = new ActivitiesController(mediator.Object);

            var result = await sut.Create(new FormFile(stream, 0, stream.Length, "test", "File")) as BadRequestObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo((int) HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_Creates_Activity()
        {
            var mediator = new Mock<IMediator>();
            CreateActivityCommand actualCommand = null;

            var dto = GetActivityDto();
            var data = JsonSerializer.SerializeToUtf8Bytes(dto);
            await using var stream = new MemoryStream(data);

            mediator.Setup(x => x.Send(It.IsAny<CreateActivityCommand>(), It.IsAny<CancellationToken>()))
                .Callback((IRequest<ActivityDto> request, CancellationToken _) =>
                {
                    actualCommand = request as CreateActivityCommand;

                }).ReturnsAsync(dto);

            var sut = new ActivitiesController(mediator.Object);

            var result = await sut.Create(new FormFile(stream, 0, stream.Length, "Activity", "File")) as OkObjectResult;
            Assert.That(result.StatusCode, Is.EqualTo((int) HttpStatusCode.OK));
            Assert.That((ActivityDto) result.Value, Is.EqualTo(dto));
            Assert.That(actualCommand.Activity, Is.EqualTo(dto));

            mediator.Verify(x => x.Send(It.IsAny<CreateActivityCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediator.VerifyNoOtherCalls();
        }

        [Test]
        public async Task GetById_Returns_BadRequest()
        {
            var mediator = new Mock<IMediator>(); 
            mediator.Setup(x => x.Send(It.IsAny<GetActivityQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((ActivityDto) null);

            var sut = new ActivitiesController(mediator.Object);

            var response = await sut.GetById("1");

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetById_Returns_Activity()
        {
            const string id = "1";
            GetActivityQuery actualQuery = null;
            var expectedResult = GetActivityDto();

            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(new GetActivityQuery(id), It.IsAny<CancellationToken>()))
                .Callback((IRequest<ActivityDto> request, CancellationToken _) =>
                {
                    actualQuery = request as GetActivityQuery;
                }).ReturnsAsync(expectedResult);

            var sut = new ActivitiesController(mediator.Object);

            var response = await sut.GetById(id);

            Assert.That(actualQuery.Id, Is.EqualTo("1"));
            Assert.That(response, Is.InstanceOf<OkObjectResult>());

            var result = response as OkObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

            var actualResult = result.Value as ActivityDto;
            Assert.That(actualResult, Is.EqualTo(actualResult));

            mediator.Verify(x => x.Send(new GetActivityQuery(id), It.IsAny<CancellationToken>()), Times.Once());
            mediator.VerifyNoOtherCalls();
        }

        private static ActivityDto GetActivityDto()
            => new()
            {
                Id = "1",
                Name = "test",
                AvgSpeed = 2,
                Description = "desc",
                Distance = 2,
                Duration = 2
            };
    }
}