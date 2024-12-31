﻿using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users.Events;
public record UserRegisteredDomainEvent(
    Guid UserId,
    string? AdminKey) : IDomainEvent;