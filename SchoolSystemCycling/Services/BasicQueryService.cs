using SchoolSystemCycling.Models.Entities;
using SchoolSystemCycling.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SchoolSystemCycling.Models.Dtos;

namespace SchoolSystemCycling.Services;

public class BasicQueryService {
    private readonly ApplicationDbContext _context;

    public BasicQueryService(ApplicationDbContext context) {
        _context = context;
    }
}