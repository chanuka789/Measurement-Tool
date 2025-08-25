using System.Collections.Generic;
using CMX.Domain.Entities;

namespace CMX.Application.Services;

public class ProjectService
{
    private readonly List<Project> _projects = new();

    public Project Create(string name)
    {
        var project = new Project { Id = _projects.Count + 1, Name = name };
        _projects.Add(project);
        return project;
    }

    public IEnumerable<Project> GetAll() => _projects;
}
