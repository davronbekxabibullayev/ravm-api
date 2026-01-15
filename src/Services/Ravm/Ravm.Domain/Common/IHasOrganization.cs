namespace Ravm.Domain.Common;

using System;

public interface IHasOrganization
{
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    Guid OrganizationId { get; set; }
}
