using Microsoft.AspNetCore.Mvc;
using PersonalBacklog.Api.Data;
using PersonalBacklog.Api.Models;

namespace PersonalBacklog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimeController : ControllerBase
{
    private readonly BacklogDbContext _context;

    public AnimeController(BacklogDbContext context)
    {
        _context = context;
    }
}