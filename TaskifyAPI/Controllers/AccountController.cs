using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskifyAPI.Data;
using TaskifyAPI.Dtos;
using TaskifyAPI.Models;

namespace TaskifyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController
{
    private readonly TaskyfyDataContext _context;

    AccountController(TaskyfyDataContext context)
    {
        _context = context;
    }
}
