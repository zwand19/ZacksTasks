using System.ComponentModel.DataAnnotations;
using AI;
using Managers.Tasks;
using Models;
using Moq;
using Repositories.Tasks;

namespace ManagersTests.Tasks;

public class TasksManagerTests
{
  private readonly TasksManager _tasksManager;
  private readonly Mock<ITasksRepository> _tasksRepository = new();
  private readonly Mock<IAITaskCreator> _aiTaskCreator = new();

  public TasksManagerTests()
  {
    _tasksManager = new TasksManager(_tasksRepository.Object, _aiTaskCreator.Object);
  }

  [Fact]
  public async Task Create_EmptyDescription_ThrowsValidationException()
  {
    await Assert.ThrowsAsync<ValidationException>(async () => await _tasksManager.Create(new ZackTask {Description = ""}));
  }

  [Fact]
  public async Task Create_SingleCharacterDescription_ThrowsValidationException()
  {
    await Assert.ThrowsAsync<ValidationException>(async () => await _tasksManager.Create(new ZackTask {Description = "A"}));
  }

  [Theory]
  [InlineData(" A")]
  [InlineData("A ")]
  [InlineData(" A ")]
  [InlineData(" 1 ")]
  public async Task Create_SingleCharacterWithLeadingOrTrailingSpaces_ThrowsValidationException(string description)
  {
    await Assert.ThrowsAsync<ValidationException>(async () => await _tasksManager.Create(new ZackTask {Description = description}));
  }

  [Theory]
  [InlineData("AB")]
  [InlineData(" AB")]
  [InlineData("AB ")]
  [InlineData(" AB ")]
  [InlineData(" -- ")] // I didn't make the rules. oh wait..
  [InlineData(" 12 ")]
  [InlineData("12")]
  public async Task Create_TwoCharacterDescription_CreatesEntity(string description)
  {
    var task = new ZackTask {Description = description};

    await _tasksManager.Create(task);

    _tasksRepository.Verify(x => x.Create(task), Times.Once);
  }

  [Fact]
  public async Task Create_RepositoryThrowsException_ThrowsException()
  {
    _tasksRepository.Setup(x => x.Create(It.IsAny<ZackTask>())).ThrowsAsync(new Exception("Repository failure"));

    await Assert.ThrowsAsync<Exception>(() => _tasksManager.Create(new ZackTask {Description = "AB"}));
  }

  [Fact]
  public async Task CreateSubTasks_ValidDescription_CreatesSubTasks()
  {
    const string taskDescription = "Parent task description";
    var subTaskDescriptions = new[] {"Subtask 1", "Subtask 2", "Subtask 3"};

    _aiTaskCreator.Setup(x => x.GetSubTasks(taskDescription)).ReturnsAsync(subTaskDescriptions);

    var result = await _tasksManager.CreateSubTasks(taskDescription);

    Assert.Equal(3, result.Length);
    Assert.Collection(result, subTask => Assert.Equal("Subtask 1", subTask.Description),
      subTask => Assert.Equal("Subtask 2", subTask.Description), subTask => Assert.Equal("Subtask 3", subTask.Description));

    _tasksRepository.Verify(x => x.Create(It.IsAny<ZackTask[]>()), Times.Once);
  }

  [Fact]
  public async Task CreateSubTasks_NoSubTasksGenerated_ReturnsEmptyArray()
  {
    const string taskDescription = "Parent task description";

    _aiTaskCreator.Setup(x => x.GetSubTasks(taskDescription)).ReturnsAsync(Array.Empty<string>());

    var result = await _tasksManager.CreateSubTasks(taskDescription);

    Assert.Empty(result);
    _tasksRepository.Verify(x => x.Create(It.IsAny<ZackTask[]>()), Times.Never);
  }

  [Fact]
  public async Task CreateSubTasks_AiTaskCreatorThrowsException_ThrowsException()
  {
    const string taskDescription = "Parent task description";

    _aiTaskCreator.Setup(x => x.GetSubTasks(taskDescription)).ThrowsAsync(new Exception("AI Task Creator failure"));

    await Assert.ThrowsAsync<Exception>(() => _tasksManager.CreateSubTasks(taskDescription));

    _tasksRepository.Verify(x => x.Create(It.IsAny<ZackTask[]>()), Times.Never);
  }
}