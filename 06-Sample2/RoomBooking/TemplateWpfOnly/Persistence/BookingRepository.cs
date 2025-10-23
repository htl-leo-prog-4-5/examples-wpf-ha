﻿using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    //TODO
}