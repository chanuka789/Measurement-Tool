using CMX.Application.Services;

var service = new ProjectService();
var project = service.Create("Demo Project");
Console.WriteLine($"Created project {project.Id}: {project.Name}");

foreach (var p in service.GetAll())
{
    Console.WriteLine($"Stored project {p.Id}: {p.Name}");
}
